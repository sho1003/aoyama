using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour
{

    private player_script player;

    public Image lifeGage;
    public Image lifeRedGage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_script>();
        this.initParameter();
    }

    void Update()
    {
        lifeGage.fillAmount = player.HP - player.maxHP;
    }

    private void initParameter()
    {
        lifeGage = GameObject.Find("GreenGage").GetComponent<Image>();
        lifeGage.fillAmount = 1;

        lifeRedGage = GameObject.Find("RedGage").GetComponent<Image>();
        lifeRedGage.fillAmount = 1;
    }
}
