using System;
using System.Collections;
using UnityEngine;
using Yarn.Unity;

public enum TimeOfDay
{
    Morning,
    Afternoon,
    Evening,
    Night
}

public class TimeManager : MonoBehaviour
{
    [SerializeField] Material[] skyboxes;
    [SerializeField] InMemoryVariableStorage variableStorage;
    [SerializeField] DialogueRunner dialogueRunner;

    public Action OnTimeChanged;

    void Awake()
    {
        // Use AddCommandHandler instead of YarnCommand for no target parameter
        dialogueRunner.AddCommandHandler<string>(
            "change_time",
            ChangeTime
        );
    }

    public IEnumerator ChangeTime(string timeString)
    {
        yield return new WaitForSeconds(0.25f);
        
        System.Enum.TryParse<TimeOfDay>(timeString, out TimeOfDay time);
        OnTimeChanged?.Invoke();
        RenderSettings.skybox = skyboxes[(int) time];
        variableStorage.SetValue("$currentTime", time.ToString());
        variableStorage.SetValue("$Marvin_hasSpokenTo", false);
        variableStorage.SetValue("$Shelly_hasSpokenTo", false);
    }

    public TimeOfDay GetCurrentTime()
    {
        variableStorage.TryGetValue("$currentTime", out string time);
        Enum.TryParse(time, out TimeOfDay timeOfDay);
        return timeOfDay;
    }
}
