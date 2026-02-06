using System.Collections.Generic;
using UnityEngine;

public class SealingBottles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isGameOver){
            return;
        }
        if(other.CompareTag("Bottle")){
            other.gameObject.transform.Find("Cap").gameObject.SetActive(true);
        }
    }

}
