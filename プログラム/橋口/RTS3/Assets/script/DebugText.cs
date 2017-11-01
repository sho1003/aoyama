using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    public Text _text = null;
    static DebugText instace = null;

    void Awake()
    {
        instace = this;
    }

    static public void AddDebugText( string str )
    {
        if( instace )instace.AddText(str);
    }

    public void AddText( string str )
    {
        if( _text )_text.text += str + "\n";
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
