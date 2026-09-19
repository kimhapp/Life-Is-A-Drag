using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject MainMenuScreen;
    [SerializeField] GameObject HowToPlayScreen;

    public void OnPlayButtonClicked()
    {
        StartCoroutine(LoadScene("Day01MorningPart01"));
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
