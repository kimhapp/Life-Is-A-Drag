using System;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]
public class PersistentDialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject interactivePoints;
    [SerializeField] Animator crossfade;
    DialogueRunner dialogueRunner;

    void Awake()
    {
        dialogueRunner = GetComponent<DialogueRunner>();

        // Use AddCommandHandler instead of YarnCommand for no target parameter
        dialogueRunner.AddCommandHandler<GameObject, bool>(
            "set_object_inactive",
            SetObjectInactive
        );
        dialogueRunner.AddCommandHandler<string, bool>(
            "set_object_active",
            SetObjectActive
        );
        dialogueRunner.AddCommandHandler(
            "wait_for_crossfade",
            WaitForCrossfade
        );  
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator SetObjectInactive(GameObject gameObj, bool immediate)
    {
        if (!immediate) yield return new WaitForSeconds(0.25f);
        gameObj.SetActive(false);
    }

    public IEnumerator SetObjectActive(string gameObjName, bool immediate)
    {
        if (!immediate) yield return new WaitForSeconds(0.25f);
        GameObject gameObj = interactivePoints.transform.Find(gameObjName).gameObject;
        gameObj.SetActive(true);
    }

    IEnumerator WaitForCrossfade()
    {
        if (crossfade == null)
        {
            Debug.LogError("Crossfade is missing!");
            yield break;
        }

        crossfade.SetTrigger("Normal");
        yield return new WaitForSeconds(1);
    }
}
