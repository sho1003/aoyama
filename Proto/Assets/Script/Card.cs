using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float timeleft;
    public bool flag;
    public bool Openflag;



    // Use this for initialization
    void Start()
    {
        timeleft = 0.0f;
        flag = false;
        Openflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == true) timeleft -= Time.deltaTime;

        Renderer ren = gameObject.GetComponent<Renderer>();

/////////////////////
       /* if (Input.GetMouseButtonDown(1))
        {
            flag = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            flag = false;
        }*/
/////////////////////////

        //カードに接触状態でタイムが3秒経過でカードが変わる
        if (Openflag == false && timeleft <= -3.0f)
        {
            ren.enabled = true;
            if (gameObject.tag == "ura") ren.enabled = false;
            

            //カードが変わるとタイムを止める
            flag = false;
            Openflag = true;

            if (gameObject.tag == "spade") Score.Spade += 1;
            if (gameObject.tag == "haret") Score.Haret += 1;
            if (gameObject.tag == "clover") Score.Clover += 1;
            if (gameObject.tag == "dia") Score.Dia += 1;
            if (gameObject.tag == "hosi") Score.Hosi += 1;
            if (gameObject.tag == "kinoko") Score.Kinoko += 1;
            if (gameObject.tag == "nico") Score.Nico += 1;


        }

    }

    //プレイヤーがカードにぶつかるとタイムが０になり動き出す
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeleft = 0.0f;
            flag = true;
        }

    }

    //プレイヤーがカードから離れるとタイムが止まる
    void OnTriggerExit(Collider other)
    {
        flag = false;

    }

}
