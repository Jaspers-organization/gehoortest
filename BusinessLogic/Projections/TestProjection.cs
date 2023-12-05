using BusinessLogic.IModels;
using System.ComponentModel;

namespace BusinessLogic.Projections;

public class TestProjection : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Title { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;

    private bool _active;
    public bool Active
    {
        get { return _active; }
        set
        {
            if (_active != value)
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public string EmployeeName { get; set; }
    public int AmountOfQuestions { get; set; }
   
    public int TargetAudienceId { get; set; }
    public List<ITextQuestion> TextQuestions { get; set; }
    public List<IToneAudiometryQuestion> ToneAudiometryQuestions { get; set; }
    
}
