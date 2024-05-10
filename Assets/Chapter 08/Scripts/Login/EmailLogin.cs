using PlayFab.ClientModels;
using PlayFab;

namespace Chapter8
{
    public class EmailLogin : ILogin
    {
        private string email;
        private string password;

        public EmailLogin(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public void Login(System.Action<LoginResult> onSuccess, System.Action<PlayFabError> onFailure)
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = password
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, onSuccess, onFailure);
        }
    }

}