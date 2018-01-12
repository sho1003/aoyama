using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float value = 1.0f;

    Vector3 BasePos;
    Vector3 BaseRot;

    Vector3 mousePos;
    bool GetKeyDown;
    // Use this for initialization
    void Start()
    {
        BasePos = transform.position;
        BaseRot = transform.eulerAngles;

        mousePos = Vector3.zero;
        GetKeyDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        //位置・回転リセット
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            transform.position = BasePos;
            transform.eulerAngles = BaseRot;
        }
        //前
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, value);
        }
        //後
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -value);
        }
        //左
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(value, 0, 0);
        }
        //右
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-value, 0, 0);
        }
        //上
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.position += new Vector3(0, value, 0);
        }
        //下
        else if (Input.GetKey(KeyCode.E))
        {
            transform.position += new Vector3(0, -value, 0);
        }
    }
}
