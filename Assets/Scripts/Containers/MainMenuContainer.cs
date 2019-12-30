using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Generated;

public class MainMenuContainer : MonoBehaviour, ILocalizable
{
    [SerializeField]
    private TMP_Text _userGreetingText;
    [SerializeField]
    private TMP_Text _startSinglePlayerGameText;
    [SerializeField]
    private TMP_Text _startMultiPlayerGameText;
    [SerializeField]
    private TMP_Text _loadGameText;
    [SerializeField]
    private TMP_Text _openSettingsText;
    [SerializeField]
    private TMP_Text _logoutUserText;
    [SerializeField]
    private TMP_Text _quitGameText;
    [SerializeField]
    private Button _startSinglePlayerGameButton;
    [SerializeField]
    private Button _startMultiPlayerGameButton;
    [SerializeField]
    private Button _loadGameButton;
    [SerializeField]
    private Button _openSettingsButton;
    [SerializeField]
    private Button _logoutUserButton;
    [SerializeField]
    private Button _quitGameButton;

    private ILocalizationManager _localizationManager;
    private ICommandDispatcher _commandDispatcher;
    private LoadSceneCommandFactory _loadSceneCommandFactory;
    private SignalBus _signalBus;
    private LogoutUserCommandFactory _logoutUserCommandFactory;
    private SettingsContainer _settingsContainer;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager,
        ICommandDispatcher commandDispatcher,
        LoadSceneCommandFactory loadSceneCommandFactory,
        SignalBus signalBus,
        LogoutUserCommandFactory logoutUserCommandFactory,
        SettingsContainer settingsContainer)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _loadSceneCommandFactory = loadSceneCommandFactory;
        _signalBus = signalBus;
        _logoutUserCommandFactory = logoutUserCommandFactory;
        _settingsContainer = settingsContainer;

        //TODO: Add difficulty selection
        _startSinglePlayerGameButton.onClick.AddListener(() =>
        {
            _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.SinglePlayerSceneIndex, false, true, Constants.MainMenuSceneIndex));
            SceneManager.UnloadSceneAsync(Constants.MainMenuSceneIndex);
        });
        _startMultiPlayerGameButton.onClick.AddListener(() =>
        {
            _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MultiPlayerSceneIndex, false, true, Constants.MainMenuSceneIndex));
            SceneManager.UnloadSceneAsync(Constants.MainMenuSceneIndex);
        });
        _openSettingsButton.onClick.AddListener(() =>
        {
            //TODO: Change to command
            _signalBus.Fire(new SettingsShouldShowChangedSignal { ShowSettings = true });
        });
        //TODO: Make confirmation menu
        _logoutUserButton.onClick.AddListener(() => 
        {
            _commandDispatcher.ExecuteCommand(_logoutUserCommandFactory.Create());
            _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.LoginSceneIndex, false, true, Constants.MainMenuSceneIndex));
        });
        _quitGameButton.onClick.AddListener(() => Application.Quit());

        OnLanguageChanged();
    }

    public void OnLanguageChanged()
    {
        _userGreetingText.text = string.Format(TranslationKeys.MainMenuGreetingTranslation, _settingsContainer.Settings.Username);
        _startSinglePlayerGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameStartSingleTranslation);
        _startMultiPlayerGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameStartMultiTranslation);
        _loadGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameLoadTranslation);
        _openSettingsText.text = _localizationManager.GetTranslation(TranslationKeys.GameOptionsTranslation);
        _logoutUserText.text = _localizationManager.GetTranslation(TranslationKeys.PlayerLogoutTranslation);
        _quitGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameQuitTranslation);
    }
}