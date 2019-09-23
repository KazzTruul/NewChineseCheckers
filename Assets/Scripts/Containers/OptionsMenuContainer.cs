using Zenject;
using TMPro;
using UnityEngine;

public class OptionsMenuContainer : MonoBehaviour, ILocalizable
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
        _localizationManager.Initialize(_localizationManager.GetPreferredLanguage());

        //OnLanguageUpdated();
    }

    public void OnLanguageChanged()
    {
        _optionsMenuTitle.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.OptionsTitle]);
        _masterVolumeSliderText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MasterVolume]);
        _musicVolumeSliderText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MusicVolume]);
        _sfxVolumeSliderText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.SFXVolume]);
        _changeLanguageText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.ChangeLanguage]);
        _goBackText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.GoBack]);
    }
}