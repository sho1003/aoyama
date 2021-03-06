﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Manager : MonoBehaviour {

    private StatusScript status;
    private AreaSkillScript AreaSkill;
    private GameSE_Script se;

    //オブジェクトの定義
    public GameObject playerPrefab;

    //定義したオブジェクトの箱をn個作る
    static int PLAYER_MAX = 7;
    GameObject[] playerObj = new GameObject[PLAYER_MAX];
    //player1_scriptを定義できる箱をn個作る
    player1_script[] player = new player1_script[PLAYER_MAX];
    private bool[] FlagDeath = new bool[PLAYER_MAX];
    private float[] time = new float[PLAYER_MAX];


    //座標位置を指定する箱をn個作る
    Vector3[] pos = new Vector3[PLAYER_MAX];

    int[] number = new int[PLAYER_MAX];

    // Use this for initialization
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        AreaSkill = GameObject.Find("Area").GetComponent<AreaSkillScript>();
        se = GameObject.Find("Sounds/SE").GetComponent<GameSE_Script>();

        for (int i = 0; i < PLAYER_MAX; i++)
        {
            //初めの座標位置を指定
            pos[i] = status.FirstPosition[i];
            number[i] = PlayerPrefs.GetInt("PlayerNum" + i);
            SetPlayer(i);
            time[i] = status.RespawnTime;
        }
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < PLAYER_MAX; i++)
        {
            if (!FlagDeath[i])
            {
                // player[i]が止まりかけるとアニメーションrunをfalseにする
                if (player[i].agent.remainingDistance <= player[i].agent.stoppingDistance)
                {
                    player[i].anime.SetBool("run", false);
                }
            }

            //　プレイヤーが死んだ判定になってなかったら
            if(!FlagDeath[i]&&!player[i].death)
            {
                //　もしリスポーンタイムが短くなるエリアを取っていれば
                //　引数一個めリスポーン初期値タイム、引数二個目(リスポーンタイムの短くなる)エリアの数
                time[i] = AreaSkill.RTimeShort(status.RespawnTime, status.AreaNum);
            }

            if (FlagDeath[i])
            {
                if (time[i] > 0.0f) time[i]--;
                else
                {
                    //SetPlayer(i);
                    player[i].gameObject.SetActive(true);

                    player[i].transform.position = pos[i];
                    player[i].HP = status.CharaHP;
                    FlagDeath[i] = false;

                    time[i] = status.RespawnTime;
                    player[i].anime.SetBool("set", false);
                }
            }

            if(player[i].death)
            {
                //DeathObject(player[i].gameObject);
                player[i].gameObject.SetActive(false);
                SceneManagerScript.Player1Click = false;
                FlagDeath[i] = true;
                player[i].death = false;

                //  攻撃時のSE実行
                se.SetSE1(se.diedSE);
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
     
        playerObj[i].name = "player1_" + i;
        player[i] = playerObj[i].GetComponent<player1_script>();
        player[i].Number = number[i];
        player[i].ID = i;
        FlagDeath[i] = false;
    }
}
