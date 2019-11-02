using Zenject;

public class TickableManager : ITickable
{
    [Inject]
    private readonly IInputManager _inputManager;

    public void Tick()
    {
        _inputManager.Tick();
    }
}