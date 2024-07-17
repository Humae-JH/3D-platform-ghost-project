using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuppetScript : ActorScript
{

    protected Animator animator;

    protected Rigidbody Rigidbody;
    private static readonly int IdleState = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Run = Animator.StringToHash("Run");
    protected float virticalSpeed = 0.0f;
    protected float horizontalSpeed = 0.0f;
    protected static readonly float idleThreshold = 0.1f;
    protected bool isRun = false;
    protected float prevX = 0.0f;
    protected float prevY = 0.0f;
    protected float prevZ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        prevX = this.gameObject.transform.position.x;
        prevY = this.gameObject.transform.position.y;
        prevZ = this.gameObject.transform.position.z;
        animator = this.gameObject.GetComponent<Animator>();
        Rigidbody = this.gameObject.GetComponent<Rigidbody>();
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = this.gameObject.transform.position.x - prevX;
        float deltaY = this.gameObject.transform.position.y - prevY;
        float deltaZ = this.gameObject.transform.position.z - prevZ;
        virticalSpeed = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.z).magnitude;
        horizontalSpeed = Mathf.Abs(Rigidbody.velocity.y);
        Debug.Log("virtualSpeed" + virticalSpeed + "horizontalSpeed" + horizontalSpeed);
        if ( virticalSpeed > idleThreshold || horizontalSpeed > idleThreshold) {
            changeAnimation(virticalSpeed, horizontalSpeed, isRun);
        }
        UpdateMotionState();
        prevX = this.gameObject.transform.position.x;
        prevY = this.gameObject.transform.position.y;
        prevZ = this.gameObject.transform.position.z;
        Debug.Log("target position : " + this.targetPosition);
    }

    protected void changeAnimation(float virticalSpeed, float horizontalSpeed, bool isRun)
    {
        if (horizontalSpeed > idleThreshold)
        {
            animator.CrossFade(Jump, 1);
        }
        else if (isRun == false && virticalSpeed > idleThreshold)
        {
            animator.CrossFade(Walk, 1);
        }
        else if (isRun == true && virticalSpeed > idleThreshold)
        {
            animator.CrossFade(Run, 1);
        }
        else
        {
            animator.CrossFade(IdleState, 1);
        }
        return;
    }


}
