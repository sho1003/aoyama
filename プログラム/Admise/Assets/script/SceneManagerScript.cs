﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript: MonoBehaviour
{


    public static bool Player1Click;
    public static bool Player2Click;

    player1_script player1;
    player2_script player2;

    GameSE_Script se;

    GameObject circleTextureOut1;
    GameObject circleTextureOut2;

    Ray ray;
    RaycastHit hit;

    public GameObject selectedunit;
    public List<GameObject> selectedunits = new List<GameObject>();

    private Vector3 MouseDownPoint, CurrentDownPoint;
    public bool IsDragging;
    private float BoxWidth, BoxHeight, BoxLeft, BoxTop;
    private Vector2 BoxStart, BoxFinish;
    public List<GameObject> UnitsOnScreenSpace = new List<GameObject>();
    public List<GameObject> UnitInDrag = new List<GameObject>();
    public static int CardFlame;

    public static bool CardChoise = false;
    CardFlameScript Flame;

    void OnGUI()
    {
        if (IsDragging)
        {
            GUI.Box(new Rect(BoxLeft, BoxTop, BoxWidth, BoxHeight), "");
        }
    }


    // Use this for initialization
    void Start()
    {
        //  スクリプト取得
        se = GameObject.Find("Sounds/SE").GetComponent<GameSE_Script>();

        Player1Click = false;
        Player2Click = false;



        //player_ = GameObject.Find("Cube").GetComponent<player_script>();


    }

    private string Player1Tag = "Player1";
    private string Player2Tag = "Player2";

    void LateUpdate()
    {
    }

    // Update is called once per frame  
    void Update()
    {
        UnitInDrag.Clear();
        if (IsDragging && UnitsOnScreenSpace.Count > 0)
        {
            selectedunit = null;
            Debug.Log("a");
            for (int i = 0; i < UnitsOnScreenSpace.Count; i++)
            {
                Debug.Log("b");
                GameObject UnitObj = UnitsOnScreenSpace[i] as GameObject;
                Unit PosScript = UnitObj.transform.GetComponent<Unit>();
                if (!UnitInDrag.Contains(UnitObj))
                {
                    Debug.Log("c");
                    if (UnitWithDrag(PosScript.ScreenPos))
                    {
                        UnitInDrag.Add(UnitObj);
                        Debug.Log("d");
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.tag != "Player1")
                {
                    if (CheckIfMouseIsDragging())
                    {
                        IsDragging = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }

        //　まだキャラが選択されていない状態
        if (!Player1Click)
        {
            //一回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == Player1Tag)
                    {
                        Player1Click = true;
                        //レイピックを飛ばして当たったオブジェクトの名前を取ってくる(Findの中身)
                        player1 = GameObject.Find(hit.transform.gameObject.name).GetComponent<player1_script>();
                        //
                        CardFlame = player1.ID;
                        //  クリップSE
                        se.SetSE1(se.enterSE);
                        //  外枠表示 & 黄色に設定
                        circleTextureOut1 = GameObject.Find(hit.transform.gameObject.name).transform.Find("CircleTextureOut").gameObject;
                        circleTextureOut1.GetComponent<Renderer>().material.color = new Color(255, 255, 0, 1.0f);
                        circleTextureOut1.SetActive(true);
                        //　カード外枠表示（別スクリプトで）
                        CardChoise = true;

                    }
                }
            }
        }
        //　キャラが選択された状態
        else
        {
            //　もし右クリックが押された場合
            if (Input.GetMouseButtonDown(1))
            {
                Player1Click = false;

                //  外枠非表示
                circleTextureOut1.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.0f);
                circleTextureOut1.SetActive(false);
                //  キャンセルSE
                se.SetSE1(se.cancelSE);
                CardChoise = false;
            }
            //二回目のクリック
            else if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag != Player1Tag)
                    {
                        if (hit.transform.tag != Player1Tag)
                        {
                            //  決定SE
                            se.SetSE1(se.enterSE);
                            //  外枠非表示
                            circleTextureOut1.SetActive(false);
                            //移動させる
                            player1.Pointer_Click();
                            Player1Click = false;
                            CardChoise = false;
                        }
                    }
                }
            }
        }
        if (!Player2Click)
        {
            //一回目のクリック
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag == Player2Tag)
                    {
                        Player2Click = true;
                        //レイピックを飛ばして当たったオブジェクトの名前を取ってくる(Findの中身)
                        player2 = GameObject.Find(hit.transform.gameObject.name).GetComponent<player2_script>();
                        //  クリップSE
                        se.SetSE1(se.enterSE);
                        //  外枠表示
                        circleTextureOut2 = GameObject.Find(hit.transform.gameObject.name).transform.Find("CircleTextureOut").gameObject;
                        circleTextureOut2.GetComponent<Renderer>().material.color = new Color(255, 255, 0, 1.0f);
                        circleTextureOut2.SetActive(true);
                    }
                }
            }
        }
        else
        {
            //　もし右クリックが押された場合
            if (Input.GetMouseButtonDown(1))
            {
                Player2Click = false;
                //  外枠非表示
                circleTextureOut2.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0.0f);
                circleTextureOut2.SetActive(false);
                //  キャンセルSE
                se.SetSE1(se.cancelSE);
            }

            //二回目のクリック
            else if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray();
                RaycastHit hit = new RaycastHit();
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
                {
                    if (hit.transform.tag != Player2Tag)
                    {
                        //  クリップSE
                        se.SetSE1(se.enterSE);
                        //  外枠非表示
                        circleTextureOut2.SetActive(false);
                        //移動させる
                        player2.Pointer_Click();
                        Player2Click = false;

                    }
                }
            }

        }

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            CurrentDownPoint = hit.point;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                MouseDownPoint = hit.point;
        }
        if (IsDragging)
        {
            BoxWidth = Camera.main.WorldToScreenPoint(MouseDownPoint).x - Camera.main.WorldToScreenPoint(CurrentDownPoint).x;
            BoxHeight = Camera.main.WorldToScreenPoint(MouseDownPoint).y - Camera.main.WorldToScreenPoint(CurrentDownPoint).y;
            BoxLeft = Input.mousePosition.x;
            BoxTop = (Screen.height - Input.mousePosition.y) - BoxHeight;

            if (BoxWidth > 0f && BoxHeight < 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            else if (BoxWidth > 0f && BoxHeight > 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + BoxHeight);
            }
            else if (BoxWidth < 0f && BoxHeight < 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y);
            }
            else if (BoxWidth < 0f && BoxHeight < 0f)
            {
                BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y + BoxHeight);
            }
            BoxFinish = new Vector2(BoxStart.x + Unsigned(BoxWidth), BoxStart.y - Unsigned(BoxHeight));
        }
    }
    float Unsigned(float val)
    {
        if (val < 0f)
            val *= -1;
        return val;
    }
    private bool CheckIfMouseIsDragging()
    {
        if (CurrentDownPoint.x - 1 >= MouseDownPoint.x || CurrentDownPoint.y - 1 >= MouseDownPoint.y || CurrentDownPoint.z - 1 >= MouseDownPoint.z ||
    CurrentDownPoint.x < MouseDownPoint.x - 1 || CurrentDownPoint.y < MouseDownPoint.y - 1 || CurrentDownPoint.z < MouseDownPoint.z - 1)
            return true;
        else
            return false;
    }
    public bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) && (UnitScreenPos.x > 0f && UnitScreenPos.y > 0f))
            return true;
        else
            return false;
    }
    public bool UnitWithDrag(Vector2 UnitScreenPos)
    {
        if ((UnitScreenPos.x > BoxStart.x && UnitScreenPos.y < BoxStart.y) && (UnitScreenPos.x < BoxFinish.x && UnitScreenPos.y > BoxFinish.y))
            return true;
        else
            return false;
    }
    public void PutUnitsFromDragIntoSelectedUnits()
    {
        if (UnitInDrag.Count > 0)
        {
            for (int i = 0; i < UnitInDrag.Count; i++)
            {
                if (!selectedunits.Contains(UnitInDrag[i]))
                    selectedunits.Add(UnitInDrag[i]);
            }
        }
        UnitInDrag.Clear();
    }


}

