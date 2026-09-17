using System;
using UnityEngine;

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
