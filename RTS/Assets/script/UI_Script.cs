using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{

    public Camera rotateCamera;
    public GameObject player;
    Vector3 offset;

    RectTransform myRectTrans;
    RectTransform childRectTrans;

    // Use this for initialization
    void Start()
    {
        rotateCamera = Camera.main;

        myRectTrans = GetComponent<RectTransform>();
        childRectTrans = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotateCamera.transform.rotation;

        OffsetAdjustment();

        var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTrans, screenPos, Camera.main, out localPos);
        childRectTrans.localPosition = localPos;
    }

    void OffsetAdjustment()
    {
        offset.x = -player.transform.position.x / 20;
        offset.y = 1.5f;
        offset.z = -player.transform.position.z / 20;
    }

    void Disable()
    {
        this.gameObject.SetActive(false);
    }

}
