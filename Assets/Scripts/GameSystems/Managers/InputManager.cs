using UnityEngine;
using Zenject;

public class InputManager : IInputManager
{
    private Pawn _selectedPawn;

    private ICommandDispatcher _commandDispatcher;

    private SignalBus _signalBus;

    private bool _paused;

    [Inject]
    public void Initialize(ICommandDispatcher commandDispatcher, SignalBus signalBus)
    {
        _commandDispatcher = commandDispatcher;
        _signalBus = signalBus;
    }

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

    private void OnPauseKeyClicked()
    {
        _paused = !_paused;
        SetGamePaused(_paused);
    }

    private void SetGamePaused(bool didBecomePaused)
    {
        Time.timeScale = didBecomePaused ? 0f : 1f;
        _signalBus.Fire(new GamePausedChangedSignal { DidBecomePaused = didBecomePaused });
    }

    public void OnTileClicked(TileClickedSignal signal)
    {

    }

    public void OnPawnClicked(PawnClickedSignal signal)
    {

    }
}