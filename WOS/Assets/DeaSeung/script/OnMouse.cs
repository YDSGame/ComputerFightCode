using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouse : MonoBehaviour {
    public GameObject BuildInven;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))

        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                BuildInven.SetActive(false);
            }

        }



       // UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

    }
    public void Click()
    {
        BuildInven.SetActive(true);
    }
}
