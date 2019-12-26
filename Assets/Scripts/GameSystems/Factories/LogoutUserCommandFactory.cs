using Zenject;
using Middleware;

public class LogoutUserCommandFactory
{
    [Inject]
    private readonly PlayFabManager _playFabManager;

    public LogoutUserCommand Create()
    {
        return new LogoutUserCommand(_playFabManager);
    }
}