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
    Transform goal;
    GoalScript goalScript= null;
    bool[,] map = null;

    float max_x = 0;
    float min_x = 0;
    float max_z = 0;
    float min_z = 0;

    public float margin = 2.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //utils = new Utils();
        goal = this.transform.Find("Goal");
        goalScript = goal.gameObject.GetComponent<GoalScript>();
        initialize();

        
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
                margin = child.transform.localScale.x * 10;
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

        map = getMap();
    }

    public bool isInGrid(Vector3 position)
    {
        float x = position.x;
        float z = position.z;

        if ( x < max_x+margin - 0.1f & x > min_x-margin +0.1f & z < max_z+margin-0.1f & z > min_z-margin + 0.1f)
        {
            return true;
        }
        return false;
    }



    protected bool[,] getMap()
    {
        int mapZ = Mathf.RoundToInt((max_z - min_z) / margin) + 1;
        int mapX = Mathf.RoundToInt((max_x - min_x) / margin) + 1;

        bool[,] map = new bool[mapX,mapZ];

        Transform obstacleParent = this.gameObject.transform.Find("Obstacles");

        for (int i = 0; i < mapZ; i++) {
            for (int j = 0; j < mapX; j++)
            {
                map[j,i] = true;
            }
        }

        for (int i = 0; i < obstacleParent.childCount; i++)
        {
            Transform child = obstacleParent.GetChild(i);
            int zIndex = getMapZAxisIndex(child.transform.position.z);
            int xIndex =  getMapXAxisIndex(child.transform.position.x);
            map[xIndex, zIndex] = false;

        }

        return map;
    }

    protected int getMapZAxisIndex(float z)
    {
        return Mathf.RoundToInt(z) / Mathf.RoundToInt(margin);
    }

    protected int getMapXAxisIndex(float x)
    {
        return Mathf.RoundToInt(x + max_x) / Mathf.RoundToInt(margin);

    }

    public bool isMovable(Vector3 position)
    {
        bool result = false;
        if (this.isInGrid(position))
        {
            int mapZIndex = getMapZAxisIndex((float)position.z);
            int mapXIndex = getMapXAxisIndex((float)position.x);
            if (map[mapXIndex, mapZIndex])
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }


        return result;
    }

}
