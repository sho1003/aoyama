using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area_Manager : MonoBehaviour
{

    private StatusScript status;
    private GameSE_Script se;
    public GameObject[] Areas;
    public int AreaNum;
    public static int Area1 = 0;
    public static int Area2 = 0;
    public static int Area3 = 0;
    public static bool AreaRandom = true;
    public static bool Areashuffle = false;
    public int AreaStorage1;
    public int AreaStorage2;
    public int AreaStorage3;


    //定義したオブジェクトの箱をn個作る
    static int Area_MAX = 5;


    // Use this for initialization
    void Start()
    {

        status = GameObject.Find("Status").GetComponent<StatusScript>();
        se = GameObject.Find("Sounds/SE").GetComponent<GameSE_Script>();
        Areashuffle = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            Areashuffle = true;
            Debug.Log("あ");
        }


        if (Areashuffle == true)
        {

            if (AreaStorage1 == Area1 && AreaStorage2 == Area2 && AreaStorage3 == Area3)
            {
                Areashuffle = false;
                Area1 = 0;//エリアリセット
                Area2 = 0;
                Area3 = 0;
            }


            else
            {

                Area1 = 0;//エリア揃わないリセット
                Area2 = 0;
                Area3 = 0;

                for (int i = 0; i < Area_MAX; i++)
                {

                    AreaNum = Random.Range(1, 4);
                    Areas[i].gameObject.tag = "Area" + AreaNum;

                }

            }

        }


        if (AreaRandom == true && Area1 > 0 && Area2 > 0 && Area3 > 0)
        {
            AreaStorage1 = Area1;//エリア数保存
            AreaStorage2 = Area2;
            AreaStorage3 = Area3;
            Area1 = 0;//エリアリセット
            Area2 = 0;
            Area3 = 0;
            AreaRandom = false;

        }

        if (AreaRandom == true)
        {
            Area1 = 0;//エリア揃わないリセット
            Area2 = 0;
            Area3 = 0;

            for (int i = 0; i < Area_MAX; i++)
            {

                AreaNum = Random.Range(1, 4);
                Areas[i].gameObject.tag = "Area" + AreaNum;

            }


        }
       

    }

}