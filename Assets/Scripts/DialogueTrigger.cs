using System;
using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public Action TriggerDialogue;

    [SerializeField] string yarnNodeName;
    DialogueRunner dialogueRunner;

    private void Start()
    {
        dialogueRunner = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            TriggerDialogue?.Invoke();
            dialogueRunner.StartDialogue(yarnNodeName);
        }
    }
}
