using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameOver gameOverScript;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bottle") && GameManager.Instance.isGameOverFirstTime)
        {
            gameOverScript.SetGameOverInterface();  
        }
    }
}
