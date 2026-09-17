using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interactableIndicator;
    [SerializeField] string transitionSceneName;
    [SerializeField] PlayerController player;

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
        Crossfade crossfade = Crossfade.Instance;
        if (crossfade == null)
        {
            Debug.LogError("Crossfade is missing!");
            yield break;
        }

        crossfade.Animator.SetTrigger("Scene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(transitionSceneName);
    }
}
