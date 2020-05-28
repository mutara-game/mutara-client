using System;

namespace Mutara.Web.Api
{
    [Serializable]
    public class CreateAccountResponse
    {
        public string UserSub { get; set; }
        public bool UserConfirmed { get; set; }
    }
}