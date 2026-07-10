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

public class BlipPlayer : MonoBehaviour, IActionMarkupHandler
{
    [SerializeField] AudioClip[] blips;
    [SerializeField] AudioSource blipAudioSource;
    [SerializeField] LinePresenter linePresenter;
    [SerializeField] TextMeshProUGUI characterName;

    public void OnLineDisplayBegin(MarkupParseResult line, TMP_Text text) { }

    public void OnLineDisplayComplete() { }

    public void OnLineWillDismiss() { }

    public void OnPrepareForLine(MarkupParseResult line, TMP_Text text) { }

    private void Start()
    {
        linePresenter.Typewriter.ActionMarkupHandlers.Add(this);
    }

    // Will play the blip sound when each character appears on the screen.
    public YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken)
    {
        char character = line.Text[currentCharacterIndex];

        if (!char.IsPunctuation(character) && !char.IsWhiteSpace(character))
        {
            BlipType blipType = characterName.text switch
            {
                "Me" => BlipType.Me,
                "Shelly" => BlipType.Shelly,
                "Marvin" => BlipType.Marvin,
                _ => BlipType.Me
            };

            AudioClip blip = blips[(int) blipType];
            blipAudioSource.PlayOneShot(blip);
        }

        return YarnTask.CompletedTask;
    }
}
