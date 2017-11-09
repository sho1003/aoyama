﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech_UIscript : MonoBehaviour {

    const float GET_LENGTH = 5.0f;

    public Vector3 offset;
    Camera rotateCamera;
    GameObject player;

    RectTransform myRectTrans;
    RectTransform SpeechBalloon;
    GameObject hukidashi;

    // Use this for initialization
    void Start()
    {
        rotateCamera = Camera.main;
        player = transform.root.gameObject;

        myRectTrans = GetComponent<RectTransform>();
        SpeechBalloon = transform.GetChild(1).GetComponent<RectTransform>();

        hukidashi = transform.Find("SpeechBalloon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotateCamera.transform.rotation;

        //一番近いプレイヤーを取得
        GameObject otherplayer = SearchTag(transform.gameObject, "Player");
        //プレイヤーとのベクトルを取得
        Vector3 vec = otherplayer.transform.position - player.transform.position;
        //絶対値を取る
        float Dis = Mathf.Abs(vec.magnitude);
        //プレイヤーとの距離が一定以下(今は５)なら
        if (Dis <= GET_LENGTH)
        {
            //Debug.Log("近い");

            if (vec.z < 0)
            {
                hukidashi.SetActive(false);
            }

            offset.x = vec.x / 2;
            offset.y = 0.5f;
            offset.z = 2.0f;
        }
        else
        {
            //Debug.Log("遠い");
            hukidashi.SetActive(true);
            //プレイヤーの位置によって微調整
            OffsetAdjustment();
        }

        //ワールド座標をスクリーン座標に変換
        var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTrans, screenPos, Camera.main, out localPos);
        SpeechBalloon.localPosition = localPos;

    }

    void OffsetAdjustment()
    {
        offset.x = 0;
        offset.y = 1.0f;
        offset.z = 2.0f;
    }

    //指定されたタグの中で距離が一番近いものを取得
    GameObject SearchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;               //距離用一時変数
        float nearDis = 0.1f;           //最も近いオブジェクトの距離
        float farDis = 100.0f;          //最も遠い距離
        GameObject targetObj = null;    //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //取得したオブジェクトと自身の距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離がnearDis以上farDis以下ならばオブジェクトを取得
            if (nearDis < tmpDis && tmpDis < farDis)
            {
                //一時変数に距離を格納
                nearDis = tmpDis;
                targetObj = obs;
            }
        }

        //オブジェクトを返す
        return targetObj;
    }

    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
