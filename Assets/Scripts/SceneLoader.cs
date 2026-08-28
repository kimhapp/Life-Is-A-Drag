using TMPro;
using UnityEngine;

public class SceneLoader : MonoBehaviour, IInteractable
{
    [SerializeField] string textIndicator;
    [SerializeField] GameObject teleportDestination;

    PlayerController player;
    TextMeshProUGUI teleportIndicator;
    Animator crossfade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        crossfade.SetTrigger("Teleport");
    }
}
