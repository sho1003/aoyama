using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speech_UIscript : MonoBehaviour
{

    const float GET_LENGTH = 3.0f;

    public Vector3 offset;
    Camera rotateCamera;
    GameObject player;
    player1_script player1;
    player2_script player2;
    TeamManager team;

    RectTransform myRectTrans;
    RectTransform SpeechBalloon;
    GameObject hukidashi;
    Text sumText;

    int player_number;

    // Use this for initialization
    void Start()
    {
        rotateCamera = Camera.main;
        player = transform.root.gameObject;

        if (player.tag == "Player1") player1 = player.GetComponent<player1_script>();
        else if (player.tag == "Player2") player2 = player.GetComponent<player2_script>();

        team = GameObject.Find("TeamManager").GetComponent<TeamManager>();

        myRectTrans = GetComponent<RectTransform>();
        SpeechBalloon = transform.GetChild(1).GetComponent<RectTransform>();

        hukidashi = transform.Find("SpeechBalloon").gameObject;
        sumText = hukidashi.transform.GetChild(0).GetComponent<Text>();

        player_number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotateCamera.transform.rotation;

        //同じタグの一番近いプレイヤーを取得

        GameObject otherplayer = SearchTag(player, player.tag);
        if (otherplayer != null)
        {
            //プレイヤーとのベクトルを取得
            Vector3 vec = otherplayer.transform.position - player.transform.position;
            //絶対値を取る
            float Dis = Mathf.Abs(vec.magnitude);
            //プレイヤーとの距離が一定以下なら
            if (Dis < GET_LENGTH)
            {
                //Debug.Log("近い");

                if (vec.z < 0.00f)
                {
                    //吹き出しを一旦消す
                    hukidashi.SetActive(false);
                }
                else
                {
                    hukidashi.SetActive(true);
                }
                
                //吹き出しの中の数字
                if (player.tag == "Player1")
                {
                    player1_script other1 = otherplayer.GetComponent<player1_script>();

                    if (team.checkTeamID(true, player1.ID,other1.ID))
                    {
                        team.connectTeam(true, player1.ID, other1.ID);
                    }

                    player_number = team.TeamSumNumber(true, player1.ID);
                    //　プレイヤー個人にもチームの数字を持たせる
                    //player1.TeamNumber = player_number;
                    //　チームを組んだフラグを立てる
                    player1.FlagTeam = true;
                    other1.FlagTeam = true;
                }
                else if (player.tag == "Player2")
                {
                    player2_script other2 = otherplayer.GetComponent<player2_script>();

                    if (team.checkTeamID(false, player2.ID, other2.ID))
                    {
                        team.connectTeam(false, player2.ID, other2.ID);
                    }

                    player_number = team.TeamSumNumber(false, player2.ID);
                    //　プレイヤー個人にもチームの数字を持たせる
                    //player2.TeamNumber = player_number;
                    //　チームを組んだフラグを立てる
                    player2.FlagTeam = true;
                    other2.FlagTeam = true;
                }

                //吹き出しの数字変更
                sumText.text = "" + player_number;

                //吹き出しの位置
                //xはメンバーの数で割る
                offset.x = vec.x / 2;
                offset.y = 4.0f;
                offset.z = 2.0f;
            }
            else
            {
                hukidashi.SetActive(true);

                if (player.tag == "Player1")
                {
                    player1_script other1 = otherplayer.GetComponent<player1_script>();

                    sumText.text = "" + player1.Number;
                    //　チームを組んでいないのでフラグを立てない
                    player1.FlagTeam = false;
                    other1.FlagTeam = false;

                    //チーム解除
                    team.RemoveTeam(true, other1.ID, player1.ID);
                }
                else if (player.tag == "Player2")
                {
                    player2_script other2 = otherplayer.GetComponent<player2_script>();

                    sumText.text = "" + player2.Number;
                    //　チームを組んでいないのでフラグを立てない
                    player2.FlagTeam = false;
                    other2.FlagTeam = false;

                    //チーム解除
                    team.RemoveTeam(false, other2.ID, player2.ID);
                }

                //プレイヤーの位置によって微調整
                OffsetAdjustment();
            }
        }

        //ワールド座標をスクリーン座標に変換
        var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myRectTrans, screenPos, Camera.main, out localPos);
        SpeechBalloon.localPosition = localPos;

    }

    void OffsetAdjustment()
    {
        offset.x = 0;
        offset.y = 4.0f;
        offset.z = 2.0f;
    }

    //指定されたタグの中で距離が一番近いものを取得
    GameObject SearchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0;               //距離用一時変数
        float nearDis = 0.1f;           //最も近いオブジェクトの距離
        float farDis = 100.0f;
        GameObject targetObj = null;    //リターンするオブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //取得したオブジェクトと自身の距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が0.1以上5.0以下ならばオブジェクトを取得
            if (nearDis < tmpDis && tmpDis < farDis)
            {
                //一時変数に距離を格納
                farDis = tmpDis;
                targetObj = obs;
            }
        }

        //オブジェクトを返す
        return targetObj;
    }

    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
