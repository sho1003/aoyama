using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    static int PLAYER_MAX = 7;

    GameObject[] p1 = new GameObject[PLAYER_MAX];
    GameObject[] p2 = new GameObject[PLAYER_MAX];
    player1_script[] player1 = new player1_script[PLAYER_MAX];
    player2_script[] player2 = new player2_script[PLAYER_MAX];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PLAYER_MAX; i++)
        {
            p1[i] = GameObject.Find("player1_" + i);
            p2[i] = GameObject.Find("player2_" + i);

            player1[i] = p1[i].GetComponent<player1_script>();
            player2[i] = p2[i].GetComponent<player2_script>();
        }
    }

    //bool friendはtagがPlayer1の場合true

    //チームを組む
    public void connectTeam(bool friend, int myID, int addID)
    {
        List<int> myIDteam = TeamAllID(friend, myID);
        List<int> addIDteam = TeamAllID(friend, addID);

        if (friend)
        {
            //myIDにaddIDを入れる
            player1[myID].edge.Add(addID);
            //addIDにmyIDを入れる
            player1[addID].edge.Add(myID);
            //myIDにチームメンバーが居るなら
            if (myIDteam.Count != 0)
            {
                //myIDのチームメンバーにaddIDを入れる
                for (int i = 0; i < myIDteam.Count; i++)
                {
                    player1[myIDteam[i]].edge.Add(addID);
                }
            }
            //addIDにチームメンバーが居るなら
            if (addIDteam.Count != 0)
            {
                //addIDのチームメンバーにmyIDを入れて
                //myIDにaddIDのチームメンバーを入れる
                for (int i = 0; i < addIDteam.Count; i++)
                {
                    player1[addIDteam[i]].edge.Add(myID);
                    player1[myID].edge.Add(addIDteam[i]);
                }
            }
        }
        else
        {
            //myIDにaddIDを入れる
            player2[myID].edge.Add(addID);
            //addIDにmyIDを入れる
            player2[addID].edge.Add(myID);
            //myIDにチームメンバーが居るなら
            if (myIDteam.Count != 0)
            {
                //myIDのチームメンバーにaddIDを入れる
                for (int i = 0; i < myIDteam.Count; i++)
                {
                    player2[myIDteam[i]].edge.Add(addID);
                }
            }
            //addIDにチームメンバーが居るなら
            if (addIDteam.Count != 0)
            {
                //addIDのチームメンバーにmyIDを入れて
                //myIDにaddIDのチームメンバーを入れる
                for (int i = 0; i < addIDteam.Count; i++)
                {
                    player2[addIDteam[i]].edge.Add(myID);
                    player2[myID].edge.Add(addIDteam[i]);
                }
            }
        }
    }

    //チームから外れる時の処理
    public void RemoveTeam(bool friend, int removeID)
    {
        int[] id = TeamID(friend, removeID);

        if (friend)
        {
            for (int i = 0; i < id.Length; i++)
            {
                player1[id[i]].edge.RemoveAll(s => s == removeID);
            }
            player1[removeID].edge.Clear();
        }
        else
        {
            for (int i = 0; i < id.Length; i++)
            {
                player2[id[i]].edge.RemoveAll(s => s == removeID);
            }
            player2[removeID].edge.Clear();
        }
    }

    //自分が所属しているチーム全員のIDを返す
    public List<int> TeamAllID(bool friend, int myID)
    {
        List<int> list = new List<int>();

        //myIDのチームメンバー取得
        GetEdge(TeamID(friend, myID), list, myID);

        RecursionEdge(list, friend, myID);

        return list;
    }

    void RecursionEdge(List<int> list, bool friend, int myID)
    {
        int id = list.Count;

        for (int i = 0; i < id; i++)
        {
            //チームメンバーを取得
            GetEdge(TeamID(friend, list[i]), list, myID);
        }

        //新しいチームメンバーが居る限りループ
        if (id != list.Count) RecursionEdge(list, friend, myID);
    }

    void GetEdge(int[] id, List<int> list, int myID)
    {
        //myIDにチームメンバーが居るなら
        if (id.Length != 0)
        {
            for (int i = 0; i < id.Length; i++)
            {
                list.Add(id[i]);
            }
        }

        //listの中身のmyIDを削除
        list.RemoveAll(s => s == myID);
        //listの中身で重複しているIDを削除
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[i] == list[j])
                {
                    list.RemoveAt(j);
                }
            }
        }
    }

    //自分と繋がっているIDのみを返す
    public int[] TeamID(bool friend, int myID)
    {
        int idCount = 0;
        int[] id;

        if (friend)
        {
            idCount = player1[myID].edge.Count;
            id = new int[idCount];

            //myIDと繋がっているIDを取得
            for (int i = 0; i < idCount; i++)
            {
                id[i] = player1[myID].edge[i];
            }

        }
        else
        {
            idCount = player2[myID].edge.Count;
            id = new int[idCount];

            for (int i = 0; i < idCount; i++)
            {
                id[i] = player2[myID].edge[i];
            }
        }

        return id;
    }

    //自分を含んだ自分と繋がっているIDを返す
    public int[] IncludeMyTeamID(bool friend, int myID)
    {
        int idCount = 0;
        int[] id;

        if (friend)
        {
            idCount = player1[myID].edge.Count + 1;
            id = new int[idCount];
            //myIDを入れる
            id[0] = player1[myID].ID;
            //myIDと繋がっているIDを取得
            for (int i = 1; i < idCount; i++)
            {
                id[i] = player1[myID].edge[i - 1];
            }

        }
        else
        {
            idCount = player2[myID].edge.Count + 1;
            id = new int[idCount];
            //myIDを入れる
            id[0] = player2[myID].ID;
            //myIDと繋がっているIDを取得
            for (int i = 1; i < idCount; i++)
            {
                id[i] = player2[myID].edge[i - 1];
            }
        }

        return id;
    }

    //チームの中で一番zの値が小さければtrueを返す
    public bool checkZ(bool friend, int myID)
    {
        int[] id = TeamID(friend, myID);

        if (friend)
        {
            //チームメンバーがいる場合
            if (id.Length != 0)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    //myIDの方がzが大きい場合
                    if (player1[myID].transform.position.z > player1[id[i]].transform.position.z)
                    {
                        return false;
                    }
                }
            }
        }
        else
        {
            //チームメンバーがいる場合
            if (id.Length != 0)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    //myIDの方がzが大きい場合
                    if (player2[myID].transform.position.z > player2[id[i]].transform.position.z)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    //チームの中に自分が居ればfalseを返す
    public bool checkTeamID(bool friend, int myID, int otherID)
    {
        if (friend)
        {
            for (int i = 0; i < player1[myID].edge.Count; i++)
            {
                if (player1[myID].edge[i] == otherID) return false;
            }
        }
        else
        {
            for (int i = 0; i < player2[myID].edge.Count; i++)
            {
                if (player2[myID].edge[i] == otherID) return false;
            }
        }

        return true;
    }

    //チームの合計数値を返す
    public int TeamSumNumber(bool friend, int myID)
    {
        int[] id = TeamID(friend, myID);
        int sumNumber = 0;

        if (friend)
        {
            sumNumber = player1[myID].Number;
            //チームメンバーがいる場合
            if (id.Length != 0)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    //合計値に足していく
                    sumNumber += player1[id[i]].Number;
                }
            }
        }
        else
        {
            sumNumber = player2[myID].Number;
            //チームメンバーがいる場合
            if (id.Length != 0)
            {
                for (int i = 0; i < id.Length; i++)
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
        int[] id = IncludeMyTeamID(friend, myID);

        if (friend)
        {
            for (int i = 0; i < id.Length; i++)
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
        }
        else
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一時変数より低い時
                if (tmpNum > player2[id[i]].Number)
                {
                    //一時変数を更新
                    tmpNum = player2[id[i]].Number;
                    //返すプレイヤーIDも更新
                    lowPlayer = player2[id[i]].ID;
                }
            }
        }

        return lowPlayer;
    }

    //チームの中で一番数値の低いプレイヤー数値を返す
    int LowPlayerNumber(bool friend, int myID)
    {
        int lowNum = 10;        //返す数値変数
        int[] id = IncludeMyTeamID(friend, myID);

        if (friend)
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が数値変数より低い時
                if (lowNum > player1[id[i]].Number)
                {
                    //数値変数を更新
                    lowNum = player1[id[i]].Number;
                }
            }
        }
        else
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が数値変数より低い時
                if (lowNum > player2[id[i]].Number)
                {
                    //数値変数を更新
                    lowNum = player2[id[i]].Number;
                }
            }
        }

        return lowNum;
    }

    //チームの中で一番数値の低いプレイヤーが複数ある場合trueを返す
    public bool MultiLowNumber(bool friend, int myID)
    {
        int count = 0;      //複数判定変数
        int lowNumber = LowPlayerNumber(friend, myID);      //チーム内で一番低い数値
        int[] id = IncludeMyTeamID(friend, myID);

        if (friend)
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一番低い数値なら
                if (lowNumber == player1[id[i]].Number)
                {
                    count++;
                }
            }
        }
        else
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一番低い数値なら
                if (lowNumber == player2[id[i]].Number)
                {
                    count++;
                }
            }
        }

        //countが2以上ならtrue
        if (count >= 2) return true;
        return false;
    }

    //チームの中で一番数値の低い かつ HPの低いプレイヤーIDを返す
    public int HPLowPlayerID(bool friend, int myID)
    {
        int lowNum = 0;         //返す用数値変数
        int lowHP = 1000;       //HP用一時変数
        int lowNumber = LowPlayerNumber(friend, myID);      //チーム内で一番低い数値
        int[] id = IncludeMyTeamID(friend, myID);

        if (friend)
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一番低い数値なら
                if (lowNumber == player1[id[i]].Number)
                {
                    //HPが一時変数より低ければ
                    if (lowHP > player1[id[i]].HP)
                    {
                        //HP変数を更新
                        lowHP = player1[id[i]].HP;
                        //数値変数を更新
                        lowNum = player1[id[i]].ID;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一番低い数値なら
                if (lowNumber == player2[id[i]].Number)
                {
                    //HPが一時変数より低ければ
                    if (lowHP > player2[id[i]].HP)
                    {
                        //HP変数を更新
                        lowHP = player2[id[i]].HP;
                        //数値変数を更新
                        lowNum = player2[id[i]].ID;
                    }
                }
            }
        }

        return lowNum;
    }

    //チームの中で一番数値の低いプレイヤーが複数ある場合trueを返す
    public bool MultiLowNumberLowHP(bool friend, int myID)
    {
        int count = 0;      //複数判定変数
        int tmpHP = 10;        //低いHPかどうか判定用
        int lowNumber = LowPlayerNumber(friend, myID);      //チーム内で一番低い数値
        int[] id = IncludeMyTeamID(friend, myID);

        if (friend)
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一番低い数値なら
                if (lowNumber == player1[id[i]].Number)
                {
                    //一時変数よりHPが少ないなら
                    if (player1[id[i]].HP < tmpHP)
                    {
                        tmpHP = player1[id[i]].HP;
                    }
                    //一時変数とHPが同じなら
                    else if (player1[id[i]].HP == tmpHP)
                    {
                        count++;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < id.Length; i++)
            {
                //プレイヤー数値が一番低い数値なら
                if (lowNumber == player2[id[i]].Number)
                {
                    //一時変数よりHPが少ないなら
                    if (player2[id[i]].HP < tmpHP)
                    {
                        tmpHP = player2[id[i]].HP;
                    }
                    //一時変数とHPが同じなら
                    else if (player2[id[i]].HP == tmpHP)
                    {
                        count++;
                    }
                }
            }
        }

        //countが2以上ならtrue
        if (count >= 2) return true;
        return false;
    }

    //チーム内の一人が戦闘状態になればチーム全員を戦闘状態にする
    //戦闘状態になるべきならtrueを返す
    public bool TeamBattle(bool friend, int myID)
    {
        int[] id = TeamID(friend, myID);

        //チームメンバーが居るなら
        if (id.Length != 0)
        {
            for (int i = 0; i < id.Length; i++)
            {
                BattleScript battle = null;
                if (friend)
                {
                    battle = p1[id[i]].GetComponent<BattleScript>();
                }
                else
                {
                    battle = p2[id[i]].GetComponent<BattleScript>();
                }
                //myIDのチームメンバーが戦闘状態ならtrue
                if (battle.GetStep() == BattleScript.BATTLE_STEP.BATTLE) return true;
            }
        }
        return false;
    }

}
