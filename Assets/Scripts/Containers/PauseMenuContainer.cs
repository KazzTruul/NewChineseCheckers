using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Generated;

public class PauseMenuContainer : MonoBehaviour, ILocalizable, IPausable
{
    [SerializeField]
    private GameObject _menuObject;
    [SerializeField]
    private TMP_Text _pauseMenuTitleText;
    [SerializeField]
    private TMP_Text _resumeGameText;
    [SerializeField]
    private TMP_Text _restartGameText;
    [SerializeField]
    private TMP_Text _settingsText;
    [SerializeField]
    private TMP_Text _mainMenuText;
    [SerializeField]
    private TMP_Text _quitGameText;
    [SerializeField]
    private Button _resumeGameButton;
    [SerializeField]
    private Button _restartGameButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _mainMenuButton;
    [SerializeField]
    private Button _quitGameButton;

    private ILocalizationManager _localizationManager;
    private IInputManager _inputManager;
    private ICommandDispatcher _commandDispatcher;
    private LoadSceneCommandFactory _loadSceneCommandFactory;
    private SetGamePausedCommandFactory _setGamePausedCommandFactory;
    private ShowSettingsCommandFactory _showSettingsCommandFactory;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager,
        ICommandDispatcher commandDispatcher,
        LoadSceneCommandFactory loadSceneCommandFactory,
        IInputManager inputManager,
        SetGamePausedCommandFactory setGamePausedCommandFactory,
        ShowSettingsCommandFactory showSettingsCommandFactory)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _loadSceneCommandFactory = loadSceneCommandFactory;
        _inputManager = inputManager;
        _setGamePausedCommandFactory = setGamePausedCommandFactory;
        _showSettingsCommandFactory = showSettingsCommandFactory;

        _resumeGameButton.onClick.AddListener(() => _commandDispatcher.ExecuteCommand(_setGamePausedCommandFactory.Create(false)));
        //TODO: Add confirmation menu
        _restartGameButton.onClick.AddListener(() => _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.SinglePlayerSceneIndex, true, true, Constants.SinglePlayerSceneIndex)));
        _settingsButton.onClick.AddListener(() => _commandDispatcher.ExecuteCommand(_showSettingsCommandFactory.Create(true)));
        _mainMenuButton.onClick.AddListener(() => _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MainMenuSceneIndex, false, true, Constants.SinglePlayerSceneIndex)));
        //TODO: Add confirmation menu
        _quitGameButton.onClick.AddListener(() => Application.Quit());

        OnLanguageChanged();
    }

    public void OnLanguageChanged()
    {
        _pauseMenuTitleText.text = _localizationManager.GetTranslation(TranslationKeys.PauseMenuTitleTranslation);
        _resumeGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameResumeTranslation);
        _restartGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameRestartTranslation);
        _settingsText.text = _localizationManager.GetTranslation(TranslationKeys.GameOptionsTranslation);
        _mainMenuText.text = _localizationManager.GetTranslation(TranslationKeys.MainMenuTranslation);
        _quitGameText.text = _localizationManager.GetTranslation(TranslationKeys.GameQuitTranslation);
    }

    public void OnPausedAndUnpaused(GamePausedChangedSignal signal)
    {
        var paused = signal.DidBecomePaused ?? !_inputManager.GamePaused;
        _menuObject.SetActive(paused);
    }
}