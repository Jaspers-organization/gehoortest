namespace BusinessLogic.Projections;

internal struct TextQuestionProjection
{
    public int QuestionNumber { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }
    public bool IsMultipleSelect { get; set; }
    public bool HasInputField { get; set; }
}
