using PlayFab;

namespace Middleware
{
    public class UserLoginFailedSignal
    {
        public PlayFabError Error;

        public string Username;

        public string Password;
    }
}
