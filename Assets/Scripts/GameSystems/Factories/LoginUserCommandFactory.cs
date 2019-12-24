using Zenject;
using Middleware;

public class LoginUserCommandFactory
{
    [Inject]
    private readonly PlayFabManager _playFabManager;

    public LoginUserCommand Create(string username, string password)
    {
        return new LoginUserCommand(_playFabManager, username, password);
    }
}
