using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private Button _startSinglePlayerGameButton;
    [SerializeField]
    private Button _startMultiPlayerGameButton;
    [SerializeField]
    private Button _loadGameButton;
    [SerializeField]
    private Button _openSettingsButton;
    [SerializeField]
    private Button _quitGameButton;

    private ILocalizationManager _localizationManager;
    private ICommandDispatcher _commandDispatcher;
    private LoadSceneCommandFactory _loadSceneCommandFactory;
    private SignalBus _signalBus;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager, ICommandDispatcher commandDispatcher, LoadSceneCommandFactory loadSceneCommandFactory, SignalBus signalBus)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _loadSceneCommandFactory = loadSceneCommandFactory;
        _signalBus = signalBus;

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
            //_commandDispatcher.ExecuteCommand(new settings)
            _signalBus.Fire(new SettingsShouldShowChangedSignal { ShowSettings = true });
        });
        //TODO: Make confirmation menu
        _quitGameButton.onClick.AddListener(() => Application.Quit());

        OnLanguageChanged();
    }

    public void OnLanguageChanged()
    {
        _startSinglePlayerGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.SinglePlayer]);
        _startMultiPlayerGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.MultiPlayer]);
        _loadGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoadGame]);
        _openSettingsText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.Options]);
        _quitGameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.QuitGame]);
    }
}