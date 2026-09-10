using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crossfade : MonoBehaviour
{
    // TODO: Make loading to scene 1s minimum and then waits for the scene to finish loading
    public static Crossfade Instance { get; private set; }
    [SerializeField] PlayerController player;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } 

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    void OnSceneUnLoaded(Scene scene)
    {
        // Make sure that every time a scene unloads, player is null
        player = null;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu" || scene.name == "Day Summary")
        {
            return;
        }

        StartCoroutine(FindPlayerDelayed());
    }

    IEnumerator FindPlayerDelayed()
    {
        // Needs to be coroutined and wait for the next frame in case the objects load slow enough 
        yield return null;

        // Make sure that every time a new scene loads, it will find the player first
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Player is missing!");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
    }

    void BeginCrossfade()
    {
        player.cannotControl();
    }

    void EndCrossfade()
    {
        player.canControl();
    }

    void TeleportCrossfade()
    {
        if (player.interactable is Teleporter teleporter)
        {
            teleporter.Teleport();
        }
    }
}
