<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour {

    public Camera rotateCamera;

    public GameObject player;

	// Use this for initialization
	void Start () {
        rotateCamera = Camera.main;

    }

    // Update is called once per frame
    void Update () {
        transform.rotation = rotateCamera.transform.rotation;

        transform.position = player.transform.position + new Vector3(0, 5, 0);

    }

    void Disable()
    {
        this.gameObject.SetActive(false);

    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour {

    public Camera rotateCamera;

    public GameObject player;

	// Use this for initialization
	void Start () {
        rotateCamera = Camera.main;

    }

    // Update is called once per frame
    void Update () {
        transform.rotation = rotateCamera.transform.rotation;

        transform.position = player.transform.position + new Vector3(0, 5, 0);

    }

    void Disable()
    {
        this.gameObject.SetActive(false);

    }
}
>>>>>>> 2281a693ef68a1adfd1f5b3ac451a5e02cdffb1b
