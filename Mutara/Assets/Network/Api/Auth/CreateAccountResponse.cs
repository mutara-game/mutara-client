using System;

namespace Mutara.Network.Api.Auth
{
    [Serializable]
    public class CreateAccountResponse
    {
        public string UserSub { get; set; }
        public bool UserConfirmed { get; set; }
    }
}