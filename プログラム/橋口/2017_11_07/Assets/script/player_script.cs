﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class player_script : NetworkBehaviour {


    private NavMeshAgent agent;
    public bool CheckClick;


    //　レイピック用変数
    private RaycastHit hit;
    private Ray ray;

    //　プレイヤーのパラメータ変数
    public readonly int maxHP = 1;    //体力の最大値
    public int HP;    //体力
    public int a = 1;
    public int PlayerATK = 3;  //敵の攻撃力
    public int Number;   //キャラクターの記されている数字
    public bool death;
    public bool isLocal;
    

    private enemy_script enemy;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        HP = maxHP; //初期体力を最大値にする
        death = false;
        CheckClick = false;
        isLocal = false;
    }

    void Update()
    {
        if (isLocalPlayer)
            return;
    }



    public void Pointer_Click()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f)&& CheckClick)
        {
            agent.SetDestination(hit.point);
        }
    }

    // 
    void OnCollisionStay(Collision collision){


        if (collision.gameObject.tag == "Enemy") {
            enemy = GameObject.Find(collision.gameObject.transform.name).GetComponent<enemy_script>();

            HP -= enemy.EnemyATK;//攻撃で体力が減少
            //Debug.Log(HP); //HPを表示
            
        }
        if (HP < 0)
        {
            death = true;
        }
    }


}

