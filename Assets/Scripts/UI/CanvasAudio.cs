using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasAudio : MonoBehaviour
{
    public void OnHover()
    {
        if (GameManager.Instance.isAudioOn)
        {
            AudioManager.Instance.PlayOnHover();
        }
    }
    
}
