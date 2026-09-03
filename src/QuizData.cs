using System.Collections.Generic;

public class QuizData
{
    public List<QuizQuestion> Questions {get; set;}
}

public class QuizQuestion
{
    public string Question {get; set;}
    public List<string> Choices {get; set;}
    public int CorrectAnswer {get; set;}
}