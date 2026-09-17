using UnityEngine;

// Auto assignment to dialogue runner after every scene
public class InteractivePoints : MonoBehaviour
{
    DialogueSystem dialogueSystem;

    void Awake()
    {
        dialogueSystem = DialogueSystem.Instance;

        if (dialogueSystem == null)  
        {
            Debug.LogError("DialogueSystem is missing!");
            return;
        }

        dialogueSystem.interactivePoints = gameObject;
    }

    void OnDestroy()
    {
        if (dialogueSystem != null && dialogueSystem.interactivePoints == gameObject)  
            dialogueSystem.interactivePoints = null;
    }
}
