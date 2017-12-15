using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFlameScript : MonoBehaviour {

    public static GameObject CardFlame;
    private StatusScript status;
    public int PlayerNumber = 0;
    public GameObject gameobject;
    public GameObject _parent;
    public int N;//生成ナンバー

    // Use this for initialization
    void Start ()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        PlayerNumber = gameobject.GetComponent<player1_script>().Number;

        _parent = transform.root.gameObject;

        if (transform.root == GameObject.Find("player1_0").transform) N = 1;
        else if (transform.root == GameObject.Find("player1_1").transform) N = 2;
        else if (transform.root == GameObject.Find("player1_2").transform) N = 3;
        else if (transform.root == GameObject.Find("player1_3").transform) N = 4;
        else if (transform.root == GameObject.Find("player1_4").transform) N = 5;
        else if (transform.root == GameObject.Find("player1_5").transform) N = 6;
        else if (transform.root == GameObject.Find("player1_6").transform) N = 7;
    }

    // Update is called once per frame
    void Update()
    {
        Image image = this.GetComponent<Image>();


        if (SceneManagerScript.CardChoise == true && SceneManagerScript.CardFlame == N-1)
        {
            //キャラ選択中にカード枠表示する
            image.enabled = true;
        }

        if (SceneManagerScript.CardChoise == false)
        {
            //キャラ選択中にカード枠表示する
            image.enabled = false;

        }
    }
}
