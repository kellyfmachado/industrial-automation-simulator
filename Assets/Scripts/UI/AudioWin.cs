using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioWin : MonoBehaviour
{
    public void OnWin()
    {
        if (!GameManager.Instance.isIdealSimulation && GameManager.Instance.isAudioOn)
        {
            AudioManager.Instance.PlayOnGameWin();
        }
    }
    
}
