using BusinessLogic.IModels;

namespace Service.IModels;

public interface ITextQuestion : IModel, IQuestion
{
    public string Question {  get; set; }
    public List<string> Options { get; set; }
    public bool IsMultiSelect { get; set; }
    public bool HasInputField { get; set; }
}
