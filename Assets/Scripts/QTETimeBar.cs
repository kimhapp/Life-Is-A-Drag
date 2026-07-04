using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class QTETimeBar : MonoBehaviour
{
    [SerializeField] InputActionReference qteLeftAction;
    [SerializeField] InputActionReference qteRightAction;
    [SerializeField] float direction = 1f;
    [SerializeField] float speed = 1f;
    [SerializeField] Image backgroundImage;

    Slider slider;
    Vector2 safeZoneValue = new(.4f, .6f);
    float maxTimeOutOfSafeZone = 2.0f;
    float currentTimeOutOfSafeZone = 0.0f;
    Color safeColor;

    void Awake()
    {

        safeColor = backgroundImage.color;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliderPosition();
        CalculateOutOfSafeZone();
    }

    void UpdateSliderPosition()
    {
        slider.value += direction * speed * Time.deltaTime;

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
                gameObject.SetActive(false);
            }
        } 
    }

    void OnEnable()
    {
        qteLeftAction.action.Enable();
        qteRightAction.action.Enable();

        qteLeftAction.action.started += TurnSliderLeft;
        qteRightAction.action.started += TurnSliderRight;
    }

    void OnDisable()
    {
        qteLeftAction.action.Disable();
        qteRightAction.action.Disable();

        qteLeftAction.action.started -= TurnSliderLeft;
        qteRightAction.action.started -= TurnSliderRight;
    }

    void TurnSliderLeft(InputAction.CallbackContext callback)
    {
        Debug.Log("left!");
        direction = -1f;
    }

    void TurnSliderRight(InputAction.CallbackContext callback)
    {
        Debug.Log("right!");
        direction = 1f;
    }
}