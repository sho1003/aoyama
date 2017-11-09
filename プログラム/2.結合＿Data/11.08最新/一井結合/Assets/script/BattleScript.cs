using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=========================================================//
//
//          BattleScript
//
//=========================================================//

public class BattleScript : MonoBehaviour {

    //  rival(ライバル)
    public GameObject rival;

    //  距離変数
    private float length;
    //  プレイヤーとライバルの距離幅
    private float space = 4.0f;

    //  フラグ
    
    public bool flg = true;

    //  行動順
    enum BATTLE_STEP
    {
        APPROACH = 0,
        BATTLE,
    } BATTLE_STEP step = 0;

    //========================================================//
    //      初期化
    //========================================================//

    void Start() 
    {

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
            //  近づくまでの処理
            case BATTLE_STEP.APPROACH:
                //  プレイヤーと相手が存在する場合
                if (this.gameObject != null && rival != null)
                {
                    //  距離計算
                    BattleLength(this.gameObject, rival);
                    //  指定距離まで近づいた場合実行
                    if (space > length)
                    {
                        Debug.Log("近づいた");
                        player_script ps = GetComponent<player_script>();
                        ps.agent.ResetPath();
                        step = BATTLE_STEP.BATTLE;
                    }
                }
                break;

            //  バトル処理
            case BATTLE_STEP.BATTLE:

                break;
        }
       
    }




}
