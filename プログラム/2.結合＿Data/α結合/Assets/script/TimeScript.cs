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

    public Text Timetext;

    private int JudgeFlag;
    private StatusScript status;
    private ScoreScript score;

    // Use this for initiailzation
    void Start()
    {
        JudgeFlag = 0;
        //time = 10.0f;
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        score = GameObject.Find("Score").GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {

        status.GameTime -= Time.deltaTime;
        Timetext.text = "   残り" + status.GameTime.ToString("f0") + "秒";

        if (status.GameTime <= 0)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            status.GameTime = 0;
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
