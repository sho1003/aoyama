using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_script : MonoBehaviour {

    //private int frame = 0;
    //private float time = 0.0f;

    ////対象のオブジェクト
    //private GameObject desObj;
    //private bool isRespon = false;
    //private Vector3 initPos;



    //// Use this for initialization 
    //void Start()
    //{
    //    //初期値設定 
    //    this.initPos = Vector3.zero;


    //}
    //// Update is called once per frame 
    //void Update()
    //{
    //    if (isRespon)
    //    {
    //        time += Time.deltaTime;
    //        if (time >= 3.0f)
    //        {
    //            time = 0.0f;
    //            isRespon = false;
    //            desObj.SetActive(true);
    //            Debug.Log("a");
    //            desObj.transform.position = initPos;
    //        }
    //    }


    //}
    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        desObj.SetActive(false);
    //        isRespon = true;
    //    }
    //}

    //public void RespawnObject(GameObject obj)
    //{
    //    desObj = obj;
    //    //desObj.SetActive(false);
    //    isRespon = true;
    //}

    private Vector3 initPos;

     GameObject Sphere;

    private player_script player;

    // Use this for initialization
    void Start()
    {
        //初期値設定 
        this.initPos = Vector3.zero;


        Sphere = GameObject.Find("Cube");
        player = Sphere.GetComponent<player_script>();

    }

    // Update is called once per frame
    void Update()
    {

        if (player.HP<0)
        {
            Sphere.SetActive(false);
            transform.position = initPos;
            player.HP = 101;
        }

        if (player.HP==101)
        {
            player.HP =- 1;
            Sphere.SetActive(true);

        }

    }
}
