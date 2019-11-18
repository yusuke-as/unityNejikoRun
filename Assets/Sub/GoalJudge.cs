using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalJudge : MonoBehaviour
{
    bool isGoal;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name== "Charactor")
        {
            isGoal = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Charactor")
        {
            isGoal = false;
        }
    }

    private void OnGUI()
    {
        string label = "";
        if (isGoal)
        {
            label = "GOAL!";
        }
        GUI.Label(new Rect(Screen.width/2,Screen.height/2, 100, 100), label);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
