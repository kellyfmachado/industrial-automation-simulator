using System;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using UnityEngine;

public class FillingMachineController : MonoBehaviour
{
    [Header("Objects")]
    public GameObject fillingNozzlesStructure;
    public List<GameObject> fillingNozzles;
    public GameObject machineGreenLed;
    public GameObject machineRedLed;
    public ConveyorBeltController belt;
    [HideInInspector]
    public List<GameObject> bottlesFilling;

    [Header("Parameters")]
    public float speed = 10f;
    
    [Header("Logic Variables")]
    [HideInInspector]
    public bool isActive;
    // [HideInInspector]
    public bool isFollowing;
    // [HideInInspector]
    public bool isReturning;
    [HideInInspector]
    public bool isOnTarget;

    [Header("Effects")]
    public GameObject juiceVFX;
    private GameObject juiceVFXObject;
    private List<GameObject> juiceVFXObjects;
    public AudioClip audioOnFilling;
    private bool isFirstPlaySound = true;
    private AudioSource audioSource;
    // [Header("ModBus Helper")]
    // Start is called before the first frame update
    void Start()
    {   
        audioSource = GetComponent<AudioSource>();
        bottlesFilling = new List<GameObject>();
        juiceVFXObjects = new List<GameObject>();
        isOnTarget = false;
        ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[1] = 0;
    }

    // Update is called once per frame
    void Update(){

        if (GameManager.Instance.isGameOver){
            audioSource.Stop();
            return;
        }

        if (!GameManager.Instance.isIdealSimulation)
        {
            if (ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[1] == 3)
            {
                isActive = true;
            }
            else if (ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[1] == 11)
            {
                isFollowing = true;
            }
        }

        if (isActive)
        {
            if (isFirstPlaySound && GameManager.Instance.isAudioOn){
                audioSource.Play();
                isFirstPlaySound = false;
            }
            InitialPosition();
        }
        if(isFollowing){
            if (isFirstPlaySound && GameManager.Instance.isAudioOn){
                audioSource.Play();
                audioSource.PlayOneShot(audioOnFilling);
                isFirstPlaySound = false;
            }
            FollowingBottles();
        }
        if(isReturning){
            ReturnPositionY();
        }

    }

    public void InitialPosition(){

        if (fillingNozzlesStructure != null)
        {
            var targetPosition = new Vector3(0,0,0.18f);

            if (fillingNozzlesStructure.transform.localPosition.z >= targetPosition.z) {
                isActive = false;
                audioSource.Stop();
                isFirstPlaySound = true;
                ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[1] = 0;
                machineGreenLed.SetActive(false);
                machineRedLed.SetActive(true);
                return;
            }

            if(machineGreenLed != null && machineRedLed != null){
                machineGreenLed.SetActive(true);
                machineRedLed.SetActive(false);
            }
   
            fillingNozzlesStructure.transform.localPosition = new Vector3(0, 0, fillingNozzlesStructure.transform.localPosition.z+(speed * Time.deltaTime));

        }

    }

    public void FollowingBottles(){

        if(machineGreenLed != null && machineRedLed != null){
            machineGreenLed.SetActive(true);
            machineRedLed.SetActive(false);
        }

        if(!isOnTarget){
            foreach (GameObject obj in fillingNozzles)
            {
                if (obj != null)
                {

                    var targetPosition = new Vector3(obj.transform.localPosition.x,-0.145f,obj.transform.localPosition.z);
                    obj.transform.localPosition = new Vector3(0, obj.transform.localPosition.y-(speed * Time.deltaTime), obj.transform.localPosition.z); 

                    if (obj.transform.localPosition.y <= targetPosition.y) {
                        isOnTarget = true; 
                        juiceVFXObject = Instantiate(juiceVFX);
                        juiceVFXObject.transform.SetParent(obj.transform,false);
                        juiceVFXObject.transform.localPosition = new Vector3(obj.transform.localPosition.x,1.275f,obj.transform.localPosition.z*1.35f);
                        juiceVFXObjects.Add(juiceVFXObject);
                    }

                }
            }
        }

        if(isOnTarget){
            FillingBottles();
        }
        
        if (fillingNozzlesStructure != null)
        {

            var targetPosition = new Vector3(fillingNozzlesStructure.transform.localPosition.x,fillingNozzlesStructure.transform.localPosition.y,-0.18f);
            fillingNozzlesStructure.transform.localPosition = new Vector3(0, 0, fillingNozzlesStructure.transform.localPosition.z-(belt.GetCurrentSpeed()*Time.deltaTime));

            if (fillingNozzlesStructure.transform.localPosition.z <= targetPosition.z) {
                ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[1] = 0;
                isFollowing = false;
                audioSource.Stop();
                isFirstPlaySound = true;
                machineGreenLed.SetActive(false);
                machineRedLed.SetActive(true); 
            } else if (fillingNozzlesStructure.transform.localPosition.z <= targetPosition.z+0.04){
                isReturning = true;
            }

        }

    }

    public void ReturnPositionY(){

        RemoveBottles();

        if(machineGreenLed != null && machineRedLed != null){
            machineGreenLed.SetActive(true);
            machineRedLed.SetActive(false);
        }

        if(isOnTarget){
            foreach (GameObject obj in fillingNozzles)
            {
                if (obj != null)
                {

                    var targetPosition = new Vector3(obj.transform.localPosition.x,0,obj.transform.localPosition.z);
                    obj.transform.localPosition = new Vector3(0, obj.transform.localPosition.y+(speed * Time.deltaTime), obj.transform.localPosition.z); 

                    if (obj.transform.localPosition.y >= targetPosition.y) {
                        machineGreenLed.SetActive(false);
                        machineRedLed.SetActive(true);
                        isOnTarget = false;
                        isReturning = false;
                        isActive = true;
                    }

                }
            }
        }

    }

    public void FillingBottles(){

        foreach (GameObject bottle in bottlesFilling)
        {
            if (bottle != null)
            {   
                if(bottle.activeSelf == false){
                    bottle.SetActive(true);
                }

                Vector3 scale = bottle.transform.localScale;
                scale.y += 1f * Time.deltaTime; 

                if(bottle.transform.localScale.y <= 1){
                    bottle.transform.localScale = scale;
                }

            }
        }

    }

    public void RemoveBottles()
    {
        for (int i = bottlesFilling.Count - 1; i >= 0; i--)
        {
            bottlesFilling.RemoveAt(i);
        }

        if(juiceVFXObjects != null){
            foreach (GameObject effect in juiceVFXObjects){
                Destroy(effect);
            }
        }

    }

}
