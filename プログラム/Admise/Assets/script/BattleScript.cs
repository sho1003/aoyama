using System.Collections;
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
	GameSE_Script se;

	//突貫工事
	public bool P1;//（東　次250）

	//  スクリプト取得
	player1_script ps1;
	player2_script ps2;
	TeamManager team;
	public bool match;
	public bool isEndAttack;

	//  rival(ライバル)
	static int OBJECT_MAX = 7;
	private GameObject[] rival = new GameObject[OBJECT_MAX];

	//  ZoneColor
	private GameObject zoneColor1;
	private GameObject[] zoneColor2 = new GameObject[OBJECT_MAX];

	//  距離変数
	private float length;

	//行動状態
	private BATTLE_STEP step;
	public BATTLE_STEP GetStep() { return step; }

	private TEAMNUM teamnum;

	private float Deathtime;                    //とりあえず攻撃モーションとHPの減るタイミングを合わすための時間

	private int PS1Sabun;                       //自分と敵の攻撃力（Number）の差分
	private int PS2Sabun;                       //自分と敵の攻撃力（Number）の差分2

	private Animator animator;

	//  行動順
	public enum BATTLE_STEP
	{
		NOT_APPROACH = -1,
		APPROACH = 0,
		BATTLE,
	} /*BATTLE_STEP step = BATTLE_STEP.NOT_APPROACH;*/

	//　戦闘時チーム数
	enum TEAMNUM
	{
		NONE,
		ONE,
		TWO
	}

	public static int ASZin; //攻撃速度を上げる為のエリア獲得数
	public float AS;
	public float ASTime = 0.25f; //エリア獲得数毎に

    //========================================================//
    //      初期化
    //========================================================//

    void Start()
    {
        animator = GetComponent<Animator>();

        //  呼び込み
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        se = GameObject.Find("Sounds/SE").GetComponent<GameSE_Script>();

        step = BATTLE_STEP.NOT_APPROACH;

        //　このスクリプトをつけてるキャラのタグが自分なら
        for (int i = 0; i < OBJECT_MAX; i++)
        {
            if (this.gameObject.tag == "Player1")
            {
                //　オブジェクト取得
                rival[i] = GameObject.Find("player2_" + i);
            }
            //　このスクリプトをつけてるキャラのタグが敵なら
            else if (this.gameObject.tag == "Player2")
            {
                //　オブジェクト取得
                rival[i] = GameObject.Find("player1_" + i);
            }
        }

        //Deathtime = 0;//とりあえず攻撃モーションとHPの減るタイミングを合わすための時間

        team = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        match = false;
        isEndAttack = true;
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
                        //　このスクリプトをつけてるキャラのタグが
                        if (this.gameObject.tag == "Player1")
                        {
                            //　スクリプトの参照
                            ps1 = GetComponent<player1_script>();
                            ps2 = rival[i].GetComponent<player2_script>();

                            //  指定距離まで近づいた場合
                            if (status.BattleSpace > length)
                            {
                                step = BATTLE_STEP.APPROACH;
                                match = false;
                                ps1.rivalID = i;
                                break;
                            }
                            //チームメンバーの誰かが戦闘状態なら
                            else if (team.TeamBattle(true, ps1.ID))
                            {
                                step = BATTLE_STEP.APPROACH;
                                match = true;
                                int rivalID = team.GetRival(true, ps1.ID);
                                ps2 = rival[rivalID].GetComponent<player2_script>();
                                break;
                            }
                        }
                        //　このスクリプトをつけてるキャラのタグが
                        else if (this.gameObject.tag == "Player2")
                        {
                            //　スクリプトの参照
                            ps1 = rival[i].GetComponent<player1_script>();
                            ps2 = GetComponent<player2_script>();

                            //  指定距離まで近づいた場合
                            if (status.BattleSpace > length)
                            {
                                step = BATTLE_STEP.APPROACH;
                                match = false;
                                ps2.rivalID = i;
                                break;
                            }
                            //チームメンバーの誰かが戦闘状態なら
                            else if (team.TeamBattle(false, ps2.ID))
                            {
                                step = BATTLE_STEP.APPROACH;
                                match = true;
                                int rivalID = team.GetRival(false, ps2.ID);
                                ps1 = rival[rivalID].GetComponent<player1_script>();
                                break;
                            }
                        }
                    }
                }
                break;

            //  接近時の処理
            case BATTLE_STEP.APPROACH:
                //  キャラクターの移動を止める
                if (ps1.agent != null) ps1.agent.ResetPath();
                if (ps2.agent != null) ps2.agent.ResetPath();
                //  攻撃時のSE実行
                se.SetSE1(se.battleSE);
                //  合戦中SE
                se.SetSE1(se.GassenSE);

                step = BATTLE_STEP.BATTLE;
                break;

            //  バトル処理
            case BATTLE_STEP.BATTLE:
                //  プレイヤーと相手が存在する場合
                if (this.gameObject != null && rival != null)
                {
                    if (ASZin > 1)
                    {
                        for (int i = 0; i < ASZin - 1; i++)
                        {
                            ASTime = ASTime * 2;
                        }
                    }
                    Deathtime = Deathtime - Time.deltaTime * (1 + ASTime);

                    //　どちらもチームでなければ
                    if (Deathtime < -1.8f && Check(ps1.FlagTeam, ps2.FlagTeam) == TEAMNUM.NONE)
                    {
                        //数値差を計算
                        Namerical();
                        ps1.HP -= ps2.PlayerATK + PS2Sabun;
                        ps2.HP -= ps1.PlayerATK + PS1Sabun;
                        Deathtime = 0;
                    }
                    //　どっちかがチームだったら
                    else if (Deathtime < -1.8f && Check(ps1.FlagTeam, ps2.FlagTeam) == TEAMNUM.ONE)
                    {
                        //数値差を計算
                        Namerical();

                        //　Player1が個人でPlayer2がチームの場合
                        if (!ps1.FlagTeam && ps2.FlagTeam)
                        {
                            ps1.HP -= ps2.PlayerATK + PS2Sabun;
                            PS2teamTarget();
                        }
                        //　Player1がチームでPlayer2が個人の場合
                        else if (ps1.FlagTeam && !ps2.FlagTeam)
                        {
                            PS1teamTarget();
                            ps2.HP -= ps1.PlayerATK + PS1Sabun;
                        }
                        Deathtime = 0;
                    }
                    //　どっちもチームだったら
                    else if (Deathtime < -1.8f && Check(ps1.FlagTeam, ps2.FlagTeam) == TEAMNUM.TWO)
                    {
                        //数値差を計算
                        Namerical();
                        //　両方チームだったら
                        if (Deathtime < -1.8f && ps1.FlagTeam && ps2.FlagTeam)
                        {
                            PS1teamTarget();
                            PS2teamTarget();
                        }
                        Deathtime = 0;
                    }

                    //　敵の方向を向く

                    //　Player1攻撃アニメーション再生
                    ps1.anime.SetBool("set", true);

                    //GetBoolでif文を使える (東)
                    if (ps1.anime.GetBool("set") == true && P1 == true)
                    {
                        GetComponent<Animator>().speed = 3;
                    }

                    //　Player2攻撃アニメーション再生
                    ps2.anime.SetBool("set", true);

                    //GetBoolでif文を使える (東)
                    if (ps2.anime.GetBool("set") == true && P1 == false)
                    {
                        GetComponent<Animator>().speed = 3;
                    }

                    //  表示
                    ps1.zone.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1.0f);
                    ps2.zone.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 231, 1.0f);

                    //  1人でも周辺にいる限り、攻撃し続ける
                    for (int i = 0; i < OBJECT_MAX; i++)
                    {
                        //自分が敵の一定距離内にいるなら
                        if (!match)
                        {
                            //  距離計算
                            BattleLength(this.gameObject, rival[i]);
                            // 敵が周辺にいる限り、攻撃し続ける
                            if (status.BattleSpace > length)
                            {
                                isEndAttack = false;
                                break;
                            }
                            //敵が周辺にいないなら
                            else
                            {
                                isEndAttack = true;
                            }
                        }
                        //チームメンバーが戦闘状態なら
                        else
                        {
                            if (this.gameObject.tag == "Player1")
                            {
                                //まだチームメンバーが戦闘状態なら
                                if (team.TeamBattle(true, ps1.ID))
                                {
                                    isEndAttack = false;
                                    break;
                                }
                                //戦闘状態のチームメンバーがいないなら
                                else
                                {
                                    isEndAttack = true;
                                    match = false;
                                }
                            }
                            else if (this.gameObject.tag == "Player2")
                            {
                                //まだチームメンバーが戦闘状態なら
                                if (team.TeamBattle(false, ps2.ID))
                                {
                                    isEndAttack = false;
                                    break;
                                }
                                //戦闘状態のチームメンバーがいないなら
                                else
                                {
                                    isEndAttack = true;
                                    match = false;
                                }
                            }
                        }
                    }

                    //  攻撃が終わる時
                    if (isEndAttack)
                    {
                        //  攻撃終了SEストップ
                        se.se.Stop();
                        match = false;

                        //  半透明
                        ps1.zone.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.3f);
                        ps2.zone.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 231, 0.3f);

                        step = BATTLE_STEP.NOT_APPROACH;

                        ps1.anime.SetBool("set", false);

                        //Player1"set"速度を戻す (東)
                        if (ps1.anime.GetBool("set") == false && P1 == true)
                        {
                            GetComponent<Animator>().speed = 1;
                        }

                        ps2.anime.SetBool("set", false);

                        step = BATTLE_STEP.NOT_APPROACH;
                    }
                    if (ps1.HP <= 0)
                    {
                        ps1.anime.SetBool("set", false);
                    }
                    else if (ps2.HP <= 0)
                    {
                        ps2.anime.SetBool("set", false);
                    }
                }
                break;
        }
    }

	private TEAMNUM Check(bool Flag1,bool Flag2)
	{
		//　どっちもチームじゃなかったら
		if (!Flag1 && !Flag2) teamnum = TEAMNUM.NONE;
		//　どっちかがチームやったら
		if ((Flag1 && !Flag2) || (!Flag1 && Flag2)) teamnum = TEAMNUM.ONE;
		//　どっちもチームやったら
		if (Flag1 && Flag2) teamnum = TEAMNUM.TWO;
		return teamnum;
	}

	private void PS1teamTarget()
	{
		//一番数値の低いプレイヤーが複数あるなら
		if (team.MultiLowNumber(false, ps1.ID))
		{
			//一番数値が低く、HPが同じプレイヤーが複数あるなら
			if (team.MultiLowNumberLowHP(false, ps1.ID))
			{
				//攻撃の対象はランダム
				if (ps1.ID == team.LowPlayerID(false, ps1.ID))
				{
					ps1.HP -= ps2.PlayerATK + PS2Sabun;
				}
			}
			//一番数値が低く、HPが低いプレイヤーを攻撃
			else
			{
				if (ps1.ID == team.HPLowPlayerID(false, ps1.ID))
				{
					ps1.HP -= ps2.PlayerATK + PS2Sabun;
                }
			}
		}
		//一番数値の低いプレイヤーを攻撃
		else
		{
			if (ps1.ID == team.LowPlayerID(false, ps1.ID))
			{
				ps1.HP -= ps2.PlayerATK + PS2Sabun;
            }
		}
	}

	private void PS2teamTarget()
	{
		//一番数値の低いプレイヤーが複数あるなら
		if (team.MultiLowNumber(false, ps2.ID))
		{
			//一番数値が低く、HPが同じプレイヤーが複数あるなら
			if (team.MultiLowNumberLowHP(false, ps2.ID))
			{
				//攻撃の対象はランダム
				if (ps2.ID == team.LowPlayerID(false, ps2.ID))
				{
					ps2.HP -= ps1.PlayerATK + PS1Sabun;
				}
			}
			//一番数値が低く、HPが低いプレイヤーを攻撃
			else
			{
				if (ps2.ID == team.HPLowPlayerID(false, ps2.ID))
				{
					ps2.HP -= ps1.PlayerATK + PS1Sabun;
				}
			}
		}
		//一番数値の低いプレイヤーを攻撃
		else
		{
			if (ps2.ID == team.LowPlayerID(false, ps2.ID))
			{
				ps2.HP -= ps1.PlayerATK + PS1Sabun;
			}
		}
	}

	private void Namerical()
	{
		PS1Sabun = 0;
		PS2Sabun = 0;

		//差分計算
		if (team.TeamSumNumber(true, ps1.ID) >= team.TeamSumNumber(false, ps2.ID))
			PS1Sabun = team.TeamSumNumber(true, ps1.ID) - team.TeamSumNumber(false, ps2.ID);
		else PS2Sabun = team.TeamSumNumber(false, ps2.ID) - team.TeamSumNumber(true, ps1.ID);
	}
	
}
