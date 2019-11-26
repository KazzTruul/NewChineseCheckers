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

    [Inject]
    public void Initialize(ILocalizationManager localizationManager, ICommandDispatcher commandDispatcher, LoadSceneCommandFactory loadSceneCommandFactory)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _loadSceneCommandFactory = loadSceneCommandFactory;

        _localizationManager.Initialize(_localizationManager.GetPreferredLanguage());

        //TODO: Add difficulty selection
        _startSinglePlayerGameButton.onClick.AddListener(() =>
        {
            _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.SinglePlayerSceneIndex, true, Constants.MainMenuSceneIndex));
            SceneManager.UnloadSceneAsync(Constants.MainMenuSceneIndex);
        });
        _startMultiPlayerGameButton.onClick.AddListener(() =>
        {
            _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MultiPlayerSceneIndex, true, Constants.MainMenuSceneIndex));
            SceneManager.UnloadSceneAsync(Constants.MainMenuSceneIndex);
        });
        //TODO: Make confirmation menu
        _quitGameButton.onClick.AddListener(() => Application.Quit());
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