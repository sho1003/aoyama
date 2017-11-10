﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZinScript : MonoBehaviour
{
    public bool Openflag;
    public Texture[] textures;
    public int frameNum;
    public float timeleft;
    public bool timeflag;
    public bool VSflag;
    public bool Redflag;
    public bool Blueflag;
    public int Colorflag;// Colorflag [1]青　[2]赤
    public static int BlueScore;
    public static int RedScore;

    public bool RedSenkyoflag;
    public bool BlueSenkyoflag;
    public bool Senkyoflag;


    // Use this for initialization
    void Start()
    {
        frameNum = 0;    // frameNum[0]占拠なし　[1]青　[2]赤
        timeleft = 0.0f;
        VSflag = false;//戦闘しているかのフラグ
        Openflag = true;

        Colorflag = 0;
        //   BlueGage = 100;
        // RedGage = -100;
        RedScore = 0;
        BlueScore = 0;
        RedSenkyoflag = false;
        BlueSenkyoflag = false;


    }

    // Update is called once per frame
    void Update()
    {


        Renderer ren = gameObject.GetComponent<Renderer>();

        /////  //if (timeflag == true) timeleft -= Time.deltaTime;
        //    if (VSflag == true) timeleft = 0.0f;//戦闘中はタイムリセットストップ

        if (Redflag == true && Blueflag == true)
        {
            VSflag = true;
        }
        else VSflag = false;


        if (VSflag == false)
        {
            if (Blueflag == true && timeleft > -3.0f) timeleft -= Time.deltaTime;
            if (Redflag == true && timeleft < 3.0f) timeleft += Time.deltaTime;
        }
        //  Debug.Log(timeleft);
        //    if (VSflag == true) timeflag = false;//戦闘中はタイムストップ

        //  if (Openflag == true && timeleft <= -3.0f && VSflag == false)



        if (BlueSenkyoflag == false && timeleft < -3.0f)
        {
            frameNum = 1;
            BlueScore += 1;
            if (RedSenkyoflag == true) RedScore -= 1;
            BlueSenkyoflag = true;
            RedSenkyoflag = false;
        }

        if (RedSenkyoflag == false && timeleft > 3.0f)
        {
            frameNum = 2;
            RedScore += 1;
            if (BlueSenkyoflag == true) BlueScore -= 1;
            RedSenkyoflag = true;
            BlueSenkyoflag = false;

        }

        ren.material.SetTexture("_MainTex", textures[frameNum]);

    }

    // void OnCollisionStay(Collision other)
    void OnTriggerStay(Collider other)
    {

        //   timeflag = true;

        //　エネミーが乗っている時の判定
        if (other.gameObject.tag == /*"ao"*/"Enemy")
        {
            //  Colorflag = 1;
            Blueflag = true;
        }
    
 

        //　プレイヤーが乗っている時の判定
        if (other.gameObject.tag == /*"aka"*/"Player")
        {
            // Colorflag = 2;
            Redflag = true;
        }
      
        

    }



    // void OnCollisionExit(Collision other)
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == /*"ao"*/"Enemy") Blueflag = false;
        if (other.gameObject.tag == /*"aka"*/"Player") Redflag = false;
    }

}
