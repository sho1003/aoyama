using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour
{
    StatusScript status;
    private player1_script player;

    public Image lifeGage;
    public Image lifeRedGage;

    void Start()
    {
        status = GameObject.Find("Status").GetComponent<StatusScript>();
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<player1_script>();
        this.initParameter();
    }

    void Update()
    {
        lifeGage.fillAmount = player.HP - status.CharaHP;
    }

    private void initParameter()
    {
        lifeGage = GameObject.Find("GreenGage").GetComponent<Image>();
        lifeGage.fillAmount = 1;

        lifeRedGage = GameObject.Find("RedGage").GetComponent<Image>();
        lifeRedGage.fillAmount = 1;
    }
}
