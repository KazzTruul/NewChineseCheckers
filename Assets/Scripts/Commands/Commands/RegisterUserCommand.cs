using Middleware;

public class RegisterUserCommand : SynchronousCommand
{
    private readonly PlayFabManager _playFabManager;
    private readonly string _username;
    private readonly string _password;

    public RegisterUserCommand(PlayFabManager playFabManager, string username, string password)
    {
        _playFabManager = playFabManager;
        _username = username;
        _password = password;
    }

    public override void Execute()
    {
        _playFabManager.RegisterUser(_username, _password);
    }
}