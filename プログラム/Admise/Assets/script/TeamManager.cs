using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    static int PLAYER_MAX = 7;

    player1_script[] player = new player1_script[PLAYER_MAX];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PLAYER_MAX; i++)
        {
            player[i] = GameObject.Find("player1_" + i).GetComponent<player1_script>();
        }
    }

    //チームであることを証明する
    void connectTeam(int myID, int teamID)
    {
        player[teamID].edge.Add(myID);
    }

    //チームから外れる時の処理
    //playerIDはチームの自分以外の全員のID
    void RemoveTeam(int[] playerID, int removeID)
    {
        for (int i = 0; i < playerID.Length; i++)
        {
            //リスト内にremoveIDがあれば削除
            player[i].edge.RemoveAll(s => s == removeID);
        }
    }

    //自分含めたチーム全員のIDを返す
    int[] TeamAllID(int myID)
    {
        int idCount = player[myID].edge.Count + 1;
        int[] id = new int[idCount];

        id[0] = player[myID].ID;
        for (int i = 1; i < idCount; i++)
        {
            id[i] = player[myID].edge[i];
        }

        return id;
    }

    //自分以外のチーム全員のIDを返す
    int[] TeamID(int myID)
    {
        int idCount = player[myID].edge.Count;
        int[] id = new int[idCount];

        for (int i = 0; i < idCount; i++)
        {
            id[i] = player[myID].edge[i];
        }

        return id;
    }

    //チームの合計数値を返す
    int TeamSumNumber(int myID)
    {
        int idCount = player[myID].edge.Count;
        int[] id = new int[idCount];
        int sumNumber = 0;

        id = TeamID(myID);

        for (int i = 0; i < idCount; i++)
        {
            //合計値に足していく
            sumNumber += player[id[i]].Number;
        }

        return sumNumber;
    }

    //チームの中で一番数値の低いプレイヤーIDを返す
    int LowPlayerID(int myID)
    {
        int lowPlayer = 0;      //返すプレイヤーID
        int tmpNum = 10;        //大きさ一時変数
        int idCount = player[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が一時変数より低い時
            if (tmpNum > player[id[i]].Number)
            {
                //一時変数を更新
                tmpNum = player[id[i]].Number;
                //返すプレイヤーIDも更新
                lowPlayer = player[id[i]].ID;
            }
        }

        return lowPlayer;
    }

    //チームの中で一番数値の低いプレイヤー数値を返す
    int LowPlayerNumber(int myID)
    {
        int lowNum = 10;        //返す数値変数
        int idCount = player[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が数値変数より低い時
            if (lowNum > player[id[i]].Number)
            {
                //数値変数を更新
                lowNum = player[id[i]].Number;
            }
        }

        return lowNum;
    }

    //チームの中で一番数値の低いプレイヤーが複数ある場合trueを返す
    bool MultiLowNumber(int myID)
    {
        int count = 0;      //複数判定変数
        int lowNumber = LowPlayerNumber(myID);      //チーム内で一番低い数値
        int idCount = player[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が一番低い数値なら
            if(lowNumber == player[id[i]].Number)
            {
                count++;
            }
        }

        //countが2以上ならtrue0
        if (count > 1) return true;
        return false;
    }

    //チームの中で一番数値の低いHPの低いプレイヤーIDを返す
    int HPLowPlayerNumber(int myID)
    {
        int lowNum = 0;         //返す用数値変数
        int lowHP = 1000;       //HP用一時変数
        int lowNumber = LowPlayerNumber(myID);      //チーム内で一番低い数値
        int idCount = player[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が一番低い数値なら
            if (lowNumber == player[id[i]].Number)
            {
                //HPが一時変数より低ければ
                if(lowHP > player[id[i]].HP)
                {
                    //HP変数を更新
                    lowHP = player[id[i]].HP;
                    //数値変数を更新
                    lowNum = player[id[i]].ID;
                }
            }
        }

        return lowNum;
    }

}
