namespace BusinessLogic.Projections;

public readonly struct TestResultProjection
{
    public readonly bool hasHearingLoss;

    public TestResultProjection(bool hasHearingLoss)
    {
        this.hasHearingLoss = hasHearingLoss;
    }
}
