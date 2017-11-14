using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickScript : MonoBehaviour {

    PickManager pick;

    // Use this for initialization
    void Start()
    {
        pick = GameObject.Find("PickManager").GetComponent<PickManager>();
    }

    public void LeftClick()
    {
        pick.SetPickCard(true);
    }

    public void RightClick()
    {
        pick.SetPickCard(false);
    }

}
