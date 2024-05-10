using PlayFab.ClientModels;
using PlayFab;

namespace Chapter8
{
    public interface ILogin
    {
        void Login(System.Action<LoginResult> onSuccess, System.Action<PlayFabError> onFailure);
    }

}