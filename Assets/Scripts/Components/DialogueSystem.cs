using System.Collections;
using TMPro;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(DialogueRunner))]
public class DialogueSystem : MonoBehaviour
{
    // Uses singleton so Yarn's variable storage persists across scenes
    public static DialogueSystem Instance { get; private set; }

    [HideInInspector] public GameObject interactivePoints;
    public DialogueRunner DialogueRunner { get; private set; }
    public LinePresenter linePresenter;
    
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
        DialogueRunner.AddCommandHandler<GameObject>(
            "set_object_inactive",
            SetObjectInactive
        );
        DialogueRunner.AddCommandHandler<string>(
            "set_object_active",
            SetObjectActive
        );
        DialogueRunner.AddCommandHandler(
            "wait_for_crossfade",
            WaitForCrossfade
        );
    }

    public void SetObjectInactive(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void SetObjectActive(string gameObjectName)
    {
        GameObject gameObject = interactivePoints.transform.Find(gameObjectName).gameObject;
        gameObject.SetActive(true);
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
