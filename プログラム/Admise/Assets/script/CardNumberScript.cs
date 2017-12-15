using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardNumberScript : MonoBehaviour {

    private StatusScript status;
    public GameObject gameobject;
    public Sprite[] CardNumber;
    public int PlayerNumber = 0;
    public Sprite sprite;

    // Use this for initialization
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        PlayerNumber = gameobject.GetComponent<player1_script>().Number;

    }

    // Update is called once per frame
    void Update()
    {
       Image image = this.GetComponent<Image>();
       image.sprite = sprite;
       sprite = CardNumber[PlayerNumber];
       
        
    }
}