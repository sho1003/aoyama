using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy_script : MonoBehaviour {

    public float speed = 0.5f;
    private Transform target;
    Vector3 move = new Vector3(0, 0, 0);



    void Start()
    {
        
        target = GameObject.Find("Cube").transform;

    }

    void Update()
    {

    }

    //stay=接触し続けている間以下を実行
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("発見");
            Chase();
        }
    }

    void Chase()
    {
        move = target.position - gameObject.transform.position;
        //Debug.Log(move);
        transform.Translate(move * speed * Time.deltaTime);
    }


    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        speed = 0;
    //    }
    //}

    //function OnTriggerExit(col : Collider)
    //{
    //    if (col.tag == "Player")
    //    {
    //        Debug.Log("見失う");
    //        moveE.SetState("wait", null);
    //    }
    //}
}
