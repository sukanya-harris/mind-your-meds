using Godot;
using System;

public partial class MainMenu : Node
{
	private Button playButton;
	private Button exitButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        playButton = GetNode<Button>("/root/MainMenu/VBoxContainer/playButton");
		exitButton = GetNode<Button>("/root/MainMenu/VBoxContainer/closeButton");

        playButton.Pressed += on_start_pressed;
		exitButton.Pressed += on_exit_pressed;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void on_start_pressed()
    {
        GetTree().ChangeSceneToFile("res://scenes/CategoryScreen.tscn");
    }

	public void on_exit_pressed()
    {
        GetTree().Quit();
    }
}
