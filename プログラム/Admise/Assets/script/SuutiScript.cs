﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuutiScript : MonoBehaviour
{

    private player1_script player1;
    private player2_script player2;
    public GameObject target;
    private int suuti;
    public Text Suutitext;
    public GameObject tuibi;
    public static int Number;
    public static int i2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = tuibi.transform.position;


        if (target.gameObject.tag == "Player1")
        {
            player1 = GameObject.Find(target.gameObject.transform.name).GetComponent<player1_script>();
            //  HP -= player.PlayerATK;//攻撃で体力が減少

            suuti = player1.Number;
            Suutitext.text = "" + suuti;

            ///  Number = player.Number;
            /// i2 = player.i2;
        }
        //   Debug.Log(i2);


        if (Number > i2)
        {

            Suutitext.enabled = true;
        }

        if (Number < i2)
        {

              Suutitext.enabled = false;

        }

        if(player1_script.SuutiByougaflag == true) Suutitext.enabled = true;
    }
}

