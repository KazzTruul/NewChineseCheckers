using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Zenject;

namespace Backend
{
    public class PlayFabManager
    {
        private const string PlayFabTitleId = "F0266";

        [Inject]
        public void Initialize()
        {
            //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = PlayFabTitleId; // Please change this value to your own titleId from PlayFab Game Manager
            }
        }

        public void LoginUser(string username, string password)
        {
            var request = new LoginWithPlayFabRequest { Username = username, Password = password };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }

        public void CreateUser(string username, string password)
        {
            var x = new RegisterPlayFabUserRequest { DisplayName = username, Password = password };
            PlayFabClientAPI.RegisterPlayFabUser(x, OnAccountCreationSuccess, OnAccountCreationFailure);
        }

        private void OnLoginSuccess(LoginResult result)
        {

        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }

        private void OnAccountCreationSuccess(RegisterPlayFabUserResult result)
        {

        }

        private void OnAccountCreationFailure(PlayFabError error)
        {

        }
    }
}