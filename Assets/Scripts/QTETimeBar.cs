using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class QTETimeBar : MonoBehaviour
{
    Slider slider;

    public float direction = 1f;
    public float speed = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
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
    }
}