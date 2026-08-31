using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] string yarnNodeName;
    [SerializeField] public Type dialogueType;
    [SerializeField] GameObject interactableIndicator;
    
    PlayerController player;
    DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }

    private void OnDisable()
    {
        if (player != null && player.interactable == this && dialogueType == Type.Interact)
        {
            player.IsInRangeToInteract = false;
            player.interactable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            if (dialogueType == Type.Auto)
            {
                dialogueRunner.StartDialogue(yarnNodeName);
                return;
            }

            interactableIndicator.SetActive(true);

            player = collidedObject.GetComponent<PlayerController>();
            player.IsInRangeToInteract = true;
            player.interactable = this;
        }
    }

    public void Interact()
    {   
        if (dialogueType == Type.Interact)
        {
            dialogueRunner.StartDialogue(yarnNodeName);
        }  
    }

    public enum Type {
        Auto,
        Interact
    }
}
