using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillScript : MonoBehaviour
{
    StatusScript status;
    public static bool Skill;
    //public float Skilltime;//


    // Use this for initialization
    void Start()
    {
        //  呼び込み
        status = GameObject.Find("Status").GetComponent<StatusScript>();


    }

    // Update is called once per frame
    void Update()
    {
        EventSystem.current.IsPointerOverGameObject();

    }

    public void Skills()
    {
        if (Skill == false && status.SkillCount > 0)
        {
            Skill = true;
            status.SkillCount = status.SkillCount - 1;
        }
    }
}
