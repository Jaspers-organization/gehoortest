using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using BusinessLogic.Projections;
using BusinessLogic.Services;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;
using UserInterface.ViewModels.Modals;

namespace UserInterface.ViewModels;

internal class TestManagementViewModel : ViewModelBase, IConfirmation
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestService testService;
    private readonly TargetAudienceService targetAudienceSerivce;
    private readonly EmployeeService employeeService;

    private readonly bool newTest;
    #endregion

    #region Commands
    public ICommand SaveTestCommand => new Command(SaveTest);
    public ICommand BackToTestOverviewCommand => new Command(BackToTestOverview);

    public ICommand OpenNewTextModalCommand => new Command(OpenNewTextModal);
    public ICommand OpenTextModalCommand => new Command(OpenTextModal);
    public ICommand DeleteTextQuestionCommand => new Command(DeleteTextQuestion);

    public ICommand OpenNewAudioModalCommand => new Command(OpenNewAudioModal);
    public ICommand OpenAudioModalCommand => new Command(OpenAudioModal);
    public ICommand DeleteAudioQuestionCommand => new Command(DeleteAudioQuestion);


    #endregion

    #region Properties
    private List<TargetAudience>? _targetAudiences;
    public List<TargetAudience>? TargetAudiences
    {
        get { return _targetAudiences; }
        set { _targetAudiences = value; OnPropertyChanged(nameof(TargetAudiences)); }
    }
    private TargetAudience initalTargetAudience;

    private TargetAudience? _selectedTargetAudience;
    public TargetAudience? SelectedTargetAudience
    {
        get { return _selectedTargetAudience; }
        set
        {
            _selectedTargetAudience = value; OnPropertyChanged(nameof(SelectedTargetAudience));
            Test.TargetAudienceId = _selectedTargetAudience.Id;
            Test.TargetAudience = _selectedTargetAudience;
        }
    }
    private string? _status;
    public string? Status
    {
        get { return _status; }
        set { _status = value; OnPropertyChanged(nameof(Status)); }
    }

    private string? _testName;
    public string? TestName
    {
        get { return _testName; }
        set { _testName = value; OnPropertyChanged(nameof(TestName)); Test.Title = value; }
    }
    private ObservableCollection<TextQuestion>? _textQuestions;
    public ObservableCollection<TextQuestion>? TextQuestions
    {
        get { return _textQuestions; }
        set { _textQuestions = value; OnPropertyChanged(nameof(TextQuestions)); }
    }

    private ObservableCollection<ToneAudiometryQuestion>? _audioQuestions;
    public ObservableCollection<ToneAudiometryQuestion>? AudioQuestions
    {
        get { return _audioQuestions; }
        set { _audioQuestions = value; OnPropertyChanged(nameof(AudioQuestions)); }
    }

    #endregion

    #region Errors
    private bool CheckValidityInput()
    {
        // Check the validity of the input
        string testNameValidation = ErrorService.ValidateInput("TestName", TestName!);
        string audienceValidation = ErrorService.ValidateInput("Audience", SelectedTargetAudience!);

        if (!string.IsNullOrEmpty(testNameValidation))
        {
            OpenErrorModal(testNameValidation);
            return false;
        }

        if (!string.IsNullOrEmpty(audienceValidation))
        {
            OpenErrorModal(audienceValidation);
            return false;
        }
        return true;
    }
    #endregion

    public bool IsConfirmed { get; set; }
    public Test Test { get; set; }
    private ConfirmationModalViewModel confirmationModalViewModel { get; set; }

    public TestManagementViewModel(NavigationStore navigationStore, Test? test = null)
    {
        //Dependencies initialization
        this.navigationStore = navigationStore;
        this.navigationStore.HideTopBar = true;
        testService = new TestService(new TestRepository());
        employeeService = new EmployeeService(new EmployeeRepository());
        targetAudienceSerivce = new TargetAudienceService(new TargetAudienceRepository());

        //set values
        SetTargetAudiences();
        if (TargetAudiences == null)
            return;

        if (test != null)
        {
            SetTestValues(test);
            testService.SetTest(test);
        }
        else
        {
            newTest = true;
            CreateTest();
            SetStatus(false);
            SetSelected(0);
        }
    }


    #region Navigation
    private void BackToTestOverview()
    {
        Action backAction = () =>
        {
            navigationStore!.CurrentViewModel = CreateTestOverviewViewModel();
        };

        OpenConfirmationModal(CreateAction(backAction), "Weet je zeker dat je terug wilt gaan? Alle wijzigingen zullen ongedaan worden gemaakt.");
    }
    #endregion

    #region Initialization
    // Sets various properties based on the provided test data
    private void SetTestValues(Test test)
    {
        // Set the overall test
        SetTest(test);

        // Set audio questions based on provided test's tone audiometry questions
        SetAudioQuestions(new ObservableCollection<ToneAudiometryQuestion>(test.ToneAudiometryQuestions));

        // Set text questions based on provided test's text questions
        SetTextQuestions(new ObservableCollection<TextQuestion>(test.TextQuestions));

        // If there's a list of audiences available
        if (TargetAudiences != null)
        {
            // Set the target audience for the test
            SetAudience(TargetAudiences.First(t => t.Id == test.TargetAudience.Id));
        }

        // Set the test name
        SetTestName(test.Title);

        // Set the status of the test (Active/Inactive)
        SetStatus(test.Active);

        // Set the selected target audience ID
        SetSelected(TargetAudiences!.FindIndex(t => t.Id == test.TargetAudience.Id));

        initalTargetAudience = test.TargetAudience;
    }

    //
       private void SetTargetAudiences() => TargetAudiences = targetAudienceSerivce.GetAllTargetAudiences();


    // Sets the overall test
    private void SetTest(Test test) => Test = test;

    // Sets text questions for the test
    private void SetTextQuestions(ObservableCollection<TextQuestion> questions) => TextQuestions = questions;

    // Sets audio questions for the test
    private void SetAudioQuestions(ObservableCollection<ToneAudiometryQuestion> questions) => AudioQuestions = questions;

    // Sets the target audience for the test
    private void SetAudience(TargetAudience audience) => SelectedTargetAudience = audience;

    // Sets the test name
    private void SetTestName(string title) => TestName = title;

    // Sets the status of the test (Active/Inactive)
    private void SetStatus(bool active) {
        Status = active ? "Actief" : "Inactief";
        Test.Active = active;
    } 

    // Sets the selected target audience ID
    private void SetSelected(int id) => SelectedTargetAudience = TargetAudiences![id];

    // Creates a new test
    private void CreateTest()
    {
        // Creates a new test using the test service
        Test = testService.CreateTest();
    }
    #endregion

    #region Modals

    #region Text
    // Open a new modal for entering text questions.
    private void OpenNewTextModal()
    {
        try
        {
            // Create a new text question and set its number.
            TextQuestion textQuestion = testService.CreateTextQuestion();

            // Open a modal for the new text question.
            navigationStore.OpenModal(new TextQuestionModalViewModel(navigationStore, textQuestion, true, this, testService));
        }
        catch (Exception ex)
        {
            // Log and display an error if opening the new text window fails.
            OpenErrorModal("Er is iets fout gegaan bij het openen van de text modal");
            Console.WriteLine("Error occurred: " + ex);
        }
    }

    // Open a modal for an existing text question based on its question number.
    private void OpenTextModal(int questionNumber)
    {
        try
        {
            // Retrieve the text question based on the question number.
            TextQuestion textQuestion = Test.TextQuestions.First(q => q.QuestionNumber == questionNumber);

            // Open a modal for the existing text question.
            navigationStore.OpenModal(new TextQuestionModalViewModel(navigationStore, textQuestion, false, this, testService));
        }
        catch (Exception ex)
        {
            // Log and display an error if opening the text window fails.
            OpenErrorModal("Er is iets fout gegaan bij het openen van de text modal");
            Console.WriteLine("Error occurred: " + ex);
        }
    }
    #endregion

    #region Audio
    // Open a new modal for entering audio questions.
    private void OpenNewAudioModal()
    {
        try
        {
            // Create a new audio question and set its number.
            ToneAudiometryQuestion audioQuestion = testService.CreateToneAudiometryQuestion();
            // Open a modal for the new audio question.
            navigationStore.OpenModal(new AudioQuestionModalViewModel(navigationStore, audioQuestion, true, this));
        }
        catch (Exception ex)
        {
            // Log and display an error if opening the new audio window fails.
            OpenErrorModal("Er is iets fout gegaan bij het openen van de audio modal");
            Console.WriteLine("Error occurred: " + ex);
        }
    }

    // Open a modal for an existing audio question based on its question number.
    private void OpenAudioModal(int questionNumber)
    {
        try
        {
            // Retrieve the audio question based on the question number.
            ToneAudiometryQuestion audioQuestion = Test.ToneAudiometryQuestions.First(q => q.QuestionNumber == questionNumber);

            // Open a modal for the existing audio question.
            navigationStore.OpenModal(new AudioQuestionModalViewModel(navigationStore, audioQuestion, false, this));
        }
        catch (Exception ex)
        {
            // Log and display an error if opening the audio window fails.
            OpenErrorModal("Er is iets fout gegaan bij het openen van de audio modal");
            Console.WriteLine("Error occurred: " + ex);
        }
    }
    #endregion

    // Create an action that invokes the provided action only if confirmation is received.
    public Action CreateAction(Action action)
    {
        return () =>
        {
            if (!IsConfirmed) return; // Do nothing if confirmation is not received.
            action?.Invoke();
        };
    }

    // Open a modal for confirming an action with the provided text.
    public void OpenConfirmationModal(Action action, string text)
    {
        // Create a confirmation modal view model and open the confirmation modal.
        confirmationModalViewModel = new ConfirmationModalViewModel(navigationStore, text, this, action);
        navigationStore.OpenModal(confirmationModalViewModel);
    }

    // Open a modal for displaying an error with the provided text.
    private void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));

    #endregion

    #region Test Modification
    #region Text

    // Adds a new text question to the test and updates the view.
    public void AddNewTextQuestion(TextQuestion question)
    {
        try
        {
            Test.TextQuestions = testService.AddTextQuestion(question);
            UpdateTextQuestionListView();
        }
        catch (Exception ex)
        {
            // Handles exceptions that occur during the addition of a text question.
            OpenErrorModal("Er is een fout opgetreden bij het maken van de tekstvraag");
            Console.WriteLine("Error occurred: " + ex);
        }
    }

    // Updates a text question in the test and refreshes the view.
    public void UpdateTextQuestion(TextQuestion question)
    {
        try
        {
            Test.TextQuestions = testService.UpdateQuestion(Test.TextQuestions.ToList(), question.QuestionNumber, question);
            UpdateTextQuestionListView();
        }
        catch (Exception ex)
        {
            // Handles exceptions that occur during the update of a text question.
            OpenErrorModal("Er is een fout opgetreden bij het bijwerken van de tekstvraag");
            Console.WriteLine("Error occurred: " + ex);

        }
    }

    // Refreshes the text question view.
    private void UpdateTextQuestionListView() => TextQuestions = new ObservableCollection<TextQuestion>(Test.TextQuestions);

    // Deletes a text question from the test.
    private void DeleteTextQuestion(int questionNumber)
    {
        Action deleteAction = () =>
        {
            Test.TextQuestions = testService.DeleteQuestion(Test.TextQuestions.ToList(), questionNumber);
            UpdateTextQuestionListView();
        };

        OpenConfirmationModal(CreateAction(deleteAction), "Weet u zeker dat u deze vraag wilt verwijderen?");
    }
    #endregion

    #region Audio

    // Adds a new tone audiometry question to the test and updates the view.
    public void AddNewToneAudiometryQuestion(ToneAudiometryQuestion question)
    {
        try
        {
            Test.ToneAudiometryQuestions = testService.AddToneAudiometryQuestion(question);
            UpdateAudioQuestionListView();
        }
        catch (Exception ex)
        {
            // Handles exceptions that occur during the addition of an audiometry question.
            OpenErrorModal($"Er is een fout opgetreden bij het toevoegen van de nieuwe audiometrie vraag");
            Console.WriteLine("Error occurred: " + ex);

        }
    }

    // Updates a tone audiometry question in the test and refreshes the view.
    public void UpdateToneAudiometryQuestion(ToneAudiometryQuestion question)
    {
        try
        {
            Test.ToneAudiometryQuestions = testService.UpdateQuestion(Test.ToneAudiometryQuestions.ToList(), question.QuestionNumber, question);
            UpdateAudioQuestionListView();
        }
        catch (Exception ex)
        {
            // Handles exceptions that occur during the update of an audiometry question.
            OpenErrorModal($"Er is een fout opgetreden bij het bijwerken van de audiometrie vraag");
            Console.WriteLine("Error occurred: " + ex);

        }
    }

    // Refreshes the audio question view.
    private void UpdateAudioQuestionListView()
    {
        AudioQuestions = new ObservableCollection<ToneAudiometryQuestion>(Test.ToneAudiometryQuestions);
    }

    // Deletes an audio question from the test.
    private void DeleteAudioQuestion(int questionNumber)
    {
        Action deleteAction = () =>
        {
            Test.ToneAudiometryQuestions = testService.DeleteQuestion(Test.ToneAudiometryQuestions.ToList(), questionNumber);
            UpdateAudioQuestionListView();
        };

        OpenConfirmationModal(CreateAction(deleteAction), "Weet je zeker dat je deze vraag wilt verwijderen?");
    }
    #endregion

    private TestOverviewViewModel CreateTestOverviewViewModel()
    {
        return new TestOverviewViewModel(navigationStore);
    }
    private void CheckTargetAudienceChanged()
    {
        if (testService.TargetAudienceChanged(Test.TargetAudience, initalTargetAudience))
            Test.Active = false;
    }
    private void SetEmployee()
    {
        Guid EmployeeId = navigationStore.LoggedInEmployee.Id;
        Test.EmployeeId = EmployeeId;
        Test.Employee = employeeService.GetEmployeeById(EmployeeId);
    }
    private void SaveTest()
    {
        try
        {
            if (!CheckValidityInput())
                return;

            if(Test.Employee == null && Test.EmployeeId == Guid.Empty)
                SetEmployee();

            CheckTargetAudienceChanged();

            testService.SaveOrUpdateTest(Test, newTest);

            // Navigates to the test overview after the save/update operation.
            navigationStore!.CurrentViewModel = CreateTestOverviewViewModel();
        }
        catch (Exception ex)
        {
            // Handles exceptions that occur during the save/update operation.
            OpenErrorModal("Er is een fout opgetreden bij het opslaan van de test.");
            Console.WriteLine("Error occurred: " + ex);

        }
    }
    #endregion
}
