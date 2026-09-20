using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Crossfade : MonoBehaviour
{
    // TODO: Make loading to scene 1s minimum and then waits for the scene to finish loading
    public static Crossfade Instance { get; private set; }
    public event Action OnBeginCrossfade;
    public event Action OnEndCrossfade;
    public event Action OnTeleportCrossfade;
    public Animator Animator { get; private set; }

    void Awake()
    {
        // Do not try to destroy the object through Main Menu
        // As this script was set to execute before any other scripts
        // Which will destroy the object when entering the game through Main Menu
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } 

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Animator = GetComponent<Animator>();
    }

    // These fuctions are called by Unity Animation event
    void BeginCrossfade()
    {
        OnBeginCrossfade?.Invoke();
    }

    void EndCrossfade()
    {
        OnEndCrossfade?.Invoke();
    }

    void TeleportCrossfade()
    {
        OnTeleportCrossfade?.Invoke();
    }
}
