using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject canvasGameOver;
    public GameObject buttonRed;
    public GameObject buttonGreen;
    public void SetGameOver()
    {   
        GameManager.Instance.isGameOver = true;
        GameManager.Instance.startProcess = false;
        GameManager.Instance.isIdealSimulation = false;
        GameManager.Instance.isGameOverFirstTime = false;
        ModbusServerUnity.InstanceModbus.ResetVariables();
        ModbusServerUnity.InstanceModbus.modbusServer.discreteInputs[6] = true;
        
        buttonRed.SetActive(true);
        buttonGreen.SetActive(false);
    }

    public void SetGameOverInterface()
    {
        if (GameManager.Instance.isAudioOn)
        {
            AudioManager.Instance.PlayOnGameOver();
        }
        canvasGameOver.SetActive(true);
        SetGameOver();
    }

    public void SetMenuInicial()
    {
        SceneManager.LoadScene("HomeInterface");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
