using System;

namespace Mutara.Web.Api
{
    [Serializable]
    public class CreateAccountRequest
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }
    }
}