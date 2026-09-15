using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactableIndicator;
    [SerializeField] GameObject teleportDestination;
    [SerializeField] GameObject cam;

    PlayerController player;
    Crossfade crossfade;
    Animator crossfadeAnimator;

    void Awake()
    {
        GameObject crossfadeGameObject = GameObject.FindWithTag("Crossfade");
        if (crossfadeGameObject != null)
        {
            crossfade = crossfadeGameObject.GetComponent<Crossfade>();
            crossfadeAnimator = crossfadeGameObject.GetComponent<Animator>();
        } else
        {
            Debug.LogError("Crossfade is missing!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (!collidedObject.CompareTag("Player")) return;
        
        player = collidedObject.GetComponent<PlayerController>();

        interactableIndicator.SetActive(true);
        player.IsInRangeToInteract = true;
        player.interactable = this;
    }

    void OnTriggerExit(Collider other)
    {
        // If 2 objects overlap, this might be a problem
        GameObject collidedObject = other.gameObject;

        if (!collidedObject.CompareTag("Player")) return;
        
        interactableIndicator.SetActive(false);
        player.IsInRangeToInteract = false;
        player.interactable = null;
    }

    public void Interact()
    {
        crossfade.onTeleportCrossfade += Teleport;
        crossfadeAnimator.SetTrigger("Teleport");
    }

    public void Teleport()
    {
        crossfade.onTeleportCrossfade -= Teleport;
        
        Teleporter teleporter = teleportDestination.GetComponent<Teleporter>();
        teleporter.cam.SetActive(true);
        cam.SetActive(false);

        player.gameObject.GetComponent<Collider>().enabled = false;
        player.transform.position = teleportDestination.transform.position;
        player.gameObject.GetComponent<Collider>().enabled = true;
    }
}
