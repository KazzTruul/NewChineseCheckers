using UnityEngine;
using Zenject;

public class InputManager : IInputManager
{
    private Pawn _selectedPawn;

    [Inject]
    private SignalBus _signalBus;
    [Inject]
    private SetGamePausedCommandFactory _setGamePausedCommandFactory;
    [Inject]
    private ICommandDispatcher _commandDispatcher;

    private bool _gamePaused;

    public bool GamePaused => _gamePaused;

    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
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
    }

    public void OnActiveSceneChanged(ActiveSceneChangedSignal signal)
    {
        _gamePaused = false;
        Time.timeScale = 1f;
    }

    private void OnPauseKeyClicked()
    {
        _commandDispatcher.ExecuteCommand(_setGamePausedCommandFactory.Create());
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