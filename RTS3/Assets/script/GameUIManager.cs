<<<<<<< HEAD
﻿using UnityEngine;
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
=======
﻿using UnityEngine;
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
>>>>>>> 2281a693ef68a1adfd1f5b3ac451a5e02cdffb1b
