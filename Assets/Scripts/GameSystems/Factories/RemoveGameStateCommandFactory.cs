using Zenject;

public class RemoveGameStateCommandFactory
{
    [Inject]
    private readonly IInputManager _inputManager;

    public RemoveGameStateCommand Create(GameState gameState)
    {
        return new RemoveGameStateCommand(_inputManager, gameState);
    }
}