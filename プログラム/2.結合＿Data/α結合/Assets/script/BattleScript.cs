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
    static int OBJECT_MAX = 7;
    //  スクリプト取得
    player1_script ps1;
    player2_script[] ps2 = new player2_script[OBJECT_MAX];

    //  rival(ライバル)
    public GameObject[] rival = new GameObject [OBJECT_MAX];

    //  距離変数
    private float[] length = new float[OBJECT_MAX];

    private BATTLE_STEP[] step = new BATTLE_STEP[OBJECT_MAX];

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
        ps1 = GetComponent<player1_script>();

        //  敵を指定(仮)
        //rival = GameObject.Find("enemy1");
        for(int i=0;i<OBJECT_MAX;i++)
        {
            length[i] = 0;
            step[i] = BATTLE_STEP.NOT_APPROACH;
            rival[i] = GameObject.Find("player2_" + (i + 1));
            if (!rival[i]) continue;
            ps2[i] = rival[i].GetComponent<player2_script>();
        }
    }

    //========================================================//
    //      距離判定
    //========================================================//

    public float BattleLength(GameObject player, GameObject rival,int i)
    {
        //  プレイヤーと相手の距離を取得
        Vector3 playerPos = player.transform.position;
        Vector3 rivalPos = rival.transform.position;
        //  距離計算
        length[i] = (playerPos - rivalPos).magnitude;

        //  計算結果を返す
        return length[i];
    }

    //========================================================//
    //      メイン処理
    //========================================================//

    void Update()
    {
        for (int i = 0; i < OBJECT_MAX; i++)
        {
            if (!ps2[i]) continue;
            switch (step[i])
            {
                //  接近していない時の処理
                case BATTLE_STEP.NOT_APPROACH:
                    //  プレイヤーと相手が存在する場合
                    if (this.gameObject != null && rival[i] != null)
                    {
                        //  距離計算
                        BattleLength(this.gameObject, rival[i],i);
                        //  指定距離まで近づいた場合実行
                        if (status.BattleSpace > length[i])
                            step[i] = BATTLE_STEP.APPROACH;
                    }
                    break;

                //  接近時の処理
                case BATTLE_STEP.APPROACH:
                    //  キャラクターの移動を止める
                    ps1.agent.ResetPath();
                    ps2[i].agent.ResetPath();
                    //GameObject Zone = this.transform.Find("Zone").gameObject;
                    //Zone.SetActive(false);
                    step[i] = BATTLE_STEP.BATTLE;
                    break;

                //  バトル処理
                case BATTLE_STEP.BATTLE:
                    //  プレイヤーと相手が存在する場合
                    if (this.gameObject != null && rival[i] != null)
                    {
                        //　ダメージ処理
                        ps1.HP -= ps2[i].PlayerATK+ps2[i].Number;
                        ps2[i].HP -= ps1.PlayerATK + ps1.Number;
                        //　攻撃アニメーション再生
                        ps1.anime.SetBool("set", true);
                        ps2[i].anime.SetBool("set", true);
                        //  距離計算
                        BattleLength(this.gameObject, rival[i],i);
                        //  指定距離から離れた場合実行
                        if (status.BattleSpace < length[i])
                        {
                            step[i] = BATTLE_STEP.NOT_APPROACH;
                            ps1.anime.SetBool("set", false);
                            ps2[i].anime.SetBool("set", false);
                        }
                    }
                    else
                        step[i] = BATTLE_STEP.NOT_APPROACH;
                    break;
            }
        }
    }
}
