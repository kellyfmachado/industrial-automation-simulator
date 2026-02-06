    using UnityEngine;
    using EasyModbus; 
    using System.Net;

    public class ModbusServerUnity : MonoBehaviour {

        public ModbusServer modbusServer = new ModbusServer();
        public static ModbusServerUnity InstanceModbus {get; private set;}
        // private int coolDown = 100;

        void Awake() {
            if(InstanceModbus != null && InstanceModbus != this) {
                Destroy(this);
            } else {
                InstanceModbus = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start() {
            try {
                modbusServer.Port = 1502;
                modbusServer.LocalIPAddress = IPAddress.Any;
                modbusServer.Listen();         
            }
            catch (System.Exception e) {
                Debug.LogError("Erro ao iniciar o servidor Modbus: " + e.Message);
            }
        }

        public void ResetVariables()
        {
            modbusServer.coils[1] = false;
            modbusServer.coils[2] = false;
            modbusServer.coils[3] = false;
            modbusServer.discreteInputs[1] = false;
            modbusServer.discreteInputs[2] = false;
            modbusServer.discreteInputs[3] = false;
            modbusServer.discreteInputs[4] = false;
            modbusServer.discreteInputs[5] = false;
            // modbusServer.discreteInputs[6] = true;
            modbusServer.holdingRegisters[1] = 0;
            modbusServer.holdingRegisters[2] = 0;
            modbusServer.holdingRegisters[3] = 0;
        }

        private void OnApplicationQuit() {
            ResetVariables();
            modbusServer.StopListening();
        }

    
    }
