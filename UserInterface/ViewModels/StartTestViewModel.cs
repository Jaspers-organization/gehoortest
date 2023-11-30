using DataAccess.Models.TestData_Management;
using UserInterface.Commands;
using UserInterface.Stores;
using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Options;

namespace UserInterface.ViewModels;

internal class StartTestViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    public TestProgressData TestProgressData { get; set; }
    private BusinessLogic.Classes.Test test = new();

    private List<string> _radioButtons = new();
    private string _selectedOption = "";
    private string _introText = "Visible";
    private string _questionText = "Hidden";
    private string _questionAudio = "Hidden";
    private string _questionRadioButtons = "Hidden";
    private string _questionInput = "Hidden";
    private string _testEnd = "Hidden";
    private string _textQuestion = "Vraag...";
    private string _questionInputText = "";
    private int frequency = 0;
    private TestResultViewModel? _testResultViewModel = null;

    public string SelectedOption
    {
        get { return _selectedOption; }
        set { _selectedOption = value; OnPropertyChanged(nameof(SelectedOption)); }
    }
    public List<string> RadioButtons
    {
        get { return _radioButtons; }
        set { _radioButtons = value; OnPropertyChanged(nameof(RadioButtons)); }
    }
    public string QuestionText
    {
        get { return _questionText; }
        set { _questionText = value; OnPropertyChanged(nameof(QuestionText)); }
    }
    public string QuestionAudio
    {
        get { return _questionAudio; }
        set { _questionAudio = value; OnPropertyChanged(nameof(QuestionAudio)); }
    }
    public string TestEnd
    {
        get { return _testEnd; }
        set { _testEnd = value; OnPropertyChanged(nameof(TestEnd)); }
    }
    public string IntroText
    {
        get { return _introText; }
        set { _introText = value; OnPropertyChanged(nameof(IntroText)); }
    }
    public string QuestionRadioButtons
    {
        get { return _questionRadioButtons; }
        set { _questionRadioButtons = value; OnPropertyChanged(nameof(QuestionRadioButtons)); }
    }
    public string QuestionInput
    {
        get { return _questionInput; }
        set { _questionInput = value; OnPropertyChanged(nameof(QuestionInput)); }
    }
    public string TextQuestion
    {
        get { return _textQuestion; }
        set { _textQuestion = value; OnPropertyChanged(nameof(TextQuestion)); }
    }
    public string QuestionInputText
    {
        get { return _questionInputText; }
        set { _questionInputText = value; OnPropertyChanged(nameof(QuestionInputText)); }
    }
    public TestResultViewModel? TestResultViewModel
    {
        get { return _testResultViewModel; }
        set { _testResultViewModel = value; OnPropertyChanged(nameof(TestResultViewModel)); }
    }

    public StartTestViewModel(NavigationStore navigationStore) {
        this.navigationStore = navigationStore;

        test = TestRepository.GetTest();

      //  TestProgressData = new(test);
    }

    public void StartTest(string test)
    {
        //Options.Add(value);
        IntroText = "Hidden";
    }
    public void SaveQuestion(object test)
    {
        string answer;

        if (SelectedOption == "")
        {
            // Change to input field
            answer = QuestionInputText;
        }
        else
        {
            answer = SelectedOption;
        }

        //TestAnswer testAnswer = new(TestProgressData.CurrentQuestion, SelectedOption);
        //TestProgressData.TestAnswers.Add(testAnswer);
        // Continue to next question
    }

    public int questionNumber = 1; // TODO: Temporary for the demo
    public void SaveAudioQuestion(object parameter)
    {
        //TestAnswer testAnswer = new(TestProgressData.CurrentQuestion, parameter.ToString());

        //// ========
        //// TODO: Temporary for the demo
        //int decibels = (testAnswer.Answer == "true") ? 30 : 65; 
        //TestProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(questionNumber, 250, Ear.Left, 30, decibels));
        questionNumber++;
        // =========

    }

    public void MakeSound(object test)
    {
        AudioManager.PlaySound(frequency, 700);
    }

    // TODO: maybe rewrite this to a command
    public void ShowTestResults()
    {
        navigationStore.CurrentViewModel = new TestResultViewModel(navigationStore, TestProgressData);
    }

   // public ICommand StartTestCommand => new CustomCommands(StartTest);
    public ICommand SaveQuestionCommand => new CustomCommands(SaveQuestion);

    public ICommand MakeSoundCommand => new CustomCommands(MakeSound);

    public ICommand SaveAudioQuestionCommand => new CustomCommands(SaveAudioQuestion);
}

