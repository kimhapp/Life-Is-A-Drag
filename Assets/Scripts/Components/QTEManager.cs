using System.Collections;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [SerializeField] QTEButton qteButton;
    [SerializeField] QTETimeBar qteTimeBar;

    bool isStarting = false;

    // Update is called once per frame
    void Update()
    {
        if (!qteButton.gameObject.activeInHierarchy && !qteTimeBar.gameObject.activeInHierarchy)
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

        int randomNum = Random.Range(1, 3);
        switch (randomNum)
        {
            case 1: 
                Debug.Log("Spawning button soon!");
                break;
            case 2:
                Debug.Log("Spawning timebar soon!");
                break;

        }
        
        yield return new WaitForSeconds(3f);

        switch (randomNum)
        {
            case 1: 
                qteButton.TurnOn();
                break;
            case 2:
                qteTimeBar.TurnOn();
                break;
        }
        
        isStarting = false;
    }
}
