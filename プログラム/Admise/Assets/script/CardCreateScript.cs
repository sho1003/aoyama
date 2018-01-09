using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreateScript : MonoBehaviour
{

    private StatusScript status;
    public GameObject gameobject;
    public int N;//生成ナンバー
   
    public GameObject _parent;
    public bool Flags = true;
    public Vector3 POS;


    // Use this for initialization
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();

        _parent = transform.root.gameObject;

        if (transform.root == GameObject.Find("player1_0").transform) N = 1;
        else if (transform.root == GameObject.Find("player1_1").transform) N = 2;
        else if (transform.root == GameObject.Find("player1_2").transform) N = 3;
        else if (transform.root == GameObject.Find("player1_3").transform) N = 4;
        else if (transform.root == GameObject.Find("player1_4").transform) N = 5;
        else if (transform.root == GameObject.Find("player1_5").transform) N = 6;
        else if (transform.root == GameObject.Find("player1_6").transform) N = 7;

        POS = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        transform.parent = GameObject.Find("OUtINtuibi").transform;

        //  親オブジェクトを取得
        if (Flags==true)
        {
            POS.x = (POS.x + 140)+ 190 * (N-1);
            transform.position = POS;
            Flags = false;
        }
    }

}
