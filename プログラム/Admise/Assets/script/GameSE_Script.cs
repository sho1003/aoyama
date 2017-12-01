using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSE_Script : MonoBehaviour {

    //  AudioSource
    private AudioSource se;

    //  SE一覧取得用
    public AudioClip areaGetSE;
    public AudioClip areaLostSE;
    public AudioClip battleSE;
    public AudioClip cancelSE;
    public AudioClip clockSE;
    public AudioClip diedSE;
    public AudioClip enterSE;

	void Start () {
        //  コンポーネント取得
        se = this.gameObject.GetComponent<AudioSource>();
	}

    //  SE実行用関数
    public void SetSE(AudioClip setSE)
    {
        se.PlayOneShot(setSE);
    }
}
