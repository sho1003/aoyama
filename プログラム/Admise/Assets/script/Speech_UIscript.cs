using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech_UIscript : MonoBehaviour
{

    const float GET_LENGTH = 3.0f;
    static int PLAYER_MAX = 7;

    GameObject player;
    player1_script player1;
    player2_script player2;
    player1_script[] otherPlayer1 = new player1_script[PLAYER_MAX];
    player2_script[] otherPlayer2 = new player2_script[PLAYER_MAX];

    Vector3 offset;
    Camera rotateCamera;
    TeamManager team;

    RectTransform myRectTrans;
    RectTransform SpeechBalloon;
    GameObject hukidashi;
    Text sumText;

    // Use this for initialization
    void Start()
    {
        rotateCamera = Camera.main;
        player = transform.root.gameObject;

        if (player.tag == "Player1")
        {
            player1 = player.GetComponent<player1_script>();
        }
        else if (player.tag == "Player2")
        {
            player2 = player.GetComponent<player2_script>();
        }

        for (int i = 0; i < PLAYER_MAX; i++)
        {
            otherPlayer1[i] = GameObject.Find("player1_" + i).GetComponent<player1_script>();
            otherPlayer2[i] = GameObject.Find("player2_" + i).GetComponent<player2_script>();
        }

        team = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        myRectTrans = GetComponent<RectTransform>();
        SpeechBalloon = transform.GetChild(1).GetComponent<RectTransform>();

        hukidashi = transform.Find("SpeechBalloon").gameObject;
        sumText = hukidashi.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotateCamera.transform.rotation;

        //一定距離内にタグのオブジェクトがあれば
        if (ExistTag(player, player.tag))
        {
            List<int> keepID = SearchTag(player, player.tag);

            if (player.tag == "Player1")
            {
                tagPlayer1LengthTrue(keepID);
            }
            else if (player.tag == "Player2")
            {
                tagPlayer2LengthTrue(keepID);
            }
        }
        //プレイヤーとの距離が一定以上離れていたら
        else
        {
            //吹き出し表示
            hukidashi.SetActive(true);

            if (player.tag == "Player1")
            {
                tagPlayer1LengthFalse();
            }
            else if (player.tag == "Player2")
            {
                tagPlayer2LengthFalse();
            }

            //プレイヤーの位置によって微調整
            OffsetAdjustment();
        }

        //ワールド座標をスクリーン座標に変換
        var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTrans, screenPos, Camera.main, out localPos);
        SpeechBalloon.localPosition = localPos;

    }

    void tagPlayer1LengthTrue(List<int> ID)
    {
        if (team.checkZ(true, player1.ID))
        {
            //吹き出しを出したまま
            hukidashi.SetActive(true);
        }
        else
        {
            //吹き出しを一旦消す
            hukidashi.SetActive(false);
        }

        for (int i = 0; i < ID.Count; i++)
        {
            //チームメンバーに自分が居なければ
            if (team.checkTeamID(true, player1.ID, ID[i]))
            {
                //チーム結成
                team.connectTeam(true, player1.ID, ID[i]);
            }
        }

        //吹き出しの位置計算
        int[] id = team.TeamID(true, player1.ID);
        Vector3 off = Vector3.zero;
        for (int i = 0; i < id.Length; i++)
        {
            off = otherPlayer1[id[i]].transform.position - player1.transform.position;
        }

        //吹き出しの位置変更
        offset.x = off.x / (id.Length + 1);     //+1は自分の分
        offset.y = 4.0f;
        offset.z = 2.0f;

        //　チームを組んだフラグを立てる
        player1.FlagTeam = true;

        //吹き出しの数字変更
        sumText.text = "" + team.TeamSumNumber(true, player1.ID);
    }

    void tagPlayer1LengthFalse()
    {
        sumText.text = "" + player1.Number;
        //　チームを組んでいないのでフラグを立てない
        player1.FlagTeam = false;

        //チーム解除
        team.RemoveTeam(true, player1.ID);
    }

    void tagPlayer2LengthTrue(List<int> ID)
    {
        if (team.checkZ(false, player2.ID))
        {
            //吹き出しを出したまま
            hukidashi.SetActive(true);
        }
        else
        {
            //吹き出しを一旦消す
            hukidashi.SetActive(false);
        }

        for (int i = 0; i < ID.Count; i++)
        {
            //チームメンバーに自分が居なければ
            if (team.checkTeamID(false, player2.ID, ID[i]))
            {
                //チーム結成
                team.connectTeam(false, player2.ID, ID[i]);
            }
        }

        //吹き出しの位置計算
        int[] id = team.TeamID(false, player2.ID);
        Vector3 off = Vector3.zero;
        for (int i = 0; i < id.Length; i++)
        {
            off = otherPlayer2[id[i]].transform.position - player2.transform.position;
        }

        //吹き出しの位置変更
        offset.x = off.x / (id.Length + 1);
        offset.y = 4.0f;
        offset.z = 2.0f;

        //　チームを組んだフラグを立てる
        player2.FlagTeam = true;

        //吹き出しの数字変更
        sumText.text = "" + team.TeamSumNumber(false, player2.ID);
    }

    void tagPlayer2LengthFalse()
    {
        sumText.text = "" + player2.Number;
        //　チームを組んでいないのでフラグを立てない
        player2.FlagTeam = false;

        //チーム解除
        team.RemoveTeam(false, player2.ID);
    }

    void OffsetAdjustment()
    {
        offset.x = 0;
        offset.y = 4.0f;
        offset.z = 2.0f;
    }

    //指定されたタグが一定距離内にあればtrueを返す
    bool ExistTag(GameObject nowObj, string tagName)
    {
        float nearDis = 0.1f;           //最短距離
        float tmpDis = 0;               //距離用一時変数

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //取得したオブジェクトと自身の距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離がGET_LENGTH以下ならば
            if (nearDis < tmpDis && tmpDis < GET_LENGTH)
            {
                return true;
            }
        }

        return false;
    }

    //指定されたタグが一定距離内にあればそのIDを返す
    List<int> SearchTag(GameObject nowObj, string tagName)
    {
        float nearDis = 0.1f;               //最短距離
        float tmpDis = 0;                   //距離用一時変数
        List<int> list = new List<int>();   //リターン用

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //取得したオブジェクトと自身の距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離がGET_LENGTH以下ならば
            if (nearDis < tmpDis && tmpDis < GET_LENGTH)
            {
                if(tagName == "Player1")
                {
                    player1_script p1 = obs.GetComponent<player1_script>();
                    list.Add(p1.ID);
                }
                if (tagName == "Player2")
                {
                    player2_script p2 = obs.GetComponent<player2_script>();
                    list.Add(p2.ID);
                }
            }
        }
        
        return list;
    }

    void Disable()
    {
        this.gameObject.SetActive(false);
    }

}
