using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TargetAudience : ITargetAudience
{
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string? Label { get; set; }
}
