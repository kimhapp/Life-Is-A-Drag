using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactableIndicator;
    [SerializeField] GameObject teleportDestination;
    [SerializeField] GameObject cam;
    [SerializeField] PlayerController player;

    Animator crossfade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crossfade = GameObject.Find("Crossfade").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(true);

            player.IsInRangeToInteract = true;
            player.interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(false);

            player.IsInRangeToInteract = false;
            player.interactable = null;
        }
    }

    public void Interact()
    {
        crossfade.SetTrigger("Teleport");
    }

    public void Teleport()
    {
        Teleporter teleporter = teleportDestination.GetComponent<Teleporter>();
        teleporter.cam.SetActive(true);
        cam.SetActive(false);

        player.gameObject.GetComponent<Collider>().enabled = false;
        player.transform.position = teleportDestination.transform.position;
        player.gameObject.GetComponent<Collider>().enabled = true;
    }
}
