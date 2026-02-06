using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCurvedConveyorBelt : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    private bool conveyorBeltMotor;
    private bool isFirstPlaySound = true;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        thisRigidbody = GetComponent<Rigidbody>();
        conveyorBeltMotor = false; 
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver){
            audioSource.Stop();
            return;
        }

        if (GameManager.Instance.isIdealSimulation){
            if (GameManager.Instance.startProcess)
            {
                conveyorBeltMotor = true;
            }
            else{
                conveyorBeltMotor = false;
            }
        }
        else{
            conveyorBeltMotor = ModbusServerUnity.InstanceModbus.modbusServer.coils[2];
        }

        if (conveyorBeltMotor && isFirstPlaySound && audioSource!=null && GameManager.Instance.isAudioOn)
        {
            isFirstPlaySound = false;
            audioSource.Play();
        }
        else if (!conveyorBeltMotor && !isFirstPlaySound && audioSource!=null && GameManager.Instance.isAudioOn)
        {
            isFirstPlaySound = true;
            audioSource.Stop();
        }
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver){
            return;
        }
        
        if(conveyorBeltMotor){
            Quaternion antiClockwiseRotation = Quaternion.Euler(0, - GameManager.Instance.curvedConveyorBeltSpeed * Time.fixedDeltaTime, 0);
            thisRigidbody.rotation *= antiClockwiseRotation;

            Quaternion clockwiseRotation = Quaternion.Euler(0, GameManager.Instance.curvedConveyorBeltSpeed * Time.fixedDeltaTime, 0);
            thisRigidbody.MoveRotation(thisRigidbody.rotation * clockwiseRotation);
        }

    }

}