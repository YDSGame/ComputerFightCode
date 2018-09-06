using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShop : MonoBehaviour {
    List<GameObject> units = new List<GameObject>();
    public GameObject buttonPrefab;
    
	// Use this for initialization
	void Start () {
        InsTantiateButton();
        ButtonSet();
    }
	void InsTantiateButton()
    {
        for (int i = 0; i < MyBuildManager.ins.myBuilds.Count; i++)
        {
            units.Add(Instantiate(buttonPrefab));                       
        }
    }
    void ButtonSet()
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].transform.parent = MyBuildManager.ins.shop.transform;
            units[i].GetComponent<UnitButton>().unitName.text = MyBuildManager.ins.myBuilds[i].UnitName;
            units[i].GetComponent<UnitButton>().unitComment.text = MyBuildManager.ins.myBuilds[i].Comment;
        }
    }	
}
