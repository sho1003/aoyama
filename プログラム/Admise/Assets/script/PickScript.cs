using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickScript : MonoBehaviour {

    PickManager pick;

    AudioSource se;
    public AudioClip enterSE;

    // Use this for initialization
    void Start()
    {
        se = this.gameObject.GetComponent<AudioSource>();
        pick = GameObject.Find("PickManager").GetComponent<PickManager>();
    }

    public void LeftClick()
    {
        se.PlayOneShot(enterSE);
        pick.SetPickCard(true);
    }

    public void RightClick()
    {
        se.PlayOneShot(enterSE);
        pick.SetPickCard(false);
    }

}
