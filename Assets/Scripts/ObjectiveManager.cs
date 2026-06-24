using System;
using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] InMemoryVariableStorage variableStorage;
    [SerializeField] DialogueRunner dialogueRunner;

    public Action OnObjectiveChanged;

    void Awake()
    {
        // Use AddCommandHandler instead of YarnCommand for no target parameter
        dialogueRunner.AddCommandHandler<string>(
            "change_objective",
            ChangeObjective
        );
    }
    
    public IEnumerator ChangeObjective(string objective)
    {
        yield return new WaitForSeconds(0.25f);

        OnObjectiveChanged?.Invoke();
        variableStorage.SetValue("$currentObjective", objective);
    }

    public string GetCurrentObjective()
    {
        variableStorage.TryGetValue("$currentObjective", out string objective);
        return objective;
    }
}
