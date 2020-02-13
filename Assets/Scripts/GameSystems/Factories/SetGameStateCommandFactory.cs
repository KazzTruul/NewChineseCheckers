using Zenject;

public class SetGameStateCommandFactory
{
    [Inject]
    private readonly IInputManager _inputManager;

    public SetGameStateCommand Create(GameState gameState)
    {
        return new SetGameStateCommand(_inputManager, gameState);
    }
}
