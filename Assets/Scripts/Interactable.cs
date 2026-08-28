using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string yarnNodeName;
    [SerializeField] GameObject interactableIndicator;
    
    PlayerController player;
    Animator crossfade;
    DialogueRunner dialogueRunner;
    InMemoryVariableStorage variableStorage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crossfade = GameObject.Find("Crossfade").GetComponent<Animator>();
        dialogueRunner = GameObject.Find("Dialogue System").GetComponent<DialogueRunner>();
        variableStorage = GameObject.Find("Dialogue System").GetComponent<InMemoryVariableStorage>();
    }

    private void OnDisable()
    {
        if (player != null && player.interactable == this)
        {

            player.IsInRangeToInteract = false;
            player.interactable = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (collidedObject.CompareTag("Player"))
        {
            interactableIndicator.SetActive(true);

            player = collidedObject.GetComponent<PlayerController>();
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

            player = collidedObject.GetComponent<PlayerController>();
            player.IsInRangeToInteract = false;
            player.interactable = null;
        }
    }

    public void Interact()
    {   
        if (yarnNodeName.Length > 0)
        {
            dialogueRunner.StartDialogue(yarnNodeName);
        } else
        {
            StartCoroutine(ChangeToSummaryScene());
        }
    }

    IEnumerator ChangeToSummaryScene()
    {
        crossfade.SetTrigger("Scene");
        SaveCharacterPoint();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Day Summary Scene");
    }

    void SaveCharacterPoint()
    {
        variableStorage.TryGetValue("$Marvin_likePoint", out float marvinLikePoint);
        variableStorage.TryGetValue("$Marvin_quirkPoint", out float quirkPoint);
        variableStorage.TryGetValue("$Marvin_lovePoint", out float lovePoint);
        variableStorage.TryGetValue("$Shelly_likePoint", out float shellyLikePoint);
        variableStorage.TryGetValue("$Shelly_positivePoint", out float positivePoint);
        variableStorage.TryGetValue("$Shelly_honestPoint", out float honestPoint);

        PlayerPrefs.SetFloat("Marvin_likePoint", marvinLikePoint);
        PlayerPrefs.SetFloat("Marvin_quirkPoint", quirkPoint);
        PlayerPrefs.SetFloat("Marvin_lovePoint", lovePoint);
        PlayerPrefs.SetFloat("Shelly_likePoint", shellyLikePoint);
        PlayerPrefs.SetFloat("Shelly_positivePoint", positivePoint);
        PlayerPrefs.SetFloat("Shelly_honestPoint", honestPoint);

        PlayerPrefs.Save();
    }
}
