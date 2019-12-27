using Zenject;
using Middleware;

public class LogoutUserCommandFactory
{
    [Inject]
    private readonly PlayFabManager _playFabManager;
    [Inject]
    private readonly SettingsContainer _settingsContainer;

    public LogoutUserCommand Create()
    {
        return new LogoutUserCommand(_playFabManager, _settingsContainer);
    }
}