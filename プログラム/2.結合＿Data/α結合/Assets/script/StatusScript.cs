using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//　数値調整用のスクリプト
public class StatusScript : MonoBehaviour {

    //　キャラの数値
    public int CharaHP = 100;
    //　キャラの移動速度の基礎値
    public float CharBaseSpeed = 4.5f;
    //　キャラの移動速度微調整用(ただ、全体に反映されるのでキャラの数値ごとにいじりたい場合は計算式をいじる)
    public float CharMainteSpeed = 0.0f;
    //　リスポーン時間(フレーム)(60で約1秒)
    public int RespawnTime = 60;
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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
