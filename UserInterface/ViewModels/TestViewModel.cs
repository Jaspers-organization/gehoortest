using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.MockData;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using NAudio.Utils;
using System;
using gehoortest_application.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using UserInterface.Commands;
using UserInterface.Stores;
using static System.Net.Mime.MediaTypeNames;
using BusinessLogic.Enums;
using DataAccess.Repositories;

namespace UserInterface.ViewModels
{
    internal class TestViewModel : ViewModelBase
    {
        #region Const
        private const string NOTVISIBLE = "Hidden";
        private const string VISIBLE = "Visible";
        #endregion Const

        private readonly NavigationStore navigationStore;
        private string _showTestExplanationView = "Hidden";
        private string _showTestTargetAudienceView = "Hidden";
        private string _showTestTextQuestionView = "Hidden";
        private string _showTestToneAudiometryView = "Hidden";
        private string _showTestResultView = "Hidden";
        private int highestTextQuestionNumber;
        private int highestAudioQuestionNumber;


        private NAudioPlayer nAudioPlayer;
        private ObservableCollection<TargetAudience> _targetAudiences;
        private Test _test;
        private TargetAudienceService targetAudienceService { get; set; }
        private TestService testService { get; set; }
        private TestProgressData testProgressData { get; set; }
        private int currentFrequency = 0;
        private bool finalDecibelToPlay = false;
        private bool testedLeftEar = false;
        private bool testedRightEar = false;
        TextQuestion currentTextQuestion;
        ToneAudiometryQuestion currentAudiometryQuestion;
        #region Properties
        private bool _answerButtonEnabled;
        public bool AnswerButtonEnabled
        {
            get { return _answerButtonEnabled; }
            set
            {
                _answerButtonEnabled = value;
                OnPropertyChanged(nameof(AnswerButtonEnabled));
            }
        }
        private int _progressValue;
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged(nameof(ProgressValue));
            }
        }

        private string _progressTextBlock;
        public string ProgressTextBlock
        {
            get { return _progressTextBlock; }
            set
            {
                _progressTextBlock = value;
                OnPropertyChanged(nameof(ProgressTextBlock));
            }
        }
        public string ShowTestExplanationView
        {
            get { return _showTestExplanationView; }
            set
            {
                _showTestExplanationView = value;
                OnPropertyChanged(nameof(ShowTestExplanationView));
            }
        }
        public string ShowTestTargetAudienceView
        {
            get { return _showTestTargetAudienceView; }
            set
            {
                _showTestTargetAudienceView = value;
                OnPropertyChanged(nameof(ShowTestTargetAudienceView));
            }
        }
        public string ShowTestTextQuestionView
        {
            get { return _showTestTextQuestionView; }
            set
            {
                _showTestTextQuestionView = value;
                OnPropertyChanged(nameof(ShowTestTextQuestionView));
            }
        }
        public string ShowTestToneAudiometryView
        {
            get { return _showTestToneAudiometryView; }
            set
            {
                _showTestToneAudiometryView = value;
                OnPropertyChanged(nameof(ShowTestToneAudiometryView));
            }
        }
        public string ShowTestResultView
        {
            get { return _showTestResultView; }
            set
            {
                _showTestResultView = value;
                OnPropertyChanged(nameof(ShowTestResultView));
            }
        }
        public string ButtonsDisabled { get; set; }
        public ObservableCollection<TargetAudience> TargetAudiences
        {
            get { return _targetAudiences; }
            set
            {
                _targetAudiences = value;
                OnPropertyChanged(nameof(TargetAudiences));
            }
        }

        public string TextQuestion
        {
            get { return textQuestion; }
            set
            {
                textQuestion = value;
                OnPropertyChanged(nameof(TextQuestion));
            }
        }
        private string textQuestion { get; set; }
        public string QuestionInputText
        {
            get { return questionInputText; }
            set
            {
                questionInputText = value;
                OnPropertyChanged(nameof(QuestionInputText));
            }
        }
        private string questionInputText { get; set; }

   
        private int playDecibel { get; set; }
        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }
        public string QuestionRadioButtons
        {
            get { return _questionRadioButtons; }
            set { _questionRadioButtons = value; OnPropertyChanged(nameof(QuestionRadioButtons)); }
        }
        private string _questionRadioButtons { get; set; }
        private int lowestDecibel = 0;
        bool isDoneText = false;
        bool isDoneAudio = false;
        public List<string> RadioButtons
        {
            get { return _radioButtons; }
            set { _radioButtons = value; OnPropertyChanged(nameof(RadioButtons)); }
        }
        private List<string> _radioButtons = new();

        public string QuestionInput
        {
            get { return _questionInput; }
            set { _questionInput = value; OnPropertyChanged(nameof(QuestionInput)); }
        }
        private string _questionInput = "Hidden";

        public string SelectedOption
        {
            get { return _selectedOption; }
            set { _selectedOption = value; OnPropertyChanged(nameof(SelectedOption)); }
        }
        private Ear currentAudioEar;
        #endregion

        #region Commands
        private string _selectedOption = "";
        public ICommand StartTestCommand => new Command(StartTest);
        public ICommand TargetAudienceSelectedCommand => new Command(GetTest);
        public ICommand PlayFrequencyCommand { get; }
        public ICommand SaveQuestionCommand => new Command(SaveTextAnswer);
        public ICommand SaveAudioQuestionCommand => new Command(SaveAudioAnswer);
        public ICommand OpenTestManagementCommand => new Command(OpenTestManagement);
        #endregion Commands
        private BackgroundWorker _worker;
        #region Constructor
        public TestViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            SetTestExplanationView(VISIBLE);
            this.navigationStore.AddPreviousViewModel(new HomeViewModel(navigationStore));


            ITargetAudienceRepository targetAudienceRepository = new TargetAudienceRepository();
            targetAudienceService = new TargetAudienceService(targetAudienceRepository);

            ITestRepository testRepository = new TestRepository();
            testService = new TestService(testRepository);

            nAudioPlayer = new NAudioPlayer();
 

        }
        #endregion Constructor

        #region SetVisibility
        private void SetTestExplanationView(string visibility)
        {
            ShowTestExplanationView = visibility;
        }
        private void SetTestTargetAudienceView(string visibility)
        {
            ShowTestTargetAudienceView = visibility;
        }
        private void SetTestToneAudiometryView(string visibility)
        {
            ShowTestToneAudiometryView = visibility;
        }
        private void SetQuestionRadioButtons(string visibility)
        {
            QuestionRadioButtons = visibility;
        }
        private void SetQuestionInput(string visibility)
        {
            QuestionInput = visibility;
        }
        private void SetTestTextQuestionView(string visibility)
        {
            ShowTestTextQuestionView = visibility;
        }
        #endregion SetVisibility
        
        private void OpenTestManagement()
        {
            navigationStore!.CurrentViewModel = new TestOverviewViewModel(navigationStore);

        }
        private void StartTest()
        {
            // nAudioPlayer.PlayFrequency(2000, 45, 0.9f);

            SetTestExplanationView(NOTVISIBLE);
            SetTestTargetAudienceView(VISIBLE);
            GetTargetAudiencesWithTest();
            TextQuestion = "Wat is uw leeftijdsgroep?";
            SetQuestionRadioButtons(VISIBLE);
        }
        private void GetTargetAudiencesWithTest()
        {
            List<TargetAudience> tempTargetAudiences = targetAudienceService.GetAllTargetAudiences();
            List<Test> tempTests = testService.GetAllTests();
            List<TargetAudience> finalList = new List<TargetAudience>();

            foreach (var test in tempTests)
            {
                foreach (var audience in tempTargetAudiences)
                {
                    if (test.TargetAudience.Id == audience.Id)
                    {
                        if (!finalList.Contains(audience))
                        {
                            finalList.Add(audience);
                        }

                    }
                }
            }

            TargetAudiences = new ObservableCollection<TargetAudience>(finalList);
            List<string> TargetAudienceOptions = new();
            foreach (TargetAudience targetAudience in TargetAudiences)
            {
                TargetAudienceOptions.Add(targetAudience.Label);
            }
            RadioButtons = TargetAudienceOptions;
        }
        private void GetTest()
        {
            TargetAudience? selectedTargetAudience = TargetAudiences.FirstOrDefault(item => item.Label == SelectedOption);
            if (selectedTargetAudience == null) return;

            SetTestTargetAudienceView(NOTVISIBLE);
            Test = testService.GetTestByTargetAudienceIdAndActive(selectedTargetAudience.Id); 
            testProgressData = new TestProgressData(Test);
     
            if(Test.TextQuestions.Count() > 0)
                DetermineTextQuestion();
            else if(Test.ToneAudiometryQuestions.Count() > 0)
                DetermineAudioQuestion();
        }
        private void DetermineTextQuestion()
        {
            if (testProgressData.CurrentQuestionNumber == 0)
            {
                TextQuestion = Test.TextQuestions.First().Question;
                currentTextQuestion = Test.TextQuestions.First();
                testProgressData.CurrentQuestionNumber = Test.TextQuestions.First().QuestionNumber;
                highestTextQuestionNumber = Test.TextQuestions.MaxBy(x => x.QuestionNumber).QuestionNumber;
            }
            else
            {
                //set new currentQuestionNumber
                testProgressData.CurrentQuestionNumber = testProgressData.CurrentQuestionNumber + 1;

                //Get new Question
                currentTextQuestion = Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
                TextQuestion = currentTextQuestion?.Question;
            }
            SetVisualsTextQuestion();
        }
        private void SetVisualsTextQuestion()
        {
            //remove given answer + everything that's ugly
            QuestionInputText = string.Empty;
            SetQuestionRadioButtons(NOTVISIBLE);
            //check if multiselect
            if (currentTextQuestion.IsMultiSelect)
            {
                SetQuestionInput(NOTVISIBLE);
                List <string> options = testService.ConvertQuestionOptionsToStrings(currentTextQuestion.Options);
                List<string> tempRadioButtons = new();
                foreach (string option in options)
                {
                    tempRadioButtons.Add(option);
                }

                RadioButtons = tempRadioButtons;
                SetQuestionRadioButtons(VISIBLE);
            }
            else if (currentTextQuestion.HasInputField)
            {
                SetTestTextQuestionView(VISIBLE);
                SetQuestionInput(VISIBLE);
            }
            SetTestTargetAudienceView(NOTVISIBLE);
            SetTestTextQuestionView(VISIBLE);
        }
        private void SetAudioVisuals()
        {
            QuestionRadioButtons = "Hidden";
            ShowTestTextQuestionView = "Hidden";
            QuestionInput = "Hidden";
            ShowTestToneAudiometryView = "Visible";
            AnswerButtonEnabled = false;
        }
        private void SaveTextAnswer()
        {
            List<string> answers = new();
            //check type of question
            if (Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).IsMultiSelect)
            {
                //ABC question
                answers.Add(SelectedOption);
            }
            else
            {
                //textQuestion
                answers.Add(QuestionInputText);
            }

            //save answers to TestProgressData
            List<string> options = testService.ConvertQuestionOptionsToStrings(Test.TextQuestions.First(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).Options);
            testProgressData.TextAnswers.Add(new TextAnswer(testProgressData.CurrentQuestionNumber, options, answers));
            DetermineNextTextStep();
        }
        private void SaveAudioAnswer(string value)
        {
            //save answers to TestProgressData
            ToneAudiometryQuestion q = testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
            testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(q.QuestionNumber, q.Frequency, currentAudioEar, q.StartingDecibels, lowestDecibel, value));
            DetermineNextAudioStep(value);
            AnswerButtonEnabled = false;
        }
        private void DetermineNextTextStep()
        {
            //if there are text questions left
            if (testProgressData.CurrentQuestionNumber != highestTextQuestionNumber)
            {
                DetermineTextQuestion();
            }
            else
            {
                if (Test.ToneAudiometryQuestions.Count() > 0)
                {
                    testProgressData.CurrentQuestionNumber = 0;
                    DetermineAudioQuestion();
                }
            }
        }
        private void DetermineAudioQuestion()
        {
            if (testProgressData.CurrentQuestionNumber == 0)
            {
                testProgressData.CurrentQuestionNumber = Test.ToneAudiometryQuestions.First().QuestionNumber;
                highestAudioQuestionNumber = Test.ToneAudiometryQuestions.MaxBy(x => x.QuestionNumber).QuestionNumber;
                DetermineWhichEar();
            }
            else
            {
                if (testProgressData.CurrentQuestionNumber != highestAudioQuestionNumber)
                {
                    testProgressData.CurrentQuestionNumber = testProgressData.CurrentQuestionNumber + 1;
                    testedLeftEar = false;
                    testedRightEar = false;
                    DetermineWhichEar();
                }
                else
                {
                    ShowResults();
                    return;
                }
            }
            currentAudiometryQuestion = Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
            currentFrequency = currentAudiometryQuestion.Frequency;
            playDecibel = currentAudiometryQuestion.StartingDecibels;
            AskAudioQuestion();
        }
        private void AskAudioQuestion()
        {
            SetAudioVisuals();
            StartProgress();
            PlayFrequency(currentFrequency, playDecibel, currentAudioEar);
        }
        private void DetermineWhichEar()
        {
            Random random = new Random();
            int ear = random.Next(0, 1);

            if (ear == 1)
            {
                testedRightEar = true;
                currentAudioEar = Ear.Right;
            }
            else{
                currentAudioEar = Ear.Left;
                testedLeftEar = true;
            }            
        }
        private void DetermineNextAudioStep(string value)
        {
            if (finalDecibelToPlay)
            {
                finalDecibelToPlay = false;
                lowestDecibel = 0;

                if (testedRightEar && testedLeftEar) 
                {
                    DetermineAudioQuestion();
                }
                else
                {
                    if (testedLeftEar)
                    {
                        currentAudioEar = Ear.Right;
                        testedRightEar = true;
                    }
                    else
                    {
                        currentAudioEar = Ear.Left;
                        testedLeftEar= true;
                    }
                    currentAudiometryQuestion = Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
                    currentFrequency = currentAudiometryQuestion.Frequency;
                    playDecibel = currentAudiometryQuestion.StartingDecibels;
                    AskAudioQuestion();
                }
            }
            else
            {
                DetermineNextDecibel(value);
            }
        }
        private void DetermineNextDecibel(string answer)
        {
            if (answer == "true")
            {
                playDecibel = playDecibel - 10;
            }
            else
            {
                playDecibel = playDecibel + 5;
                lowestDecibel = playDecibel;
                finalDecibelToPlay = true;
            }
            StartProgress();
            PlayFrequency(currentFrequency, playDecibel, currentAudioEar);
        }
        public void StartProgress()
        {
            _worker = new BackgroundWorker();
            _worker.RunWorkerCompleted += WorkCompleted;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += Worker_DoWork;
            _worker.ProgressChanged += Worker_ProgressChanged;

            _worker.RunWorkerAsync();

        }
        private void WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressValue = 100;
            ProgressTextBlock = "";
            _worker.Dispose();
            AnswerButtonEnabled = true;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressValue = e.ProgressPercentage;
            ProgressTextBlock = (string)e.UserState;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(100, String.Format("proces it 100."));
            for (int i = 100; i >= 0; i = i - 20)
            {
                Thread.Sleep(500); // Simulate a delay
                _worker.ReportProgress(i - 10, string.Format("proc it {0}.", i - 10));
            }
            worker.ReportProgress(0, "doneeee");
        }
        private void PlayFrequency(int frequency, int decibel, Ear ear)
        {
            Random random = new Random();
            int delay = random.Next(0, 3) * 1000;

            Task.Delay(delay).ContinueWith(_ =>
            {
                nAudioPlayer.PlayFrequency(frequency, decibel, ear);
            });
        }
        private void ShowResults()
        {
            SetTestTextQuestionView(NOTVISIBLE);
            SetTestToneAudiometryView(NOTVISIBLE);

            // toegevoegd door jasper
            navigationStore!.CurrentViewModel = new TestResultViewModel(navigationStore, testProgressData);
        }
    }
}
