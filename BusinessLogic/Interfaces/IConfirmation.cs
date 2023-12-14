namespace BusinessLogic.Interfaces;

public interface IConfirmation
{
    bool IsConfirmed { get; set; }
    Action CreateAction(Action action);
    void OpenConfirmationModal(Action action, string text);
}

