using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHPScript : MonoBehaviour
{

    private StatusScript status;
    public GameObject gameobject;
    public float CardHPs;
    public GameObject[] Cards;
    public GameObject[] IdouCards;
    public int frameNum;
    public int N;
    public int M;

    //オブジェクトの定義
    public GameObject playerPrefab;

    //定義したオブジェクトの箱をn個作る
    static int PLAYER_MAX = 7;
    GameObject[] playerObj = new GameObject[PLAYER_MAX];
    //player1_scriptを定義できる箱をn個作る
    player1_script[] player = new player1_script[PLAYER_MAX];

    //座標位置を指定する箱をn個作る
    Vector3[] pos = new Vector3[PLAYER_MAX];

    int[] number = new int[PLAYER_MAX];

    const int MAX_GET_NUM = 3;

    public GameObject target;
    public GameObject _parent;
    public bool Flags = true;
    public Vector3 POS;


    // Use this for initialization
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();

        int GageHP1 = gameobject.GetComponent<player1_script>().HP;
        CardHPs = GageHP1 / 10;



        _parent = transform.root.gameObject;

     //    transform.position = new Vector3(0 ,0, 0);
        // transform.position = new Vector3(0, 0, 0);


        if (transform.root == GameObject.Find("player1_0").transform) N = 1;
        else if (transform.root == GameObject.Find("player1_1").transform) N = 2;
        else if (transform.root == GameObject.Find("player1_2").transform) N = 3;
        else if (transform.root == GameObject.Find("player1_3").transform) N = 4;
        else if (transform.root == GameObject.Find("player1_4").transform) N = 5;
        else if (transform.root == GameObject.Find("player1_5").transform) N = 6;
        else if (transform.root == GameObject.Find("player1_6").transform) N = 7;

        POS = transform.position;

    }
    public float time;

    // Update is called once per frame
    void Update()
    {

        int i=1;

        //    IdouCards[1] = GameObject.Find("card1(1)");

        //Debug.Log("Parent:" + _parent.name);
       


        int GageHP1 = gameobject.GetComponent<player1_script>().HP;
       

        time = time + Time.deltaTime;
        transform.parent = GameObject.Find("OUtINtuibi").transform;

       
      
        //if (N == 1) transform.position = IdouCards[1].transform.position;
        //if (N == 2) transform.position = IdouCards[2].transform.position;
        //if (N == 3) transform.position = IdouCards[3].transform.position;
        //if (N == 4) transform.position = IdouCards[4].transform.position;
        //if (N == 5) transform.position = IdouCards[5].transform.position;
        //if (N == 6) transform.position = IdouCards[6].transform.position;
        //if (N == 7) transform.position = IdouCards[0].transform.position;

        //  親オブジェクトを取得
        if (time > 0 && Flags==true)
        {
            POS.x = POS.x + 190 * (N-1);
            transform.position = POS;
            //  transform.position = new Vector3((10 * N), 0, 0);
            //transform.position = transform.FindChild()
            // transform.rotation = Quaternion.Euler(0, 0, 0);
            // transform.localScale = transform.localScale + new Vector3(15,15,0);
            Flags = false;
        }
    }


    //   Cards[frameNum]






}
