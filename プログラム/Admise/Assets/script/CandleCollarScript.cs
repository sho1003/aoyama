using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleCollarScript : MonoBehaviour {


    public Texture[] textures;
    public int frameNum = 0;
    public AreaScript AreaCollre;
    public GameObject BlueSenkyoflag;
    private StatusScript status;
    public bool Whiteflag = true;

    // Use this for initialization
    void Start () {
        frameNum = 0;
        status = GameObject.Find("Status").GetComponent<StatusScript>();
    }
	
	// Update is called once per frame
	void Update () {

        Renderer ren = gameObject.GetComponent<Renderer>();

        
        if (Whiteflag == true) { frameNum = 0; }//白色
        else if (AreaCollre.Redflag == true && AreaCollre.timeleft > 0) { frameNum = 1; }//赤色ろうそく
        else if (AreaCollre.Blueflag == true && AreaCollre.timeleft < 0) { frameNum = 2; }//青色


        ren.material.SetTexture("_MainTex", textures[frameNum]);

    }
}
