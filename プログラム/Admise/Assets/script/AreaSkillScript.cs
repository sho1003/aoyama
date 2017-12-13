using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkillScript : MonoBehaviour {

    private StatusScript status;
    private float speed;

	// Use this for initialization
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //　エリアをひとつ取ると移動速度があがる
    public float MoveSpeedUp(float CharSpeed,int AreaNum)
    {
        speed = CharSpeed;
        //　スピードアップエリアを取った数に応じてスピードが変わる
        switch(AreaNum)
        {
            case 0:
                break;
            case 1:
                speed = CharSpeed * status.SpeedAreaStage1;
                break;
            case 2:
                speed = CharSpeed * status.SpeedAreaStage2;
                break;
            case 3:
                speed = CharSpeed * status.SpeedAreaStage3;
                break;
        }
        return speed;
    }

    //　エリアをひとつ取るとリスポーンタイムが短くなる
    public float RTimeShort(float RespownTime,int AreaNum)
    {
        float respown = RespownTime;
        //　リスポーンタイム短縮エリアの数に応じてリスポーン時間が短くなる
        switch (AreaNum)
        {
            case 0:
                break;
            case 1:
                respown = RespownTime *　status.RespownTimeAreaStage1;
                break;
            case 2:
                respown = RespownTime * status.RespownTimeAreaStage2;
                break;
            case 3:
                respown = RespownTime * status.RespownTimeAreaStage3;
                break;
        }
        return respown;
    }
}
