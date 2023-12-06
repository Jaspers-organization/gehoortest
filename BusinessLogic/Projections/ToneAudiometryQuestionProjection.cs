namespace BusinessLogic.Projections;

internal struct ToneAudiometryQuestionProjection 
{
    public int QuestionNumber { get;set; }
    public int Frequency { get;set; }
    public int StartingDecibels { get; set; }
}
