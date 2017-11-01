using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZinScript : MonoBehaviour
{
    public bool Openflag;
    public Texture[] textures;
    private int frameNum;
    public float timeleft;
    public bool timeflag;
    public bool VSflag;
    public bool Redflag;
    public bool Blueflag;
    public int Colorflag;// Colorflag [1]青　[2]赤

    public int BlueGage;
    public int RedGage;


    // Use this for initialization
    void Start()
    {
        frameNum = 0;    // frameNum[0]占拠なし　[1]青　[2]赤
        timeleft = 0.0f;
        VSflag = false;//戦闘しているかのフラグ
        Openflag = true;

        Colorflag = 0;
        BlueGage = 100;
        RedGage = -100;

    }

    // Update is called once per frame
    void Update()
    {


        Renderer ren = gameObject.GetComponent<Renderer>();

        /////  //if (timeflag == true) timeleft -= Time.deltaTime;
        //    if (VSflag == true) timeleft = 0.0f;//戦闘中はタイムリセットストップ

        if (VSflag==false)
        {
            if (Blueflag == true) timeleft -= Time.deltaTime;
            if (Redflag == true) timeleft += Time.deltaTime;
        }
        //    if (VSflag == true) timeflag = false;//戦闘中はタイムストップ

        //  if (Openflag == true && timeleft <= -3.0f && VSflag == false)
        if (timeleft < -3.0f)
        {
            timeleft = -3.0f;
            frameNum = 1;
        }

        if (timeleft > 3.0f)
        {
            timeleft = 3.0f;
            frameNum = 2;
        }

        {
           //if (Colorflag == 1/*青*/) frameNum = 1;
          //  if (Colorflag == 2/*赤*/) frameNum = 2;
            ///////   timeflag = false;
        }
        //スコアスクリプトを作る
        //if (Openflag == true && frameNum == 1) ScoreScript.RedScore += 1;
        //if (Openflag == true && frameNum == 2) ScoreScript.BlueScore += 1;


        ren.material.SetTexture("_MainTex", textures[frameNum]);

        if (Redflag == true && Blueflag == true)
        {
            VSflag = true;
        }
        else VSflag = false;

    }

    void OnCollisionStay(Collision other)
    //        void OnTriggerStay(Collider other)
    {

        //   timeflag = true;

        if (other.gameObject.tag == /*"ao"*/"Enemy")
        {
            Colorflag = 1;
            Blueflag = true;
        }

        else Blueflag = false;


        if (other.gameObject.tag == /*"aka"*/"Player")
        {
            Colorflag = 2;
            Redflag = true;
        }

        else Redflag = false;


    }


}
