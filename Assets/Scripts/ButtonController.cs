using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Animator crossfade;
    [SerializeField] GameObject MainMenuScreen;
    [SerializeField] GameObject HowToPlayScreen;

    public void OnPlayButtonClicked()
    {
        StartCoroutine(LoadScene("Main Scene"));
    }

    public void OnReturnButtonClicked()
    {
        StartCoroutine(LoadScene("Main Menu"));
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
        crossfade.SetTrigger("Scene");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}
