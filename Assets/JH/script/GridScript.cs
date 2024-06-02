using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UtilitySpace;

public class GridScript : MonoBehaviour
{
    protected Utils utils;
    Grid grid;
    Object goal;
    GoalScript goalScript= null;

    // Start is called before the first frame update
    void Start()
    {
        utils = new Utils();
        goal = utils.FindChildObject(this.transform.gameObject, "Goal");
        goalScript = goal.GetComponent<GoalScript>();
        Debug.Log("goal : " + goal.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goalScript.isWin())
        {
            Debug.Log("Win!! ");
        }
    }

}
