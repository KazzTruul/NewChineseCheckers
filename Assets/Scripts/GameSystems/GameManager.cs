using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private ICommandHandler _commandHandler;

    [Inject]
    private void Initialize(ICommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
        
        _commandHandler.ExecuteCommand(new InitializeSettingsCommand());
        _commandHandler.ExecuteCommand(new InitializeLocalizationCommand());
    }
}