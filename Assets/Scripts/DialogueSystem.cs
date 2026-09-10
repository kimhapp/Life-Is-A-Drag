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
    [SerializeField] GameObject interactivePoints;
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

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
    }

    void OnSceneUnLoaded(Scene scene)
    {
        // Make sure that every time a scene unloads, interactive points is null
        interactivePoints = null;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu" || scene.name == "Day Summary")
        {
            return;
        }

        StartCoroutine(FindInteractivePointsDelayed());
    }

    IEnumerator FindInteractivePointsDelayed()
    {
        // Needs to be coroutined and wait for the next frame in case the objects load slow enough 
        yield return null;

        // Make sure that every time a new scene loads, it will find the interactive points first
        interactivePoints = GameObject.FindWithTag("InteractivePoints");
        if (interactivePoints == null)
        {
            Debug.LogError("Interatcive Points is missing!");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
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
