using System.Collections;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] QTEButton qteButton;

    bool isStarting = false;

    // Update is called once per frame
    void Update()
    {
        if (!qteButton.gameObject.activeInHierarchy)
        {
            if (!isStarting)
            {
                StartCoroutine(StartQTE());
            }
        }
    }

    IEnumerator StartQTE()
    {
        isStarting = true;
        Debug.Log("Spawning soon!");
        yield return new WaitForSeconds(3f);
        qteButton.gameObject.SetActive(true);
        isStarting = false;
    }
}
