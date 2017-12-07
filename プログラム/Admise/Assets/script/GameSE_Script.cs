using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSE_Script : MonoBehaviour {

    //  AudioSource
     [System.NonSerialized]
    public AudioSource se;
    //  スクリプト
    private StatusScript status;

    //  SE一覧取得用
    public AudioClip areaGetSE;
    public AudioClip areaLostSE;
    public AudioClip battleSE;
    public AudioClip cancelSE;
    public AudioClip clockSE;
    public AudioClip diedSE;
    public AudioClip enterSE;
    public AudioClip StartSE;
    public AudioClip TimeUpSE;
    public AudioClip GassenSE;

    public AudioClip choiceSE;

    //  
    public bool flgSE = true;
    private bool timeUpEnd = false;
    private bool gassenEnd = false;
    //private int stepSE = -1;

	void Start () {
        //  コンポーネント取得
        se = this.gameObject.GetComponent<AudioSource>();
        //  スクリプト取得
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        //  スタートSE
        se.PlayOneShot(StartSE);
	}

    //  SE実行用関数
    public void SetSE1(AudioClip setSE)
    {
        //  基本これ
        se.PlayOneShot(setSE);
        
    }

    //  呼び出しづらいやつ用←
    public void SetSE2(AudioClip setSE)
    {
        //  タイムアップ
        if (setSE == TimeUpSE && status.GameTime <= 5 && timeUpEnd == false)
        {
            se.PlayOneShot(TimeUpSE);
            timeUpEnd = true;
        }
        //  合戦中
        if (setSE == GassenSE && gassenEnd == false)
        {
            se.PlayOneShot(GassenSE);
            gassenEnd = true;
        }

    }

    void Update()
    {
        //  タイムアップ
        SetSE2(TimeUpSE);

    }
}
