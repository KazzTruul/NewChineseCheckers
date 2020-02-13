using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputManager : IInputManager
{
    [Inject]
    private readonly SignalBus _signalBus;
    [Inject]
    private readonly SetGamePausedCommandFactory _setGamePausedCommandFactory;
    [Inject]
    private readonly PopupSystemContainer _popupSystemContainer;

    [Inject]
    private readonly ICommandDispatcher _commandDispatcher;

    private bool _gamePaused;

    public bool GamePaused => _gamePaused;

    private readonly Stack<GameState> _gameStateStack = new Stack<GameState>();
    
    private Pawn _selectedPawn;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                var clickable = hit.transform.GetComponent<IClickable>();

                if (clickable == null)
                    return;

                clickable.OnClick();
            }
        }

        if (Input.GetKeyDown(Constants.PauseKey))
        {
            OnPauseKeyClicked();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _commandDispatcher.ExecuteCommand(new ShowPopupCommand<SettingsMenuPopupContainer>(_popupSystemContainer));
        }
    }

    public void OnActiveSceneChanged(ActiveSceneChangedSignal signal)
    {
        _gameStateStack.Clear();
        _gameStateStack.Push(GameState.Default);
        _gamePaused = false;
        Time.timeScale = 1f;
    }

    private void OnPauseKeyClicked()
    {
        var currentState = _gameStateStack.Peek();

        if (currentState == GameState.Default)
        {
            _gameStateStack.Push(GameState.Paused);
            _commandDispatcher.ExecuteCommand(_setGamePausedCommandFactory.Create());
            return;
        }

        switch (_gameStateStack.Pop())
        {
            case GameState.Popup:

                break;

            case GameState.Settings:

                //_commandDispatcher.ExecuteCommand(_showSettingsCommandFactory.Create(false));

                break;

            case GameState.Paused:

                _commandDispatcher.ExecuteCommand(_setGamePausedCommandFactory.Create());

                break;
        }
    }

    public void SetGameState(GameState newState)
    {
        _gameStateStack.Push(newState);
    }

    public void RemoveGameState(GameState obsoleteState)
    {
        if (obsoleteState == GameState.Default)
        {
            return;
        }

        var reverseStack = new Stack<GameState>();

        var lastKnownState = _gameStateStack.Peek();

        while (lastKnownState != obsoleteState && lastKnownState != GameState.Default)
        {
            lastKnownState = _gameStateStack.Peek();
            reverseStack.Push(_gameStateStack.Pop());
        }

        if (lastKnownState != GameState.Default)
        {
            _gameStateStack.Pop();
        }

        while(reverseStack.Count > 0)
        {
            _gameStateStack.Push(reverseStack.Pop());
        }
    }

    public void SetGamePaused(bool? didBecomePaused)
    {
        _gamePaused = didBecomePaused ?? !_gamePaused;

        Time.timeScale = _gamePaused ? 0f : 1f;

        _signalBus.Fire(new GamePausedChangedSignal { DidBecomePaused = _gamePaused });
    }

    public void OnTileClicked(TileClickedSignal signal)
    {

    }

    public void OnPawnClicked(PawnClickedSignal signal)
    {

    }
}