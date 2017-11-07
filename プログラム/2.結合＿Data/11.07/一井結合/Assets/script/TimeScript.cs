using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{

    public float time;
    public Text Timetext;

    // Use this for initiailzation
    void Start()
    {


        //time = 10.0f;

    }

    // Update is called once per frame
    void Update()
    {

        time -= Time.deltaTime;
        Timetext.text = "   残り" + time.ToString("f0") + "秒";

        if (time <= 0)
        {

            Application.LoadLevel("Result");
            time = 0;
            //↓リザルト判定

        }

    }
}
