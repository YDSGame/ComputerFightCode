using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Node cNode;
    public GameObject gBuild; // 노드의 터렛확인유무
    public GameObject gBuildCreat; // 건물 건설
    public Vector3 vPostionOffSet; // 노드의 중앙
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void BuildCreat()
    {
        for (int i = 0; i < Gamemanager.GetInstance().Node.Count; i++)
        {
            if (Gamemanager.GetInstance().Node[i].gBuild != null) // 빌드가 널아니라면 건물을 짓을수없다
            {
                Debug.Log("건물 이미있음");
                continue;
            }
            
            else //if (Gamemanager.GetInstance().Node[i].gBuild == null)
            {
                Gamemanager.GetInstance().Node[i].gBuild = (GameObject)Instantiate(gBuildCreat, Gamemanager.GetInstance().Node[i].transform.position + vPostionOffSet, transform.rotation);
            }
            print("i" + i);
            break;
        }
   
    } 
}
