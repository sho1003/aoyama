using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{

    public Text text;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("JudgeFlag") ==
            (int)TimeScript.Winner.RED)
        {
            text.text = "赤の勝利！！";
            text.color = Color.red;
        }
        else if (PlayerPrefs.GetInt("JudgeFlag") ==
            (int)TimeScript.Winner.BLUE)
        {
            text.text = "青の勝利！！";
            text.color = Color.blue;
        }
        else
        {
            text.text = "引き分け！！";
            text.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

}