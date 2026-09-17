using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shellyStats;
    [SerializeField] TextMeshProUGUI marvinStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shellyStats.text = "Like Point: " + PlayerPrefs.GetFloat("Shelly_likePoint") + "\n" +
                            "Positive Point: " + PlayerPrefs.GetFloat("Shelly_positivePoint") + "\n" +
                            "Honest Point: " + PlayerPrefs.GetFloat("Shelly_honestPoint");

        marvinStats.text = "Like Point: " + PlayerPrefs.GetFloat("Marvin_likePoint") + "\n" +
                    "Love Point: " + PlayerPrefs.GetFloat("Marvin_lovePoint") + "\n" +
                    "Quirk Point: " + PlayerPrefs.GetFloat("Marvin_quirkPoint");
    }
}
