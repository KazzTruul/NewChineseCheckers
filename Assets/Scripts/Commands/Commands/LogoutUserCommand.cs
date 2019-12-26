using Middleware;

public class LogoutUserCommand : SynchronousCommand
{
    private readonly PlayFabManager _playFabManager;

    public LogoutUserCommand(PlayFabManager playFabManager)
    {
        _playFabManager = playFabManager;
    }

    public override void Execute()
    {
        _playFabManager.LogoutUser();
    }
}