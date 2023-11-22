using BusinessLogic.IModels;

namespace Service.IModels;

public interface ITargetAudience : IModel
{
    public int From { get; set; }
    public int To { get; set; }
    public string Label { get; set; }
}
