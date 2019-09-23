using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuContainer : MonoBehaviour, ILocalizable
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
    [SerializeField]
    Button enButton;
    [SerializeField]
    Button svButton;

    private ILocalizationManager _localizationManager;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;
        _localizationManager.Initialize(_localizationManager.GetPreferredLanguage());

        //TODO: Move to options menu
        enButton.onClick.AddListener(() => _localizationManager.SetPreferredLanguage("en"));
        svButton.onClick.AddListener(() => _localizationManager.SetPreferredLanguage("sv"));
    }

    public void OnLanguageChanged()
    {
        _startSinglePlayerGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.SinglePlayerTranslation]);
        _startMultiPlayerGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MultiPlayerTranslation]);
        _loadGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoadGameTranslation]);
        _openSettingsText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.OptionsTranslation]);
        _quitGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.QuitGameTranslation]);
    }
}