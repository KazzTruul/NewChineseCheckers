using Zenject;
using TMPro;
using UnityEngine;

public class MainMenuUIInstaller : MonoInstaller
{
    [SerializeField]
    private readonly TMP_Text _startSinglePlayerGameText;
    [SerializeField]
    private readonly TMP_Text _startMultiPlayerGameText;
    [SerializeField]
    private readonly TMP_Text _loadGameText;
    [SerializeField]
    private readonly TMP_Text _openSettingsText;
    [SerializeField]
    private readonly TMP_Text _quitGameText;

    private readonly ILocalizationManager _localizationManager;

    [Inject]
    public MainMenuUIInstaller(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;

        _startSinglePlayerGameText.text = _localizationManager.GetTranslation(Constants.SinglePlayerTranslation);
        _startMultiPlayerGameText.text = _localizationManager.GetTranslation(Constants.MultiPlayerTranslation);
        _loadGameText.text = _localizationManager.GetTranslation(Constants.LoadGameTranslation);
        _openSettingsText.text = _localizationManager.GetTranslation(Constants.OptionsTranslation);
        _quitGameText.text = _localizationManager.GetTranslation(Constants.QuitGameTranslation);
    }
}
