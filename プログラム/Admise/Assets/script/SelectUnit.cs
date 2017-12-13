using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectUnit : MonoBehaviour {

    public GameObject selectedunit;
    public List<GameObject> selectedunits = new List<GameObject>();
    RaycastHit hit;

    private Vector3 MouseDownPoint, CurrentDownPoint;
    public bool IsDragging;
    private float BoxWidth, BoxHeight, BoxLeft, BoxTop;
    private Vector2 BoxStart, BoxFinish;
    public List<GameObject> UnitsOnScreenSpace = new List<GameObject>();
    public List<GameObject> UnitInDrag = new List<GameObject>();

    void OnGUI()
    {
        if (IsDragging)
        {
            GUI.Box(new Rect(BoxLeft, BoxTop, BoxWidth, BoxHeight), "");
        }
    }
    void LateUpdate()
    {
        UnitInDrag.Clear();
        if (IsDragging && UnitsOnScreenSpace.Count > 0)
        {
            selectedunit = null;
            for(int i=0; i<UnitsOnScreenSpace.Count; i++)
            {
                GameObject UnitObj = UnitsOnScreenSpace[i] as GameObject;
                Unit PosScript = UnitObj.transform.GetComponent<Unit>();
                GameObject selectmarker=UnitObj.transform.Find("Marker").gameObject;
                if (!UnitInDrag.Contains(UnitObj))
                {
                    if (UnitWithDrag(PosScript.ScreenPos))
                    {
                        selectmarker.SetActive(true);
                        UnitInDrag.Add(UnitObj);
                    }
                    else
                    {
                        if (!UnitInDrag.Contains(UnitObj))
                            selectmarker.SetActive(false);
                    }
                }
            }
        }
    }

    //void Start () {

    //}

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.tag != "SelectableUnit")
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
            PutUnitsFromDragIntoSelectedUnits();
            IsDragging = false;
        }

        if (selectedunit == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "SelectableUnit")
                    {
                        selectedunit = hit.transform.gameObject;
                        selectedunit.transform.Find("Marker").gameObject.SetActive(true);

                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "SelectableUnit")
                    {
                        selectedunit.transform.Find("Marker").gameObject.SetActive(false);
                        selectedunit = null;
                        selectedunit = hit.transform.gameObject;
                        selectedunit.transform.Find("Marker").gameObject.SetActive(true);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                if (hit.transform.tag == "SelectableUnit")
                {
                    if (selectedunit != null)
                    {
                        selectedunits.Add(selectedunit);
                        selectedunit = null;
                    }
                    selectedunits.Add(hit.transform.gameObject);
                    for(int i=0; i<selectedunits.Count; i++)
                    {
                        selectedunits[i].transform.Find("Marker").gameObject.SetActive(true);
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
            for(int i=0; i<UnitInDrag.Count; i++)
            {
                if (!selectedunits.Contains(UnitInDrag[i]))
                    selectedunits.Add(UnitInDrag[i]);
            }
        }
        UnitInDrag.Clear();
    }
}

