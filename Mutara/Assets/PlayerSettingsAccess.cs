using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Handles access to local settings, stuff like
/// persistent player ID and whatever.
///
/// Eventually player ID would be in the keychain
/// or somewhere secure; for now, just moving forward
/// with something simple.
/// </summary>
public class PlayerSettingsAccess : MonoBehaviour
{
    // set in Awake() because not allowed to access Application.persistentDataPath before that point
    private static string playerSettingsFilePath; 
    private PlayerSettings settings;

    public static PlayerSettingsAccess Instance { get; private set; }

    public PlayerSettings PlayerSettings
    {
        get => settings;
        set
        {
            settings = value;
            Save(settings);
        }
    }

    void Awake()
    {
        Debug.Log("PlayerSettingsAccess awake");
        playerSettingsFilePath = Application.persistentDataPath + "/PlayerSettings.json";
        PlayerSettings = Read();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private static PlayerSettings Read()
    {
        Debug.Log($"Reading {playerSettingsFilePath}");
        if (!File.Exists(playerSettingsFilePath))
        {
            return new PlayerSettings();
        }

        using (var fs = File.Open(playerSettingsFilePath, FileMode.Open))
        {
            using (var sr = new StreamReader(fs))
            {
                return JsonConvert.DeserializeObject<PlayerSettings>(sr.ReadToEnd());
            }
        }
    }

    private static void Save(PlayerSettings settings)
    {
        Debug.Log($"Saving {playerSettingsFilePath}");
        using (var fs = File.Create(playerSettingsFilePath))
        {
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(JsonConvert.SerializeObject(settings));
            }
        }
    }
}

public class PlayerSettings
{
    public Guid UserId { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public bool UserAccountCreated => UserId != Guid.Empty;
}
