using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountigSensor : MonoBehaviour
{
    public SpawnBottles SpawnBottles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (GameManager.Instance.isGameOver){
            return;
        }

        if (other.CompareTag("Bottle"))
        {
            if (GameManager.Instance.isIdealSimulation)
            {
                SpawnBottles.coutingBottles++;
            }
            else
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[5] = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.isGameOver){
            return;
        }
        
        if (!GameManager.Instance.isIdealSimulation)
        {
            if (other.CompareTag("Bottle"))
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[5] = false;
            }
        }
    }

}
