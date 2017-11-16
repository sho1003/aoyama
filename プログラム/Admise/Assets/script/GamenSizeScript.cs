using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamenSizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.fullScreen = !Screen.fullScreen;
    }
	
	// Update is called once per frame
	void Update () {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
}
