using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_Manager : NetworkBehaviour {


    //オブジェクトの定義
    public GameObject playerPrefab;


    //定義したオブジェクトの箱をn個作る
    static int PLAYER_MAX = 7;
    GameObject[] playerObj = new GameObject[PLAYER_MAX];
    //player_scriptを定義できる箱をn個作る
    player_script[] player = new player_script[PLAYER_MAX];
    private bool[] FlagDeath = new bool[PLAYER_MAX];
    static int RespawnTime = 60;
    private int[] time = new int[PLAYER_MAX];


    //座標位置を指定する箱をn個作る
    Vector3[] pos = new Vector3[PLAYER_MAX];


    // Use this for initialization
    void Start()
    {
        if(!isLocalPlayer)
        {
            //初めの座標位置を指定
            pos[0] = new Vector3(10.0f, 0.6f, 15.0f);
            pos[1] = new Vector3(5.0f, 0.6f, 15.0f);
            pos[2] = new Vector3(0.0f, 0.6f, 15.0f);
            pos[3] = new Vector3(-5.0f, 0.6f, 15.0f);
            pos[4] = new Vector3(-10.0f, 0.6f, 15.0f);
            pos[5] = new Vector3(5.0f, 0.6f, 11.0f);
            pos[6] = new Vector3(-5.0f, 0.6f, 11.0f);
        }
        else
        {
            //初めの座標位置を指定
            pos[0] = new Vector3(10.0f, 0.6f, -15.0f);
            pos[1] = new Vector3(5.0f, 0.6f, -15.0f);
            pos[2] = new Vector3(0.0f, 0.6f, -15.0f);
            pos[3] = new Vector3(-5.0f, 0.6f, -15.0f);
            pos[4] = new Vector3(-10.0f, 0.6f, -15.0f);
            pos[5] = new Vector3(5.0f, 0.6f, -11.0f);
            pos[6] = new Vector3(-5.0f, 0.6f, -11.0f);
        }
        for (int i = 0; i < PLAYER_MAX; i++)
        {
            SetPlayer(i);
            time[i] = RespawnTime;
        }

    }

    // Update is called once per frame
    void Update () {
            for (int i = 0; i < PLAYER_MAX; i++)
            {
                RpcPlayerMove(i);
                if (FlagDeath[i])
                {
                    if (time[i] > 0) time[i]--;
                    else
                    {
                        SetPlayer(i);
                        time[i] = RespawnTime;
                    }
                }

                if (player[i].death)
                {
                    DeathObject(player[i].gameObject);
                    FlagDeath[i] = true;
                    player[i].death = false;
                }
            }
		
	}

    [ClientRpc]
    void RpcPlayerMove(int i)
    {
        if (!player[i].CheckClick)
        {
            //一回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.name == "player" + i && !player[i].isLocal)
                    {
                        player[i].CheckClick = true;
                        Debug.Log("押した");
                    }
                }
            }
        }
        else
        {
            //二回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    //移動させる
                    player[i].Pointer_Click();
                    CmdDebug(10);
                    RpcDebug(10);
                    player[i].CheckClick = false;
                }
            }
        }
    }

    void DeathObject(GameObject obj)
    {
        Destroy(obj);
    }

    void SetPlayer(int i)
    {
        //playerObj[i]の座標位置、付与するscriptを指示している
        playerObj[i] = Instantiate(playerPrefab, pos[i], Quaternion.identity);
        //playerObj[i]として呼び出されるオブジェクト名を変えられる
        playerObj[i].name = "player" + i;
        player[i] = playerObj[i].GetComponent<player_script>();
        player[i].Number = i + 1;
        FlagDeath[i] = false;
        if (isLocalPlayer) player[i].isLocal = true;
    }

    public override void OnStartLocalPlayer()
    {
        //GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    [Command]
    void CmdDebug(int i)
    {
        DebugText.AddDebugText( "Call CmdDebug(" + i.ToString() + ")" );
    }

    [ClientRpc]
    void RpcDebug(int i)
    {
        DebugText.AddDebugText( "Call RpcDebug(" + i.ToString() + ")" );
    }
}
