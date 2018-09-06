using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreBuntton : MonoBehaviour {

    public  Build m_cBuild;
    public Text m_cText;
	// Use this for initialization
	void Start () {
        m_cBuild = GetComponent<Build>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetText(Build build)
    {
        m_cBuild = build;
        m_cText.text = build.Name;
    }

}
