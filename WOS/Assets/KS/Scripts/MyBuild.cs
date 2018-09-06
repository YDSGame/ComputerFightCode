using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBuild : MonoBehaviour {

    string unitName;
    string comment;
    int unitCost;
    
    public string UnitName
    {
        get { return unitName; }
        set { unitName = value; }
    }
    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }
    public int UnitCost
    {
        get { return unitCost; }
        set { unitCost = value; }
    }
    public MyBuild (string _unit, string _com, int _cost) //생성자(자기 이름)
    {
        SetBuild(_unit, _com, _cost);
    }
    void SetBuild(string _unitName, string _comment, int _unitCost)
    {
        UnitName = _unitName;
        Comment = _comment;
        UnitCost = _unitCost;
    }    
}
