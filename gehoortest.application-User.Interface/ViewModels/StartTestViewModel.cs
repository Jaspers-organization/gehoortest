using CommunityToolkit.Mvvm.ComponentModel;
using gehoortest.application_Repository.Models.TestData_Management;
using gehoortest.application_User.Interface.Commands;
using gehoorttest.application_Service;
using gehoorttest.application_Service.Classes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.ViewModels;

public class StartTestViewModel : ObservableObject
{
    public TestProgressData TestProgressData { get; set; }
    private gehoorttest.application_Service.Classes.Test test = new();

    private List<string> _radioButtons = new();
    private string _selectedOption = "";
    private string _introText = "Visable";
    private string _questionText = "Hidden";
    private string _questionAudio = "Hidden";
    private string _questionRadioButtons = "Hidden";
    private string _questionInput = "Hidden";
    private string _testEnd = "Hidden";
    private string _textQuestion = "Vraag...";
    private string _questionInputText = "";
    private int frequency = 0;

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

    public StartTestViewModel(TestRepository context)
    {
        test = TestRepository.GetTest();

        TestProgressData = new(test);
    }

    public void StartTest(object test)
    {
        IntroText = "Hidden";
        ShowNextQuestion();
    }

    public async void ShowNextQuestion()
    {
        // Reset to defaults
        QuestionText = "Hidden";
        QuestionAudio = "Hidden";

        Question? nextQuestion = TestProgressData.GetNextQuestion();

        if (nextQuestion is null)
        {
            TestEnd = "Visible";
            return;
        }

        if (nextQuestion is TextQuestion textQuestion)
        {
            // Reset to defaults
            QuestionRadioButtons = "Hidden";
            QuestionInput = "Hidden";
            QuestionText = "Visible";

            TextQuestion = textQuestion.Question;

            if (textQuestion.IsMultipleSelect)
            {
                List<string> tempRadioButtons = new();
                foreach (string option in textQuestion.Options)
                {
                    tempRadioButtons.Add(option);
                }
                RadioButtons = tempRadioButtons;
                QuestionRadioButtons = "Visible";
            }

            if (textQuestion.HasInputField)
            {
                QuestionInputText = "";
                QuestionInput = "Visible";
            }
            return;
        }

        if (nextQuestion is AudiometryQuestion audiometryQuestion)
        {
            QuestionAudio = "Visible";
            frequency = audiometryQuestion.Frequency;

            await Task.Delay(TimeSpan.FromSeconds(1));
            MakeSound(TestProgressData);

            return;
        }
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

        TestAnswer testAnswer = new(TestProgressData.CurrentQuestion, SelectedOption);
        TestProgressData.TestAnswers.Add(testAnswer);

        // Continue to next question
        ShowNextQuestion();
    }

    public void SaveAudioQuestion(object parameter)
    {
        TestAnswer testAnswer = new(TestProgressData.CurrentQuestion, parameter.ToString());
        ShowNextQuestion();
    }

    public void MakeSound(object test)
    {
        AudioManager.PlaySound(frequency, 700);
    }

    public ICommand StartTestCommand => new CustomCommands(StartTest);

    public ICommand SaveQuestionCommand => new CustomCommands(SaveQuestion);

    public ICommand MakeSoundCommand => new CustomCommands(MakeSound);

    public ICommand SaveAudioQuestionCommand => new CustomCommands(SaveAudioQuestion);
}

