using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGM_Script : MonoBehaviour {

    private StatusScript status;

    //  ゲームBGM
    private AudioSource bgm;
    //  クリップ挿入用
    public AudioClip clipGameBGM;

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
        bgm = this.gameObject.GetComponent<AudioSource>();
        bgm.clip = clipGameBGM;
        bgm.Play();
    }

    void Update()
    {
        bgm.pitch = pitch;
        //  設定時間にピッチ変更
        if (status.GameTime <= pitchChengeTime)
        {
            pitch = pitchChenge;
        }
    }
}
