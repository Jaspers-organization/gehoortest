using Azure;
using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;
using DataAccess;
using DataAccess.Models.TestData_Management;
using DataAccess.Repositorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels
{
    internal class TestViewModel : ViewModelBase
    {
        private readonly NavigationStore navigationStore;
        private string _showTestExplanationView = "Hidden";
        private string _showTestTargetAudienceView = "Hidden";
        private string _showTestTextQuestionView = "Hidden";
        private string _showTestToneAudiometryView = "Hidden";
        private string _showTestResultView = "Hidden";
        private ObservableCollection<ITargetAudience> _targetAudiences;
        private ITest _test;
        private  BusinessLogic.Services.TargetAudienceService targetAudienceService { get; set; }

        private BusinessLogic.Services.TestService testService { get;set; }
        private TestProgressData testProgressData { get; set; }
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

        public int selectedTargetAudience
        {
            get { return _selectedTargetAudience; }
            set
            {
                _selectedTargetAudience = value;
                OnPropertyChanged(nameof(selectedTargetAudience));
                GetTest();
            }
        }
        private int _selectedTargetAudience { get;set; }

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
        private string _selectedOption = "";
        public ICommand StartTestCommand => new Command(StartTest);
        public ICommand TargetAudienceSelectedCommand => new Command(GetTest);
        public ICommand PlayFrequencyCommand { get; }
        public ICommand SaveQuestionCommand => new Command(SaveAnswer);
        public ICommand SaveAudioQuestionCommand => new Command(SaveAudioQuestion);

        public TestViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            ShowTestExplanationView = "Visible";
            ITargetAudienceRepository targetAudienceRepository = new TargetAudienceRepository();
            targetAudienceService = new BusinessLogic.Services.TargetAudienceService(targetAudienceRepository);

            ITestRepository testRepository = new DataAccess.Repositorys.TestRepository();
            testService = new BusinessLogic.Services.TestService(testRepository);
        }

        public void StartTest()
        {
            ShowTestExplanationView = "Hidden";
            ShowTestTargetAudienceView = "Visible";
            TargetAudiences = new ObservableCollection<ITargetAudience>(targetAudienceService.GetTargetAudiences());
            TextQuestion = "Wat is uw leeftijdsgroep?";
        }
        private void GetTest()
        {
            ShowTestTargetAudienceView = "Hidden";
            Test = testService.GetTest(selectedTargetAudience);
            testProgressData = new TestProgressData(Test);
            AskFirstQuestion();
        }

        private void AskFirstQuestion()
        {
            TextQuestion = Test.TextQuestions.First().Question;
            testProgressData.CurrentQuestionNumber = Test.TextQuestions.First().QuestionNumber;
            ShowTestTextQuestionView = "Visible";
            QuestionInput = "Visible";
        }

        private void SaveAnswer()
        {
            List<string> answers = new();

            //check type of question
            if (Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).IsMultipleSelect)
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
        private void SaveAudioQuestion(string value)
        {
            List<string> answers = new();
            answers.Add(value);

            //save answers to TestProgressData
            IToneAudiometryQuestion q = testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
            testProgressData.ToneAudiometryAnswers.Add(new ToneAudiometryAnswer(q.Frequency, q.StartingDecibels, 20, BusinessLogic.Enums.Ear.Left));

            //remove given answer + everything that's ugly
            NextQuestion();
        }
        private void NextQuestion()
        {
            ITextQuestion e = Test.TextQuestions.MaxBy(x => x.QuestionNumber);
            if (testProgressData.CurrentQuestionNumber < e.QuestionNumber)
            {
                //Get new Question
                testProgressData.CurrentQuestionNumber = testProgressData.CurrentQuestionNumber + 1;
                TextQuestion = Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).Question;

                //check if multiselect
                if (Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).IsMultipleSelect)
                {
                    QuestionInput = "Hidden";
                    List<string> tempRadioButtons = new();
                    foreach (string option in Test.TextQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber).Options)
                    {
                        tempRadioButtons.Add(option);
                    }

                    RadioButtons = tempRadioButtons;
                    QuestionRadioButtons = "Visible";
                }

                //check if next question is audio question or not
                if (testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber) != null)
                {

                    //yes there is an audio question
                    ShowTestTextQuestionView = "Hidden";
                    ShowTestToneAudiometryView = "Visible";

                    IToneAudiometryQuestion q = testProgressData.Test.ToneAudiometryQuestions.FirstOrDefault(x => x.QuestionNumber == testProgressData.CurrentQuestionNumber);
                    PlayFrequency(q.Frequency);
                }
            }
            else
            {
                ShowResults();
            }
        }
        private void PlayFrequency(int frequency)
        {
            Task.Delay(2000).ContinueWith(_ =>
            {
                AudioManager.PlaySound(frequency, 700);
            });
        }

        private void ShowResults()
        {
            ShowTestTextQuestionView = "Hidden";
            ShowTestToneAudiometryView = "Hidden";
            ShowTestResultView = "Visible";
        }
    }
}
