﻿using BusinessLogic.BusinessRules;
using BusinessLogic.Models;
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
    #region Properties
    private int _selectedFrequency;
    public int SelectedFrequency
    {
        get { return _selectedFrequency; }
        set
        {
            _selectedFrequency = value;
            OnPropertyChanged(nameof(SelectedFrequency));
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
        SelectedFrequency = toneAudiometryQuestion.Frequency;
    }

    private void SaveQuestion()
    {
        try
        {
            TestBusinessRules.ValidateToneaudiometryValues(StartingDecibelsString);

            // Creates a new ToneAudiometryQuestion object based on the provided data
            ToneAudiometryQuestion question = new ToneAudiometryQuestion
            {
                Id = toneAudiometryQuestion.Id,
                StartingDecibels = int.Parse(StartingDecibelsString),
                Frequency = SelectedFrequency,
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
            OpenErrorModal(ex.Message);
        }
    }

    private void CloseModal()
    {
        // Closes the current modal
        navigationStore.CloseModal();
    }

}