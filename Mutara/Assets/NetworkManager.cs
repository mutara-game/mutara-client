using Amazon;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{



    public static NetworkManager Instance { get; private set; }

    void Awake()
    {
        Debug.Log("NetworkManager awake!");
        
        Instance = this;           
        // This object will persist until the game is closed
        DontDestroyOnLoad(gameObject);
        // Initialize AWS SDK
        UnityInitializer.AttachToGameObject(gameObject);
        
        // Now that the network manager is ready, head to ...somewhere.
         SceneManager.LoadScene("SampleScene");
    }
   
}
