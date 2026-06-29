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

    Slider slider;
    Vector2 safezoneMinMax = new(0.45f, 0.55f);

    void Awake()
    {
        qteLeftAction.action.Enable();
        qteRightAction.action.Enable();

        qteLeftAction.action.started += TurnSliderLeft;
        qteRightAction.action.started += TurnSliderRight;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();

        // Debug enabled state
        Debug.Log($"Move action enabled: {qteLeftAction.action.enabled}");
        Debug.Log($"Interact action enabled: {qteRightAction.action.enabled}");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value += direction * speed * Time.deltaTime;

        if (slider.value >= slider.maxValue)
        {
            direction = -1f;
        }
        else if (slider.value <= slider.minValue)
        {
            direction = 1f;
        }

        if (slider.value >= safezoneMinMax.x && slider.value <= safezoneMinMax.y)
        {
            Debug.Log("In safezone!");
        }
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