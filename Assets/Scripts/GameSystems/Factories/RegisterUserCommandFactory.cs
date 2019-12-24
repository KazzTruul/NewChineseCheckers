using Zenject;
using Middleware;

public class RegisterUserCommandFactory
{
    [Inject]
    private readonly PlayFabManager _playFabManager;

    public RegisterUserCommand Create(string username, string password)
    {
        return new RegisterUserCommand(_playFabManager, username, password);
    }
}