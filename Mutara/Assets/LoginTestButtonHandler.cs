
using System;
using Mutara.Network;
using UnityEditor;
using UnityEditor.Compilation;
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
            // TODO call an endpoint that requires authentication here.
            var response = await NetworkManager.Instance.Player.GetPlayerDetails(Guid.NewGuid());

            EditorUtility.DisplayDialog("Auth", $"{response.PlayerId} + {response.Magic}", "Yay");
        }
    }
}
