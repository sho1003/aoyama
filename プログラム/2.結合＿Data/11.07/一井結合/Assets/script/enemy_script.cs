using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class enemy_script : MonoBehaviour {

    /*  */
    public float speed = 5f;
    private Transform target;
    Vector3 move = new Vector3(0, 0, 0);


    /*  enemyのステータス変数  */
    public readonly int maxHP = 10000;    //体力の最大値
    public int HP;    //体力
    public int EnemyATK = 5;  //敵の攻撃力
    public int Number;   //キャラクターの記されている数字


    private player_script player;
    public int i;

    void Start()
    {

        //target = GameObject.Find(transform.gameObject.name).GetComponent<player_script>();

        HP = maxHP; //初期体力を最大値にする

    }

    void Update()
    {
        i = Number;
    }

    //stay=接触し続けている間以下を実行
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            Chase(col.gameObject);
        }

        if (col.gameObject.tag == "Player")
        {
            //  あたっている相手のScript取得
            player = GameObject.Find(col.gameObject.transform.name).GetComponent<player_script>();
            //  enemey側がプレイヤーと攻撃力が同じか、上回った場合は１ダメージ
            if (EnemyATK == player.PlayerATK || EnemyATK > player.PlayerATK)
                HP--;
            //  下回った場合はその分ダメージ加算
            else HP -= (player.PlayerATK - EnemyATK);
        }
        if (HP < 0)
        {
            col.transform.position = new Vector3(0, 50, 0);
            Destroy(gameObject);

        }
    }

    //　引数：コライダーに当たったオブジェクトを入れる
    void Chase(GameObject obj)
    {
        move = obj.transform.position - gameObject.transform.position;
        //Debug.Log(move);
        transform.Translate(move * speed * Time.deltaTime);
    }

    // 
    void OnCollisionStay(Collision collision)
    {

        //if (collision.gameObject.tag == "Player")
        //{
        //    player = GameObject.Find(collision.gameObject.transform.name).GetComponent<player_script>();

        //    HP -= player.PlayerATK;//攻撃で体力が減少
        //    //Debug.Log(HP); //HPを表示

        //}
        //if (HP < 0)
        //{
        //    Destroy(gameObject);
        //}
    }

}
