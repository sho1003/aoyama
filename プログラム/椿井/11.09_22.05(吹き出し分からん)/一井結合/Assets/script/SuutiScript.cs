using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuutiScript : MonoBehaviour {

    private player_script player;
    public GameObject target;
    private int suuti;
    public Text Suutitext;
    public GameObject tuibi;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = tuibi.transform.position;

        if (target.gameObject.tag == "Player")
        {
            player = GameObject.Find(target.gameObject.transform.name).GetComponent<player_script>();
            //  HP -= player.PlayerATK;//攻撃で体力が減少

        suuti = player.Number;
        Suutitext.text = ""+suuti ;
    }


    }

}
