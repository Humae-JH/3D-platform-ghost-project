using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.U2D.ScriptablePacker;
using static UnityEngine.GraphicsBuffer;

public class FallingRockScript : BaseAttack
{
    [SerializeField]
    protected GameObject[] rocks;
    protected float tmpTime = 0.0f;
    protected float rockMaxHeight = 3.0f;
    protected float rockAvgHeight = 0.0f;
    protected IEnumerator floatCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        initialize();
        base.setAttackCollider(this.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        tmpTime += Time.deltaTime;
        if (tmpTime > 5.0f)
        {
            Attack();
        }
        rockAvgHeight = getRockAvgHeight();
    }

    protected float getRockAvgHeight()
    {
        float tmp = 0.0f;
        for (int i = 0; i < rocks.Length; i++)
        {
            tmp = tmp + rocks[i].transform.position.y;
        }
        return tmp / rocks.Length;
    }

    protected override void initialize()
    {
        base.initialize();
        for(int i = 0;i < rocks.Length; i++)
        {
            rocks[i].GetComponent<Rigidbody>().isKinematic = true;
            rocks[i].GetComponent<Rigidbody>().useGravity = false;
        }
        floatCoroutine = floatingEffect();
        StartCoroutine(floatCoroutine);
    }

    public override void Attack()
    {
        // activate rocks gravity to fall down
        StopCoroutine(floatCoroutine);
        rockFall();

        // check the trigger
        if (target != null)
        {

            Debug.Log("target collision : " + target);
            target.GetComponent<GhostActorScript>().Damaged();
        }
        // stop flowing effect and destroy

        GameObject.Destroy(this.gameObject, 3.0f);

    }


    protected void rockFall()
    {
        for (int i = 0; i < rocks.Length; i++)
        {
            rocks[i].GetComponent<Rigidbody>().useGravity = true;
            rocks[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    public IEnumerator floatingEffect()
    {
        float time = 0.0f;
        while (rockAvgHeight < rockMaxHeight)
        {
            // slowly float the rock
            for (int i = 0; i < rocks.Length; i++)
            {
                Vector3 rockTransform = rocks[i].transform.position;
                rocks[i].transform.position = Vector3.MoveTowards(rockTransform, rockTransform + new Vector3(0, 5 * Time.deltaTime, 0), 1);
            }
            yield return null;
        }
        while (true)
        {
            time += Time.deltaTime;

            // flow around the area
            for (int i = 0; i < rocks.Length; i++)
            {
                Vector3 rockTransform = rocks[i].transform.position;
                rocks[i].transform.position = Vector3.MoveTowards(rockTransform, new Vector3(rockTransform.x, this.rockMaxHeight + Mathf.Sin((time + 2.0f*i/rocks.Length) * Mathf.PI) * 0.5f, rockTransform.z), 1);
                rocks[i].transform.RotateAround(this.gameObject.transform.position + new Vector3(0, this.rockMaxHeight, 0), Vector3.up, 20 * Time.deltaTime);
            }
            yield return null;

        }

         yield break;
    }

    public void OnDestroy() {
        for (int i = 0;i < rocks.Length; i++)
        {
            GameObject.Destroy(rocks[i].gameObject);
        }
        GameObject.Destroy(this.gameObject);
    }
}
