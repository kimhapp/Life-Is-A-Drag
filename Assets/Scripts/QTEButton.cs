
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class QTEButton : MonoBehaviour
{
    [SerializeField] InputActionReference qteButtonAction;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] DialogueRunner dialogueRunner;

    float timeElapsed = 0f;
    float length = 0f;

    void Awake()
    {
        // dialogueRunner.AddCommandHandler(
        //     "qte_button",
        //     () => gameObject.SetActive(true)
        // );
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= length + .5f)
        {
            MissQTE();
        }   
    }

    void OnEnable()
    {
        qteButtonAction.action.Enable();
        qteButtonAction.action.started += PressQTE;

        animator.SetTrigger("OnEnable");
        length = animationClip.length;
        timeElapsed = 0f;
    }

    void OnDisable()
    {
        qteButtonAction.action.Disable();
        qteButtonAction.action.started -= PressQTE;
    }

    void PressQTE(InputAction.CallbackContext callback)
    {
        if (timeElapsed < length - .15f)
        {
            Debug.Log("Too early!");
        } else
        {
            Debug.Log("Pressed QTE");
        }

        gameObject.SetActive(false);
    }

    void MissQTE()
    {
        Debug.Log("Miss QTE");
        gameObject.SetActive(false);
    }
}
