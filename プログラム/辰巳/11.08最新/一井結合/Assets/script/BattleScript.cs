using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=========================================================//
//
//          BattleScript
//
//=========================================================//

public class BattleScript : MonoBehaviour {

    //  スクリプト取得
    player_script ps;

    //  rival(ライバル)
    public GameObject rival;

    //  距離変数
    private float length;
    //  プレイヤーとライバルの距離幅
    public float space = 3.0f;

    //  行動順
    enum BATTLE_STEP
    {
        NOT_APPROACH = -1,
        APPROACH = 0,
        BATTLE,
    } BATTLE_STEP step = BATTLE_STEP.NOT_APPROACH;

    //========================================================//
    //      初期化
    //========================================================//

    void Start() 
    {
        //  呼び込み
        ps = GetComponent<player_script>();
        //  敵を指定(仮)
        rival = GameObject.Find("enemy1");
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
                            //  距離計算
                            BattleLength(this.gameObject, rival);
                            //  指定距離まで近づいた場合実行
                            if (space > length)
                                step = BATTLE_STEP.APPROACH;
                        }
                        break;

                        //  接近時の処理
                    case BATTLE_STEP.APPROACH:
                        //  キャラクターの移動を止める
                        ps.agent.ResetPath();
                        GameObject Zone = this.transform.Find("Zone").gameObject;
                        Zone.SetActive(false);
                        
                        Debug.Log("近づいた");

                        step = BATTLE_STEP.BATTLE;
                        break;

                        //  バトル処理
                    case BATTLE_STEP.BATTLE:





                        //  プレイヤーと相手が存在する場合
                        if (this.gameObject != null && rival != null)
                        {
                            //  距離計算
                            BattleLength(this.gameObject, rival);
                            //  指定距離から離れた場合実行
                            if (space < length)
                            {
                                Debug.Log("離れた");
                                step = BATTLE_STEP.NOT_APPROACH;
                            }
                        }
                        break;
                }
    }
}
