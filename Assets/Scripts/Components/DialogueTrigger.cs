using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] string yarnNodeName;
    [SerializeField] GameObject interactableIndicator;
    
    PlayerController player;
    DialogueRunner dialogueRunner;
    UnityAction OnDialogueStartHandler;
    UnityAction OnDialogueCompleteHandler;


    void Awake()
    {
        dialogueRunner = DialogueSystem.Instance.DialogueRunner;

        if (dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner is missing!");
        }
    }

    void OnDisable()
    {
        // If the object suddenly disappears
        // This will make sure the player is not pointing to this object
        if (player != null && player.interactable == (IInteractable)this)
        {
            player.IsInRangeToInteract = false;
            player.interactable = null;
        }
        
        dialogueRunner.onDialogueStart.RemoveListener(OnDialogueStartHandler);
        dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueCompleteHandler);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(true);

            player = collidedObject.GetComponent<PlayerController>();
            player.IsInRangeToInteract = true;
            player.interactable = this;

            // Use action to hold as a listener in order to remove them later
            OnDialogueStartHandler = () => interactableIndicator.SetActive(false);
            OnDialogueCompleteHandler = () => { 
                if (player.interactable == (IInteractable)this) interactableIndicator.SetActive(true);
            };

            dialogueRunner.onDialogueStart.AddListener(OnDialogueStartHandler);
            dialogueRunner.onDialogueComplete.AddListener(OnDialogueCompleteHandler);
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(false);

            player = collidedObject.GetComponent<PlayerController>();
            player.IsInRangeToInteract = false;
            player.interactable = null;

            dialogueRunner.onDialogueStart.RemoveListener(OnDialogueStartHandler);
            dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueCompleteHandler);
        }
    }

    public void Interact()
    {   
        dialogueRunner.StartDialogue(yarnNodeName);
    }
}
