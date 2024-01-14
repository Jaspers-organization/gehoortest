using BusinessLogic.Classes;
using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using BusinessLogic.Enums;
using DataAccess.Repositories;
using System.Windows;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.Interfaces.Repositories;

namespace UserInterface.ViewModels;

internal class TestViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;

    #region Services
    private TargetAudienceService targetAudienceService { get; set; }
    private TestService testService { get; set; }
    private TestProgressData testProgressData { get; set; }
    #endregion Services

    private AudioPlayer.AudioPlayer audioPlayer;
    TextQuestion currentTextQuestion;
    ToneAudiometryQuestion currentAudiometryQuestion;
    private BackgroundWorker worker;
    #region Properties
    private bool answerButtonEnabled;
    public bool AnswerButtonEnabled
    {
        get { return answerButtonEnabled; }
        set
        {
            answerButtonEnabled = value;
            OnPropertyChanged(nameof(AnswerButtonEnabled));
        }
    }
    private int progressValue;
    public int ProgressValue
    {
        get { return progressValue; }
        set
        {
            progressValue = value;
            OnPropertyChanged(nameof(ProgressValue));
        }
    }

    private string progressTextBlock;
    public string ProgressTextBlock
    {
        get { return progressTextBlock; }
        set
        {
            progressTextBlock = value;
            OnPropertyChanged(nameof(ProgressTextBlock));
        }
    }

    private bool targetAudienceBtnEnabled;
    public bool TargetAudienceBtnEnabled
    {
        get { return targetAudienceBtnEnabled; }
        set
        {
            targetAudienceBtnEnabled = value;
            OnPropertyChanged(nameof(TargetAudienceBtnEnabled));
        }
    }

    private bool textBtnAnswerEnabled;
    public bool TextBtnAnswerEnabled 
    {
        get { return textBtnAnswerEnabled; }
        set
        {
            textBtnAnswerEnabled = value;
            OnPropertyChanged(nameof(TextBtnAnswerEnabled));
        }
    }

    #region Visibility

    private Visibility showTestExplanationStartAudioView = Visibility.Hidden;
    public Visibility ShowTestExplanationStartAudioView
    {
        get { return showTestExplanationStartAudioView; }
        set
        {
            showTestExplanationStartAudioView = value;
            OnPropertyChanged(nameof(ShowTestExplanationStartAudioView));
        }
    }
    private Visibility showTestExplanationView = Visibility.Hidden;
    public Visibility ShowTestExplanationView
    {
        get { return showTestExplanationView; }
        set
        {
            showTestExplanationView = value;
            OnPropertyChanged(nameof(ShowTestExplanationView));
        }
    }
    private Visibility showTestTargetAudienceView = Visibility.Hidden;
    public Visibility ShowTestTargetAudienceView
    {
        get { return showTestTargetAudienceView; }
        set
        {
            showTestTargetAudienceView = value;
            OnPropertyChanged(nameof(ShowTestTargetAudienceView));
        }
    }
    private Visibility showTestTextQuestionView = Visibility.Hidden;
    public Visibility ShowTestTextQuestionView
    {
        get { return showTestTextQuestionView; }
        set
        {
            showTestTextQuestionView = value;
            OnPropertyChanged(nameof(ShowTestTextQuestionView));
        }
    }
    private Visibility showTestToneAudiometryView = Visibility.Hidden;
    public Visibility ShowTestToneAudiometryView
    {
        get { return showTestToneAudiometryView; }
        set
        {
            showTestToneAudiometryView = value;
            OnPropertyChanged(nameof(ShowTestToneAudiometryView));
        }
    }
    private Visibility showTestResultView = Visibility.Hidden;
    public Visibility ShowTestResultView
    {
        get { return showTestResultView; }
        set
        {
            showTestResultView = value;
            OnPropertyChanged(nameof(ShowTestResultView));
        }
    }
    private Visibility showQuestionRadioButtons { get; set; } = Visibility.Hidden;
    public Visibility ShowQuestionRadioButtons
    {
        get { return showQuestionRadioButtons; }
        set 
        {
            showQuestionRadioButtons = value; 
            OnPropertyChanged(nameof(ShowQuestionRadioButtons));
        }
    }
    private Visibility showQuestionInput = Visibility.Hidden;
    public Visibility ShowQuestionInput
    {
        get { return showQuestionInput; }
        set 
        { 
            showQuestionInput = value; 
            OnPropertyChanged(nameof(ShowQuestionInput)); 
        }
    }

    #endregion Visibility

    private string selectedTextOption = string.Empty;
    public string SelectedTextOption
    {
        get { return selectedTextOption; }
        set
        {
            selectedTextOption = value;
            OnPropertyChanged(nameof(SelectedTextOption));
            if (value != null)
            {
                TargetAudienceBtnEnabled = true;
                TextBtnAnswerEnabled = true;
            }
        }
    }

    public string ButtonsDisabled { get; set; }
    private ObservableCollection<TargetAudience> targetAudiences;
    public ObservableCollection<TargetAudience> TargetAudiences
    {
        get { return targetAudiences; }
        set
        {
            targetAudiences = value;
            OnPropertyChanged(nameof(TargetAudiences));
        }
    }

    private string textQuestion { get; set; }
    public string TextQuestion
    {
        get { return textQuestion; }
        set
        {
            textQuestion = value;
            OnPropertyChanged(nameof(TextQuestion));
        }
    }

    private string questionInputText { get; set; }
    public string QuestionInputText
    {
        get { return questionInputText; }
        set
        {
            questionInputText = value;
            OnPropertyChanged(nameof(QuestionInputText));
            TextBtnAnswerEnabled = !string.IsNullOrEmpty(value.Trim());
        }
    }

    private Test test;
    public Test Test
    {
        get { return test; }
        set
        {
            test = value;
            OnPropertyChanged(nameof(Test));
        }
    }
    private List<string> radioButtons = new();
    public List<string> RadioButtons
    {
        get { return radioButtons; }
        set 
        { 
            radioButtons = value;
            OnPropertyChanged(nameof(RadioButtons));
        }         
} 

    #endregion

    #region Commands
    public ICommand StartTestCommand => new Command(GetTargetAudiences);
    public ICommand TargetAudienceSelectedCommand => new Command(StartTextQuestions);
    public ICommand AnswerTextQuestionCommand => new Command(AnswerTextQuestion);
    public ICommand StartAudiometryTestCommand => new Command(StartToneAudioMetryQuestions);
    public ICommand SaveAudioQuestionCommand => new Command(AnswerToneAudiometryQuestion);
    public ICommand OpenTestManagementCommand => new Command(OpenTestManagement);
    #endregion Commands

    #region Constructor
    public TestViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        
        ShowTestExplanationView = Visibility.Visible;
        this.navigationStore.AddPreviousViewModel(new HomeViewModel(navigationStore));


        ITestRepository testRepository = new TestRepository();
        testService = new TestService(testRepository);

        ITargetAudienceRepository targetAudienceRepository = new TargetAudienceRepository();
        targetAudienceService = new TargetAudienceService(targetAudienceRepository, testRepository);


        audioPlayer = new AudioPlayer.AudioPlayer();
        Guid ee = Guid.NewGuid();
    }
    #endregion Constructor
  
    private void GetTargetAudiences()
    {
        TargetAudiences = new ObservableCollection<TargetAudience>(targetAudienceService.GetAllActiveWithTest());
        List<string> TargetAudienceOptions = new();
        foreach (TargetAudience targetAudience in TargetAudiences)
        {
            TargetAudienceOptions.Add(targetAudience.Label);
        }
        RadioButtons = TargetAudienceOptions;

        TextQuestion = "Wat is uw leeftijdsgroep?";
        ShowTestExplanationView = Visibility.Hidden;
        ShowTestTargetAudienceView = Visibility.Visible;
        ShowQuestionRadioButtons = Visibility.Visible;
        TargetAudienceBtnEnabled = false;
    }
    private void StartTextQuestions()
    {
        GetTest();

        if (test.TextQuestions.Count == 0)
        {
            StartToneAudioMetryQuestions();
            return;
        }

        DetermineFirstTextQuestion();
        SetVisualsTextQuestion();
    }
    private void GetTest()
    {
        ShowTestTargetAudienceView = Visibility.Hidden;
        TargetAudience? selectedTargetAudience = TargetAudiences.FirstOrDefault(item => item.Label == SelectedTextOption);
        if (selectedTargetAudience == null) return;
        Test = testService.GetTestByTargetAudienceIdAndActive(selectedTargetAudience.Id);
        testProgressData = new TestProgressData(Test);
    }
    private void DetermineFirstTextQuestion()
    {
        TextQuestion = Test.TextQuestions.First().Question;
        currentTextQuestion = Test.TextQuestions.First();
        testProgressData.CurrentQuestionNumber = Test.TextQuestions.First().QuestionNumber;
    }
    private void SetVisualsTextQuestion()
    {
        ShowQuestionInput = Visibility.Hidden;
        QuestionInputText = string.Empty;
        ShowQuestionRadioButtons = Visibility.Hidden;

        if (currentTextQuestion.IsMultiSelect)
        {
            List<string> options = testService.ConvertQuestionOptionsToStrings(currentTextQuestion.Options.ToList());
            List<string> tempRadioButtons = new();
            foreach (string option in options)
            {
                tempRadioButtons.Add(option);
            }

            RadioButtons = tempRadioButtons;
            ShowQuestionRadioButtons = Visibility.Visible;
        }
        
        if (currentTextQuestion.HasInputField)
        {
            ShowQuestionInput = Visibility.Visible;
        }
        ShowTestTextQuestionView = Visibility.Visible;
        ShowTestTargetAudienceView = Visibility.Hidden;
        TextBtnAnswerEnabled = false;
    }
    private void StartToneAudioMetryQuestions()
    {
        if (test.ToneAudiometryQuestions.Count == 0)
        {
            ShowResults();
            return;
        }

        ShowTestExplanationStartAudioView = Visibility.Hidden;
        DetermineFirstAudioQuestion();
        SetVisualsAudioQuestion();
    }
    private void DetermineFirstAudioQuestion()
    {
        currentAudiometryQuestion = Test.ToneAudiometryQuestions.First();
        testProgressData.Decibel = currentAudiometryQuestion.StartingDecibels;
        testProgressData.CurrentQuestionNumber = currentAudiometryQuestion.QuestionNumber;
       

        AskAudioQuestion();
    }
    private void AnswerTextQuestion()
    {
        string answerToAdd = QuestionInputText.Trim().IsNullOrEmpty() ?  SelectedTextOption : QuestionInputText;
        testProgressData.Add(answerToAdd, currentTextQuestion);

        currentTextQuestion = testProgressData.GetNextTextQuestion();
        if (currentTextQuestion == null)
        {
            ShowTestExplanationStartAudioView = Visibility.Visible;
        }
        else
        {
            TextQuestion = currentTextQuestion.Question;
            SetVisualsTextQuestion();
        }
    }
    private void AnswerToneAudiometryQuestion(string value)
    {
        AnswerButtonEnabled = false;
        bool answer = value == "true" ? true : false;
        testProgressData.Add(answer, testProgressData.CurrentEar, currentAudiometryQuestion);

        currentAudiometryQuestion = testProgressData.GetNextToneAudiometryQuestion();
        if (currentAudiometryQuestion == null)
        {
            ShowResults();
        }
        else
        {
            AskAudioQuestion(); 
        }
    }
    private void PlayFrequency(int frequency, Ear ear)
    {
        Random random = new Random();
        int delay = random.Next(0, 3) * 1000;

        Task.Delay(delay).ContinueWith(_ =>
        {
            audioPlayer.PlayFrequency(frequency, testProgressData.Decibel, ear);
        });
    }
    private void ShowResults()
    {
        ShowTestTextQuestionView = Visibility.Hidden;
        ShowTestToneAudiometryView = Visibility.Hidden;
        navigationStore!.CurrentViewModel = new TestResultViewModel(navigationStore, testProgressData);
    }
    private void AskAudioQuestion()
    {
        StartProgress();
        PlayFrequency(currentAudiometryQuestion.Frequency, testProgressData.CurrentEar);
    }
    public void StartProgress()
    {
        worker = new BackgroundWorker();
        worker.RunWorkerCompleted += WorkCompleted;
        worker.WorkerReportsProgress = true;
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerAsync();
    }
    private void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        ProgressValue = 100;
        worker.Dispose();
        AnswerButtonEnabled = true;
    }
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        ProgressValue = e.ProgressPercentage;
    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        var worker = sender as BackgroundWorker;
        worker.ReportProgress(100, String.Format("process 100."));
        for (int i = 100; i >= 0; i = i - 20)
        {
            Thread.Sleep(500);
            worker.ReportProgress(i - 10, string.Format("process {0}.", i - 10));
        }
        worker.ReportProgress(0, "process done");
    }
    private void SetVisualsAudioQuestion()
    {
        ShowQuestionRadioButtons = Visibility.Hidden;
        ShowTestTextQuestionView = Visibility.Hidden;
        ShowQuestionInput = Visibility.Hidden;
        ShowTestToneAudiometryView = Visibility.Visible;
        AnswerButtonEnabled = false;
    }
    private void OpenTestManagement()
    {
        navigationStore!.CurrentViewModel = new TestOverviewViewModel(navigationStore);

    }

}
