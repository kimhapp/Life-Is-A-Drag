using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class QTEButton : MonoBehaviour
{
    [SerializeField] InputActionReference qteButtonAction;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] float speed = 1f;
    
    float timeElapsed = 0f;
    float length = 0f;
    bool? result;

    void Awake()
    {
        // Make sure to not disable the gameobject since for some reasons 
        // Awake is not called if the object is disabled before runtime 
        
        dialogueRunner.AddCommandHandler<float>(
            "qte_button",
            TurnOn
        );

        gameObject.SetActive(false);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Can be 15% late
        if (timeElapsed >= length * 1.15f)
        {
            OnFail();
        }   
    }

    void OnEnable()
    {
        qteButtonAction.action.Enable();
        qteButtonAction.action.started += PressQTE;

        // Reset all states
        animator.SetFloat("Speed", speed);
        animator.SetTrigger("OnEnable");
        length = animationClip.length / speed; // Must manually calculate length due to animator not getting the clip's actual length with speed
        timeElapsed = 0f;
    }

    void OnDisable()
    {
        qteButtonAction.action.Disable();
        qteButtonAction.action.started -= PressQTE;
    }

    public IEnumerator TurnOn(float speed = 1f)
    {
        result = null;
        this.speed = speed;
        gameObject.SetActive(true);

        while (result == null)
        {
            yield return null;
        }

        dialogueRunner.VariableStorage.SetValue("$qteButtonResult", result?.ToString());
    }

    void OnPass()
    {
        result = true;
        resultText.text = "Pass!";
        resultText.color = Color.green;
        gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
    }

    void OnFail()
    {
        result = false;
        resultText.text = "Fail!";
        resultText.color = Color.red;
        gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
    }

    void PressQTE(InputAction.CallbackContext callback)
    {
        // Can be 15% early
        if (timeElapsed < length * 0.85f)
        {
            OnFail();
        } else
        {
            OnPass();
        }

        gameObject.SetActive(false);
    }
}
