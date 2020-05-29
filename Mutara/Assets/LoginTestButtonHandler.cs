using System;
using System.Threading.Tasks;
using Mutara.Network;
using UnityEditor;
using UnityEngine;

namespace Mutara
{
    public class LoginTestButtonHandler : MonoBehaviour
    {

        public void Start()
        {
            // UnityInitializer.AttachToGameObject(this.gameObject);
        }

        public async void OnClick()
        {
            var settings = PlayerSettingsAccess.Instance.GetSettings();
            if (!settings.UserAccountCreated)
            {
                settings.UserId = Guid.NewGuid();
                settings.Password = Guid.NewGuid().ToString();
                Debug.Log("creating a user account...");
                settings.UserSub = await CreateUserAccount(settings);
                PlayerSettingsAccess.Instance.SaveSettings(settings);
            }

            string s = await CallSignIn(settings);
            // TODO remove Amazon.Cognito* if it turns out to be lame and not needed.
            EditorUtility.DisplayDialog("Clicked", s, "Great");
        }

        private async Task<string> CreateUserAccount(PlayerSettings settings)
        {
            var createAccountResponse =
                await NetworkManager.Instance.Auth.CreateAccount(settings.UserId, settings.Password);
            return createAccountResponse.UserSub;
        }

        private async Task<string> CallSignIn(PlayerSettings settings)
        {
            var signInResponse = await NetworkManager.Instance.Auth.SignIn(settings.UserId, settings.Password);
            // TODO error handling..
            return signInResponse.IdToken;


            /*
            var client = new HttpClient();
            var signInRequest = new SignInRequest
            {
                UserId = Guid.NewGuid(),
                Password = Guid.NewGuid().ToString(), //Placeholder
                ClientVersion = "123", // TODO fix this
            };
            string signInRequestString = JsonConvert.SerializeObject(signInRequest);
            Debug.Log(signInRequestString.ToString());
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://localhost:5001/Auth/signin"),
                Method = HttpMethod.Post,
                Content = new StringContent(signInRequestString, Encoding.UTF8, "application/json"),
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
            HttpResponseMessage response = await client.SendAsync(request);
            // TODO all sorts of error conditions here!!
            string raw = await response.Content.ReadAsStringAsync();
            ApiOkResponse<SignInResponse> responseObject = JsonConvert.DeserializeObject<ApiOkResponse<SignInResponse>>(raw);
            // lets extract the idToken and pour that back into /Player/details/someGuid
            // and see if the token works round trip.
            // TODO
            
            
            return responseObject.Content.IdToken;
            */
        }
    }
}
