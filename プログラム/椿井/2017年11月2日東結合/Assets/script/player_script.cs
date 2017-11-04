using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_script : MonoBehaviour
{


    private NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;


    public readonly int maxHP = 100;    //体力の最大値
    public int HP;    //体力
    public int a = 1;
    public int PlayerATK = 3;  //プレイヤーの攻撃力
    public int Number;   //キャラクターの記されている数字
    public bool death;
    public float Deathtime;
  

    private enemy_script enemy;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        transform.rotation = Quaternion.Euler(0, 180, 0);
        HP = maxHP; //初期体力を最大値にする
        death = false;
    }

    void Update()
    {
        if (HP < 0)
        {
            transform.position = new Vector3(0, 50, 0);

            Deathtime = Deathtime - Time.deltaTime;
            if (Deathtime <= 0.1f)
            {
                Deathtime = 0;
                death = true;
                // Debug.Log("死亡しました");
            }
        }
    }



    public void Pointer_Click()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            agent.SetDestination(hit.point);
        }
    }

    // 
    void OnCollisionStay(Collision collision)
    {

        //if (collision.gameObject.tag == "Enemy") {
        //    enemy = GameObject.Find(collision.gameObject.transform.name).GetComponent<enemy_script>();

        //    HP -= enemy.EnemyATK;//攻撃で体力が減少
        //    //Debug.Log(HP); //HPを表示

        //}
        //if (HP < 0)
        //{
        //    death = true;
        //}
    }

    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            enemy = GameObject.Find(col.gameObject.transform.name).GetComponent<enemy_script>();

            //HP -= enemy.EnemyATK;//攻撃で体力が減少
                                 //Debug.Log(HP); //HPを表示

        }
        
    }
}

