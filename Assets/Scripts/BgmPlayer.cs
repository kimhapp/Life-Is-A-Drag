using UnityEngine;

public class BgmPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioSource bgmAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeManager.OnTimeChanged += PlayBgm;
    }

    void PlayBgm()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = bgms[(int) timeManager.GetCurrentTime()];
        bgmAudioSource.Play();
    }
}
