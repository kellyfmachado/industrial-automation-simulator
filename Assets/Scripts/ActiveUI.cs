using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveUI : MonoBehaviour
{
    private Animator boxAnimator;
    public GameObject winUI;
    void Start()
    {
        boxAnimator = winUI.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateUI()
    {
        winUI.SetActive(true);
    }

    public void ActivateBoxUI()
    {
        boxAnimator.SetTrigger("Rotate");
        gameObject.SetActive(false);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("HomeInterface");
    }
        
}
