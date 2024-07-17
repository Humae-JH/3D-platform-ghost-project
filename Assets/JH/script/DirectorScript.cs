using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DirectorScript : MonoBehaviour
{
    public GameObject player;
    protected PuppetScript playerScript;
    public GameObject grid;
    protected GridScript gridScript;
    protected float oneStep;

    // Start is called before the first frame update
    void Start()
    {
        gridScript = grid.GetComponent<GridScript>();
        playerScript = player.GetComponent<PuppetScript>();
        oneStep = playerScript.oneStep;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
    }

    protected void getInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 newPosition = playerScript.getTargetPosition() + (player.transform.forward * oneStep);
            if (gridScript.isMovable(newPosition))
            {
                playerScript.setTargetPosition(newPosition);
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 newPosition = playerScript.getTargetPosition() + (player.transform.forward * oneStep * -1);
            if (gridScript.isMovable(newPosition))
            {
                playerScript.setTargetPosition(newPosition);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float angle = -90.0f;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            playerScript.setTargetDirection(rotation * playerScript.getTargetDirection());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float angle = 90.0f;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            playerScript.setTargetDirection(rotation * playerScript.getTargetDirection());

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerScript.Attack();
        }
    }

}
