using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleScript : MonoBehaviour {

    public Animator anim;
    private AnimatorStateInfo info;
    // Use this for initialization
    public float time;  // 0~1 → 0％～100％
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        info = anim.GetCurrentAnimatorStateInfo(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (time <= 0) time = 0;
        if (time >= 1) time = 1;


        if (Input.GetKey(KeyCode.G))
        {
            time += 0.01f;
            anim.Play(info.shortNameHash, -1, time);
        }

        if (Input.GetKey(KeyCode.H))
        {
            time -= 0.01f;
            anim.Play(info.shortNameHash, -1, time);
        }
    }
}
