
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class QTEButton : MonoBehaviour
{
    [SerializeField] InputActionReference qteButtonAction;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip animationClip;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] float speed = 1f;
    
    float timeElapsed = 0f;
    float length = 0f;

    void Awake()
    {
        // dialogueRunner.AddCommandHandler<float>(
        //     "qte_button",
        //     TurnOn
        // );
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Can be 15% late
        if (timeElapsed >= length * 1.15f)
        {
            MissQTE();
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

    public void TurnOn(float speed = 1f)
    {
        this.speed = speed;
        gameObject.SetActive(true);
    }

    void PressQTE(InputAction.CallbackContext callback)
    {
        // Can be 15% early
        if (timeElapsed < length * 0.85f)
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
