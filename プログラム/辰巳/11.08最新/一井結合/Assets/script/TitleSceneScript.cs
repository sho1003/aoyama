using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    public void OnClick()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene("test");
    }
}
