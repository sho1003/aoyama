using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickScript : MonoBehaviour {

    enum HowManyTimes
    {
        first,
        second,
        third
    }
    HowManyTimes times;

    const int PICK_SUM = 10;
    const int PICK_TIMES = 3;
    const int CARD_MAX = 7;

    int[] leftpick = new int[PICK_TIMES];
    int[] rightpick = new int[PICK_TIMES];
    int[] pickcard = new int[CARD_MAX];
    public void LeftClick()
    {

    }

    public void RightClick()
    {

    }

    // Use this for initialization
    void Start()
    {
        times = HowManyTimes.first;

        //数字決定
        //switch (times)
        //{
        //    case HowManyTimes.first:
        //        Num2RandomPick(leftpick[0], leftpick[1]);
        //        Num2RandomPick(rightpick[0], rightpick[1]);
        //        break;
        //    case HowManyTimes.second:
        //        Num2RandomPick(leftpick[0], leftpick[1]);
        //        Num2RandomPick(rightpick[0], rightpick[1]);
        //        break;
        //    case HowManyTimes.third:
        //        Num3RandomPick(leftpick[0], leftpick[1], leftpick[2]);
        //        Num3RandomPick(rightpick[0], rightpick[1], rightpick[2]);
        //        break;
        //}

        //数字の組み合わせが同じかチェック
        OverlapCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rightpick[0]);
    }

    //int型の関数で値を返す
    //void Num2RandomPick(int pick0, int pick1)
    //{
    //    pick0 = Random.Range(1, 9);
    //    pick1 = PICK_SUM - pick0;
    //}

    //void Num3RandomPick(int pick0, int pick1, int pick2)
    //{
    //    pick0 = Random.Range(1, 8);
    //    pick1 = Random.Range(1, 9 - pick0);
    //    pick2 = PICK_SUM - pick1;
    //}

    void OverlapCheck()
    {
        if (leftpick[0] == rightpick[0])
        {
            //Num2RandomPick(rightpick[0], rightpick[1]);
            OverlapCheck();
        }
        else times++;
    }

}
