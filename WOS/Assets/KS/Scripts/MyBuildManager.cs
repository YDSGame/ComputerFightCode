using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBuildManager : MonoBehaviour {
    public static MyBuildManager ins;
    public List<MyBuild> myBuilds = new List<MyBuild>();
    public GameObject shop;
    public Text comment;
    private void Awake()
    {
        ins = this;

        myBuilds.Add(new MyBuild("곰탱이", "비용 100, 건물당 3마리 소환", 100));
        myBuilds.Add(new MyBuild("토께이", "비용 200, 건물당 2마리 소환", 200));
        myBuilds.Add(new MyBuild("총재비", "비용 300, 건물당 5마리 소환", 300));
        myBuilds.Add(new MyBuild("덩치", "비용 400, 건물당 1마리 소환", 400));
        
    }
    // Use this for initialization
    void Start () {
    }
		
}
