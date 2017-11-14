using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player1_script : MonoBehaviour
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

    private readonly float BaseSpeed = 5.0f;        //　キャラクターの基礎速度
    public float Speed;                             //　調整用のスピード変数

    private player1_script player1;
    private player2_script player2;

    public bool tasi;

   public int i2;//相手側の数値

    public int i3;//相手側の数値保管
    public bool zeroflag;
    public static bool SuutiByougaflag=false;
  //  private object player2;

    void Start()
    {
        anime = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        //　キャラ速度の設定
        //　基礎速度からキャラの数字割る２で0.5単位で変化させてる(＋Speedで外部から微調整できる(キャラ全体での調整、キャラ数値で調整したいなら計算式を変更する))
        agent.speed = BaseSpeed - Number / 2 + Speed;

    //    transform.rotation = Quaternion.Euler(0, 180, 0);
        HP = maxHP; //初期体力を最大値にする
        death = false;
        tasi = true;
        zeroflag = false;
    }

    void Update()
    {

      //  HP -= 1;
        SuutiScript.Number = Number;
        SuutiScript.i2 = i2;

        
        if (HP < 0)
        {
            anime.SetBool("run", false);
            if (gameObject.tag=="Player1")  transform.position = new Vector3(0, 50, 0);

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
    

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player1")
        {
            SuutiByougaflag = false;
            player1 = GameObject.Find(col.gameObject.transform.name).GetComponent<player1_script>();

            // Number = Number + player.Number;

         if (tasi == true && zeroflag == false) i2 = player1.Number;
        }



        if (Number >= i2)
        {
            player1 = GameObject.Find(col.gameObject.transform.name).GetComponent<player1_script>();
            if (tasi == true)
            {
                i3 = i2;
                Number = Number + i3;
                tasi = false;
                zeroflag = true;


            }
        }
        else if (Number < i2)
        {
            if (tasi == true)
            {
                i3 = Number;
               //Number = 0;プレイヤーの順番的にバグる
                tasi = false;
                //player.tasi = false;//相手の方↑でfalseにしてる?
                zeroflag = true;

            }
        }

    }

    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player1")
        {
            player1 = GameObject.Find(col.gameObject.transform.name).GetComponent<player1_script>();

            //Number = Number - player.Number;
            //   Number = Number - i2;
            zeroflag = false;
            SuutiByougaflag = true;
        }

        if (col.gameObject.tag == "Player2")

        {
            anime.SetBool("set", false);
        }



        if (Number > i2)
        {
   
            if (tasi == false)
            {
                Number = Number - i3;
                tasi = true;
               // player1.tasi = false;
                

            }
        }

        else if (Number <= i2)
        {
            if (tasi == false)
            {
                Number = i3;
                tasi = true;
                //player.tasi = false;//相手の方↑でfalseにしてる?
              
            }
        }

    }

    void OnTriggerStay(Collider col)
    {


        //if (col.gameObject.tag == "Player1")
        //{
        //    player = GameObject.Find(col.gameObject.transform.name).GetComponent<player1_script>();

        //  //  i2 = player.Number;

        //    //    if (tasi == true)
        //    //{
        //    //    Number = Number + i2;
        //    //    tasi = false;
        //    //    player.tasi = false;

        //    //}



        if (col.gameObject.tag == "Player2")
        {
            // enemyのcollisionに当たるとsetアニメーションをtrueにする
            anime.SetBool("set", true);

            player2 = col.gameObject.GetComponent<player2_script>();
            HP -= player2.Number; //攻撃で体力が減少
        }




    }

}

