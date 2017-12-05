using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public AnimationCurve animCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public Vector3 inPosition;        // スライドイン後の位置
    public Vector3 outPosition;      // スライドアウト後の位置
    public float duration = 1.0f;    // スライド時間（秒）

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // スライドイン（ボタンが押されたときに、これを呼ぶ）
    public void SlideIn()
    {
        StartCoroutine(StartSlidePanel(true));
        
    }

    // スライドアウト
    public void SlideOut()
    {
        StartCoroutine(StartSlidePanel(false));
    }

    private IEnumerator StartSlidePanel(bool isSlideIn)
    {
        float startTime = Time.time;    // 開始時間
        Vector3 startPos = transform.localPosition;  // 開始位置
        Vector3 moveDistance;            // 移動距離および方向

        if (isSlideIn)
        {
            moveDistance = (inPosition);
        }

        else
        {
            moveDistance = (outPosition);
        }

            while ((Time.time - startTime) < duration)
            {
                transform.localPosition = startPos + moveDistance * animCurve.Evaluate((Time.time - startTime) / duration);
                yield return 0;        // 1フレーム後、再開
            }
 
             transform.localPosition = startPos + moveDistance;
           
        
    }
}