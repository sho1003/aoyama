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
        Skilltime = status.Skilltime;

    }

    // Update is called once per frame
    void Update()
    {
        if (SkillScript.Skill == true)
        {
            Skilltime = Skilltime - Time.deltaTime;
            Suutitext.enabled = true;

            if (Skilltime < 0) SkillScript.Skill = false;
        }
            
        if (SkillScript.Skill == false)
        {
            Suutitext.enabled = false;
            Skilltime = status.Skilltime;
        }

        if (Input.GetKeyDown(KeyCode.X)) SkillScript.Skill = true;//デバック用
        if (Input.GetKeyDown(KeyCode.Z)) SkillScript.Skill = false;//デバック用



    }

}

