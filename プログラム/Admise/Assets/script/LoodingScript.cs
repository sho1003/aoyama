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

public class LoodingScript : MonoBehaviour {

    //  タイトルBGM
    private AudioSource bgm;
    public AudioClip clipTitleBGM;



    //  ローディングテキスト
    public Text loadingText;
    //  ローディングバー
    public Image loadingBar;

    void Awake()
    {
    }

	void Start () {

        bgm = this.gameObject.GetComponent<AudioSource>();
        bgm.PlayOneShot(clipTitleBGM);
	}


    //--------------------------------------------------------------------------------------------------------------//
    //  ローディング処理
    //--------------------------------------------------------------------------------------------------------------//
    //IEnumerator Loading()
    //{
    //    //  ローディング時に一時停止されていないことを確認
    //    if (Time.timeScale == 0) Time.timeScale = 1;
    //    //  非同期でロードを開始
    //    AsyncOperation async = SceneManager.LoadSceneAsync("Game");
    //    //  ロード完了時にシーン切り替えが勝手にされないようにする(デフォルトはtrue)
    //    async.allowSceneActivation = false;
    //    //  ロード状態が90%以上になるまでの処理
    //    while (async.progress < 0.9f)
    //    {
    //        //  テキストでローディングの％を表示
    //        loadingText.text = (async.progress * 100).ToString("F0") + "%";
    //        //  バー表示
    //        loadingBar.fillAmount = async.progress;
    //        //  スクリーン上のレンダリング終了まで待機
    //        yield return new WaitForEndOfFrame();
    //    }
    //    //  100%にならない(なぜか) ので100%に上げる
    //    loadingText.text = "100%";
    //    loadingBar.fillAmount = 1;
    //    //  指定した秒数の間だけコルーチンの実行待機
    //    yield return new WaitForSeconds(1);
    //    //  シーンを切り替えを許可
    //    async.allowSceneActivation = true;
    //}
}
