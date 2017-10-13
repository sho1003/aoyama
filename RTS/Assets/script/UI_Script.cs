using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{

    public Camera rotateCamera;
    public GameObject player;
    public Vector3 offset = Vector3.zero;

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

        //transform.position = player.transform.position + new Vector3(0,5,0);

        var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTrans, screenPos, Camera.main, out localPos);
        childRectTrans.localPosition = localPos;
    }

    void Disable()
    {
        this.gameObject.SetActive(false);

    }

}
