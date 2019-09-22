using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public void OnLanguageUpdated()
    {
        _startSinglePlayerGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.SinglePlayerTranslation]);
        _startMultiPlayerGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MultiPlayerTranslation]);
        _loadGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoadGameTranslation]);
        _openSettingsText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.OptionsTranslation]);
        _quitGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.QuitGameTranslation]);
    }
}

public class SettingsMenuContainer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _optionsMenuTitle;
    [SerializeField]
    private TMP_Text _masterVolumeSliderText;
    [SerializeField]
    private TMP_Text _musicVolumeSliderText;
    [SerializeField]
    private TMP_Text _sfxVolumeSliderText;
    [SerializeField]
    private TMP_Text _changeLanguageText;
    [SerializeField]
    private TMP_Text _goBackText;

    private ILocalizationManager _localizationManager;
    private SettingsContainer _settingsContainer;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager, SettingsContainer settingsContainer)
    {
        _localizationManager = localizationManager;
        _settingsContainer = settingsContainer;

        OnLanguageUpdated();
    }

    public void OnLanguageUpdated()
    {
        _optionsMenuTitle.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.OptionsTitle]);
        _masterVolumeSliderText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MasterVolume]);
        _musicVolumeSliderText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MusicVolume]);
        _sfxVolumeSliderText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.SFXVolume]);
        _changeLanguageText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.ChangeLanguage]);
        _goBackText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.GoBack]);
    }
}
