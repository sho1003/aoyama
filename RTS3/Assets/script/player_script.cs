<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_script : MonoBehaviour {


    private NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;


    public readonly int maxHP = 100;    //体力の最大値
    public int HP;    //体力
    public int a = 1;
    public int PlayerATK = 3;  //敵の攻撃力
    public int Number;   //キャラクターの記されている数字
    

    private enemy_script enemy;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        HP = maxHP; //初期体力を最大値にする
        
    }

    void Update()
    {
        if (HP <= 0)
        {
            Debug.Log("死亡しました");
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
    void OnCollisionStay(Collision collision){ 

        if (collision.gameObject.tag == "Enemy") {
            enemy = GameObject.Find(collision.gameObject.transform.name).GetComponent<enemy_script>();

            HP -= enemy.EnemyATK;//攻撃で体力が減少
            Debug.Log(HP); //HPを表示
            
        }
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }

}

=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_script : MonoBehaviour {


    private NavMeshAgent agent;

    private RaycastHit hit;
    private Ray ray;


    public readonly int maxHP = 100;    //体力の最大値
    public int HP;    //体力
    public int a = 1;
    public int PlayerATK = 3;  //敵の攻撃力
    public int Number;   //キャラクターの記されている数字
    

    private enemy_script enemy;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        HP = maxHP; //初期体力を最大値にする
        
    }

    void Update()
    {
        if (HP <= 0)
        {
            Debug.Log("死亡しました");
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
    void OnCollisionStay(Collision collision){ 

        if (collision.gameObject.tag == "Enemy") {
            enemy = GameObject.Find(collision.gameObject.transform.name).GetComponent<enemy_script>();

            HP -= enemy.EnemyATK;//攻撃で体力が減少
            Debug.Log(HP); //HPを表示
            
        }
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }

}

>>>>>>> 2281a693ef68a1adfd1f5b3ac451a5e02cdffb1b
