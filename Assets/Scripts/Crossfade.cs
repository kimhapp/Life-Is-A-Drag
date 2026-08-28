using UnityEngine;
using Yarn.Unity;

public class Crossfade : MonoBehaviour
{
    [SerializeField] Animator crossfade;
    [SerializeField] PlayerController player;
    [SerializeField] DialogueTrigger[] dialogueTriggers;
    [SerializeField] DialogueRunner dialogueRunner;

    bool dialogueHasTriggered = false;

    void Awake()
    {
        // Use AddCommandHandler instead of YarnCommand for no target parameter
        dialogueRunner.AddCommandHandler(
            "play_crossfade_normal",
            PlayCrossfadeNormal
        );  
    }

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

    public void PlayCrossfadeNormal()
    {
        crossfade.SetTrigger("Normal");
    }
}
