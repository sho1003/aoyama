using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_UIscript : MonoBehaviour {

    Camera rotateCamera;
    GameObject player;
    Vector3 offset;

    RectTransform myRectTrans;
    RectTransform playerHP;

    // Use this for initialization
    void Start()
    {
        rotateCamera = Camera.main;
        player = transform.root.gameObject;

        myRectTrans = GetComponent<RectTransform>();
        playerHP = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotateCamera.transform.rotation;

        //プレイヤーの位置によって微調整
        OffsetAdjustment();

        //ワールド座標をスクリーン座標に変換
        var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTrans, screenPos, Camera.main, out localPos);
        playerHP.localPosition = localPos;
    }

    void OffsetAdjustment()
    {
        offset.x = -player.transform.position.x / 20;
        offset.y = 4.0f;
        offset.z = -player.transform.position.z / 20;
    }

    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
