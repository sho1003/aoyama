using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZinScript : MonoBehaviour
{
    public bool Openflag;
    public Texture[] textures;
    private int frameNum;
    public float timeleft;
    public bool timeflag;
    public bool VSflag;
    public int Colorflag;// Colorflag [1]青　[2]赤



    // Use this for initialization
    void Start()
    {
        frameNum = 0;    // frameNum[0]占拠なし　[1]青　[2]赤
        timeleft = 0.0f;
        VSflag = false;//戦闘しているかのフラグ
        Openflag=true;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer ren = gameObject.GetComponent<Renderer>();

        if (timeflag == true) timeleft -= Time.deltaTime;
        if (VSflag == true) timeleft = 0.0f;//戦闘中はタイムリセットストップ

        if (Openflag == true && timeleft <= -3.0f)
        {
            if (Colorflag == 1/*青*/) frameNum = 1;
            if (Colorflag == 2/*赤*/) frameNum = 2;

        }

        ren.material.SetTexture("_MainTex", textures[frameNum]);

    }

    void OnCollisionStay(Collision other)
    //        void OnTriggerStay(Collider other)
    {
        if (VSflag == false)
        {
            timeflag = true;
            if (other.gameObject.tag == /*"ao"*/"Enemy") Colorflag = 1;
            if (other.gameObject.tag == /*"aka"*/"Player") Colorflag = 2;


        }
    }

}
