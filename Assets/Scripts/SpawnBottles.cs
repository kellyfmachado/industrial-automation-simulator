using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBottles : MonoBehaviour
{
    public int quantity = 1;
    public float interval = 2;
    public GameObject bottle;
    public GameObject spawnPoint;

    [HideInInspector]
    public bool supplyActuator;
    [HideInInspector]
    public int coutingBottles = 0;
    private float cooldown; 
    // Start is called before the first frame update
    void Start()
    {
        cooldown = interval;
        supplyActuator = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver){
            supplyActuator = false;
            return;
        }
        
        if (GameManager.Instance.isIdealSimulation) {
            if (GameManager.Instance.startProcess)
            {
                if (coutingBottles == 9)
                {
                    supplyActuator = false;
                }
                else
                {
                    supplyActuator = true;
                }
            }
            else
            {
                supplyActuator = false;
            }
        }
        else {
            supplyActuator = ModbusServerUnity.InstanceModbus.modbusServer.coils[1];
        }

        if(supplyActuator){
            if((cooldown -= Time.deltaTime) <= 0f && quantity > 0){
                Instantiate(bottle, spawnPoint.transform.position, bottle.transform.rotation);
                cooldown = interval;
                quantity--;
            }
        }

    }
}
