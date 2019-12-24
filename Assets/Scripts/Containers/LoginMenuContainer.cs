using Zenject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenuContainer : MonoBehaviour, ILocalizable
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

    private readonly VerifyUsernameValidityStrategy _verifyUsernameValidityStrategy = new VerifyUsernameValidityStrategy();

    private string _username = "";
    private string _userPassword = "";

    [Inject]
    private void Initialize(
        ILocalizationManager localizationManager,
        ICommandDispatcher commandDispatcher,
        RegisterUserCommandFactory registerUserCommandFactory,
        LoginUserCommandFactory loginUserCommandFactory)
    {
        _localizationManager = localizationManager;
        _commandDispatcher = commandDispatcher;
        _registerUserCommandFactory = registerUserCommandFactory;
        _loginUserCommandFactory = loginUserCommandFactory;

        _usernameInputField.onValueChanged.AddListener(OnUsernameInputChanged);
        _registerUserButton.onClick.AddListener(OnRegisterUser);
        _loginUserButton.onClick.AddListener(OnLoginUser);

        OnLanguageChanged();
    }

    private void OnUsernameInputChanged(string username)
    {
        _username = username;

        _registerUserButton.interactable = _verifyUsernameValidityStrategy.IsValidUsername(_username);
        _loginUserButton.interactable = _verifyUsernameValidityStrategy.IsValidUsername(_username);
    }

    private void OnUserPasswordInputChanged(string password)
    {
        _userPassword = password;
    }

    private void OnRegisterUser()
    {
        _commandDispatcher.ExecuteCommand(_registerUserCommandFactory.Create(_username, _userPassword));
    }

    private void OnLoginUser()
    {
        _commandDispatcher.ExecuteCommand(_loginUserCommandFactory.Create(_username, _userPassword));
    }

    public void OnLanguageChanged()
    {
        _loginMenuTitleText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoginMenuTitle]);
        _usernameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PlayerDisplayName]);
        _passwordText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PlayerPassword]);
        _loginUserButtonText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.Login]);
        _registerUserButtonText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.CreateAccount]);
    }
}