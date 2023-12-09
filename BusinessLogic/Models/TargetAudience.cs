using BusinessLogic.IModels;

namespace BusinessLogic.Models;

public class TargetAudience : IModel
{
    public Guid Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string? Label { get; set; }

    public ICollection<Test> Tests { get; set; }
}
