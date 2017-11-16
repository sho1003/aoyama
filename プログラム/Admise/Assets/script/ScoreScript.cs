using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour {


    public Text RedScorePoint;
    public Text BlueScorePoint;

    public int RedScore = 0;
    public int BlueScore = 0;
    // Use this for initialization

    /*-------------------FPS(フレームレート)関連-------------------*/

    //  テキスト
    public Text fpsText;
    //  速度カウント
    private int frameCount;
    //  タイマー設定
    private float nextTime;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RedScorePoint.text = "× " + RedScore;
        BlueScorePoint.text = "× " + BlueScore;
        RedScore = ZinScript.RedScore;
        BlueScore = ZinScript.BlueScore;

        //  FPS表示
        frameRate();
    }

    //-------------------------------------------------------------------------------------------------------------------------
    //  FPS(フレームレート)
    //-------------------------------------------------------------------------------------------------------------------------
    void frameRate()
    {
        //  速度チェック
        frameCount++;
        //  タイマーとの比較
        if (Time.time >= nextTime)
        {
            //  テキストへ速度を入れる
            fpsText.text = "FPS : " + frameCount;
            //  ループ設定
            frameCount = 0;
            nextTime += 1;
        }
    }
}
