using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Move_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1f, 0, 0);
    }
}
