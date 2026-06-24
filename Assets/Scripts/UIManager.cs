using TMPro;
using UnityEngine;
using Yarn.Unity;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator crossfade;
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] TextMeshProUGUI locationText;
    [SerializeField] InMemoryVariableStorage variableStorage;
    [SerializeField] TimeManager timeManager;
    [SerializeField] ObjectiveManager objectiveManager;
    [SerializeField] LocationManager locationManager;
    [SerializeField] DialogueRunner dialogueRunner;

    void Awake()
    {
        // Use AddCommandHandler instead of YarnCommand for no target parameter
        dialogueRunner.AddCommandHandler(
            "play_crossfade_normal",
            PlayCrossfadeNormal
        );

        timeManager.OnTimeChanged += ChangeTimeText;
        objectiveManager.OnObjectiveChanged += ChangeObjectiveText;
        locationManager.OnLocationChanged += ChangeLocationText;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dayText.text = "Day: 1";
        ChangeTimeText();
        ChangeObjectiveText();
        ChangeLocationText();
    }

    void ChangeTimeText()
    {
        timeText.text = "Time: " + timeManager.GetCurrentTime();
    }

    void ChangeObjectiveText()
    {
        objectiveText.text = "Objective: " + objectiveManager.GetCurrentObjective();
    }

    void ChangeLocationText()
    {
        locationText.text = "Location: " + locationManager.GetCurrentLocation();
    }
    
    public void PlayCrossfadeNormal()
    {
        crossfade.SetTrigger("Normal");
    }
}
