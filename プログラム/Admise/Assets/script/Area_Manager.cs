using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Manager : MonoBehaviour {

    private StatusScript status;
    private GameSE_Script se;

    //定義したオブジェクトの箱をn個作る
    static int Area_MAX = 5;

    //座標位置を指定する箱をn個作る
    Vector3[] pos = new Vector3[Area_MAX];

    // Use this for initialization
    void Start () {

        status = GameObject.Find("Status").GetComponent<StatusScript>();
        se = GameObject.Find("Sounds/SE").GetComponent<GameSE_Script>();

        for (int i = 0; i < Area_MAX; i++)
        {
            SetArea(i);
           
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    void SetArea(int i)
    {



    }




}
