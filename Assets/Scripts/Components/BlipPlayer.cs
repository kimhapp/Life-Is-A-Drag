using System.Threading;
using TMPro;
using UnityEngine;
using Yarn.Markup;
using Yarn.Unity;

public enum BlipType
{
    Me,
    Shelly,
    Marvin
}

[RequireComponent(typeof(AudioSource))]
public class BlipPlayer : MonoBehaviour, IActionMarkupHandler
{
    // TODO: Swap array with something more concrete like 2D array, Dictionary 
    // As for now blip order must match enum order which is super fragile
    [SerializeField] AudioClip[] blips;

    LinePresenter linePresenter;
    TMP_Text characterNameGUI;
    AudioSource blipAudioSource;

    public void OnLineDisplayBegin(MarkupParseResult line, TMP_Text text) { }

    public void OnLineDisplayComplete() { }

    public void OnLineWillDismiss() { }

    public void OnPrepareForLine(MarkupParseResult line, TMP_Text text) { }

    void Awake()
    {
        blipAudioSource = GetComponent<AudioSource>();

        linePresenter = DialogueSystem.Instance.linePresenter;

        if(linePresenter == null) 
        { 
            Debug.LogError("Line Presenter is missing!");
            return;
        }
        
        characterNameGUI = linePresenter.characterNameText;
    }

    void Start()
    {
        // Needs to be added in Start
        // As the typewriter only get instantiated in linePresenter's Awake
        linePresenter.Typewriter.ActionMarkupHandlers.Add(this);
    }

    // Will play the blip sound when each character appears on the screen.
    public YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken)
    {
        char character = line.Text[currentCharacterIndex];

        if (!char.IsPunctuation(character) && !char.IsWhiteSpace(character))
        {
            BlipType blipType = characterNameGUI.text switch
            {
                "Me" => BlipType.Me,
                "Shelly" => BlipType.Shelly,
                "Marvin" => BlipType.Marvin,
                _ => BlipType.Me
            };

            AudioClip blip = blips[(int) blipType];

            if (!blipAudioSource.isPlaying)
            {
                blipAudioSource.PlayOneShot(blip);
            }
        }

        return YarnTask.CompletedTask;
    }

    void OnDestroy()
    {
        if (linePresenter != null)
        {
            linePresenter.Typewriter.ActionMarkupHandlers.Remove(this);
        }
    }
}
