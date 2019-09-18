using UnityEngine;
using Zenject;

public class InitializeLocalizationCommand : CommandBase
{
    private Settings _settings;
    private ILocalizationManager _localizationManager;

    [Inject]
    public void Initialize(Settings settings, ILocalizationManager localizationManager)
    {
        _settings = settings;
        _localizationManager = localizationManager;
        Debug.Log("??");
    }

    public override void Execute()
    {
        if(_localizationManager == null)
        {
            Debug.Log("No localizationManager");
        }
        if (_settings == null)
        {
            Debug.Log("No settings");
        }
        if (_settings.Language == null)
        {
            Debug.Log("No language");
        }
        _localizationManager.Initialize(_settings.Language);
        Debug.Log("Localization Initialized!");

    }
}