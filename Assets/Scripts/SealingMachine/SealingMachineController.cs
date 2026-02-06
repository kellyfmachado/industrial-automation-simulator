using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealingMachineController : MonoBehaviour
{
    public GameObject sealingNozzleStructure;
    public float speed = 10f;
    [HideInInspector]
    public bool isOnTarget;
    private bool isOnInitialPosition;
    private bool isSealing;
    private bool isMachineOn;
    private float coolDown = 20;
    public AudioClip audioOnShot;
    private AudioSource audioSource;
    private bool isFirstPlaySound = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isOnTarget=false;
        isOnInitialPosition=true;
        isSealing = false;
        isMachineOn = false;
        ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[2] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver){
            audioSource.Stop();
            return;
        }

        if (GameManager.Instance.startProcess && isFirstPlaySound && audioSource!=null && GameManager.Instance.isAudioOn)
        {
            isFirstPlaySound = false;
            audioSource.Play();
        }
        else if (!GameManager.Instance.startProcess && !isFirstPlaySound && audioSource!=null && GameManager.Instance.isAudioOn)
        {
            isFirstPlaySound = true;
            audioSource.Stop();
        }
        
        if (!GameManager.Instance.isIdealSimulation)
        {
            if (ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[2] == 7 && isMachineOn == false)
            {
                isOnTarget = true;
                if (GameManager.Instance.isAudioOn)
                {
                    audioSource.PlayOneShot(audioOnShot);
                }
                ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[2] = 0;
            }
        }

        SealingBottles();
    }

    public void SealingBottles(){
        if (sealingNozzleStructure != null){
            if(isOnTarget){
                isMachineOn = true;
                var targetPosition = new Vector3(sealingNozzleStructure.transform.localPosition.x,-0.11f,sealingNozzleStructure.transform.localPosition.z);

                if (sealingNozzleStructure.transform.localPosition.y <= targetPosition.y) {
                    isOnTarget = false;
                    isSealing = true;
                }

                sealingNozzleStructure.transform.localPosition = new Vector3(0, sealingNozzleStructure.transform.localPosition.y-(speed * Time.deltaTime), sealingNozzleStructure.transform.localPosition.z);
            }
            if(isSealing){
                coolDown--;
                if(coolDown<=0){
                    coolDown = 20;
                    isSealing = false;
                    isOnInitialPosition = false;
                }
            }
            if(isOnInitialPosition==false){
                var initialPosition = new Vector3(sealingNozzleStructure.transform.localPosition.x,0,sealingNozzleStructure.transform.localPosition.z);
                sealingNozzleStructure.transform.localPosition = new Vector3(0, sealingNozzleStructure.transform.localPosition.y+(speed * Time.deltaTime), sealingNozzleStructure.transform.localPosition.z); 

                if (sealingNozzleStructure.transform.localPosition.y >= initialPosition.y) {
                    isOnInitialPosition = true; 
                    isMachineOn = false;
                }
            }
        }   
    }
}
