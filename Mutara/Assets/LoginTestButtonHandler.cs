using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://localhost:5001/Auth/signin"),
            Method = HttpMethod.Post,
            Content = new StringContent("{\"userName\":\"testdood\",\"password\":\"xyzzy\"}", Encoding.UTF8, "application/json"),
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }
    
    public class SignInResponse
    {
        public string SessionId { get; set; }
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}
