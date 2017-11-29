using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicScript : MonoBehaviour {

    private StatusScript status;

    //  ゲームBGM
    private AudioSource[] bgm;
    //  クリップ挿入用
    public AudioClip clipGameBGM;

    public AudioClip SE_Click;
    //  ピッチ
    public float pitch;
    //  ピッチ変更
    public float pitchChenge;
    //  残り時間
    public float pitchChengeTime;

    void Start()
    {

        //  スクリプト取得
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        //  オーディオコンポーネント取得
        for (int i = 0; i < 2; i++) bgm[i] = gameObject.GetComponent<AudioSource>();
        
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            bgm[0].clip = clipGameBGM;
            bgm[0].Play();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            bgm[1].clip = SE_Click;
            bgm[1].Play();
        }
        //bgm[0].pitch = pitch;
        ////  設定時間にピッチ変更
        //if (status.GameTime <= pitchChengeTime)
        //{
        //    pitch = pitchChenge;
        //}
    }
}
