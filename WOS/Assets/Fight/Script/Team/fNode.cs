using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fNode : MonoBehaviour {

    public fNode cNode;
    public GameObject gBuild; // 노드위에 건물 확인유무 
    public GameObject gBuildCreat; // 건물 건설
    public Vector3 vPostionOffSet; // 노드의 중앙

  
    public void BlueBuildCreat()
    {
        
            for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++)
            {
                if (Gamemanager1.GetInstance().BlueNode[i].gBuild != null) // 빌드가 널아니라면 건물을 짓을수없다
                {
                    //Debug.Log("건물 이미있음");
                    continue; // 다음 i 번째를 가기위해서 
                }

                else
                {
                    Gamemanager1.GetInstance().BlueNode[i].gBuild = (GameObject)Instantiate(gBuildCreat, Gamemanager1.GetInstance().BlueNode[i].transform.position + vPostionOffSet, transform.rotation);
                    Gamemanager1.GetInstance().BlueNode[i].gBuild.transform.parent = Gamemanager1.GetInstance().BlueNode[i].transform; // 노드에 상속이 됨 
                }
                //print("i" + i);
                break;
            }
      
    }
    public void RedBuildCreat()
    {

        for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++)
        {
            if (Gamemanager1.GetInstance().RedNode[i].gBuild != null) // 빌드가 널아니라면 건물을 짓을수없다
            {
                //Debug.Log("건물 이미있음");
                continue; // 다음 i 번째를 가기위해서 
            }

            else
            {
                Gamemanager1.GetInstance().RedNode[i].gBuild = (GameObject)Instantiate(gBuildCreat, Gamemanager1.GetInstance().RedNode[i].transform.position + vPostionOffSet, transform.rotation);
                Gamemanager1.GetInstance().RedNode[i].gBuild.transform.parent = Gamemanager1.GetInstance().RedNode[i].transform; // 노드에 상속이 됨 
            }
            //print("i" + i);
            break;
        }
    }
}
