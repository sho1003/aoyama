﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player1_script : MonoBehaviour
{
    private StatusScript status;
    public Animator anime;

    public NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;

    public int HP;    //体力
   
    public int PlayerATK = 1;  //プレイヤーの攻撃力
    public int Number;   //キャラクターの記されている数字
    public bool death;
    public float Deathtime;
    public bool FlagTeam;
    public int TeamNumber;

    public bool tasi;

    public List<int> edge;
    public int ID;

   public int i2;//相手側の数値

    public int i3;//相手側の数値保管
    public bool zeroflag;
    public static bool SuutiByougaflag=false;

    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        anime = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        //　キャラ速度の設定
        //　基礎速度からキャラの数字割る２で0.5単位で変化させてる(＋Speedで外部から微調整できる(キャラ全体での調整、キャラ数値で調整したいなら計算式を変更する))
        agent.speed = status.CharBaseSpeed - Number / 2 + status.CharMainteSpeed;

        //    transform.rotation = Quaternion.Euler(0, 180, 0);
        HP = status.CharaHP; //初期体力を最大値にする
        death = false;
        tasi = true;
        zeroflag = false;
        FlagTeam = false;
        edge = new List<int>();
        TeamNumber = 0;

        edge = new List<int>();
    }

    void Update()
    {
        SuutiScript.Number = Number;
        SuutiScript.i2 = i2;

        
        if (HP <= 0)
        {
            anime.SetBool("run", false);
            death = true;
        }
    }



    public void Pointer_Click()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            agent.SetDestination(hit.point);
            anime.SetBool("run", true);
        }
    }
    

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player1")
        {
            SuutiByougaflag = false;

            // Number = Number + player.Number;

         if (tasi == true && zeroflag == false) i2 = Number;
        }



        if (Number >= i2)
        {
            if (tasi == true)
            {
            //    i3 = i2;
             //   Number = Number + i3;
                tasi = false;
                zeroflag = true;
            }
        }
        else if (Number < i2)
        {
            if (tasi == true)
            {
            //    i3 = Number;
               //Number = 0;プレイヤーの順番的にバグる
                tasi = false;
                //player.tasi = false;//相手の方↑でfalseにしてる?
                zeroflag = true;

            }
        }

    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player1")
        {

            //Number = Number - player.Number;
            //   Number = Number - i2;
            zeroflag = false;
            SuutiByougaflag = true;
        }



        if (Number > i2)
        {
   
            if (tasi == false)
            {
              //  Number = Number - i3;
                tasi = true;
               // player1.tasi = false;
                

            }
        }

        else if (Number <= i2)
        {
            if (tasi == false)
            {
              //  Number = i3;
                tasi = true;
                //player.tasi = false;//相手の方↑でfalseにしてる?
              
            }
        }
    }
}

