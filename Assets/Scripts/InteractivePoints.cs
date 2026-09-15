using UnityEngine;
using Yarn.Unity;

// Auto assignment to dialogue runner after every scene
public class InteractivePoints : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    
    void Awake()
    {
        dialogueRunner = GameObject.FindWithTag("DialogueSystem").GetComponent<DialogueRunner>();
        if (dialogueRunner == null) Debug.LogError("DialogueRunner is missing!");
    }
}
