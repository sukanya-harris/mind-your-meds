using Godot;
using System;

public partial class GameScreen : Control
{
	private QuizData quizData;
	private int currentIndex = 0;
	private Label questionLabel;
	private Button[] choiceButtons;

	private int correctCount = 0;
	private int wrongCount = 0;

	private TextureRect feedbackIcon;
	private TextureRect animBunnie;
	private Texture2D correctTexture;
	private Texture2D wrongTexture;
	private AnimationPlayer feedbackAnim;

	private ProgressBar progBar;

	public override void _Ready()
    {
        questionLabel = GetNode<Label>("questionLabel");
		choiceButtons = new Button[]
        {
            GetNode<Button>("GridContainer/choiceButton1"),
			GetNode<Button>("GridContainer/choiceButton2"),
			GetNode<Button>("GridContainer/choiceButton3"),
			GetNode<Button>("GridContainer/choiceButton4")
		};
		feedbackAnim = GetNode<AnimationPlayer>("feedbackAnimationPlayer");

		for (int i = 0; i < choiceButtons.Length; i++)
        {
            int choiceIndex = i;
			choiceButtons[i].Pressed += () => OnChoiceSelected(choiceIndex);
        }

		progBar = GetNode<ProgressBar>("progressBar");

		var gameState = GetNode<GameState>("/root/GameState");
		var quizManager = new QuizManager();
		quizData = quizManager.LoadQuestions(gameState.SelectedCategory);

		if (quizData == null || quizData.Questions.Count == 0)
		{
			GD.PrintErr("No questions loaded for category: " + gameState.SelectedCategory);
			return;
		}

		quizData.Questions.Shuffle();

		feedbackIcon = GetNode<TextureRect>("feedbackIcon");
		feedbackIcon.Visible = false;

		animBunnie = GetNode<TextureRect>("pharmaBunnie");

		correctTexture = GD.Load<Texture2D>("res://img/correctIcon.png");
		wrongTexture = GD.Load<Texture2D>("res://img/wrongIcon.png");

		progBar.MaxValue = quizData.Questions.Count;

		ShowQuestion(currentIndex);
    }

	private void ShowQuestion(int index)
    {
        QuizQuestion q = quizData.Questions[index];
		q.ShuffleChoices();
		questionLabel.Text = q.Question;

		for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < q.Choices.Count)
            {
                choiceButtons[i].Text = q.Choices[i];
				choiceButtons[i].Visible = true;
            }
			else
            {
                choiceButtons[i].Visible = false;
            }
        }
		progBar.Value = index + 1;
    }

	private async void OnChoiceSelected(int choiceIndex)
    {
        QuizQuestion q = quizData.Questions[currentIndex];
		bool isCorrect = choiceIndex == q.CorrectAnswer;

		if (isCorrect)
        {
            correctCount++;
        }
		else
        {
            wrongCount++;
        }
		
		SetChoicesEnabled(false);
		choiceButtons[q.CorrectAnswer].Modulate = new Color(0.4f, 1f, 0.4f); //green
		if (!isCorrect)
		{
			choiceButtons[choiceIndex].Modulate = new Color(1f, 0.4f, 0.4f); //red
		}

		feedbackIcon.Texture = isCorrect ? correctTexture : wrongTexture;
		feedbackIcon.Visible = true;
		feedbackAnim.Play(isCorrect ? "correct" : "wrong");

		await ToSignal(feedbackAnim, AnimationPlayer.SignalName.AnimationFinished);
		//await ToSignal(GetTree().CreateTimer(1.0), Timer.SignalName.Timeout);

		foreach (var button in choiceButtons)
		{
			button.Modulate = Colors.White;
		}
		
		feedbackIcon.Visible = false;
		SetChoicesEnabled(true);
		

		currentIndex++;
		if (currentIndex < quizData.Questions.Count)
        {
            ShowQuestion(currentIndex);
        }
		else
        {
            ShowFinishScreen();
        }
    }

	private void SetChoicesEnabled(bool enabled)
    {
        foreach (Button button in choiceButtons)
        {
            button.Disabled = !enabled;
        }
    }

	private void ShowFinishScreen()
    {
		var gameState = GetNode<GameState>("/root/GameState");
		gameState.CorrectCount = correctCount;
		gameState.WrongCount = wrongCount;

		GetTree().ChangeSceneToFile("res://scenes/GameFinish.tscn");
        
		//old finish screen code
		//var finishScreen = GD.Load<PackedScene>("res://scenes/GameFinish.tscn");
		//var finishInstance = finishScreen.Instantiate<GameFinish>();

		//finishInstance.SetResults(correctCount, wrongCount);

		//GetTree().Root.AddChild(finishInstance);
		//QueueFree();
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
