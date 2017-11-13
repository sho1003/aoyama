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
    public readonly int maxHP = 100;    //体力の最大値
    public int HP;    //体力
    public int EnemyATK = 5;  //敵の攻撃力
    public int Number;   //キャラクターの記されている数字


    private player1_script player;
    public int i;

    Vector3 moveEnemy;
    public GameObject point;

    public float Deathtime=0;

    void Start()
    {

        //target = GameObject.Find(transform.gameObject.name).GetComponent<player_script>();

        HP = maxHP; //初期体力を最大値にする

    }

    void Update()
    {
        i = Number;

        moveEnemy = point.gameObject.transform.position - transform.position;
        //Debug.Log(move);
        transform.Translate(moveEnemy * speed * Time.deltaTime * 3);

        if (HP < 0)
        {

            if (gameObject.tag == "Enemy") transform.position = new Vector3(0, 50, 0);
            Deathtime = Deathtime - Time.deltaTime;

            if (Deathtime <= -0.1f)
            {
                Deathtime = 0;
                Destroy(gameObject);
            }
        }
        


    }

    //stay=接触し続けている間以下を実行
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player1")
        {
            Chase(col.gameObject);
        }

        if (col.gameObject.tag == "Player1")
        {
           player = GameObject.Find(col.gameObject.transform.name).GetComponent<player1_script>();
            //  HP -= player.PlayerATK;//攻撃で体力が減少

            HP -= player.Number;//

            //Debug.Log(HP); //HPを表示

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
