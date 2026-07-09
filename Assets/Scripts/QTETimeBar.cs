using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Yarn.Unity;

[RequireComponent(typeof(Slider))]
public class QTETimeBar : MonoBehaviour
{
    [SerializeField] InputActionReference qteLeftAction;
    [SerializeField] InputActionReference qteRightAction;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] Slider slider;
    [SerializeField] float speed = 1f;
    [SerializeField] Image backgroundImage;
    [SerializeField] float duration = 2f;
    [SerializeField] float maxTimeOutOfSafeZone = 1f;

    float timeElapsed = 0f;
    Vector2 safeZoneValue = new(0.4f, 0.6f);
    float currentTimeOutOfSafeZone = 0f;
    Color safeColor;

    void Awake()
    {
        safeColor = backgroundImage.color;

        // dialogueRunner.AddCommandHandler<float, float, float>(
        //     "qte_timebar",
        //     TurnOn
        // );
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDuration();
        UpdateSliderPosition();
        CalculateOutOfSafeZone();
    }

    void UpdateSliderPosition()
    {
        slider.value += speed * Time.deltaTime;

        if (slider.value >= slider.maxValue)
        {
            slider.value = slider.maxValue;
            speed = -Math.Abs(speed);
        }
        else if (slider.value <= slider.minValue)
        {
            slider.value = slider.minValue;
            speed = Math.Abs(speed);
        }
    }

    void CalculateOutOfSafeZone()
    {
        if (slider.value <= safeZoneValue.x || slider.value >= safeZoneValue.y)
        {
            currentTimeOutOfSafeZone += Time.deltaTime;
            float timeElapsed = currentTimeOutOfSafeZone / maxTimeOutOfSafeZone;
            backgroundImage.color = Color.Lerp(safeColor, Color.red, timeElapsed);

            if (currentTimeOutOfSafeZone >= maxTimeOutOfSafeZone)
            {   
                Debug.Log("Fail!");
                gameObject.SetActive(false);
            }
        } 
    }

    void UpdateDuration()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= duration)
        {
            Debug.Log("Pass!");
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        qteLeftAction.action.Enable();
        qteRightAction.action.Enable();

        qteLeftAction.action.started += TurnSliderLeft;
        qteRightAction.action.started += TurnSliderRight;

        // Reset all states
        timeElapsed = 0f;
        currentTimeOutOfSafeZone = 0f;
        slider.value = 0.5f;
        backgroundImage.color = safeColor;
    }

    void OnDisable()
    {
        qteLeftAction.action.Disable();
        qteRightAction.action.Disable();

        qteLeftAction.action.started -= TurnSliderLeft;
        qteRightAction.action.started -= TurnSliderRight;
    }

    public void TurnOn(float speed = 0.5f, float duration = 2f, float maxTimeOutOfSafeZone = 1f)
    {
        this.speed = speed;
        this.duration = duration;
        this.maxTimeOutOfSafeZone = maxTimeOutOfSafeZone;
        gameObject.SetActive(true);
    }

    void TurnSliderLeft(InputAction.CallbackContext callback)
    {
        speed = -Math.Abs(speed);
    }

    void TurnSliderRight(InputAction.CallbackContext callback)
    {
        speed = Math.Abs(speed);
    }
}