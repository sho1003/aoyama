using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    //------------------------スクリプト関連------------------------//
    private StatusScript status;
    private GameSE_Script se;

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
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        se = GameObject.Find("Sounds/SE").GetComponent<GameSE_Script>();

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
        BlueSenkyoflag = false;//
        parent = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //どのエリアかの判定をとる
        if (Area_Manager.AreaRandom == true || Area_Manager.Areashuffle == true)
        {
                if (parent.gameObject.tag == "Area1") Area_Manager.Area1 += 1;
                if (parent.gameObject.tag == "Area2") Area_Manager.Area2 += 1;
                if (parent.gameObject.tag == "Area3") Area_Manager.Area3 += 1;
            
        }

       

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
            if (Blueflag == true && timeleft > -status.GetAreaTime) timeleft -= Time.deltaTime;
            if (Redflag == true && timeleft < status.GetAreaTime) timeleft += Time.deltaTime;
        }
        //  Debug.Log(timeleft);
        //    if (VSflag == true) timeflag = false;//戦闘中はタイムストップ

        //  if (Openflag == true && timeleft <= -3.0f && VSflag == false)


        //  蒼組占領
        if (BlueSenkyoflag == false && timeleft < -status.GetAreaTime)
        {
            frameNum = 1;
            BlueScore += 1;
            if (RedSenkyoflag == true) RedScore -= 1;
            BlueSenkyoflag = true;
            RedSenkyoflag = false;
            //  占領時のSE実行
            se.SetSE1(se.areaGetSE);
        }

        //  紅組占領
        if (RedSenkyoflag == false && timeleft > status.GetAreaTime)
        {
            frameNum = 2;
            RedScore += 1;
            if (BlueSenkyoflag == true) BlueScore -= 1;
            RedSenkyoflag = true;
            BlueSenkyoflag = false;
            //  占領時のSE実行
            se.SetSE1(se.areaGetSE);
        }

        ren.material.SetTexture("_MainTex", textures[frameNum]);

    }

    // void OnCollisionStay(Collision other)
    void OnTriggerStay(Collider other)
    {

        //   timeflag = true;

        //　エネミーが乗っている時の判定
        if (other.gameObject.tag == /*"ao"*/"Player2")
        {
            //  Colorflag = 1;
            Blueflag = true;
        }
    
 

        //　プレイヤーが乗っている時の判定
        if (other.gameObject.tag == /*"aka"*/"Player1")
        {
            // Colorflag = 2;
            Redflag = true;
        }
      
        

    }



    // void OnCollisionExit(Collision other)
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == /*"ao"*/"Player2") Blueflag = false;
        if (other.gameObject.tag == /*"aka"*/"Player1") Redflag = false;
    }

}
