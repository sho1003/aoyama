﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {


    bool Player1Click;
    bool Player2Click;

    player1_script player1;
    player2_script player2;



    // Use this for initialization
    void Start()
    {

        Player1Click = false;
        Player2Click = false;

        //player_ = GameObject.Find("Cube").GetComponent<player_script>();
    }

    private string Player1Tag = "Player1";
    private string Player2Tag = "Player2";

    // Update is called once per frame  
    void Update()
    {

        if (!Player1Click)
        {
            //一回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == Player1Tag)
                    {
                        Player1Click = true;
                        //レイピックを飛ばして当たったオブジェクトの名前を取ってくる(Findの中身)
                        player1 = GameObject.Find(hit.transform.gameObject.name).GetComponent<player1_script>();
                    }
                }
            }
        }
        else
        {
            //二回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    //移動させる
                    player1.Pointer_Click();
                    Player1Click = false;
                }
            }
        }
        if (!Player2Click)
        {
            //一回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == Player2Tag)
                    {
                        Player2Click = true;
                        //レイピックを飛ばして当たったオブジェクトの名前を取ってくる(Findの中身)
                        player2 = GameObject.Find(hit.transform.gameObject.name).GetComponent<player2_script>();
                    }
                }
            }
        }
        else
        {
            //二回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    //移動させる
                    player2.Pointer_Click();
                    Player2Click = false;
                }
            }
        }

    }

}
