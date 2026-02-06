using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public float conveyorBeltSpeed = 0.4f;
    public float curvedConveyorBeltSpeed = 0.5f;
    public bool startProcess;
    public bool isIdealSimulation;
    [HideInInspector]
    public bool isGameOver = false;
    [HideInInspector]
    public bool isGameOverFirstTime = true;
    [HideInInspector]
    public bool isAudioOn = true;
    void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startProcess = false;
    }

    // Update is called once per frame
    void Update()
    {
        ModbusServerUnity.InstanceModbus.modbusServer.coils[3] = startProcess;
    }

    public void StartIdealSimulation()
    {
        isIdealSimulation = true;
        isGameOverFirstTime = true;
        isGameOver = false;
        SceneManager.LoadScene("Simulation");
    }

    public void StartRealSimulation()
    {
        isIdealSimulation = false;
        isGameOverFirstTime = true;
        isGameOver = false;
        SceneManager.LoadScene("Simulation");
    }
}
