using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour {


    public Text RedScorePoint;
    public Text BlueScorePoint;

    public int RedScore = 0;
    public int BlueScore = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RedScorePoint.text = "× " + RedScore;
        BlueScorePoint.text = "× " + BlueScore;
        RedScore = ZinScript.RedScore;
        BlueScore = ZinScript.BlueScore;
    }
}
