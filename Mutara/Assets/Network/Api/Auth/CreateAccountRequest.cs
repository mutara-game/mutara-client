using System;

namespace Mutara.Network.Api.Auth
{
    [Serializable]
    public class CreateAccountRequest
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }
    }
}