using Middleware;
using System;

public class LoginUserCommand : SynchronousCommand
{
    private readonly PlayFabManager _playFabManager;
    private readonly string _username;
    private readonly string _password;

    public LoginUserCommand(PlayFabManager playFabManager, string username, string password)
    {
        _playFabManager = playFabManager;
        _username = username;
        _password = password;
    }

    public override void Execute()
    {
        _playFabManager.LoginUser(_username, _password);
    }
}