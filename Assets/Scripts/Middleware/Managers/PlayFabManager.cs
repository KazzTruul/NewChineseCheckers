﻿using PlayFab;
using PlayFab.ClientModels;
using Zenject;

namespace Middleware
{
    public sealed class PlayFabManager
    {
        private SignalBus _signalBus;

        [Inject]
        public void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;

            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = MiddlewareConstants.PlayFabTitleId;
            }
        }

        #region UserRegistration
        public void RegisterUser(string username, string password)
        {
            var request = new RegisterPlayFabUserRequest { DisplayName = username, Password = password };

            PlayFabClientAPI.RegisterPlayFabUser(request,
                result => OnUserRegistrationSuccess(result, username, password),
                error => OnUserRegistrationFailure(error, username, password));
        }

        private void OnUserRegistrationSuccess(RegisterPlayFabUserResult result, string username, string password)
        {
            _signalBus.Fire(new UserRegistrationSucceededSignal { Result = result, Username = username, Password = password });
        }

        private void OnUserRegistrationFailure(PlayFabError error, string username, string password)
        {
            _signalBus.Fire(new UserRegistrationFailedSignal { Error = error, Username = username, Password = password });
        }
        #endregion UserRegistration

        #region UserLogin
        public void LoginUser(string username, string password)
        {
            var request = new LoginWithPlayFabRequest { Username = username, Password = password };

            PlayFabClientAPI.LoginWithPlayFab(request,
                result => OnUserLoginSuccess(result, username, password),
                error => OnUserLoginFailure(error, username, password));
        }

        private void OnUserLoginSuccess(LoginResult result, string username, string password)
        {
            _signalBus.Fire(new UserLoginSucceededSignal { Result = result, Username = username, Password = password });
        }

        private void OnUserLoginFailure(PlayFabError error, string username, string password)
        {
            _signalBus.Fire(new UserLoginFailedSignal { Error = error, Username = username, Password = password });
        }
        #endregion UserLogin
    }
}