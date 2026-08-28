using TMPro;
using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] string textIndicator;
    [SerializeField] GameObject teleportDestination;
    [SerializeField] GameObject cam;

    PlayerController player;
    TextMeshProUGUI teleportIndicator;
    Animator crossfade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Jerome").GetComponent<PlayerController>();
        teleportIndicator = GameObject.Find("UI/Teleport Indicator").GetComponent<TextMeshProUGUI>();
        crossfade = GameObject.Find("Crossfade").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            teleportIndicator.text = textIndicator;

            player.IsInRangeToInteract = true;
            player.interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            teleportIndicator.text = "";

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
