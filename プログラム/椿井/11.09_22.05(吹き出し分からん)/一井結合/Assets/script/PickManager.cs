using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class PickManager : MonoBehaviour
{
    public GameObject CardPrefab;
    Text timesDisplay;

    enum HowManyTimes
    {
        first,
        second,
        third
    }
    HowManyTimes times;

    const int PICK_SUM = 10;
    const int CARD_MAX = 7;
    const int MAX_GET_NUM = 3;

    int[] leftpick = new int[CARD_MAX];
    int[] rightpick = new int[CARD_MAX];
    static int[] pickcard = new int[CARD_MAX];

    Vector3[] leftcardPos = new Vector3[MAX_GET_NUM];
    Vector3[] rightcardPos = new Vector3[MAX_GET_NUM];

    //trueならカード生成
    bool form;

    //trueならleftを、falseならrightを入れる
    public void SetPickCard(bool flg)
    {
        int loopMax = 0;
        switch (times)
        {
            case HowManyTimes.first:
            case HowManyTimes.second:
                loopMax = 2;
                break;
            case HowManyTimes.third:
                loopMax = 3;
                break;
        }

        //pickcardに選ばれた数字を入れる
        if (flg)
        {
            for (int i = (int)times * 2; i < loopMax + (int)times * 2; i++)
            {
                pickcard[i] = leftpick[i];
            }
        }
        else
        {
            for (int i = (int)times * 2; i < loopMax + (int)times * 2; i++)
            {
                pickcard[i] = rightpick[i];
            }
        }

        times++;
        form = true;
    }

    // Use this for initialization
    void Start()
    {
        timesDisplay = GameObject.Find("HowmanyTimes").GetComponent<Text>();

        times = HowManyTimes.first;
        form = true;

        leftcardPos[0] = new Vector3(275, 150, 0);
        leftcardPos[1] = new Vector3(125, 150, 0);
        leftcardPos[2] = new Vector3(200, 300, 0);

        rightcardPos[0] = new Vector3(475, 150, 0);
        rightcardPos[1] = new Vector3(625, 150, 0);
        rightcardPos[2] = new Vector3(550, 300, 0);

        while (times <= HowManyTimes.third)
        {
            //数字決定
            switch (times)
            {
                case HowManyTimes.first:
                case HowManyTimes.second:
                    Num2Random(true);
                    Num2Random(false);
                    break;
                case HowManyTimes.third:
                    Num3Random(true);
                    Num3Random(false);
                    break;
            }

            //数字の組み合わせが同じかチェック
            SameNumCheck();

            times++;
        }

        times = HowManyTimes.first;
    }

    // Update is called once per frame
    void Update()
    {

        //for (int i = 0; i < CARD_MAX; i++)
        //{
        //    Debug.Log(i + "pickcard" + pickcard[i]);
        //}

        //formがtrueならカード生成
        if (form)
        {
            form = false;

            //生成したカードを消去
            var clones = GameObject.FindGameObjectsWithTag("Card");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }

            string howmanytimes = "";
            int loopMax = 0;
            switch (times)
            {
                case HowManyTimes.first:
                    howmanytimes = "１回目";
                    loopMax = 2;
                    break;
                case HowManyTimes.second:
                    howmanytimes = "２回目";
                    loopMax = 2;
                    break;
                case HowManyTimes.third:
                    howmanytimes = "３回目";
                    loopMax = 3;
                    break;
            }

            timesDisplay.text = howmanytimes;

            for (int i = 0; i < loopMax; i++)
            {
                CardForm(true, i, (int)times * 2 + i);
                CardForm(false, i, (int)times * 2 + i);
            }
        }

        //3回カードを選んだ時
        if (times > HowManyTimes.third)
        {
            for (int i = 0; i < CARD_MAX/*6*/; i++)
            {
                //プレイヤーの数字を渡す
                PlayerPrefs.SetInt("PlayerNum" + i, pickcard[i]);
            }
            //シーン遷移(また後で
            UnityEngine.SceneManagement.SceneManager.LoadScene("test");
        }
    }

    //controlがtrueならleftpick
    void Num2Random(bool control)
    {
        int decide = (int)times * 2;

        if (control)
        {
            leftpick[decide] = Random.Range(1, 9);
            leftpick[decide + 1] = PICK_SUM - leftpick[decide];
        }
        else
        {
            rightpick[decide] = Random.Range(1, 9);
            rightpick[decide + 1] = PICK_SUM - rightpick[decide];
        }
    }

    //controlがtrueならleftpick
    void Num3Random(bool control)
    {
        int decide = (int)times * 2;

        if (control)
        {
            leftpick[decide] = Random.Range(1, 8);
            leftpick[decide + 1] = Random.Range(1, 9 - leftpick[decide]);
            leftpick[decide + 2] = PICK_SUM - leftpick[decide] - leftpick[decide + 1];
        }
        else
        {
            rightpick[decide] = Random.Range(1, 8);
            rightpick[decide + 1] = Random.Range(1, 9 - rightpick[decide]);
            rightpick[decide + 2] = PICK_SUM - rightpick[decide] - rightpick[decide + 1];
        }
    }

    void SameNumCheck()
    {
        int check = (int)times * 2;

        switch (times)
        {
            case HowManyTimes.first:
            case HowManyTimes.second:
                //0番目同士が同じ数字か確認                 //0と1番目が同じ数字か確認
                if (leftpick[check] == rightpick[check] || leftpick[check] == rightpick[check + 1])
                {
                    //同じならleftの数字を変更
                    Num2Random(true);

                    //またチェック
                    SameNumCheck();
                }
                break;
            case HowManyTimes.third:
                //0番目同士と1番目同士が同じ数字か確認
                if (leftpick[check] == rightpick[check] || leftpick[check + 1] == rightpick[check + 1])
                {
                    Num3Random(true);

                    //またチェック
                    SameNumCheck();
                }
                break;
        }
    }

    //controlがtrueならleftpick
    void CardForm(bool control, int posnum, int picknum)
    {
        GameObject obj;
        if (control)
        {
            obj = Instantiate(CardPrefab, leftcardPos[posnum], Quaternion.identity, GameObject.Find("Canvas").transform);
            obj.transform.GetChild(0).GetComponent<Text>().text = "" + leftpick[picknum];
        }
        else
        {
            obj = Instantiate(CardPrefab, rightcardPos[posnum], Quaternion.identity, GameObject.Find("Canvas").transform);
            obj.transform.GetChild(0).GetComponent<Text>().text = "" + rightpick[picknum];
        }
    }

}
