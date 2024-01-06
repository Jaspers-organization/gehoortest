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
using System.Windows;

namespace UserInterface.ViewModels
{
    internal class TestViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;

        #region Services
        private TargetAudienceService targetAudienceService { get; set; }
        private TestService testService { get; set; }
        private TestProgressData testProgressData { get; set; }
        #endregion Services

        private int highestTextQuestionNumber;
        private int highestAudioQuestionNumber;


        private AudioPlayer.AudioPlayer audioPlayer;
        private ObservableCollection<TargetAudience> _targetAudiences;
        private Test _test;
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
        #region Visibility

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

   
        public Test Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }

        public List<string> RadioButtons
        {
            get { return _radioButtons; }
            set { _radioButtons = value; OnPropertyChanged(nameof(RadioButtons)); }
        }
        private List<string> _radioButtons = new();

        #endregion

        #region Commands
        public ICommand StartTestCommand => new Command(GetTargetAudiences);
        public ICommand TargetAudienceSelectedCommand => new Command(StartTextQuestions);
        public ICommand AnswerTextQuestionCommand => new Command(AnswerTextQuestion);
        public ICommand SaveAudioQuestionCommand => new Command(AnswerToneAudiometryQuestion);
        public ICommand OpenTestManagementCommand => new Command(OpenTestManagement);

        #endregion Commands
        private BackgroundWorker worker;
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
        }
        #endregion Constructor



        #region Jasper rebuild
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
        }

        private void StartTextQuestions()
        {
            GetTest();
            DetermineTextQuestionJ();
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

        private void DetermineTextQuestionJ()
        {
            TextQuestion = Test.TextQuestions.First().Question;
            currentTextQuestion = Test.TextQuestions.First();
            testProgressData.CurrentQuestionNumber = Test.TextQuestions.First().QuestionNumber;
        }

        private void SetVisualsTextQuestion()
        {
            QuestionInputText = string.Empty;
            ShowQuestionRadioButtons = Visibility.Hidden;

            if (currentTextQuestion.IsMultiSelect)
            {
                ShowQuestionInput = Visibility.Hidden;
                List<string> options = testService.ConvertQuestionOptionsToStrings(currentTextQuestion.Options.ToList());
                List<string> tempRadioButtons = new();
                foreach (string option in options)
                {
                    tempRadioButtons.Add(option);
                }

                RadioButtons = tempRadioButtons;
                ShowQuestionRadioButtons = Visibility.Visible;
            }
            else if (currentTextQuestion.HasInputField)
            {
                ShowQuestionInput = Visibility.Visible;
            }
            ShowTestTextQuestionView = Visibility.Visible;
            ShowTestTargetAudienceView = Visibility.Hidden;
        }

        private void StartToneAudioMetryQuestions()
        {
            DetermineAudioQuestionJ();
            SetVisualsAudioQuestion();
        }

        private void DetermineAudioQuestionJ()
        {
            currentAudiometryQuestion = Test.ToneAudiometryQuestions.First();
            testProgressData.Decibel = currentAudiometryQuestion.StartingDecibels;
            testProgressData.CurrentQuestionNumber = currentAudiometryQuestion.QuestionNumber;
           

            AskAudioQuestion();
        }
        private void AnswerTextQuestion()
        {
            string answerToAdd = currentTextQuestion.IsMultiSelect ? SelectedTextOption : QuestionInputText;
            testProgressData.Add(answerToAdd, currentTextQuestion);
        
            currentTextQuestion = testProgressData.GetNextTextQuestion();
            if (currentTextQuestion == null)
            {
                StartToneAudioMetryQuestions();
            }
            else
            {
                TextQuestion = currentTextQuestion.Question;
            }
        }

        private void AnswerToneAudiometryQuestion(string value)
        {
            bool answer = value == "true" ? true : false;
            testProgressData.Add(answer, testProgressData.CurrentEar, currentAudiometryQuestion);

            currentAudiometryQuestion = testProgressData.GetNextTOneAudiometryQuestion();
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

        #endregion Jasper rebuild

        private void OpenTestManagement()
        {
            navigationStore!.CurrentViewModel = new TestOverviewViewModel(navigationStore);

        }




        #region OLD
        //private void GetTargetAudiencesWithTest()
        //{
        //    //TODO ERROR HERE.
        //    List<TargetAudience> tempTargetAudiences = targetAudienceService.GetAllTargetAudiences();
        //    List<Test> tempTests = testService.GetAllTests();
        //    List<TargetAudience> finalList = new List<TargetAudience>();

        //    foreach (var test in tempTests)
        //    {
        //        foreach (var audience in tempTargetAudiences)
        //        {
        //            if (test.TargetAudience.Id == audience.Id && test.Active)
        //            {
        //                if (!finalList.Contains(audience))
        //                {
        //                    finalList.Add(audience);
        //                }

        //            }
        //        }
        //    }

        //    TargetAudiences = new ObservableCollection<TargetAudience>(finalList);
        //    List<string> TargetAudienceOptions = new();
        //    foreach (TargetAudience targetAudience in TargetAudiences)
        //    {
        //        TargetAudienceOptions.Add(targetAudience.Label);
        //    }
        //    RadioButtons = TargetAudienceOptions;
        //}
        private void StartTest()
        {
            ShowTestExplanationView = Visibility.Hidden;
            ShowTestTargetAudienceView = Visibility.Visible;
            GetTargetAudiences();
            TextQuestion = "Wat is uw leeftijdsgroep?";
            ShowQuestionRadioButtons = Visibility.Visible;

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
        }
        private void SaveTextAnswer()
        {
            List<string> answers = new();
            //check type of question
            if (Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).IsMultiSelect)
            {
                //ABC question
                answers.Add(SelectedTextOption);
            }
            else
            {
                //textQuestion
                answers.Add(QuestionInputText);
            }

            //save answers to TestProgressData
            List<string> options = testService.ConvertQuestionOptionsToStrings(Test.TextQuestions.First(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).Options.ToList());
            testProgressData.TextAnswers.Add(new TextAnswer(testProgressData.CurrentQuestionNumber, options, answers));
            DetermineNextTextStep();
        }
        private void SaveAudioAnswer(string value)
        {
            ToneAudiometryQuestion q = testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
            //testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(q.QuestionNumber, q.Frequency, currentAudioEar, q.StartingDecibels, DecibelManager.LowestDecibel, value));
            //DetermineNextAudioStep(value);
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
                    //DetermineAudioQuestion();
                }
            }
        }

        #endregion





    }
}
