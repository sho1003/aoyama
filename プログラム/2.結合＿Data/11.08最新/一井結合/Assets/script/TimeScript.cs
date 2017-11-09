using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeScript : MonoBehaviour
{
    public enum Winner
    {
        RED,
        BLUE,
        DROW
    }

    public float time;
    public Text Timetext;

    private int JudgeFlag;
    private ScoreScript score;

    // Use this for initiailzation
    void Start()
    {
        JudgeFlag = 0;
        //time = 10.0f;
        score = GameObject.Find("Score").GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        Timetext.text = "   残り" + time.ToString("f0") + "秒";

        if (time <= 0)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            time = 0;
            //↓リザルト判定
            if (score.RedScore > score.BlueScore)
                //　赤の勝利
                JudgeFlag = (int)Winner.RED;
            else if (score.RedScore < score.BlueScore)
                //　青の勝利
                JudgeFlag = (int)Winner.BLUE;
            else
                //　引き分け
                JudgeFlag = (int)Winner.DROW;

            PlayerPrefs.SetInt("JudgeFlag", JudgeFlag);
        }

    }

}
