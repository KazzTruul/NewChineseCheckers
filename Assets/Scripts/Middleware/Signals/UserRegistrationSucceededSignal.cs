using PlayFab.ClientModels;

namespace Middleware
{
    public class UserRegistrationSucceededSignal
    {
        public RegisterPlayFabUserResult Result;

        public string Username;

        public string Password;
    }
}
