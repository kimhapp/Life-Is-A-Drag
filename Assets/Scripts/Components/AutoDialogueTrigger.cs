using UnityEngine;
using Yarn.Unity;

public class AutoDialogueTrigger : MonoBehaviour
{
    [SerializeField] string yarnNodeName;
    DialogueRunner dialogueRunner;

    void Awake()
    {
        dialogueRunner = DialogueSystem.Instance.DialogueRunner;

        if (dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner is missing!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            dialogueRunner.StartDialogue(yarnNodeName);
        }
    }
}
