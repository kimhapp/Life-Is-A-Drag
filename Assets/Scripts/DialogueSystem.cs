using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]
public class DialogueSystem : MonoBehaviour
{
    // Uses singleton so Yarn's variable storage persists across scenes
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] Animator crossfade;
    GameObject interactivePoints;
    DialogueRunner dialogueRunner;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

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

        // Crossfade normal has no begin and end trigger to avoid override yarn's event
        crossfade.SetTrigger("Normal");
        yield return new WaitForSeconds(1);
    }
}
