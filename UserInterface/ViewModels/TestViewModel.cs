using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.IModels;
using BusinessLogic.IRepositories;
using BusinessLogic.Services;
using DataAccess.MockData;
using gehoortest_application.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private TargetAudienceService targetAudienceService { get; set; }

        private TestService testService { get; set; }
        private TestProgressData testProgressData { get; set; }
        private readonly Repository repository;
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
        #endregion

        private string _selectedOption = "";
        public ICommand StartTestCommand => new Command(StartTest);
        public ICommand TargetAudienceSelectedCommand => new Command(GetTest);
        public ICommand PlayFrequencyCommand { get; }
        public ICommand SaveQuestionCommand => new Command(SaveAnswer);
        public ICommand SaveAudioQuestionCommand => new Command(SaveAudioQuestion);
        public ICommand OpenTestManagementCommand => new Command(OpenTestManagement);

        public TestViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            ShowTestExplanationView = "Visible";
            ITargetAudienceRepository targetAudienceRepository = new TargetAudienceMockRepository();
            targetAudienceService = new TargetAudienceService(targetAudienceRepository);

            ITestRepository testRepository = new TestMockRepository();
            testService = new TestService(testRepository);
        }

        private void OpenTestManagement()
        {
            navigationStore!.CurrentViewModel = new TestOverviewViewModel(navigationStore, repository);

        }
        public void StartTest()
        {
            ShowTestExplanationView = "Hidden";
            ShowTestTargetAudienceView = "Visible";
            TargetAudiences = GetAllTargetAudiencesWithTests();
            TextQuestion = "Wat is uw leeftijdsgroep?";

            // toegevoegd door jasper
            List<string> tempTargetAudiences = new();
            foreach (ITargetAudience targetAudience in TargetAudiences)
            {
                tempTargetAudiences.Add(targetAudience.Label);
            }
            RadioButtons = tempTargetAudiences;
            QuestionRadioButtons = "Visible";
            // =====
        }

        private ObservableCollection<ITargetAudience> GetAllTargetAudiencesWithTests()
        {
            var x = targetAudienceService.GetAllTargetAudiences();
            var y = new ObservableCollection<ITest>(testService.GetAllTests());

            var z = new ObservableCollection<ITargetAudience>();

            foreach (var test in y)
            {
                foreach (var audience in x)
                {
                    if (test.TargetAudience.Id == audience.Id)
                    {
                        if (!z.Contains(audience))
                        {
                            z.Add(audience);
                        }

                    }
                }
            }

            return z;
        }
        private void GetTest()
        {
            // toegevoegd door jasper
            ITargetAudience? selectedTargetAudience = TargetAudiences.FirstOrDefault(item => item.Label == SelectedOption);
            if (selectedTargetAudience == null) return;
            // =====

            ShowTestTargetAudienceView = "Hidden";
            Test = testService.GetTestByTargetAudienceId(selectedTargetAudience.Id); // toegevoegd door jasper
            testProgressData = new TestProgressData(Test);
            testProgressData.CurrentQuestionNumber = 1;
            AskFirstQuestion();
        }

        private void AskFirstQuestion()
        {
            // toegevoegd door jasper
            RadioButtons = new List<string>();
            QuestionRadioButtons = "Hidden";
            // =====

            TextQuestion = Test.TextQuestions.First().Question;
            testProgressData.CurrentQuestionNumber = Test.TextQuestions.First().QuestionNumber;
            ShowTestTextQuestionView = "Visible";
            QuestionInput = "Visible";
        }

        private void SaveAnswer()
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
        bool isDoneText = false;
        bool isDoneAudio = false;
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
                    PlayFrequency(q.Frequency);
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

            // toegevoegd door jasper
            navigationStore!.CurrentViewModel = new TestResultViewModel(navigationStore, testProgressData);
        }
    }
}
