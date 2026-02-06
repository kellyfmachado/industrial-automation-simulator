using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionSensor : MonoBehaviour
{
    public FillingMachineController fillingMachineController;
    private float coolDown = 3;
    private List<GameObject> bottlesFilling;
    // Start is called before the first frame update
    void Start()
    {
        bottlesFilling = new List<GameObject>();
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

        if (other.CompareTag("Bottle"))
        {
            bottlesFilling.Add(other.gameObject.transform.Find("Filling").gameObject);
            coolDown--;
            if(coolDown <= 0){
                coolDown = 3;
                if (GameManager.Instance.isIdealSimulation)
                {
                    fillingMachineController.isFollowing = true;
                }
                foreach(GameObject bottle in bottlesFilling){
                    fillingMachineController.bottlesFilling.Add(bottle);
                }
            }

            if (!GameManager.Instance.isIdealSimulation)
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[2] = true;
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
            if(other.CompareTag("Bottle")){
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[2] = false;
            }
        }
        
    }

}
