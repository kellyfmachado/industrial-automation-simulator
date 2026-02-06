using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{  
    public bool IsCurved = false;
    private bool conveyorBeltMotor;
    public float currentSpeed;     
    // private float totalMass = 0f;    
    private Rigidbody thisRigidbody;
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
            if (audioSource!=null)
            {
                audioSource.Stop();
            }
            return;
        }

        if (GameManager.Instance.isIdealSimulation)
        {
            if (GameManager.Instance.startProcess)
            {
                conveyorBeltMotor = true;
            }
            else{
                conveyorBeltMotor = false;
            }
        }
        else
        {
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

        var direction = new Vector3(0,0,0);

        if(IsCurved){
            direction = transform.right;
        }
        else{
            direction = transform.forward;
        }

        if (conveyorBeltMotor){
            currentSpeed = GameManager.Instance.conveyorBeltSpeed;

            Vector3 position = thisRigidbody.position;
            thisRigidbody.position += direction * currentSpeed * Time.fixedDeltaTime;
            thisRigidbody.MovePosition(position);
        }

    }

    // void OnCollisionEnter(Collision other)
    // {
    //     Rigidbody otherRigidbody = other.rigidbody;
    //     if (otherRigidbody != null)
    //     {
    //         totalMass += otherRigidbody.mass;
    //     }
    // }

    // void OnCollisionExit(Collision other)
    // {
    //     Rigidbody otherRigidbody = other.rigidbody;
    //     if (otherRigidbody != null)
    //     {
    //         totalMass -= otherRigidbody.mass;
    //         totalMass = Mathf.Max(totalMass, 0f); 
    //     }
    // }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    // public void SetReferenceSpeed(float newSpeed){
    //     referenceSpeed = newSpeed;
    // }
    
}
