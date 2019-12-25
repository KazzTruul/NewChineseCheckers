using PlayFab;

namespace Middleware
{
    public class UserRegistrationFailedSignal
    {
        public PlayFabError Error;

        public string Username;

        public string Password;
    }
}