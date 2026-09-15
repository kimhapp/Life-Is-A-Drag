using System.Collections;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]
public class DialogueSystem : MonoBehaviour
{
    // Uses singleton so Yarn's variable storage persists across scenes
    public static DialogueSystem Instance { get; private set; }

    [HideInInspector] public GameObject interactivePoints;
    public DialogueRunner DialogueRunner { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        DialogueRunner = GetComponent<DialogueRunner>();

        // Use AddCommandHandler instead of YarnCommand for no target parameter
        DialogueRunner.AddCommandHandler<GameObject, bool>(
            "set_object_inactive",
            SetObjectInactive
        );
        DialogueRunner.AddCommandHandler<string, bool>(
            "set_object_active",
            SetObjectActive
        );
        DialogueRunner.AddCommandHandler(
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
        Animator crossfade = Crossfade.Instance.Animator;

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
