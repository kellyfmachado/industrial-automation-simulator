using System.Collections.Generic;
using UnityEngine;

public class SealingPositionSensor : MonoBehaviour
{
    public SealingMachineController sealingMachineController;
    public AudioClip audioOnShot;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                sealingMachineController.isOnTarget = true;
                if (GameManager.Instance.isAudioOn)
                {
                    audioSource.PlayOneShot(audioOnShot);
                }
            }
            else
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[3] = true;
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
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[3] = false;
            }
        }
        
    }

}
