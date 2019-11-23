using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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
    [SerializeField]
    private Button _enButton;
    [SerializeField]
    private Button _svButton;

    private ILocalizationManager _localizationManager;
    private ZenjectSceneLoader _zenjectSceneLoader;

    [Inject]
    public void Initialize(ILocalizationManager localizationManager, ZenjectSceneLoader sceneLoader)
    {
        _localizationManager = localizationManager;
        _zenjectSceneLoader = sceneLoader;
        _localizationManager.Initialize(_localizationManager.GetPreferredLanguage());

        //TODO: Move to options menu
        _enButton.onClick.AddListener(() => _localizationManager.SetPreferredLanguage("en"));
        _svButton.onClick.AddListener(() => _localizationManager.SetPreferredLanguage("sv"));
        _startSinglePlayerGameButton.onClick.AddListener(() =>
        {
            _zenjectSceneLoader.LoadSceneAsync(Constants.SinglePlayerSceneIndex, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(Constants.MainMenuSceneIndex);
        });
        _startMultiPlayerGameButton.onClick.AddListener(() =>
        {
            _zenjectSceneLoader.LoadSceneAsync(Constants.MultiPlayerSceneIndex, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(Constants.MainMenuSceneIndex);
        });
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