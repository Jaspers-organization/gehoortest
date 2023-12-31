﻿using BusinessLogic.Models;
using System;
using System.Windows.Input;
using UserInterface.Commands;
using UserInterface.Stores;

namespace UserInterface.ViewModels.Modals;

internal class AudioQuestionModalViewModel : ViewModelBase
{
    #region Dependencies
    private readonly NavigationStore navigationStore;
    private readonly TestManagementViewModel testManagementViewModel;
    private readonly ToneAudiometryQuestion toneAudiometryQuestion;
    private readonly bool newQuestion;
    #endregion

    #region Commands
    public ICommand CloseModalCommand => new Command(CloseModal);
    public ICommand SaveQuestionCommand => new Command(SaveQuestion);
    #endregion

    #region Propertys
    private string _frequencyString;
    public string FrequencyString
    {
        get { return _frequencyString; }
        set
        {
            _frequencyString = value;
            OnPropertyChanged(nameof(FrequencyString));
        }
    }
    private int _frequency;
    public int Frequency
    {
        get { return _frequency; }
        set
        {
            _frequency = value;
            OnPropertyChanged(nameof(Frequency));
        }
    }

    private string _startingDecibelsString;
    public string StartingDecibelsString
    {
        get { return _startingDecibelsString; }
        set
        {
            _startingDecibelsString = value;
            OnPropertyChanged(nameof(StartingDecibelsString));
        }
    }
    private int _startingDecibels;
    public int StartingDecibels
    {
        get { return _startingDecibels; }
        set
        {
            _startingDecibels = value;
            OnPropertyChanged(nameof(StartingDecibels));
        }
    }
    #endregion

    #region Errors

    private bool CheckValidityInput()
    {
        string frequencyValidation = ErrorService.ValidateInput("Frequency", FrequencyString);
        string decibelValidation = ErrorService.ValidateInput("StartingDecibelsString", StartingDecibelsString);

        if (!string.IsNullOrEmpty(frequencyValidation))
        {
            OpenErrorModal(frequencyValidation);
            return false;
        }
        else
        {
            Frequency = ErrorService.ParseStringToInt(FrequencyString);//not sure of this.
        }

        if (!string.IsNullOrEmpty(decibelValidation))
        {
            OpenErrorModal(decibelValidation);
            return false;
        }
        else
        {
            StartingDecibels = ErrorService.ParseStringToInt(StartingDecibelsString);
        }
        return true;
    }
    #endregion

    public AudioQuestionModalViewModel(NavigationStore navigationStore, ToneAudiometryQuestion toneAudiometryQuestion, bool newQuestion, TestManagementViewModel testManagementViewModel)
    {
        this.navigationStore = navigationStore;
        this.testManagementViewModel = testManagementViewModel;
        this.toneAudiometryQuestion = toneAudiometryQuestion;
        this.newQuestion = newQuestion;

        SetValues();
    }
    public void OpenErrorModal(string text) => navigationStore.OpenModal(new ErrorModalViewModal(navigationStore, text));
    public void SetValues()
    {
        StartingDecibelsString = toneAudiometryQuestion.StartingDecibels.ToString();
        FrequencyString = toneAudiometryQuestion.Frequency.ToString();
    }

    private void SaveQuestion()
    {
        try
        {
            // Checks the validity of input data before proceeding
            if (!CheckValidityInput())
                return;

            // Creates a new ToneAudiometryQuestion object based on the provided data
            ToneAudiometryQuestion question = new ToneAudiometryQuestion
            {
                Id = toneAudiometryQuestion.Id,
                StartingDecibels = StartingDecibels,
                Frequency = Frequency,
                QuestionNumber = toneAudiometryQuestion.QuestionNumber
            };

            // Adds a new tone audiometry question if it's a new question, otherwise updates the existing one
            if (newQuestion)
                testManagementViewModel.AddNewToneAudiometryQuestion(question);
            else
                testManagementViewModel.UpdateToneAudiometryQuestion(question);

            // Closes the modal after successful question saving or updating
            CloseModal();
        }
        catch (Exception ex)
        {
            // If an exception occurs during the saving process, opens an error modal
            OpenErrorModal("Er is wat foutgegaan bij het opslaan van de vraag");
        }
    }

    private void CloseModal()
    {
        // Closes the current modal
        navigationStore.CloseModal();
    }

}