using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    public static DeadZone ins;
    GameObject gDeadOb;

    private void OnTriggerStay(Collider other)
    {
        print("닿았다 데드존에");
        gDeadOb = other.gameObject;        
    }   
    // Use this for initialization
    void Start () {
        ins = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject deadOB()
    {
        if(gDeadOb != null)
        {
            return gDeadOb;
        }
        return null;
    }
}
