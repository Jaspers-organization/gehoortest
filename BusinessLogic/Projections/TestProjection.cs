namespace BusinessLogic.Projections;

public  struct TestProjection
{
    public readonly string Title;
    public readonly int AmountOfTextQuestions;
    public readonly int AmountOfToneAudiometryQuestions;
    public readonly bool Active;
    public readonly string EmployeeName;

    public TestProjection(string title, int amountOfTextQuestions, int amountOfToneAudiometryQuestions, bool active, string employeeName)
    {
        Title = title;
        AmountOfTextQuestions = amountOfTextQuestions;
        AmountOfToneAudiometryQuestions = amountOfToneAudiometryQuestions;
        Active = active;
        EmployeeName = employeeName;
    }
}
