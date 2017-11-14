using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life1_script : MonoBehaviour
{

    public Slider slider;
    // private player_script player;
    public GameObject gameobject;



    //    private float level;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
          // slider.value -= 0.1f;

     
          int GageHP1 = gameobject.GetComponent<player1_script>().HP;
          slider.value = GageHP1;
        

     


    }
}
