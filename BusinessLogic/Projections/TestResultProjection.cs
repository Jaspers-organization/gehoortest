namespace BusinessLogic.Projections;

public struct TestResultProjection
{
    public Guid TestResultId { get; set; }
    public string TestResultText { get; set; }
    public string TestResultExplanation { get;  set; }
}
