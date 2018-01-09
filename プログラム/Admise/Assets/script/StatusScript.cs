using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　数値調整用のスクリプト
public class StatusScript : MonoBehaviour {

    //　キャラの数値
    public int CharaHP = 10;
    //　キャラの数値(ゲージ)
    public int maxValue = 20;
    //　キャラの移動速度の基礎値
    public float CharBaseSpeed = 4.5f;
    //　キャラの移動速度微調整用(ただ、全体に反映されるのでキャラの数値ごとにいじりたい場合は計算式をいじる)
    public float CharMainteSpeed = 0.0f;
    //　リスポーン時間(フレーム)(60で約1秒)
    public float RespawnTime = 60;
    //　オブジェクトの最大数
    public static int OBJECT_MAX = 7;
    //　戦闘が起こる敵との距離幅
    public float BattleSpace = 3.0f;
    //　エリアに乗ってるとそのエリアを獲得できる時間
    public float GetAreaTime = 3.0f;
    //　初期位置設定
    public Vector3[] FirstPosition = new Vector3[OBJECT_MAX];
    
    //　ゲーム時間
    public float GameTime = 100;


    //　スキル1の発動回数
    public int Skill1Count = 2;

    //　スキル2の発動回数
    public int Skill2Count = 2;

    //　スキル3の発動回数
    public int Skill3Count = 2;

    //　スキル3の発動時間
    public float Skill1time = 1f;

    //　スキル3の発動時間
    public float Skill3time = 10f;

    //　リスポーンタイム短縮エリアの時間設定
    public float RespownTimeAreaStage1 = 0.9f;
    public float RespownTimeAreaStage2 = 0.75f;
    public float RespownTimeAreaStage3 = 0.5f;
    

    //　移動速度アップエリアのスピード設定
    public float SpeedAreaStage1 = 1.1f;
    public float SpeedAreaStage2 = 1.25f;
    public float SpeedAreaStage3 = 1.5f;

    //　デバッグ用
    public int AreaNum = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
