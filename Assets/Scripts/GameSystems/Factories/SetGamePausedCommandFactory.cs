using Zenject;

public class SetGamePausedCommandFactory
{
    [Inject]
    private IInputManager _inputManager;

    public SetGamePausedCommand Create(bool? pauseGame = null)
    {
        return new SetGamePausedCommand(_inputManager, pauseGame);
    }
}