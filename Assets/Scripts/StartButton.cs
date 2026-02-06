using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject buttonRed;
    public GameObject buttonGreen;
    public AudioClip audioOnTurnOn;
    public AudioClip audioOnTurnOff;
    public GameObject arrowPrefab = null;
    [HideInInspector]
    public GameObject arrow = null;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GameManager.Instance.isIdealSimulation){
            arrow = Instantiate(arrowPrefab, arrowPrefab.transform.position, arrowPrefab.transform.rotation);
        }
        ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[6] = false;
    }

    public void StartProcess()
    {
        if(GameManager.Instance.isGameOver == false){

            GameManager.Instance.startProcess = !GameManager.Instance.startProcess;

            if (GameManager.Instance.startProcess)
            {
                buttonRed.SetActive(false);
                buttonGreen.SetActive(true);
                if (GameManager.Instance.isAudioOn)
                {
                    audioSource.PlayOneShot(audioOnTurnOn);
                }
                if (arrow != null)
                {
                    Destroy(arrow);
                    arrow = null;
                }
                
            }
            else
            {
                buttonRed.SetActive(true);
                buttonGreen.SetActive(false);
                if (GameManager.Instance.isAudioOn)
                {
                    audioSource.PlayOneShot(audioOnTurnOff);
                }
            }
        }
    }
}
