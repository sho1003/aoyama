using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float timeleft;
    public float timeleft2;
    public bool flag;
    public bool Openflag;
    public bool Closeflag;
    public int Stayflag=0;
    public bool CardCloseflag;


    // Use this for initialization
    void Start()
    {
        timeleft = 0.0f;
        timeleft2 = 0.0f;
        flag = false;
        Openflag = false;
        Closeflag = false;
        // Stayflag = false;
        CardCloseflag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (flag == true) timeleft -= Time.deltaTime;
        if (Closeflag == true) timeleft2 -= Time.deltaTime;

        Renderer ren = gameObject.GetComponent<Renderer>();

        

        

        

        //マーク表示後〇秒で非表示
        if (timeleft2 <= -5.0f)// && )
        {
            ren.enabled = false;
            if (gameObject.tag == "ura") ren.enabled = true;

            //カードが非表示になったらa
            if (CardCloseflag == true)
            {
                if (gameObject.tag == "spade") Score.Spade += -1;
                if (gameObject.tag == "haret") Score.Haret += -1;
                if (gameObject.tag == "clover") Score.Clover += -1;
                if (gameObject.tag == "dia") Score.Dia += -1;
                if (gameObject.tag == "hosi") Score.Hosi += -1;
                if (gameObject.tag == "kinoko") Score.Kinoko += -1;
                if (gameObject.tag == "nico") Score.Nico += -1;
                Closeflag = false;
                CardCloseflag = false;
            }

        }
 

        //カードに接触状態でタイムが3秒経過でカードが変わる
        if (Openflag == true && timeleft <= -3.0f )
        {
            ren.enabled = true;
            if (gameObject.tag == "ura") ren.enabled = false;

            if (CardCloseflag == false)
            {
                //タグ毎に表示されているかの判定
                if (gameObject.tag == "spade") Score.Spade += 1;
                if (gameObject.tag == "heart") Score.Haret += 1;
                if (gameObject.tag == "clover") Score.Clover += 1;
                if (gameObject.tag == "dia") Score.Dia += 1;
                if (gameObject.tag == "hosi") Score.Hosi += 1;
                if (gameObject.tag == "kinoko") Score.Kinoko += 1;
                if (gameObject.tag == "nico") Score.Nico += 1;
                CardCloseflag = true;

            }
            Openflag = false;//判定が1回で終わるように
            //if (Stayflag < 2 && Closeflag == false)
            //{
            //    //タグ毎に表示されているかの判定
            //    if (gameObject.tag == "spade") Score.Spade += -1;
            //    if (gameObject.tag == "haret") Score.Haret += -1;
            //    if (gameObject.tag == "clover") Score.Clover += -1;
            //    if (gameObject.tag == "dia") Score.Dia += -1;
            //    if (gameObject.tag == "hosi") Score.Hosi += -1;
            //    if (gameObject.tag == "kinoko") Score.Kinoko += -1;
            //    if (gameObject.tag == "nico") Score.Nico += -1;

            //}
            //Openflag = false;//判定が1回で終わるように

            //カードが変わるとタイムを止める
            flag = false;
        }
        //カードが揃うと消す
        if (Score.Spade > 2) if (gameObject.tag == "spade") Destroy(gameObject);
        if (Score.Haret > 2) if (gameObject.tag == "heart") Destroy(gameObject);
        if (Score.Clover > 2) if (gameObject.tag == "clover") Destroy(gameObject);
        if (Score.Dia > 2) if (gameObject.tag == "dia") Destroy(gameObject);
        if (Score.Hosi > 2) if (gameObject.tag == "hosi") Destroy(gameObject);
        if (Score.Kinoko > 2) if (gameObject.tag == "kinoko") Destroy(gameObject);
        if (Score.Nico > 2) if (gameObject.tag == "nico") Destroy(gameObject);


    }

    
    void OnTriggerEnter(Collider other)
    {
       
        //プレイヤーがカードにぶつかるとタイムが０になり動き出す
        if (other.gameObject.tag == "Player") if (Stayflag < 2) timeleft = 0.0f;
        Stayflag += 1;
        if(Stayflag < 2)Openflag = true;
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            flag = true;
            timeleft2 = 0.0f;
        //    Closeflag = false;
        }
    }

           
    void OnTriggerExit(Collider other)
    {
        Stayflag -= 1;
        //プレイヤーがカードから離れるとタイムが止まる
        timeleft2 = 0.0f;
         Closeflag = true;

        if (Stayflag < 1)Openflag = false;
      
    }
}
