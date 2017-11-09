using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_script : MonoBehaviour
{
    public Animator anime;

    public NavMeshAgent agent;

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

        anime = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        transform.rotation = Quaternion.Euler(0, 180, 0);
        HP = maxHP; //初期体力を最大値にする
        death = false;
    }

    void Update()
    {
        Life_script.GageHP = HP;
       // HP -= 1;
        if (HP < 0)
        {
         if(gameObject.tag=="Player")  transform.position = new Vector3(0, 50, 0);

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
            anime.SetBool("run", true);
        }
    }

    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            // enemyのcollisionに当たるとsetアニメーションをtrueにする
            //anime.SetBool("set", true);
            enemy = GameObject.Find(col.gameObject.transform.name).GetComponent<enemy_script>();
            HP -= enemy.EnemyATK;//攻撃で体力が減少
        }

        //
        //anime.SetBool("set", false);

    }
}

