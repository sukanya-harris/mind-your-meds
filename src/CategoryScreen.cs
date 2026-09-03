using Godot;
using System;

public partial class CategoryScreen : Control
{
	private GridContainer buttonsContainer;
	private readonly string[] categories = {"drugs", "sig codes", "pharmacology", "federal laws"};
	public override void _Ready()
	{
		buttonsContainer = GetNode<GridContainer>("categoryButtons");
		foreach (Node child in buttonsContainer.GetChildren())
		{
			if (child is Button button)
			{
				string category = button.Name;
				button.Pressed += () => OnCategorySelected(category); 
			}
		}
	}

	private void OnCategorySelected(string category)
	{
		var gameState = GetNode<GameState>("/root/GameState");
		gameState.SelectedCategory = category;
		GetTree().ChangeSceneToFile("res://scenes/GameScreen.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
