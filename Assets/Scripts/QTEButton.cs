using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class QTEButton : MonoBehaviour
{
    [SerializeField] InputActionReference qteButtonAction;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip animationClip;

    float timeElapsed = 0f;
    float length = 0f;

    void Awake()
    {
        qteButtonAction.action.Enable();

        qteButtonAction.action.started += PressQTE;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        Debug.Log($"time: {timeElapsed}");

        if (timeElapsed >= length + .5f)
        {
            MissQTE();
        }   
    }

    void OnEnable()
    {
        animator.SetTrigger("OnEnable");
        length = animationClip.length;
        timeElapsed = 0f;
    }

    void PressQTE(InputAction.CallbackContext callback)
    {
        if (timeElapsed < length)
        {
            Debug.Log("Too early!");
        } else
        {
            Debug.Log("Pressed QTE");
        }
    }

    void MissQTE()
    {
        Debug.Log("Miss QTE");
    }
}
