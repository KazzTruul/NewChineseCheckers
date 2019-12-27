using Middleware;

public class LogoutUserCommand : SynchronousCommand
{
    private readonly PlayFabManager _playFabManager;
    private readonly SettingsContainer _settingsContainer;

    public LogoutUserCommand(PlayFabManager playFabManager, SettingsContainer settingsContainer)
    {
        _playFabManager = playFabManager;
        _settingsContainer = settingsContainer;
    }

    public override void Execute()
    {
        _playFabManager.LogoutUser();
        _settingsContainer.SetCurrentUser("", "");
    }
}