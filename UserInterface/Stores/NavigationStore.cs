using BusinessLogic.Projections;
using System;
using System.Collections.Generic;
using UserInterface.ViewModels;

namespace UserInterface.Stores;

internal class NavigationStore
{
    public event Action? CurrentViewModelChanged;
    public event Action? IsModalOpenChanged;
    public event Action? PreviousViewModelChanged;

    #region properties
    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get { return _currentViewModel; }
        set { _currentViewModel = value; OnCurrentViewModelChanged(); }
    }

    private ViewModelBase? _currentModalViewModel;
    public ViewModelBase? CurrentModalViewModel
    {
        get { return _currentModalViewModel; }
        set { _currentModalViewModel = value; }
    }

    private bool _isModalOpen = false;
    public bool IsModalOpen
    {
        get { return _isModalOpen; }
        set { _isModalOpen = value; OnIsModalOpenChanged(); }
    }
    private EmployeeProjection? _loggedInEmployee = null;
    public EmployeeProjection? LoggedInEmployee
    {
        get { return _loggedInEmployee; }
        set { _loggedInEmployee = value; }
    }

    private Stack<ViewModelBase> _previousViewModel = new Stack<ViewModelBase>();
    public Stack<ViewModelBase> PreviousViewModel
    {
        get { return _previousViewModel; }
        set { _previousViewModel = value; OnPreviousViewModelChanged(); }
    }
    #endregion

    public void OpenModal(ViewModelBase viewModal)
    {
        CurrentModalViewModel = viewModal;
        IsModalOpen = true;
    }
    
    public void CloseModal()
    {
        CurrentModalViewModel = null;
        IsModalOpen = false;
    }

    public void CloseModal(ViewModelBase viewModal)
    {
        CurrentModalViewModel = null;
        CurrentViewModel = viewModal;
        IsModalOpen = false;
    }

    public void AddPreviousViewModel(ViewModelBase viewModal)
    {
        Stack<ViewModelBase> temp = PreviousViewModel;
        temp.Push(viewModal);

        PreviousViewModel = temp;
    }

    public void ClearPreviousViewModel()
    {
        PreviousViewModel = new Stack<ViewModelBase>();
    }

    private void OnCurrentViewModelChanged() => CurrentViewModelChanged?.Invoke();

    private void OnIsModalOpenChanged() => IsModalOpenChanged?.Invoke();

    private void OnPreviousViewModelChanged() => PreviousViewModelChanged?.Invoke();
   
}