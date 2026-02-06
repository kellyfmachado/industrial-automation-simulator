using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalStartSensor : MonoBehaviour
{
    public FillingMachineController fillingMachineController;
    private bool isFirstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[1] = false;
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
            if (GameManager.Instance.isIdealSimulation)
            {
                fillingMachineController.isActive = true;
                gameObject.SetActive(false);
            }
            else
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[1] = true;
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.isGameOver){
            return;
        }

        if (!GameManager.Instance.isIdealSimulation)
        {
            if (other.CompareTag("Bottle")){
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[1] = false;
            }
        }
    }

}
