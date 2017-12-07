using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour {

    private AudioSource se;
    public AudioClip enterSE;

	// Use this for initialization
	void Start () {
        //  コンポーネント取得
        se = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
    public void OnClick()
    {
        se.PlayOneShot(enterSE);
       UnityEngine.SceneManagement.SceneManager.LoadScene("2pick");
    }
}
