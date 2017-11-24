﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=========================================================//
//
//          BattleScript
//
//=========================================================//

public class BattleScript : MonoBehaviour
{
    StatusScript status;
    //  スクリプト取得
    player1_script ps1;
    player2_script ps2;

    //  rival(ライバル)
    static int OBJECT_MAX = 7;
    private GameObject[] rival = new GameObject[OBJECT_MAX];

    //  ZoneColor
    //private GameObject zoneColor1;
    //private GameObject[] zoneColor2 = new GameObject[OBJECT_MAX];
    //private Color semitransparent = new Color(0, 0, 0, 0.1f);

    //  距離変数
    private float length;

    private BATTLE_STEP step;

    public float Deathtime;//とりあえず攻撃モーションとHPの減るタイミングを合わすための時間

    private int PS1Sabun;//自分と敵の攻撃力（Number）の差分
    private int PS2Sabun;//自分と敵の攻撃力（Number）の差分2

    private string SmallNumberObjectName;

    //  行動順
    enum BATTLE_STEP
    {
        NOT_APPROACH = -1,
        APPROACH = 0,
        BATTLE,
    } /*BATTLE_STEP step = BATTLE_STEP.NOT_APPROACH;*/

    //========================================================//
    //      初期化
    //========================================================//

    void Start()
    {
        //  呼び込み
        status = GameObject.Find("Status").GetComponent<StatusScript>();

        //  敵を指定(仮)
        //rival = GameObject.Find("enemy1");
        length = 0;
        step = BATTLE_STEP.NOT_APPROACH;
        //ps2 = rival.GetComponent<player2_script>();
        //　このスクリプトをつけてるキャラのタグが自分なら
        for (int i = 0; i < OBJECT_MAX; i++)
        {
            if (this.gameObject.tag == "Player1")
            {
                //　オブジェクト取得
                rival[i] = GameObject.Find("player2_" + (i+1));
            }
            //　このスクリプトをつけてるキャラのタグが敵なら
            else if (this.gameObject.tag == "Player2")
            {
                //　オブジェクト取得
                rival[i] = GameObject.Find("player1_" + (i+1));
            }
        }

        //  Zoneオブジェクト取得
        //zoneColor1 = ps1.transform.Find("CircleTextureIn").gameObject;
        //zoneColor2[i] = ps2[i].transform.Find("CircleTextureIn").gameObject;
        ////  半透明
        //zoneColor1.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0.1f);
        //zoneColor2[i].GetComponent<Renderer>().material.color = new Color(0, 255, 231, 0.1f);

        Deathtime = 0;//とりあえず攻撃モーションとHPの減るタイミングを合わすための時間
        SmallNumberObjectName = "";
    }

    //========================================================//
    //      距離判定
    //========================================================//

    public float BattleLength(GameObject player, GameObject rival)
    {
        //  プレイヤーと相手の距離を取得
        Vector3 playerPos = player.transform.position;
        Vector3 rivalPos = rival.transform.position;
        //  距離計算
        length = (playerPos - rivalPos).magnitude;

        //  計算結果を返す
        return length;
    }

    //========================================================//
    //      メイン処理
    //========================================================//

    void Update()
    {
        switch (step)
        {
            //  接近していない時の処理
            case BATTLE_STEP.NOT_APPROACH:
                //  プレイヤーと相手が存在する場合
                if (this.gameObject != null && rival != null)
                {
                    for (int i = 0; i < OBJECT_MAX; i++)
                    {
                        //  距離計算
                        BattleLength(this.gameObject, rival[i]);
                        //  指定距離まで近づいた場合実行
                        if (status.BattleSpace > length)
                        {
                            if (this.gameObject.tag == "Player1")
                            {
                                //　スクリプトの参照
                                ps1 = GetComponent<player1_script>();
                                ps2 = rival[i].GetComponent<player2_script>();
                            }
                            //　このスクリプトをつけてるキャラのタグが敵なら
                            else if (this.gameObject.tag == "Player2")
                            {
                                //　スクリプトの参照
                                ps1 = rival[i].GetComponent<player1_script>();
                                ps2 = GetComponent<player2_script>();
                            }
                            step = BATTLE_STEP.APPROACH;
                        }                        
                    }
                }
                break;

            //  接近時の処理
            case BATTLE_STEP.APPROACH:
                //  キャラクターの移動を止める
                if (ps1.agent != null) ps1.agent.ResetPath();
                if (ps2.agent != null) ps2.agent.ResetPath();
                step = BATTLE_STEP.BATTLE;
                break;

            //  バトル処理
            case BATTLE_STEP.BATTLE:
                //  プレイヤーと相手が存在する場合
                if (this.gameObject != null && rival != null)
                {
                    Deathtime = Deathtime - Time.deltaTime;
                    //　どちらもチームでなければ
                    if (Deathtime < -1.8f && !ps1.FlagTeam && !ps2.FlagTeam)
                    {
                        PS1Sabun = 0;
                        PS2Sabun = 0;
                        if (ps1.Number >= ps2.Number) PS1Sabun = ps1.Number - ps2.Number;
                        if (ps1.Number < ps2.Number) PS2Sabun = ps2.Number - ps1.Number;
                        ps1.HP -= ps2.PlayerATK + PS2Sabun;// + ps2[i].Number;
                        ps2.HP -= ps1.PlayerATK + PS1Sabun; //+ ps1.Number;
                        Deathtime = 0;
                    }
                    //　どちらか又はどっちもチームだったら
                    else if (Deathtime < -1.8f && ps1.FlagTeam || ps2.FlagTeam)
                    {
                        //　敵味方の数値差を計算
                        if (ps1.TeamNumber >= ps2.TeamNumber) PS1Sabun = ps1.TeamNumber - ps2.TeamNumber;
                        if (ps1.TeamNumber < ps2.TeamNumber) PS2Sabun = ps2.TeamNumber - ps1.TeamNumber;
                        //　チーム内(Player1目線)で自分が一番数値が低い場合
                        if (SmallNumberObjectName == gameObject.name && ps1.FlagTeam)
                        {
                            ps1.HP -= ps2.PlayerATK + PS2Sabun;
                            ps2.HP -= ps1.PlayerATK + PS1Sabun;
                        }
                        //　チーム内(Player2目線)で自分()が一番数値が低い場合
                        else if (SmallNumberObjectName == gameObject.name && ps2.FlagTeam)
                        {
                            ps1.HP -= ps2.PlayerATK + PS2Sabun;
                            ps2.HP -= ps1.PlayerATK + PS1Sabun;
                        }
                        //　Player1が個人でPlayer2がチームの場合
                        else if (SmallNumberObjectName == gameObject.name && !ps1.FlagTeam && ps2.FlagTeam)
                        {
                            ps1.HP -= ps2.PlayerATK + PS2Sabun;
                            ps2.HP -= ps1.PlayerATK + PS1Sabun;
                        }
                        //　Player1がチームでPlayer2が個人の場合
                        else if (SmallNumberObjectName == gameObject.name && ps1.FlagTeam && !ps2.FlagTeam)
                        {
                            ps1.HP -= ps2.PlayerATK + PS2Sabun;
                            ps2.HP -= ps1.PlayerATK + PS1Sabun;
                        }
                        Deathtime = 0;
                    }
                    //　敵の方向を向く

                    //　攻撃アニメーション再生
                    ps1.anime.SetBool("set", true);
                    ps2.anime.SetBool("set", true);
                    //  半透明
                    //zoneColor1.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 1.0f);
                    //zoneColor2[i].GetComponent<Renderer>().material.color = new Color(0, 255, 231, 1.0f);

                    //  1人でも周辺にいる限り、攻撃し続ける
                    bool isEndAttack = true;
                    for (int i = 0; i < OBJECT_MAX; i++)
                    {
                        //  距離計算
                        BattleLength(this.gameObject, rival[i]);
                        //  指定距離から離れた場合実行
                        //if (status.BattleSpace < length)
                        //{
                        //    break;
                        //}

                        //  1人でも周辺にいる限り、攻撃し続ける
                        if (status.BattleSpace > length)
                        {
                            isEndAttack = false;
                            break;
                        }

                    }

                    //  攻撃が終わる時
                    if( isEndAttack )
                    {
                        //  攻撃終了

                        //  半透明
                        //zoneColor1.GetComponent<Renderer>().material.color = new Color(255, 0, 0, 0.1f);
                        //zoneColor2[i].GetComponent<Renderer>().material.color = new Color(0, 255, 231, 0.1f);
                        step = BATTLE_STEP.NOT_APPROACH;
                        ps1.anime.SetBool("set", false);
                        ps2.anime.SetBool("set", false);
                    }
                    if (ps1.HP <= 0)
                    {
                        ps1.anime.SetBool("set", false);
                    }
                    else if(ps2.HP <=0)
                    {
                        ps2.anime.SetBool("set", false);

                    }
                }
                break;
        }
    }

    public void SetObjectName(string name)
    {
        SmallNumberObjectName = name;
    }
}
