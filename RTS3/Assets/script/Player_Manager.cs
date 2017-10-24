using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour {


    //オブジェクトの定義
    public GameObject playerPrefab;


    //定義したオブジェクトの箱をn個作る
    static int PLAYER_MAX = 3;
    GameObject[] playerObj = new GameObject[PLAYER_MAX];
    //player_scriptを定義できる箱をn個作る
    player_script[] player = new player_script[PLAYER_MAX];


    //座標位置を指定する箱をn個作る
    Vector3[] pos = new Vector3[PLAYER_MAX];


    // Use this for initialization
    void Start()
    {

        //初めの座標位置を指定
        pos[0] = new Vector3(0.0f, 0.6f, 13.5f);
        pos[1] = new Vector3(5.0f, 0.6f, 14.0f);
        pos[2] = new Vector3(-5.0f, 0.6f, 13.5f);


        for (int i = 0; i < PLAYER_MAX; i++)
        {
            //playerObj[i]の座標位置、付与するscriptを指示している
            playerObj[i] = Instantiate(playerPrefab, pos[i], Quaternion.identity);
            //playerObj[i]として呼び出されるオブジェクト名を変えられる
            playerObj[i].name = "player" + i;
            player[i] = playerObj[i].GetComponent<player_script>();
        }

        //playerObj[1]の座標位置、付与するscriptを指示している
        //playerObj[1] = Instantiate(playerPrefab, pos[1], Quaternion.identity);
        //playerObj[1].name = "player1";
        //player[1] = playerObj[1].GetComponent<player_script>();

        ////playerObj[2]の座標位置、付与するscriptを指示している
        //playerObj[2] = Instantiate(playerPrefab, pos[2], Quaternion.identity);
        //playerObj[2].name = "player2";
        //player[2] = playerObj[2].GetComponent<player_script>();


        //player[0].Number = 5;
    }

    // Update is called once per frame
    void Update () {

		
	}
}
