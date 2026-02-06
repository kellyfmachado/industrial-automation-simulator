using UnityEngine;
using System.Collections.Generic;

public class LabelingSensor : MonoBehaviour
{
    public List<GameObject> bottlesLabeling;
    private bool isFirstPlaySound = true;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            if (ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[3] == 25)
            {
                foreach(GameObject bottle in bottlesLabeling){
                    bottle.transform.Find("Label").gameObject.SetActive(true);
                }
                ModbusServerUnity.InstanceModbus.modbusServer.holdingRegisters[3] = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isGameOver){
            return;
        }

        if(other.CompareTag("Bottle")){
            if (GameManager.Instance.isIdealSimulation)
            {
                other.gameObject.transform.Find("Label").gameObject.SetActive(true);
            }
            else
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[4] = true;
                bottlesLabeling.Add(other.gameObject);
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
            if (other.CompareTag("Bottle"))
            {
                ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[4] = false;
                bottlesLabeling.Remove(other.gameObject);
            }
        }
    }

}
