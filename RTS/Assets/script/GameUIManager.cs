using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour
{

    private player_script player;

    public Image GreenGage;
    public Image RedGage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_script>();
        this.initParameter();
    }

    void Update()
    {
        GreenGage.fillAmount = player.HP / player.maxHP;
    }

    private void initParameter()
    {
        GreenGage = GameObject.Find("GreenGage").GetComponent<Image>();
        GreenGage.fillAmount = 1;

        RedGage = GameObject.Find("RedGage").GetComponent<Image>();
        RedGage.fillAmount = 1;
    }
}
