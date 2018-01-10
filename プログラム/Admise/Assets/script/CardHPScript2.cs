using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHPScript2 : MonoBehaviour
{

    private StatusScript status;
    public GameObject gameobject;
    public float CardHPs; //メニュー用ＨＰカード1つ分の数値
    public float CardHP; //「CardHPs」を保存しておくボックス
    public float PlayerHP;//初期ＨＰを入れるボックス
    public int i = 0;
    

    // Use this for initialization
    void Start()
    {
      
        float GageHP1 = gameobject.GetComponent<player1_script>().HP;
        CardHPs = GageHP1 / 10;
        CardHP = CardHPs;
        PlayerHP = GageHP1;
    }

    // Update is called once per frame
    void Update()
    {



        status = GameObject.Find("Status").GetComponent<StatusScript>();

        float GageHP1 = gameobject.GetComponent<player1_script>().HP;

        if (GageHP1 <= 0)
        {
            CardHPs = CardHP;
            i = 0;
            gameObject.GetComponent<Image>().enabled = true;
   
        }

       else if (GageHP1 <= PlayerHP - CardHPs)
        {

            CardHPs = CardHPs + CardHP;
            
            if (this.name == "Green" + (10 - i)) gameObject.GetComponent<Image>().enabled = false;
            if (this.name == "Red" + (10 - i)) gameObject.GetComponent<Image>().enabled = false;
            i++;
        }

     }
}
