using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life_script : MonoBehaviour
{

    public Slider slider;
    // private player_script player;

    public static int GageHP; //ゲージ用体力

    //    private float level;

    // Use this for initialization
    void Start()
    {
        //slider.value = 100;//体力
        //player = GetComponent<player_script>();

        //      level = slider.value;

    }

    // Update is called once per frame
    void Update()
    {
        // slider.value -= 0.1f;

        slider.value = GageHP;
        if (slider.value == 0)
        {
           // Destroy(transform.parent.parent.parent.gameObject);//とりあえずプレイヤ消す

        }
    }
}
