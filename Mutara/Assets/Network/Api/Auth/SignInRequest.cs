using System;

namespace Mutara.Network.Api.Auth
{
    // TODO add support for facebook, google, etc...
    [Serializable]
    public class SignInRequest
    {
        
        public Guid UserId { get; set; } 

        public string Password { get; set; } 
        
        public string ClientVersion { get; set; }
    }
}