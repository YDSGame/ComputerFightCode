using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitButton : MonoBehaviour {

    public Text unitName;
    public Text unitComment;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ButtonClick()
    {
        MyBuildManager.ins.comment.text = unitComment.text;
    }

}
