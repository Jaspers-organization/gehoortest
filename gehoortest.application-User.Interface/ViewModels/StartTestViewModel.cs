using CommunityToolkit.Mvvm.ComponentModel;
using DataAccess.Repositories;
using gehoortest.application_User.Interface.Commands;
using gehoorttest.application_Service.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace gehoortest.application_User.Interface.ViewModels;

public class StartTestViewModel : ObservableObject
{
    public TestProgressData TestProgressData { get; set; }
    private Test test = new();

    private List<string> _radioButtons = new();
    private string _selectedOption = "";
    private string _introText = "Visable";
    private string _questionText = "Hidden";
    private string _questionRadioButtons = "Hidden";
    private string _questionInput = "Hidden";
    private string _textQuestion = "Vraag...";

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

    public StartTestViewModel(TestRepository context)
    {
        // Create dummy data

        // Create questions text
        List<string> agesList = new();
        agesList.Add("-18");
        agesList.Add("19-29");
        agesList.Add("30-49");
        agesList.Add("50-69");
        agesList.Add("70-79");
        agesList.Add("80-89");
        TextQuestion textQuestion1 = new(1, "In welke leeftijdsgroep bevindt u zich?", agesList, true, false);
        test.AddTextQuestion(textQuestion1);

        List<string> optionsList = new();
        TextQuestion textQuestion2 = new(2, "Wat is uw naam?", optionsList, false, true);
        test.AddTextQuestion(textQuestion2);

        List<string> workFieldList = new();
        workFieldList.Add("Kantoor baan");
        workFieldList.Add("Op de bouw");
        TextQuestion textQuestion3 = new(3, "In welk werkgebied werkt u?", workFieldList, true, true);
        test.AddTextQuestion(textQuestion3);

        List<string> hearTypesList = new();
        hearTypesList.Add("Uitstekend");
        hearTypesList.Add("Goed");
        hearTypesList.Add("Matig");
        hearTypesList.Add("Slecht");
        TextQuestion textQuestion4 = new(4, "Hoe omschrijft u uw gehoor?", hearTypesList, true, false);
        test.AddTextQuestion(textQuestion4);


        // Create questionsaudimetry
        /* AudiometryQuestion audiometryQuestion1 = new(5, 500, 40);
         test.AddAudiometryQuestion(audiometryQuestion1);

         AudiometryQuestion audiometryQuestion2 = new(6, 1500, 30);
         test.AddAudiometryQuestion(audiometryQuestion2);*/


        TestProgressData = new(test);
    }

    public void StartTest(object test)
    {
        IntroText = "Hidden";
        ShowNextQuestion();
    }

    public void ShowNextQuestion()
    {
        // Reset to defaults
        QuestionText = "Hidden";

        Question nextQuestion = TestProgressData.GetNextQuestion();

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
                QuestionInput = "Visible";
            }
            return;
        }

        if (nextQuestion is AudiometryQuestion audiometryQuestion)
        {
            MessageBox.Show("Audio");
            return;
        }

        MessageBox.Show("Alle vragen zijn voltooid!");
        throw new System.Exception("No question found");
    }

    public void SaveQuestion(object test)
    {
        string answer;

        if (SelectedOption == "")
        {
            // Change to input field
            answer = "No answer";
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

    public ICommand StartTestCommand => new CustomCommands(StartTest);

    public ICommand SaveQuestionCommand => new CustomCommands(SaveQuestion);
}

