using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaMark : MonoBehaviour {

    public Texture[] textures;
    public int frameNum=0; 
    public GameObject parent;

    // Use this for initialization

    void Start()
    {
        parent = transform.parent.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Renderer ren = gameObject.GetComponent<Renderer>();

       // if () { frameNum = 0; }//なし
        if (parent.gameObject.tag == "Area1") { frameNum = 1; }//移動速度
        if (parent.gameObject.tag == "Area2") { frameNum = 2; }//リスポーン
        if (parent.gameObject.tag == "Area3") { frameNum = 3; }//攻撃速度
        

        ren.material.SetTexture("_MainTex", textures[frameNum]);

        float scroll = Mathf.Repeat(Time.time * -0.1f, 1);
        Vector2 offset = new Vector2(0, scroll);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);

    }





    }

