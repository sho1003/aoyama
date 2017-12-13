using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    SelectUnit selectunit;
    NavMeshAgent agent;
    RaycastHit hit;
    public Vector2 ScreenPos;
    bool OnScreen = false;

	void Start () {
        selectunit = GameObject.Find("_SelectUnitManager").GetComponent<SelectUnit>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        if (selectunit.UnitWithinScreenSpace(ScreenPos))
        {
            OnScreen = true;
            if (!selectunit.UnitsOnScreenSpace.Contains(this.gameObject))
                selectunit.UnitsOnScreenSpace.Add(this.gameObject);
        }
        else
        {
            if (OnScreen)
            {
                selectunit.UnitsOnScreenSpace.Remove(this.gameObject);
                OnScreen = false;
            }
        }
		if(selectunit.selectedunit==this.gameObject || selectunit.selectedunits.Contains(this.gameObject))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
                {
                    if (hit.transform.tag == "Floor")
                    {
                        agent.destination = hit.point;
                    }
                }
            }
        }
	}
}
