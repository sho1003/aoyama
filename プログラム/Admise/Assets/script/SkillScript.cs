using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillScript : MonoBehaviour
{
    StatusScript status;
    public static bool Skill;//スキル発動しているかどうかの判定
    public Canvas ren; //スキル選択時カードのレイヤーの順番
    public static bool Skill1END = false; //スキル1終了の瞬間の判定
    public static bool Skill2END = false; //スキル2終了の瞬間の判定
    public static bool Skill3END = false; //スキル3終了の瞬間の判定
 



    // Use this for initialization
    void Start()
    {
        //  呼び込み
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        Skill = false;

    }

    // Update is called once per frame
    void Update()
    {
        EventSystem.current.IsPointerOverGameObject();

        if (Skill1END == true && gameObject.name == "skill1") //スキル1終了
        {
            transform.localScale = new Vector3(0.52f, 1.2f, 1);
            this.ren.sortingOrder = 1;
            Skill1END = false;
            //移動もどす
        }

        if (Skill2END == true && gameObject.name == "skill2") //スキル2終了
        {
            transform.localScale = new Vector3(0.52f, 1.2f, 1);
            this.ren.sortingOrder = 2;
            Skill2END = false;
            //移動もどす
        }


        if (Skill3END == true && gameObject.name == "skill3") //スキル3終了
        {
            transform.localScale = new Vector3(0.52f, 1.2f, 1);
            this.ren.sortingOrder = 3;
            Skill3END = false;
            //移動もどす
            if (status.Skill3Count == 0) gameObject.GetComponent<GraphicRaycaster>().enabled = false;
        }
    }


    public void Skills()//カードの上にマウスがある間
    {
        if (Skill == false)//カードが選ばれている間は反応しない
        {
            this.ren.sortingOrder = 4;
            transform.localScale = new Vector3(0.7f, 1.5f, 1);
        }
            if (gameObject.name == "skill1") { }
            if (gameObject.name == "skill2") { }
            if (gameObject.name == "skill3") { }
    }

    public void SkillsOff()//カードからマウスが離れたら
    {
        if (Skill == false)//カードが選ばれている間は反応しない
        {
            if (gameObject.name == "skill1") { this.ren.sortingOrder = 1; }
            if (gameObject.name == "skill2") { this.ren.sortingOrder = 2; }
            if (gameObject.name == "skill3") { this.ren.sortingOrder = 3; }

            transform.localScale = new Vector3(0.52f, 1.2f, 1);

        }

        

    }

    public void Skill1()//カード1をクリックしたら
    {

        if (Skill == false && status.Skill1Count > 0)
        {
            Skill = true;//カードが選んだ
            status.Skill1Count = status.Skill1Count - 1;//スキル1の発動回数1減少  
        }
    }


    public void Skill2()//カード2をクリックしたら
    {

        if (Skill == false && status.Skill2Count > 0)
        {
            Skill = true;//カードが選んだ
            status.Skill2Count = status.Skill2Count - 1;//スキル2の発動回数1減少  
        }
    }


    public void Skill3()//カードをクリックしたら
    {

        if (Skill == false && status.Skill3Count > 0)
        {
            Skill = true;//カードが選んだ
            status.Skill3Count = status.Skill3Count - 1;//スキル３の発動回数1減少  
        }
    }

}