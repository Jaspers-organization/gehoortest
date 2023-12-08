using BusinessLogic.Enums;
using BusinessLogic.IModels;
using BusinessLogic.Interfaces;
using BusinessLogic.IRepositories;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DataAccess.MockData;
using DataAccess.Repositories;
using gehoortest_application.Repository;
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
    private readonly ITestRepository testRepository;
    private readonly ITargetAudienceRepository targetAudienceRepository;
    private readonly TestService testService;
    private readonly TargetAudienceService targetAudienceSerivce;
    private readonly TestOverviewViewModel testOverviewViewModel;

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
    private List<TargetAudience>? _audiencesList;
    public List<TargetAudience>? AudiencesList
    {
        get { return _audiencesList; }
        set { _audiencesList = value; OnPropertyChanged(nameof(AudiencesList)); }
    }
    private TargetAudience? _audience;
    public TargetAudience? Audience
    {
        get { return _audience; }
        set { _audience = value; OnPropertyChanged(nameof(Audience)); }
    }
    private int _selected;
    public int Selected
    {
        get { return _selected; }
        set
        {
            _selected = value; OnPropertyChanged(nameof(Selected));
            TargetAudience audience = AudiencesList[value];
            Audience = audience;
            Test.TargetAudience = audience;
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
    public string this[string columnName]
    {
        get
        {
            string? validationMessage = null;

            switch (columnName)
            {
                case "TestName":
                    // Check if the test name is empty or contains illegal characters
                    validationMessage = ValidateTestName(TestName!);
                    break;
                case "Audience":
                    // Validate if the audience is correctly selected
                    validationMessage = ValidateAudience(Audience!);
                    break;
                default:
                    break;
            }
            return validationMessage ?? string.Empty;
        }
    }
    private string ValidateTestName(string str)
    {
        // Validate if the test name is empty or contains illegal characters
        if (TestService.IsEmptyString(str!))
            return ErrorStore.ErrorTestName;
        else if (str.Contains(ErrorStore.IllegalCharacters))
            return ErrorStore.ErrorIllegalCharacters;
        return string.Empty;
    }
    private string ValidateAudience(TargetAudience targetAudience)
    {
        // Validate if the audience is correctly selected
        if (targetAudience == null || TestService.IsEmptyString(targetAudience.Label))
            return ErrorStore.ErrorAudience;
        return string.Empty;
    }
    private bool CheckValidityInput()
    {
        // Check the validity of the input
        string testNameValidation = this["TestName"];
        string audienceValidation = this["Audience"];

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

        testRepository = new TestRepository();
        targetAudienceRepository = new TargetAudienceRepository();
        testService = new TestService(testRepository);
        targetAudienceSerivce = new TargetAudienceService(targetAudienceRepository);


        //set values
        SetTargetAudiences();

        if (test != null)
        {
            SetTestValues(test);
        }
        else
        {
            newTest = true;
            CreateTest();
            SetStatus(false);
            //SetSelected(new Guid()); todo
        }
    }

    private void SetTargetAudiences() => AudiencesList = targetAudienceSerivce.GetAllTargetAudiences();


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
        if (AudiencesList != null)
        {
            // Set the target audience for the test
            SetAudience(AudiencesList.First(t => t.Id == test.TargetAudience.Id));
        }

        // Set the test name
        SetTestName(test.Title);

        // Set the status of the test (Active/Inactive)
        SetStatus(test.Active);

        // Set the selected target audience ID

        SetSelected(AudiencesList!.FindIndex(t => t.Id == test.TargetAudience.Id));
    }

    // Sets the overall test
    private void SetTest(Test test) => Test = test;

    // Sets text questions for the test
    private void SetTextQuestions(ObservableCollection<TextQuestion> questions) => TextQuestions = questions;

    // Sets audio questions for the test
    private void SetAudioQuestions(ObservableCollection<ToneAudiometryQuestion> questions) => AudioQuestions = questions;

    // Sets the target audience for the test
    private void SetAudience(TargetAudience audience) => Audience = audience;

    // Sets the test name
    private void SetTestName(string title) => TestName = title;

    // Sets the status of the test (Active/Inactive)
    private void SetStatus(bool active) => Status = active ? "Actief" : "Inactief";

    // Sets the selected target audience ID
    private void SetSelected(int id) => Selected = id;

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

    private void SaveTest()
    {
        try
        {
            if (!CheckValidityInput())
                return;

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
