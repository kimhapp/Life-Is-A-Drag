using UnityEngine;

public class Crossfade : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] DialogueTrigger[] dialogueTriggers;

    bool dialogueHasTriggered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (DialogueTrigger trigger in dialogueTriggers)
        {
            trigger.TriggerDialogue += OnTriggerDialogue;
        }
    }

    void BeginCrossfade()
    {
        Debug.Log("This goes second");
        player.cannotControl();
    }

    void EndCrossfade()
    {
        if (dialogueHasTriggered)
        {
            dialogueHasTriggered = false;
        } else
        {
            player.canControl();
        }
    }

    void TeleportCrossfade()
    {
        if (player.interactable is Teleporter teleporter)
        {
            teleporter.Teleport();
        }
    }

    void OnTriggerDialogue()
    {
        dialogueHasTriggered = true;
    }
}
