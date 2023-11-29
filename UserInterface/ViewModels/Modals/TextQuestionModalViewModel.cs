using BusinessLogic.IModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands.TestManagementCommands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class TextQuestionModalViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;

    public ICommand AddOptionCommand => new StringCommand(AddOption);
    public ICommand RemoveOptionCommand => new StringCommand(RemoveOption);

    public ICommand CloseModalCommand => new ObjectCommand(CloseModal);

    private string _optionText;
    public string OptionText
    {
        get { return _optionText; }
        set
        {
            _optionText = value;
            OnPropertyChanged(nameof(OptionText));
        }
    }

    private string _testQuestion;
    public string TestQuestion
    {
        get { return _testQuestion; }
        set
        {
            _testQuestion = value;
            OnPropertyChanged(nameof(TestQuestion));
        }
    }

    private ObservableCollection<string> _options;
    public ObservableCollection<string> Options
    {
        get { return _options; }
        set
        {
            _options = value;
            OnPropertyChanged(nameof(Options));
        }
    }

    private bool _multipleChoice;
    public bool MultipleChoice
    {
        get { return _multipleChoice; }
        set
        {
            _multipleChoice = value;
            OnPropertyChanged(nameof(MultipleChoice));
        }
    }
    private bool _hasInputField;
    public bool HasInputField
    {
        get { return _hasInputField; }
        set
        {
            _hasInputField = value;
            OnPropertyChanged(nameof(HasInputField));
        }
    }

    private readonly ITextQuestion textQuestion;
    private readonly TestManagementViewModel testManagementViewModel;

    public TextQuestionModalViewModel(NavigationStore navigationStore, ITextQuestion textQuestion, TestManagementViewModel testManagementViewModel)
    {
        this.navigationStore = navigationStore;
        this.textQuestion = textQuestion;
        this.testManagementViewModel = testManagementViewModel;

        MultipleChoice = textQuestion.IsMultiSelect;
        HasInputField = textQuestion.HasInputField;
        TestQuestion = textQuestion.Question;
        Options = new ObservableCollection<string>(textQuestion.Options);
    }
    public void AddOption(string value)
    {
        Options.Add(value);
        OnPropertyChanged(nameof(Options));
    }
    public void RemoveOption(string value)
    {
        Options.Remove(value);
        OnPropertyChanged(nameof(Options));
    }
    private void CloseModal(object obj)
    {
        navigationStore.CloseModal(testManagementViewModel);
    }
}
