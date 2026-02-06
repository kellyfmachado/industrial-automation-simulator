using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingValidation : MonoBehaviour
{
    public GameOver gameOver;
    private int coutingBottles = 0;
    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.isGameOver){
            return;
        }

        if (other.CompareTag("Bottle") && !GameManager.Instance.isIdealSimulation)
        {
            coutingBottles++;
            if (coutingBottles > 9 && GameManager.Instance.isGameOverFirstTime)
            {
                coutingBottles = 0;
                gameOver.SetGameOverInterface();
            }
        }
    }
}
