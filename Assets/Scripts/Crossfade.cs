using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crossfade : MonoBehaviour
{
    // TODO: Make loading to scene 1s minimum and then waits for the scene to finish loading
    public static Crossfade Instance { get; private set; }
    public event Action onBeginCrossfade;
    public event Action onEndCrossfade;
    public event Action onTeleportCrossfade;
    public Animator Animator { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } 

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Animator = GetComponent<Animator>();
    }

    void BeginCrossfade()
    {
        onBeginCrossfade?.Invoke();
    }

    void EndCrossfade()
    {
        onEndCrossfade?.Invoke();
    }

    void TeleportCrossfade()
    {
        onTeleportCrossfade?.Invoke();
    }
}
