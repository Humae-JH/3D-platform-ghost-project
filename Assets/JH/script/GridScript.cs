using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UtilitySpace;

public class GridScript : MonoBehaviour
{
    protected Utils utils;
    Grid grid;
    GameObject goal;
    GoalScript goalScript= null;

    float max_x = 0;
    float min_x = 0;
    float max_z = 0;
    float min_z = 0;

    public float margin = 2.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        utils = new Utils();
        goal = utils.FindChildObject(this.transform.gameObject, "Goal");
        goalScript = goal.GetComponent<GoalScript>();
        initialize();

        Debug.Log("4, 4" + isMovable(new Vector3(4, 0, 4)) + "0, 0" + isMovable(new Vector3(0, 0, 0)));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goalScript.isWin())
        {
            Debug.Log("Win!! ");
        }
    }

    protected void initialize()
    {
        Transform child = null;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            child = this.transform.GetChild(i);
            if (child.tag == "Plane") {
                if (min_x > child.transform.position.x)
                {
                    min_x = child.transform.position.x;
                }
                if (max_x < child.transform.position.x)
                {
                    max_x = child.transform.position.x;
                }
                if (min_z > child.transform.position.z)
                {
                    min_z = child.transform.position.z;
                }
                if (max_z < child.transform.position.z)
                {
                    max_z = child.transform.position.z;
                }
            }
        }
        Debug.Log("[GridScript] Initialize Complete ...");
        Debug.Log("Grid information ... x : " + new Vector2(min_x, max_x) + " z : " + new Vector2(min_z, max_z));
    }

    public bool isMovable(Vector3 position)
    {
        float x = position.x;
        float z = position.z;

        if ( x < max_x+margin & x > min_x-margin & z < max_z+margin & z > min_z-margin)
        {
            return true;
        }
        return false;
    }
}
