using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
    private SignalBus _signalBus;
    private SetGamePausedCommandFactory _setGamePausedCommandFactory;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager,
        ICommandDispatcher commandDispatcher,
        LoadSceneCommandFactory loadSceneCommandFactory,
        IInputManager inputManager,
        SignalBus signalBus,
        SetGamePausedCommandFactory setGamePausedCommandFactory)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _loadSceneCommandFactory = loadSceneCommandFactory;
        _inputManager = inputManager;
        _signalBus = signalBus;
        _setGamePausedCommandFactory = setGamePausedCommandFactory;

        _resumeGameButton.onClick.AddListener(() => _commandDispatcher.ExecuteCommand(_setGamePausedCommandFactory.Create(false)));
        _mainMenuButton.onClick.AddListener(() => _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MainMenuSceneIndex, true, Constants.SinglePlayerSceneIndex)));

        OnLanguageChanged();
    }

    public void OnLanguageChanged()
    {
        _pauseMenuTitleText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PauseMenuTitle]);
        _resumeGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.ResumeGame]);
        _restartGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.RestartGame]);
        _settingsText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.Options]);
        _mainMenuText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MainMenu]);
        _quitGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.QuitGame]);
    }

    public void OnPausedAndUnpaused(GamePausedChangedSignal signal)
    {
        var paused = signal.DidBecomePaused ?? !_inputManager.GamePaused;
        _menuObject.SetActive(paused);
    }
}