using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class player_script : MonoBehaviour
{

    public Slider slider;


    private NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;


    public readonly int maxHP = 1000;    //体力の最大値
    public int HP;    //体力
    public int a = 1;
    public int PlayerATK = 0;  //プレイヤーの攻撃力
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


    void OnTriggerStay(Collider collision)
    {
        //  合った相手がエネミーの場合
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = GameObject.Find
                (collision.gameObject.transform.name).GetComponent<enemy_script>();

            //  プレイヤーと敵の攻撃力が同じか、上回った場合は１ダメージ
            if (PlayerATK == enemy.EnemyATK 
                || PlayerATK > enemy.EnemyATK)
                HP--;
            //  下回った場合はその分ダメージ加算
            else HP -= (enemy.EnemyATK - PlayerATK);
        }
        if (HP < 0) death = true;
           
    }
}

