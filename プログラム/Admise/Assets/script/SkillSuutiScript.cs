using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSuutiScript : MonoBehaviour
{

    public Text Suutitext;
    public float Skilltime;
    StatusScript status;

    // Use this for initialization
    void Start()
    {
           Suutitext.enabled = false;

        //  呼び込み
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        Skilltime = status.Skill3time;

    }

    // Update is called once per frame
    void Update()
    {
        if (SkillScript.Skill == true)
        {
            Skilltime = Skilltime - Time.deltaTime;//代入した時間分スキル発動
            Suutitext.enabled = true;

            if (Skilltime < 0)
            {
                SkillScript.Skill = false;
                SkillScript.Skill3END = true; 
            }

        }
            

        if (SkillScript.Skill == false)
        {
            Suutitext.enabled = false;
            Skilltime = status.Skill3time;
        }

        if (Input.GetKeyDown(KeyCode.X)) SkillScript.Skill = true;//デバック用
        if (Input.GetKeyDown(KeyCode.Z)) SkillScript.Skill = false;//デバック用



    }

}

