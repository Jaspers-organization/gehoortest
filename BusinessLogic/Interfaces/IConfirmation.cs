namespace BusinessLogic.Interfaces;

public interface IConfirmation
{
    bool IsConfirmed { get; set; }
    void SetConfirmed(bool value);
    Action CreateAction(Action action);
    void OpenConfirmationModal(Action action, string text);
}

