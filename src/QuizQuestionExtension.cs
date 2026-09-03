using Godot;
using System;
using System.Collections.Generic;

public static class QuizQuestionExtension
{
    public static void ShuffleChoices(this QuizQuestion question)
    {
        var ranChoice = new Random();
        var corrChoiceTxt = question.Choices[question.CorrectAnswer];
        question.Choices.Shuffle();
        question.CorrectAnswer = question.Choices.IndexOf(corrChoiceTxt);
    }
}