using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Services;
using DataAccess.MockData;
using DataAccess.Models.TestData_Management;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels
{
    internal class TestViewModel : ViewModelBase
    {
        #region Const
        private const string NOTVISIBLE = "Hidden";
        private const string VISIBLE = "Visibile";
        #endregion Const

        private readonly NavigationStore navigationStore;
        private string _showTestExplanationView = "Hidden";
        private string _showTestTargetAudienceView = "Hidden";
        private string _showTestTextQuestionView = "Hidden";
        private string _showTestToneAudiometryView = "Hidden";
        private string _showTestResultView = "Hidden";

        private ObservableCollection<ITargetAudience> _targetAudiences;
        private ITest _test;
        private TargetAudienceService targetAudienceService { get; set; }
        private TestService testService { get; set; }
        private TestProgressData testProgressData { get; set; }
        private int currentFrequency = 0;
        private bool heard = false;
        private bool finalDecibelToPlay = false;
        #region Properties

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
        public ObservableCollection<ITargetAudience> TargetAudiences
        {
            get { return _targetAudiences; }
            set
            {
                _targetAudiences = value;
                OnPropertyChanged(nameof(TargetAudiences));
            }
        }

        // toegevoegd door jasper
        //public int selectedTargetAudience
        //{
        //    get { return _selectedTargetAudience; }
        //    set
        //    {
        //        _selectedTargetAudience = value;
        //        OnPropertyChanged(nameof(selectedTargetAudience));
        //        GetTest();
        //    }
        //}
        //private int _selectedTargetAudience { get; set; }

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
        public ITest Test
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
        #endregion

        private string _selectedOption = "";
        public ICommand StartTestCommand => new Command(StartTest);
        public ICommand TargetAudienceSelectedCommand => new Command(GetTest);
        public ICommand PlayFrequencyCommand { get; }
        public ICommand SaveQuestionCommand => new Command(SaveTextAnswer);
        public ICommand SaveAudioQuestionCommand => new Command(SaveAudioAnswer);
        public ICommand OpenTestManagementCommand => new Command(OpenTestManagement);

        #region Constructor
        public TestViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            SetTestExplanationView(VISIBLE);
            ITargetAudienceRepository targetAudienceRepository = new TargetAudienceRepository();
            targetAudienceService = new TargetAudienceService(targetAudienceRepository);

            ITestRepository testRepository = new TestRepository();
            testService = new TestService(testRepository);
        }
        #endregion Constructor
        private void OpenTestManagement()
        {
            navigationStore!.CurrentViewModel = new TestOverviewViewModel(navigationStore);

        }

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

        private void StartTest()
        {
            SetTestExplanationView(NOTVISIBLE);
            SetTestTargetAudienceView(VISIBLE);
            GetTargetAudiencesWithTest();
            TextQuestion = "Wat is uw leeftijdsgroep?";
            SetQuestionRadioButtons(VISIBLE);
        }
        private void GetTargetAudiencesWithTest()
        {
            List<ITargetAudience> tempTargetAudiences = targetAudienceService.GetAllTargetAudiences();
            List<ITest> tempTests = testService.GetAllTests();
            List<ITargetAudience> finalList = new List<ITargetAudience>();

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

            TargetAudiences = new ObservableCollection<ITargetAudience>(finalList);
            List<string> TargetAudienceOptions = new();
            foreach (ITargetAudience targetAudience in TargetAudiences)
            {
                TargetAudienceOptions.Add(targetAudience.Label);
            }
            RadioButtons = TargetAudienceOptions;
        }
        private void GetTest()
        {
            // toegevoegd door jasper
            ITargetAudience? selectedTargetAudience = TargetAudiences.FirstOrDefault(item => item.Label == SelectedOption);
            if (selectedTargetAudience == null) return;
            // =====

            SetTestTargetAudienceView(NOTVISIBLE);
            Test = testService.GetTest(selectedTargetAudience.Id); // toegevoegd door jasper
            testProgressData = new TestProgressData(Test);
            testProgressData.CurrentQuestionNumber = 1;
            AskFirstQuestion();
        }
        private void AskFirstQuestion()
        {
            // toegevoegd door jasper
            RadioButtons = new List<string>();
            // =====

            SetQuestionRadioButtons(NOTVISIBLE);
            TextQuestion = Test.TextQuestions.First().Question;
            testProgressData.CurrentQuestionNumber = Test.TextQuestions.First().QuestionNumber;
            SetTestTextQuestionView(VISIBLE);
            SetQuestionInput(VISIBLE);
        }
        private void SaveTextAnswer()
        {
            List<string> answers = new();
            if (Test == null)
            {
                return;
            }
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
            List<string> options = Test.TextQuestions.First(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).Options;
            testProgressData.TextAnswers.Add(new TextAnswer(testProgressData.CurrentQuestionNumber, options, answers));

            //remove given answer + everything that's ugly
            QuestionInputText = string.Empty;
            NextQuestion();
        }
        private Ear DetermineWhichEar()
        {
            return Ear.Left;
        }
        private void SaveAudioAnswer(string value)
        {
            //save answers to TestProgressData
            IToneAudiometryQuestion q = testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
            testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(q.QuestionNumber, q.Frequency, DetermineWhichEar(), q.StartingDecibels, lowestDecibel, value));
            DetermineNextStep(value);
        }
        private void DetermineNextStep(string value)
        {
            if (finalDecibelToPlay)
            {
                finalDecibelToPlay = false;
                lowestDecibel = 0;
                NextQuestion();
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
            PlayFrequency(currentFrequency, playDecibel);
        }
        private void NextQuestion()
        {
            ITextQuestion e = Test.TextQuestions.MaxBy(x => x.QuestionNumber);
            IToneAudiometryQuestion a = Test.ToneAudiometryQuestions.MaxBy(x => x.QuestionNumber);

            // toegevoegd door jasper
            bool isLastTextQuestion = testProgressData.CurrentQuestionNumber == e.QuestionNumber;
            if (!isLastTextQuestion && !isDoneText)
            {
                //Get new Question
                testProgressData.CurrentQuestionNumber = testProgressData.CurrentQuestionNumber + 1;
                TextQuestion = Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber)?.Question; // toegevoegd door jasper

                //check if multiselect
                if (Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).IsMultiSelect)
                {
                    QuestionInput = "Hidden";
                    List<string> tempRadioButtons = new();
                    foreach (string option in Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).Options)
                    {
                        tempRadioButtons.Add(option);
                    }

                    RadioButtons = tempRadioButtons;
                    QuestionRadioButtons = "Visible";
                    return;
                }
                else if (Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).HasInputField)
                {
                    ShowTestTextQuestionView = "Visible";
                    QuestionInput = "Visible";
                    return;
                }

                //check if next question is audio question or not

            }
            else
            {
                if (!isDoneText)
                {
                    testProgressData.CurrentQuestionNumber = 1;
                }
                else
                {
                    testProgressData.CurrentQuestionNumber++;
                }
                isDoneText = true;
                
            }

            if (testProgressData.CurrentQuestionNumber <= a.QuestionNumber && !isDoneAudio)
            {
                QuestionRadioButtons = "Hidden";
                ShowTestTextQuestionView = "Hidden";
                QuestionInput = "Hidden";
                if (testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber) != null)
                {
                    //yes there is an audio question
                    ShowTestToneAudiometryView = "Visible";
                    IToneAudiometryQuestion q = testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
                    currentFrequency = q.Frequency;
                    playDecibel = q.StartingDecibels;
                    PlayFrequency(currentFrequency, playDecibel);
                }
            }
            else
            {
                isDoneAudio = true;
            }

            if (isDoneAudio && isDoneText)
            {
                ShowResults();
            }
        }
        private void PlayFrequency(int frequency, int decibel)
        {
            //NAUDIO!!
            //add decibel as a variable to change

            Random random = new Random();
            int delay = random.Next(0, 3) * 1000;

            Task.Delay(delay).ContinueWith(_ =>
            {
                AudioManager.PlaySound(frequency, 700);
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
