using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private ICommandHandler _commandHandler;
    private ILocalizationManager _localizationManager;
    private SettingsContainer _settingsContainer;

    [Inject]
    private void Initialize(ICommandHandler commandHandler, ILocalizationManager localizationManager, SettingsContainer settingsManager)
    {
        _commandHandler = commandHandler;
        _localizationManager = localizationManager;
        _settingsContainer = settingsManager;

        _commandHandler.ExecuteCommand(new InitializeSettingsCommand(_settingsContainer, _localizationManager));
        _commandHandler.ExecuteCommand(new InitializeLocalizationCommand(_settingsContainer, _localizationManager));
    }
}