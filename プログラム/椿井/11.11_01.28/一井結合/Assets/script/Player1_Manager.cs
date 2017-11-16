﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Manager : MonoBehaviour
{


    //オブジェクトの定義
    public GameObject playerPrefab;

    //定義したオブジェクトの箱をn個作る
    static int PLAYER_MAX = 7;
    GameObject[] playerObj = new GameObject[PLAYER_MAX];
    //player1_scriptを定義できる箱をn個作る
    player1_script[] player = new player1_script[PLAYER_MAX];
    private bool[] FlagDeath = new bool[PLAYER_MAX];
    static int RespawnTime = 60;
    private int[] time = new int[PLAYER_MAX];


    //座標位置を指定する箱をn個作る
    Vector3[] pos = new Vector3[PLAYER_MAX];

    int[] number = new int[PLAYER_MAX];

    // Use this for initialization
    void Start()
    {
        Tubai();
        //初めの座標位置を指定
        pos[0] = new Vector3(10.0f, 0.6f, -15.0f);
        pos[1] = new Vector3(5.0f, 0.6f, -15.0f);
        pos[2] = new Vector3(0.0f, 0.6f, -15.0f);
        pos[3] = new Vector3(-5.0f, 0.6f, -15.0f);
        pos[4] = new Vector3(-10.0f, 0.6f, -15.0f);
        pos[5] = new Vector3(5.0f, 0.6f, -11.0f);
        pos[6] = new Vector3(-5.0f, 0.6f, -11.0f);

        for (int i = 0; i < PLAYER_MAX; i++)
        {
            number[i] = PlayerPrefs.GetInt("PlayerNum" + i);
        }

        for (int i = 0; i < PLAYER_MAX; i++)
        {
            SetPlayer(i);
            time[i] = RespawnTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("椿井２");
        for (int i = 0; i < PLAYER_MAX; i++)
        {
            if (FlagDeath[i] == false)
            {
                // player[i]が止まりかけるとアニメーションrunをfalseにする
                if (player[i].agent.remainingDistance <= player[i].agent.stoppingDistance)
                {
                    player[i].anime.SetBool("run", false);
                }
            }

            if (FlagDeath[i])
            {
                if (time[i] > 0) time[i]--;
                else
                {
                    SetPlayer(i);
                    time[i] = RespawnTime;
                }
            }

            if (player[i].death)
            {
                DeathObject(player[i].gameObject);
                FlagDeath[i] = true;
                player[i].death = false;
            }
        }

    }



    void DeathObject(GameObject obj)
    {

        Destroy(obj);
    }

    void SetPlayer(int i)
    {
        //playerObj[i]の座標位置、付与するscriptを指示している
        playerObj[i] = Instantiate(playerPrefab, pos[i], Quaternion.identity);
        //playerObj[i]として呼び出されるオブジェクト名を変えられる

        playerObj[i].name = "player1_" + (i + 1);
        player[i] = playerObj[i].GetComponent<player1_script>();

        player[i].Number = number[i];
        FlagDeath[i] = false;

    }

    void Tubai()
    {
        for(int i=0;i<3;i++)
        {
            Debug.Log("椿井");
            Debug.Log("橋口");
        }
    }
}