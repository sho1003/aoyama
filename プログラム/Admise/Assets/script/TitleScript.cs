using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//--------------------------------------------------------------------------------------------------------------//
//
//  タイトル設定
//
//--------------------------------------------------------------------------------------------------------------//

public class TitleScript : MonoBehaviour {

    //  タイトルBGM
    private AudioSource bgm;
    public AudioClip clipTitleBGM;
    //  ピッチ
    public  float pitch;

    //  ローディングテキスト
    public Text loadingText;
    //  ローディングバー
    public Image loadingBar;

	void Start () {
        bgm = this.gameObject.GetComponent<AudioSource>();
        bgm.PlayOneShot(clipTitleBGM);
	}

    void Update()
    {
        bgm.pitch = pitch;
    }

    //--------------------------------------------------------------------------------------------------------------//
    //  スタートボタン処理
    //--------------------------------------------------------------------------------------------------------------//
    //public void StartButtonClick()
    //{
    //    //  ローディングへ移行
    //    StartCoroutine("Loading");
    //}
}
