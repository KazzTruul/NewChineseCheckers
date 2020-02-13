public class SetGamePausedCommand : SynchronousCommand
{
    private readonly IInputManager _inputManager;
    private readonly bool? _pauseGame;

    public SetGamePausedCommand(IInputManager inputManager, bool? pauseGame = null)
    {
        _inputManager = inputManager;
        _pauseGame = pauseGame;
    }

    public override void Execute()
    {
        _inputManager.SetGamePaused(_pauseGame);
    }
}
