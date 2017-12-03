using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    static int PLAYER_MAX = 7;

    player1_script[] player1 = new player1_script[PLAYER_MAX];
    player2_script[] player2 = new player2_script[PLAYER_MAX];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PLAYER_MAX; i++)
        {
            player1[i] = GameObject.Find("player1_" + i).GetComponent<player1_script>();
            player2[i] = GameObject.Find("player2_" + i).GetComponent<player2_script>();
        }
    }

    //チームであることを証明する
    public void connectTeam(bool friend, int myID, int teamID)
    {
        if (friend)
        {
            player1[teamID].edge.Add(myID);
            player1[myID].edge.Add(teamID);
        }
        else
        {
            player2[teamID].edge.Add(myID);
            player2[myID].edge.Add(teamID);
        }
    }

    //チームから外れる時の処理
    //playerIDはチームの自分以外の全員のID
    public void RemoveTeam(bool friend, int menberID, int removeID)
    {
        //for (int i = 0; i < menberID.Length; i++)
        if(friend)
        {
            //リスト内にremoveIDがあれば削除
            player1[menberID].edge.RemoveAll(s => s == removeID);
            player1[removeID].edge.RemoveAll(s => s == menberID);
        }
        else
        {
            //リスト内にremoveIDがあれば削除
            player2[menberID].edge.RemoveAll(s => s == removeID);
            player2[removeID].edge.RemoveAll(s => s == menberID);
        }
    }

    //自分含めたチーム全員のIDを返す
    int[] TeamAllID(bool friend, int myID)
    {
        int idCount = 0;
        int[] id = new int[idCount];

        if (friend)
        {
            idCount = player1[myID].edge.Count + 1;
            id[0] = player1[myID].ID;

            for (int i = 1; i < idCount; i++)
            {
                id[i] = player1[myID].edge[i];
            }
        }
        else
        {
            idCount = player2[myID].edge.Count + 1;
            id[0] = player2[myID].ID;

            for (int i = 1; i < idCount; i++)
            {
                id[i] = player2[myID].edge[i];
            }
        }

        return id;
    }

    //自分以外のチーム全員のIDを返す
    public int[] TeamID(bool friend, int myID)
    {
        int idCount = 0;
        int[] id = new int[idCount];

        if (friend)
        {
            idCount = player1[myID].edge.Count;
            for (int i = 0; i < idCount; i++)
            {
                id[i] = player1[myID].edge[i];
            }
        }
        else
        {
            idCount = player2[myID].edge.Count;
            for (int i = 0; i < idCount; i++)
            {
                id[i] = player2[myID].edge[i];
            }
        }

        return id;
    }

    //チームメンバーの数
    public int TeamMember(bool friend, int myID)
    {
        int num = 0;

        //+1は自分の分
        if (friend)
        {
            num = player1[myID].edge.Count + 1;
        }
        else
        {
            num = player2[myID].edge.Count + 1;
        }

        return num;
    }

    //チームの中に自分が居ればfalseを返す
    public bool checkTeamID(bool friend, int myID,int otherID)
    {
        int idCount = 0;
        int[] id = TeamID(friend, myID);

        if (friend)
        {
            idCount = player1[myID].edge.Count;
            for (int i = 0; i < idCount; i++)
            {
                if (player1[myID].edge[i] == otherID) return false;
            }
        }
        else
        {
            idCount = player2[myID].edge.Count;
            for (int i = 0; i < idCount; i++)
            {
                if (player2[myID].edge[i] == otherID) return false;
            }
        }

        return true;
    }

    //チームの合計数値を返す
    public int TeamSumNumber(bool friend, int myID)
    {
        int idCount = 0;
        int[] id = TeamID(friend, myID);
        int sumNumber = 0;

        if (friend)
        {
            idCount = player1[myID].edge.Count;
            sumNumber = player1[myID].Number;
            //チームメンバーがいる場合
            if (idCount != 0)
            {
                for (int i = 0; i < idCount; i++)
                {
                    //合計値に足していく
                    sumNumber += player1[id[i]].Number;
                }
            }
        }
        else
        {
            idCount = player2[myID].edge.Count;
            sumNumber = player2[myID].Number;
            //チームメンバーがいる場合
            if (idCount != 0)
            {
                for (int i = 0; i < idCount; i++)
                {
                    //合計値に足していく
                    sumNumber += player2[id[i]].Number;
                }
            }
        }

        return sumNumber;
    }

    //チームの中で一番数値の低いプレイヤーIDを返す
    public int LowPlayerID(bool friend, int myID)
    {
        int lowPlayer = 0;      //返すプレイヤーID
        int tmpNum = 10;        //大きさ一時変数
        int idCount = 0;
        int[] id = new int[idCount];

        id = TeamID(friend, myID);

        idCount = player1[myID].edge.Count;
        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が一時変数より低い時
            if (tmpNum > player1[id[i]].Number)
            {
                //一時変数を更新
                tmpNum = player1[id[i]].Number;
                //返すプレイヤーIDも更新
                lowPlayer = player1[id[i]].ID;
            }
        }

        return lowPlayer;
    }

    //チームの中で一番数値の低いプレイヤー数値を返す
    int LowPlayerNumber(bool friend, int myID)
    {
        int lowNum = 10;        //返す数値変数
        int idCount = player1[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(friend, myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が数値変数より低い時
            if (lowNum > player1[id[i]].Number)
            {
                //数値変数を更新
                lowNum = player1[id[i]].Number;
            }
        }

        return lowNum;
    }

    //チームの中で一番数値の低いプレイヤーが複数ある場合trueを返す
    bool MultiLowNumber(bool friend, int myID)
    {
        int count = 0;      //複数判定変数
        int lowNumber = LowPlayerNumber(friend, myID);      //チーム内で一番低い数値
        int idCount = player1[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(friend, myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が一番低い数値なら
            if(lowNumber == player1[id[i]].Number)
            {
                count++;
            }
        }

        //countが2以上ならtrue0
        if (count > 1) return true;
        return false;
    }

    //チームの中で一番数値の低い かつ HPの低いプレイヤーIDを返す
    int HPLowPlayerNumber(bool friend, int myID)
    {
        int lowNum = 0;         //返す用数値変数
        int lowHP = 1000;       //HP用一時変数
        int lowNumber = LowPlayerNumber(friend, myID);      //チーム内で一番低い数値
        int idCount = player1[myID].edge.Count;
        int[] id = new int[idCount];

        id = TeamID(friend, myID);

        for (int i = 0; i < idCount; i++)
        {
            //プレイヤー数値が一番低い数値なら
            if (lowNumber == player1[id[i]].Number)
            {
                //HPが一時変数より低ければ
                if(lowHP > player1[id[i]].HP)
                {
                    //HP変数を更新
                    lowHP = player1[id[i]].HP;
                    //数値変数を更新
                    lowNum = player1[id[i]].ID;
                }
            }
        }

        return lowNum;
    }

    //チーム内の一人が戦闘状態になればチーム全員を戦闘状態にする
    //チームメンバーのBATTLE_STEPをBATTLEにする


}
