using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicScript : MonoBehaviour {

    private StatusScript status;

    //  タイトルBGM
    private AudioSource bgm;
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
        //  音楽再生
        bgm.PlayOneShot(clipGameBGM);
    }

    void Update()
    {
        bgm.pitch = pitch;

        if (status.GameTime <= pitchChengeTime)
        {
            pitch = pitchChenge;
        }
    }
}
