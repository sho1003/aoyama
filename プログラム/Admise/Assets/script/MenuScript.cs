using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public Vector3 inPosition;        // スライドイン後の位置
    public Vector3 keepPosition;      // スライドの固定
    public Vector3 outPosition;      // スライドアウト後の位置
    public float duration = 1.0f;    // スライド時間（秒）
    public bool MenuIn = false;
    public float OutInTime = 0.8f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if (OutInTime < 1) OutInTime += Time.deltaTime;
        if (OutInTime >= 1) OutInTime = 1;
    }

    // スライドイン（ボタンが押されたときに、これを呼ぶ）
    public void SlideIn()
    {
        if(OutInTime == duration)StartCoroutine(StartSlidePanel(true));
        
    }


    private IEnumerator StartSlidePanel(bool isSlideIn)
    {
        float startTime = Time.time;    // 開始時間
        //メニューボタンを押した後の待機時間
        Vector3 startPos = transform.localPosition;  // 開始位置
        Vector3 moveDistance;            // 移動距離および方向

  

        if (isSlideIn && MenuIn == false)
        { 
            OutInTime = 0;
            moveDistance = (inPosition);
            MenuIn = true;
        }

        else if(isSlideIn && MenuIn == true)
        {
            OutInTime = 0;
            moveDistance = (outPosition);
            MenuIn = false;
        }

        else
        {
            moveDistance = (keepPosition);
        }

            while ((Time.time - startTime) < duration)
            {
                transform.localPosition = startPos + moveDistance * animCurve.Evaluate((Time.time - startTime) / duration);
                yield return 0;        // 1フレーム後、再開
            }
 
             transform.localPosition = startPos + moveDistance;

    }
}