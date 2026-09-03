using Godot;
using System.Text.Json;

public partial class QuizManager : Node
{
    //private const string QuestionsPath = "res://data/drugQuestions.json";

    public QuizData LoadQuestions(string category)
    {
        string path = $"res://data/{category}.json";
        if (!FileAccess.FileExists(path))
        {
            GD.PrintErr($"Questions file not found at {path}");
            return null;
        }

        using var file =FileAccess.Open(path, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.PrintErr($"Failed to open file: {FileAccess.GetOpenError()}");
            return null;
        }

        string jsonText = file.GetAsText();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        try
        {
            return JsonSerializer.Deserialize<QuizData>(jsonText, options);  
        }
        catch (JsonException e)
        {
            GD.PrintErr($"Failed to parse questions.json: {e.Message}");
            return null;
        }
    }
}