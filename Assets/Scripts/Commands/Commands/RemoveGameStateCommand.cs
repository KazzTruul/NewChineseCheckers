public class RemoveGameStateCommand : SynchronousCommand
{
    private readonly IInputManager _inputManager;
    private readonly GameState _gameState;

    public RemoveGameStateCommand(IInputManager inputManager, GameState gameState)
    {
        _inputManager = inputManager;
        _gameState = gameState;
    }

    public override void Execute()
    {
        _inputManager.RemoveGameState(_gameState);
    }
}