using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static int score;
    public Text ScorePoint;

    public static int Spade = 0;
    public static int Haret = 0;
    public static int Clover = 0;
    public static int Dia = 0;
    public static int Hosi = 0;
    public static int Kinoko = 0;
    public static int Nico = 0;



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {


        ScorePoint.text = "スコア" + score;

        if (Spade == 2) { score += 1; Spade += 1; }
        if (Haret == 2) { score += 1; Haret += 1; }
        if (Clover == 2) { score += 1; Clover += 1; }
        if (Dia == 2) { score += 1; Dia += 1; }
        if (Hosi == 2) { score += 1; Hosi += 1; }
        if (Kinoko == 2) { score += 1; Kinoko += 1; }
        if (Nico == 2) { score += 1; Nico += 1; }
    }
}
