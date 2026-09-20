using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject MainMenuScreen;
    [SerializeField] GameObject HowToPlayScreen;

    bool hasPressedPlay = false;

    public void OnPlayButtonClicked()
    {
        // To prevent more than 1 input
        if (!hasPressedPlay)
        {
            hasPressedPlay = true;
            StartCoroutine(LoadScene("Day01MorningPart01"));
        }
    }

    public void OnHowToPlayButtonClicked()
    {
        HowToPlayScreen.SetActive(true);
        MainMenuScreen.SetActive(false);
    }

    public void OnHowToPlayCloseClicked()
    {
        HowToPlayScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator LoadScene(string scene)
    {
        Crossfade crossfade = Crossfade.Instance;
        if (crossfade == null)
        {
            Debug.LogError("Crossfade is missing!");
            yield break;
        }

        crossfade.Animator.SetTrigger("Scene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
