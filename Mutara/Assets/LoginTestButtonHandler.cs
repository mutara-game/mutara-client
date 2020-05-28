using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mutara.Web.Api;
using Mutara.Web.Api.Auth;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class LoginTestButtonHandler : MonoBehaviour
{

    public void Start()
    {
        // UnityInitializer.AttachToGameObject(this.gameObject);
    }

    public async void OnClick()
    {
        string s = await CallSignIn();
        // TODO remove Amazon.Cognito* if it turns out to be lame and not needed.
        EditorUtility.DisplayDialog("Clicked", s, "Great");
    }

    private async Task<string> CallSignIn()
    {
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
    }
}
