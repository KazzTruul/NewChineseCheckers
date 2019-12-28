using System.Linq;
using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsMenuContainer : MonoBehaviour, ILocalizable
{
    [SerializeField]
    private GameObject _menuObject;
    [SerializeField]
    private TMP_Text _menuTitleText;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private TMP_Text _masterVolumeText;
    [SerializeField]
    private Slider _masterVolumeSlider;
    [SerializeField]
    private TMP_Text _musicVolumeText;
    [SerializeField]
    private Slider _musicVolumeSlider;
    [SerializeField]
    private TMP_Text _sfxVolumeText;
    [SerializeField]
    private Slider _sfxVolumeSlider;
    [SerializeField]
    private TMP_Text _languageText;
    [SerializeField]
    private TMP_Dropdown _languageSelectionDropdown;
    [SerializeField]
    private TMP_Text _autoSaveText;
    [SerializeField]
    private Toggle _autoSaveToggle;
    [SerializeField]
    private TMP_Text _autoLoginText;
    [SerializeField]
    private Toggle _autoLoginToggle;
    [SerializeField]
    private TMP_Text _saveAndLeaveText;
    [SerializeField]
    private Button _saveAndExitButton;

    private ILocalizationManager _localizationManager;
    private SignalBus _signalbus;
    private SettingsContainer _settingsContainer;
    private ChangeVolumeCommandFactory _changeVolumeCommandFactory;
    private ChangeLanguageCommandFactory _changeLanguageCommandFactory;
    private ICommandDispatcher _commandDispatcher;

    [Inject]
    private void Initialize(
        ILocalizationManager localizationManager,
        SignalBus signalBus,
        SettingsContainer settingsContainer,
        ChangeVolumeCommandFactory changeVolumeCommandFactory,
        ChangeLanguageCommandFactory changeLanguageCommandFactory,
        ICommandDispatcher commandDispatcher)
    {
        _localizationManager = localizationManager;
        _signalbus = signalBus;
        _settingsContainer = settingsContainer;
        _changeVolumeCommandFactory = changeVolumeCommandFactory;
        _changeLanguageCommandFactory = changeLanguageCommandFactory;
        _commandDispatcher = commandDispatcher;
        
        _backButton.onClick.AddListener(OnCloseSettings);

        _masterVolumeSlider.value = _settingsContainer.Settings.MasterVolume;
        _masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        _musicVolumeSlider.value = _settingsContainer.Settings.MusicVolume;
        _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxVolumeSlider.value = _settingsContainer.Settings.SFXVolume;
        _sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        _languageSelectionDropdown.onValueChanged.AddListener(language => _commandDispatcher.ExecuteCommand(_changeLanguageCommandFactory.Create(Constants.SupportedLanuages[language])));
        _languageSelectionDropdown.AddOptions(Constants.SupportedLanuages.Select(language => new TMP_Dropdown.OptionData(Constants.IsoToLocalizedLanguages[language])).ToList());
        _languageSelectionDropdown.value = Constants.SupportedLanuages.Contains(_localizationManager.CurrentLanguage) ? Array.IndexOf(Constants.SupportedLanuages, _localizationManager.CurrentLanguage) : 0;

        _autoSaveToggle.isOn = _settingsContainer.Settings.AutoSave;
        _autoSaveToggle.onValueChanged.AddListener(SetAutoSaveEnabled);

        _autoLoginToggle.isOn = _settingsContainer.Settings.AutoLogin;
        _autoLoginToggle.onValueChanged.AddListener(SetAutoLoginEnabled);

        _saveAndExitButton.onClick.AddListener(() =>
        {
            SaveChanges();
            OnCloseSettings();
        });

        OnLanguageChanged();
    }

    private void SetVolume(SoundType soundType, float volume)
    {
        _commandDispatcher.ExecuteCommand(_changeVolumeCommandFactory.SetVolume(soundType, volume));
    }

    private void SetMasterVolume(float volume)
    {
        SetVolume(SoundType.Master, volume);
    }

    private void SetMusicVolume(float volume)
    {
        SetVolume(SoundType.Music, volume);
    }

    private void SetSFXVolume(float volume)
    {
        SetVolume(SoundType.SFX, volume);
    }

    private void SetAutoSaveEnabled(bool enableAutoSave)
    {
        _settingsContainer.SetAutoSave(enableAutoSave);
    }

    private void SetAutoLoginEnabled(bool enableAutoLogin)
    {
        _settingsContainer.SetAutoLogin(enableAutoLogin);
    }

    public void OnLanguageChanged()
    {
        _menuTitleText.text = _localizationManager.GetTranslation(TranslationKeys.OptionsTitleTranslation);
        _masterVolumeText.text = _localizationManager.GetTranslation(TranslationKeys.VolumeMasterTranslation);
        _musicVolumeText.text = _localizationManager.GetTranslation(TranslationKeys.VolumeMusicTranslation);
        _sfxVolumeText.text = _localizationManager.GetTranslation(TranslationKeys.VolumeSfxTranslation);
        _languageText.text = _localizationManager.GetTranslation(TranslationKeys.OptionsLanguageTranslation);
        _autoSaveText.text = _localizationManager.GetTranslation(TranslationKeys.OptionsAutoSaveTranslation);
        _autoLoginText.text = _localizationManager.GetTranslation(TranslationKeys.OptionsAutoLoginTranslation);
        _saveAndLeaveText.text = _localizationManager.GetTranslation(TranslationKeys.OptionsSaveLeaveTranslation);
    }

    public void OnShowSettingsChanged(SettingsShouldShowChangedSignal signal)
    {
        _menuObject.SetActive(signal.ShowSettings);
    }

    private void ResetToDefault()
    {
        _settingsContainer.ResetToDefault(_localizationManager);
    }

    private void SaveChanges()
    {
        _settingsContainer.SaveChanges();
    }

    public void DiscardChanges()
    {
        _settingsContainer.DiscardChanges();
    }

    private void OnCloseSettings()
    {
        DiscardChanges();
        _signalbus.Fire(new SettingsShouldShowChangedSignal { ShowSettings = false });
    }
}