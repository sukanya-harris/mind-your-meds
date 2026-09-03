using System.Dynamic;
using Godot;

public partial class GameState : Node
{
    public string SelectedCategory {get; set;}
    public int CorrectCount {get; set;}
    public int WrongCount {get; set;}
}