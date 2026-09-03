using Godot;
using System;

public partial class GameFinish : Control
{
	private Label resultsLabel;
	private Button restartButton;
	private int correctCount;
	private int wrongCount;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        resultsLabel = GetNode<Label>("resultsLabel");
		restartButton = GetNode<Button>("restartButton");

		restartButton.Pressed += OnRestartPressed;

		var gameState = GetNode<GameState>("/root/GameState");
		correctCount = gameState.CorrectCount;
		wrongCount = gameState.WrongCount;

		UpdateResultsText();
    }

	public void SetResults(int correct, int wrong)
    {
		correctCount = correct;
		wrongCount = wrong;

		if (IsNodeReady())
        {
            UpdateResultsText();
        }
    }

	private void UpdateResultsText()
    {
		int total = correctCount + wrongCount;
		if (total == 0) return;

		double percentage = (double)correctCount / total * 100;
		resultsLabel.Text = $"Quiz Finished!\n\nCorrect: {correctCount}\nWrong: {wrongCount}\nScore: {correctCount}/{total} ({percentage:F0}%)";
	}

	private void OnRestartPressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/CategoryScreen.tscn");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
