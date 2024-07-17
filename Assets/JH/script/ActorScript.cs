using Sample;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ActorScript : MonoBehaviour
{

    public float oneStep = 0.0f;
    protected Vector3 targetPosition;
    protected Vector3 targetDirection;
    protected float flickTime = 2.0f;
    protected float timeBuffer;
    protected bool isDamaged = false;
    public int HP = 1;
    public Object Renderer;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // update targetPosition, targetDirection
        //ControlMotion();

        // Update movement
        // Control movement through targetPosition and targetDirection

        UpdateMotionState();
    }

    protected virtual void Initialize()
    {
        targetPosition = gameObject.transform.position;
        targetDirection = gameObject.transform.forward;
        timeBuffer = 0.0f;
        oneStep = 2.0f;
        flickTime = 2.0f;
    }

    public void setTargetPosition(Vector3 newPosition)
    {
        this.targetPosition = newPosition;
    }

    public Vector3 getTargetPosition() {  return targetPosition; }

    public void setTargetDirection(Vector3 newDirection)
    {
        this.targetDirection = newDirection;
    }

    public Vector3 getTargetDirection() { return this.targetDirection; }


    /*protected virtual void ControlMotion()
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
    }*/
    
    protected virtual void UpdateMotionState()
    {
        Move(targetPosition);
        Turn(targetDirection);
        if (isDamaged) { this.Damaged(); }
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

    public virtual void Attack()
    {
        // create collider for Attack collision
    }

    public virtual void Damaged()
    {
        this.isDamaged = true;
        timeBuffer = timeBuffer + Time.deltaTime;

        isDamaged = false;
        this.HP = this.HP - 1;
        if (this.HP <= 0)
        {
            Destroy(gameObject);
        }


    }

    protected virtual void flickering(float timeBuffer)
    {
        // you can override this function for every new objects.
        float frequency = (Mathf.Sin(timeBuffer * Mathf.PI * 2) + 1) / 2;
        gameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(1, 1, 1, frequency);
    }
}
