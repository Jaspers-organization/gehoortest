using BusinessLogic.IModels;

namespace BusinessLogic.IModels;

public interface IToneAudiometryQuestion: IModel, IQuestion
{   
    public int Frequency { get; set; }
    public int StartingDecibels { get; set; }
}
