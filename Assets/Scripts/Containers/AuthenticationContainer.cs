using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Middleware;

public class AuthenticationContainer : MonoBehaviour, ILocalizable
{
    [SerializeField]
    private TMP_Text _loginMenuTitleText;
    [SerializeField]
    private TMP_InputField _usernameInputField;
    [SerializeField]
    private TMP_Text _usernameText;
    [SerializeField]
    private TMP_InputField _passwordInputField;
    [SerializeField]
    private TMP_Text _passwordText;
    [SerializeField]
    private Button _loginUserButton;
    [SerializeField]
    private TMP_Text _loginUserButtonText;
    [SerializeField]
    private Button _registerUserButton;
    [SerializeField]
    private TMP_Text _registerUserButtonText;
    
    private ILocalizationManager _localizationManager;
    private ICommandDispatcher _commandDispatcher;
    private RegisterUserCommandFactory _registerUserCommandFactory;
    private LoginUserCommandFactory _loginUserCommandFactory;
    private SettingsContainer _settingsContainer;
    private LoadSceneCommandFactory _loadSceneCommandFactory;

    private readonly VerifyUsernameValidityStrategy _verifyUsernameValidityStrategy = new VerifyUsernameValidityStrategy();
    private readonly VerifyPasswordValidityStrategy _verifyPasswordValidityStrategy = new VerifyPasswordValidityStrategy();

    private string _username = "";
    private string _userPassword = "";

    [Inject]
    private void Initialize(
        ILocalizationManager localizationManager,
        ICommandDispatcher commandDispatcher,
        RegisterUserCommandFactory registerUserCommandFactory,
        LoginUserCommandFactory loginUserCommandFactory,
        SettingsContainer settingsContainer,
        LoadSceneCommandFactory loadSceneCommandFactory)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _registerUserCommandFactory = registerUserCommandFactory;
        _loginUserCommandFactory = loginUserCommandFactory;
        _settingsContainer = settingsContainer;
        _loadSceneCommandFactory = loadSceneCommandFactory;

        _usernameInputField.onValueChanged.AddListener(OnUsernameInputChanged);
        _passwordInputField.onValueChanged.AddListener(OnUserPasswordInputChanged);
        _registerUserButton.onClick.AddListener(OnRegisterUser);
        _loginUserButton.onClick.AddListener(OnLoginUser);

        OnLanguageChanged();

        _username = _settingsContainer.Settings.Username;
        _userPassword = _settingsContainer.Settings.Password;

        DetermineRegisterLoginButtonsInteractable();

        if (ShouldAttemptAutoLogin())
        {
            OnLoginUser();
        }
    }

    private bool ShouldAttemptAutoLogin()
    {
        return _settingsContainer.Settings.AutoLogin
            && !string.IsNullOrEmpty(_username)
            && !string.IsNullOrEmpty(_userPassword);
    }

    private void DetermineRegisterLoginButtonsInteractable()
    {
        var validUsername = _verifyUsernameValidityStrategy.IsValidUsername(_username);
        var validPassword = _verifyPasswordValidityStrategy.IsValidPassword(_userPassword);

        _registerUserButton.interactable = validUsername && validPassword;
        _loginUserButton.interactable = validUsername && validPassword;
    }

    private void OnUsernameInputChanged(string username)
    {
        _username = username;

        DetermineRegisterLoginButtonsInteractable();
    }

    private void OnUserPasswordInputChanged(string password)
    {
        _userPassword = password;

        DetermineRegisterLoginButtonsInteractable();
    }

    private void OnRegisterUser()
    {
        _commandDispatcher.ExecuteCommand(_registerUserCommandFactory.Create(_username, _userPassword));
    }

    private void OnLoginUser()
    {
        _commandDispatcher.ExecuteCommand(_loginUserCommandFactory.Create(_username, _userPassword));
    }

    public void OnRegisterUserSuccess(UserRegistrationSucceededSignal signal)
    {
        _settingsContainer.SetCurrentUser(_username, _userPassword);
        _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MainMenuSceneIndex, false, true, Constants.LoginSceneIndex));
    }

    public void OnRegisterUserFailure(UserRegistrationFailedSignal signal)
    {
        Debug.LogError($"Error on registration: {signal.Error}");
    }

    public void OnLoginUserSuccess(UserLoginSucceededSignal signal)
    {
        _settingsContainer.SetCurrentUser(_username, _userPassword);
        _commandDispatcher.ExecuteCommand(_loadSceneCommandFactory.Create(Constants.MainMenuSceneIndex, false, true, Constants.LoginSceneIndex));
    }

    public void OnLoginUserFailure(UserLoginFailedSignal signal)
    {
        Debug.LogError($"Error on login: {signal.Error}");
    }

    public void OnLanguageChanged()
    {
        _loginMenuTitleText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoginMenuTitle]);
        _usernameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PlayerUsername]);
        _passwordText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PlayerPassword]);
        _loginUserButtonText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoginUser]);
        _registerUserButtonText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.CreateAccount]);
    }
}