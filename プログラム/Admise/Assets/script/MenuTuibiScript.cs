﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTuibiScript : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = target.transform.position;
       
    }
}
