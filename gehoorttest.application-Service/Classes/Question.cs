﻿namespace gehoorttest.application_Service.Classes;

public abstract class Question
{
    int QuestionNumber { get; set; }

    public Question() { }

    public Question(int QuestionNumber)
    {
        this.QuestionNumber = QuestionNumber;
    }
}
