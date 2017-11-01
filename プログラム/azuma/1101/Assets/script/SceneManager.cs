using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {


    bool CubeClick;
    player_script player_;



    // Use this for initialization
    void Start()
    {
        CubeClick = false;
        
        //player_ = GameObject.Find("Cube").GetComponent<player_script>();
    }

    public string cubeTag = "Player";

    // Update is called once per frame  
    void Update()
    {

        if (!CubeClick)
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
                    if (hit.transform.tag == cubeTag)
                    {
                        CubeClick = true;
                        Debug.Log("押した");
                        //レイピックを飛ばして当たったオブジェクトの名前を取ってくる(Findの中身)
                        player_ = GameObject.Find(hit.transform.gameObject.name).GetComponent<player_script>();
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
                    player_.Pointer_Click();
                    CubeClick = false;
                }
            }
        }

    }
}
