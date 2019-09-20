using Zenject;
using TMPro;
using UnityEngine;

public class MainMenuContainer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _startSinglePlayerGameText;
    [SerializeField]
    private TMP_Text _startMultiPlayerGameText;
    [SerializeField]
    private TMP_Text _loadGameText;
    [SerializeField]
    private TMP_Text _openSettingsText;
    [SerializeField]
    private TMP_Text _quitGameText;

    private ILocalizationManager _localizationManager;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;
        _localizationManager.Initialize(_localizationManager.GetPreferredLanguage());

        _startSinglePlayerGameText.text = _localizationManager.GetTranslation(Constants.SinglePlayerTranslation);
        _startMultiPlayerGameText.text = _localizationManager.GetTranslation(Constants.MultiPlayerTranslation);
        _loadGameText.text = _localizationManager.GetTranslation(Constants.LoadGameTranslation);
        _openSettingsText.text = _localizationManager.GetTranslation(Constants.OptionsTranslation);
        _quitGameText.text = _localizationManager.GetTranslation(Constants.QuitGameTranslation);
    }
}
