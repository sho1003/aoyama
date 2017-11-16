using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove_script : MonoBehaviour {


    public float speed = 10f;
    Vector3 move;

    public GameObject point;



    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        move = point.gameObject.transform.position - transform.position;
        //Debug.Log(move);
        transform.Translate(move * speed * Time.deltaTime * 3);

    }

}
