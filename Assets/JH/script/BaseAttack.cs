using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    protected BoxCollider attackCollider;
    public int damage = 1;
    protected Collider target;
    protected GameObject[] colliderObject;
    public int colliderCnt = 1;
    // Start is called before the first frame update
    void Start()
    {
        colliderObject = new GameObject[colliderCnt];
        for (int i = 0; i < colliderCnt; i++)
        {
            colliderObject[i] = new GameObject("collider");
            colliderObject[i].AddComponent<BoxCollider>();
            colliderObject[i].GetComponent<BoxCollider>().enabled = true;
            colliderObject[i].GetComponent<BoxCollider>().isTrigger = true;
            colliderObject[i].transform.parent = gameObject.transform;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAttackCollider(Vector3 coordinates)
    {
        colliderObject[0].GetComponent<BoxCollider>().transform.position = coordinates;
    }

    public Vector3 getAttackCollider()
    {
        return colliderObject[0].GetComponent<BoxCollider>().transform.position;
    }

    public virtual void Attack()
    {
        Debug.Log("target collision : " + target);
        target.GetComponent<ActorScript>().Damaged();
    }

    private void OnTriggerStay(Collider collision) 
    {


        if (collision.gameObject.tag == "Player")
        {

        }
        else if (collision.gameObject.tag == "Enemy")
        {

        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("collision object : " + collision.transform.name);
            target = collision;
            //collision.gameObject.GetComponent<ActorScript>().damaged();
        }
    }
}
