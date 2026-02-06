using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonSimulation : MonoBehaviour
{

    public void SetIdealSimulation()
    {
        GameManager.Instance.StartIdealSimulation();
    }

    public void SetRealSimulation()
    {
        GameManager.Instance.StartRealSimulation();
    }

}
