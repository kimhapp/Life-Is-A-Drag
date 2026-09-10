using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactableIndicator;
    [SerializeField] string transitionSceneName;
    [SerializeField] PlayerController player;

    Animator crossfade;

    void Start()
    {
        crossfade = GameObject.Find("Crossfade").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(true);

            player.IsInRangeToInteract = true;
            player.interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(false);

            player.IsInRangeToInteract = false;
            player.interactable = null;
        }
    }

    public void Interact()
    {
        StartCoroutine(StartCrossfade());
    }

    IEnumerator StartCrossfade()
    {
        if (crossfade == null)
        {
            Debug.LogError("Crossfade is missing!");
            yield break;
        }

        crossfade.SetTrigger("Scene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(transitionSceneName);
    }
}
