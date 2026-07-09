using System.Collections;
using UnityEngine;

public class ResultText : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
