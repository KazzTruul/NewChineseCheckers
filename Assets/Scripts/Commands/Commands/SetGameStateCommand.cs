public class SetGameStateCommand : SynchronousCommand
{
    private readonly IInputManager _inputManager;
    private readonly GameState _gameState;

    public SetGameStateCommand(IInputManager inputManager, GameState gameState)
    {
        _inputManager = inputManager;
        _gameState = gameState;
    }

    public override void Execute()
    {
        _inputManager.SetGameState(_gameState);
    }
}
