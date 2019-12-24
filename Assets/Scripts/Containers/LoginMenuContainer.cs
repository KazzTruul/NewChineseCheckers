using Zenject;
using TMPro;
using UnityEngine;
using Backend;

public class LoginMenuContainer : MonoBehaviour, ILocalizable
{
    [SerializeField]
    private TMP_Text _loginMenuTitleText;
    [SerializeField]
    private TMP_Text _displayNameText;
    [SerializeField]
    private TMP_Text _passwordText;
    [SerializeField]
    private TMP_Text _loginAccountButtonText;
    [SerializeField]
    private TMP_Text _createNewAccountButtonText;

    private ILocalizationManager _localizationManager;

    private string _username = "";
    private string _userPassword = "";

    [Inject]
    private void Initialize(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;

        OnLanguageChanged();
    }

    public void OnLanguageChanged()
    {
        _loginMenuTitleText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.LoginMenuTitle]);
        _displayNameText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PlayerDisplayName]);
        _passwordText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.PlayerPassword]);
        _loginAccountButtonText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.Login]);
        _createNewAccountButtonText.text = _localizationManager.GetTranslation(Constants.Translations[TranslationIdentifier.CreateAccount]);
    }
}