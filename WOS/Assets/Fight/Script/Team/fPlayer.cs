using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fPlayer : MonoBehaviour {
    public int Jelly = 60000; // 젤리자원 
    public int JellyPlus = 0; // 젤리봉지 갯수
    public bool JellyTime = false; // 쿨타임 확인유무
    public float JellyIns = 0; // 쿨타임
    public List<fNode> Node = new List<fNode>(); // 플레이어의 노드(땅)

    public int BearBuild = 0;
    public int ELBuild = 0;
    public int RabbitBuild = 0;
    public int GunBuild = 0;
    public int DogBuild = 0;
    public int SheepBuild = 0;
    public int SpaceBuild = 0;
    public int ClownBuild = 0;

    public bool b_Jelly = false;
    public bool b_BearBuild = false;
    public bool b_ELBuild = false;
    public bool b_RabbitBuild = false;
    public bool b_GunBuild = false;
    public bool b_DogBuild = false;
    public bool b_SheepBuild = false;
    public bool b_SpaceBuild = false;
    public bool b_ClownBuild = false;
    
    public int Nodes = 0;

    
}
