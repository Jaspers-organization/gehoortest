using BusinessLogic;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Repositorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        public ITest Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged(nameof(Test));
            }
        }


        public ICommand StartTestCommand => new Command(StartTest);
        public ICommand NextQuestionCommand { get; }
        public ICommand TargetAudienceSelectedCommand => new Command(GetTest);
        public ICommand PlayFrequencyCommand { get; }

        public TestViewModel()
        {
        }

        public TestViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            ShowTestExplanationView = "Visible";
            ITargetAudienceRepository targetAudienceRepository = new TargetAudienceRepository();
            targetAudienceService = new BusinessLogic.Services.TargetAudienceService(targetAudienceRepository);

            ITestRepository testRepository = new TestRepository();
            testService = new BusinessLogic.Services.TestService(testRepository);
        }

        public void StartTest()
        {
            ShowTestExplanationView = "Hidden";
            ShowTestTargetAudienceView = "Visible";
            GetTargetAudiences();
        }


        private void GetTargetAudiences()
        {
            TargetAudiences = new ObservableCollection<ITargetAudience>(targetAudienceService.GetTargetAudiences());
        }

        private void GetTest()
        {
            ShowTestTargetAudienceView = "Hidden";
            Test = testService.GetTest(selectedTargetAudience);

        }

        private void StartTextTest()
        {

        }

        private void StartToneAudiometryTest() 
        {
        
        }

        private void NextQuestion()
        {

        }

        private void PlayFrequency()
        {

        }
    }
}
