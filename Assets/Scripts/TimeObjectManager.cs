using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class TimeObjectManager : MonoBehaviour
{
    [SerializeField] GameObject[] timeObjects;
    [SerializeField] TimeManager timeManager;
    [SerializeField] DialogueRunner dialogueRunner;

    void Awake()
    {
        // Use AddCommandHandler instead of YarnCommand for no target parameter
        dialogueRunner.AddCommandHandler<GameObject, bool>(
            "set_object_inactive",
            SetObjectInactive
        );
        dialogueRunner.AddCommandHandler<string, bool>(
            "set_object_active",
            SetObjectActive
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
        GameObject gameObj = timeObjects[(int) timeManager.GetCurrentTime()].transform.Find(gameObjName).gameObject;
        gameObj.SetActive(true);
    }
}
