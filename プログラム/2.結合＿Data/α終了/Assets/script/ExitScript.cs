using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
           Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Space)) { }
	}

    
}
