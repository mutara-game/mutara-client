using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoginTestButtonHandler : MonoBehaviour
{
    public void OnClick()
    {

        EditorUtility.DisplayDialog("Clicked", "You clicked!", "Great");
    }
}
