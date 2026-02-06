using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBottle : MonoBehaviour
{
    public GameOver gameOverScript;
    public int numBottles = 9;
    public GameObject box;
    public GameObject spawnPoint;
    private Animator boxAnimator;
    private List<GameObject> caps;
    private List<GameObject> fillings;
    private List<GameObject> labels;
    private int count = 0;
    public int countValid = 0;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        boxAnimator = box.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        
        if (GameManager.Instance.isGameOver){
            return;
        }

        if (other.CompareTag("Bottle"))
        {   
            count++;
            if (other.gameObject.transform.Find("Filling").gameObject.activeSelf
            && other.gameObject.transform.Find("Cap").gameObject.activeSelf
            && other.gameObject.transform.Find("Label").gameObject.activeSelf)
            {
                countValid++;
            }
            Destroy(other.gameObject);
            if (count == numBottles)
            {
                count = 0;
                if (GameManager.Instance.isIdealSimulation)
                {
                    gameOverScript.SetGameOver();
                    boxAnimator.SetTrigger("LoadScene");    
                }
                else
                {
                    if (countValid == numBottles && GameManager.Instance.isGameOverFirstTime)
                    {
                        countValid = 0;
                        boxAnimator.SetTrigger("Finish");
                        gameOverScript.SetGameOver();
                    }
                    else if (countValid != numBottles && GameManager.Instance.isGameOverFirstTime)
                    {
                        countValid = 0;
                        gameOverScript.SetGameOverInterface();
                    }
                }
                
            } 
                
        }   
    }
    
}
