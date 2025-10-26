namespace Qonqr.Models;

/// <summary>
/// Represents the current state of the application
/// </summary>
public enum ApplicationState
{
    /// <summary>
    /// Initial state - user needs to log in
    /// </summary>
    NotLoggedIn,

    /// <summary>
    /// Login is in progress
    /// </summary>
    LoggingIn,

    /// <summary>
    /// User is logged in and idle
    /// </summary>
    LoggedIn,

    /// <summary>
    /// Application is performing an operation (scanning, harvesting, launching, etc.)
    /// </summary>
    Busy,

    /// <summary>
    /// An error has occurred
    /// </summary>
    Error
}

/// <summary>
/// Manages application state transitions and notifies observers
/// </summary>
public class StateManager
{
    private ApplicationState _currentState;
    private readonly object _stateLock = new object();

    public ApplicationState CurrentState
    {
        get
        {
            lock (_stateLock)
            {
                return _currentState;
            }
        }
        private set
        {
            lock (_stateLock)
            {
                if (_currentState != value)
                {
                    var oldState = _currentState;
                    _currentState = value;
                    OnStateChanged(oldState, value);
                }
            }
        }
    }

    /// <summary>
    /// Event raised when state changes
    /// </summary>
    public event EventHandler<StateChangedEventArgs>? StateChanged;

    public StateManager()
    {
        _currentState = ApplicationState.NotLoggedIn;
    }

    /// <summary>
    /// Attempts to transition to a new state
    /// </summary>
    public bool TryTransitionTo(ApplicationState newState)
    {
        if (CanTransitionTo(newState))
        {
            CurrentState = newState;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Forces a state transition (use with caution)
    /// </summary>
    public void ForceTransitionTo(ApplicationState newState)
    {
        CurrentState = newState;
    }

    /// <summary>
    /// Checks if transition to new state is valid
    /// </summary>
    public bool CanTransitionTo(ApplicationState newState)
    {
        return CurrentState switch
        {
            ApplicationState.NotLoggedIn => newState == ApplicationState.LoggingIn,
            ApplicationState.LoggingIn => newState == ApplicationState.LoggedIn || newState == ApplicationState.Error || newState == ApplicationState.NotLoggedIn,
            ApplicationState.LoggedIn => newState == ApplicationState.Busy || newState == ApplicationState.NotLoggedIn,
            ApplicationState.Busy => newState == ApplicationState.LoggedIn || newState == ApplicationState.Error,
            ApplicationState.Error => newState == ApplicationState.NotLoggedIn || newState == ApplicationState.LoggedIn,
            _ => false
        };
    }

    /// <summary>
    /// Checks if the application is in a busy state
    /// </summary>
    public bool IsBusy => CurrentState == ApplicationState.Busy || CurrentState == ApplicationState.LoggingIn;

    /// <summary>
    /// Checks if the application is logged in
    /// </summary>
    public bool IsLoggedIn => CurrentState == ApplicationState.LoggedIn || CurrentState == ApplicationState.Busy;

    protected virtual void OnStateChanged(ApplicationState oldState, ApplicationState newState)
    {
        StateChanged?.Invoke(this, new StateChangedEventArgs(oldState, newState));
    }
}

/// <summary>
/// Event args for state change events
/// </summary>
public class StateChangedEventArgs : EventArgs
{
    public ApplicationState OldState { get; }
    public ApplicationState NewState { get; }

    public StateChangedEventArgs(ApplicationState oldState, ApplicationState newState)
    {
        OldState = oldState;
        NewState = newState;
    }
}
