using Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ActorScript : MonoBehaviour
{

    public float oneStep = 2.0f;
    protected Vector3 targetPosition;
    protected Vector3 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        ControlMotion();

        // Update movement
        // Control movement through targetPosition and targetDirection

        UpdateMotionState();
    }

    protected virtual void Initialize()
    {
        targetPosition = new Vector3(0, 0.5f, 0);
        targetDirection = new Vector3(0, 0, 1);
    }

    protected virtual void ControlMotion()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition = transform.position + (transform.forward * oneStep);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition = transform.position + (transform.forward * oneStep * -1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            float angle = -90.0f;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            targetDirection = rotation * targetDirection;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            float angle = 90.0f;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            targetDirection = rotation * targetDirection;

        }


        return;
    }
    
    protected virtual void UpdateMotionState()
    {
        Move(targetPosition);
        Turn(targetDirection);
    }

    protected virtual void Move(Vector3 position)
    {
        if (transform.position != position)
        {
            float step = oneStep * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, position, step);
        }
        return;
    }

    protected virtual void Turn(Vector3 targetDirection)
    {
        if (transform.forward != targetDirection)
        {
            // rotate toward direction
            // Determine which direction to rotate towards
            // Vector3 targetVector = targetDirection - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = oneStep * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
    }

    protected virtual void attack()
    {
        // create collider for Attack collision
    }
}
