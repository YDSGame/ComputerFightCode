using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{    
    //플레이어당 최대 풀링 수 ( 곰 = 30, 거너 = 25, 토끼의숲 = 150, 코끼리 = 30)
    public static Nexus insNexus;    
    public GameObject[] redCharacters;
    public GameObject[] blueCharacters;    
    public GameObject poolRedStarter;
    public GameObject poolBlueStarter;    
    [HideInInspector] public List<GameObject> redUnits;
    [HideInInspector] public List<GameObject> blueUnits;    
    public int poolCount = 5;
    int bearPool = 30;
    int gunnerPool = 25;
    int bunnyPool = 150;
    int el = 30;
    private void Awake()
    {
        insNexus = this;
    }
    // Use this for initialization
    void Start()
    {
        poolRedStarter = GM.ins.redStartpos;
        poolBlueStarter = GM.ins.blueStartPos;
        
        ObjectPool();
        //print(gUnits.Count);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    void ObjectPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            for (int j = 0; j < redCharacters.Length; j++)
            {
                GameObject unit = (GameObject)Instantiate(redCharacters[j]);
                unit.gameObject.name = unit.gameObject.name + i;
                unit.transform.parent = poolRedStarter.transform;
                unit.transform.GetChild(0).rotation = poolRedStarter.transform.rotation;
                unit.transform.position = poolRedStarter.transform.position;
                unit.GetComponent<UnitAIUp>().wayPoint = poolBlueStarter;                
                unit.SetActive(false);
                unit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
                redUnits.Add(unit);
            }

            for (int j = 0; j < blueCharacters.Length; j++)
            {
                GameObject unit = (GameObject)Instantiate(blueCharacters[j]);
                unit.gameObject.name = unit.gameObject.name + i;
                unit.transform.parent = poolBlueStarter.transform;
                unit.transform.GetChild(0).rotation = poolBlueStarter.transform.rotation;
                unit.GetComponent<UnitAIUp>().wayPoint = poolRedStarter;
                unit.SetActive(false);
                unit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
                blueUnits.Add(unit);
            }
        }
    }
    public GameObject RedUnitsIns()
    {
        for (int i = 0; i < redUnits.Count; i++)
        {
            if (!redUnits[i].activeInHierarchy)
            {
                return redUnits[i];
            }

        }
        return null;
    }
    public GameObject BlueUnitsIns()
    {
        for (int i = 0; i < blueUnits.Count; i++)
        {
            if (!blueUnits[i].activeInHierarchy)
            {
                return blueUnits[i];
            }

        }
        return null;
    }
    public GameObject UnitsIns()
    {
        for (int i = 0; i < redUnits.Count; i++)
        {
            if (!redUnits[i].activeInHierarchy)
            {
                return redUnits[i];
            }

        }

        for (int i = 0; i < blueUnits.Count; i++)
        {
            if (!blueUnits[i].activeInHierarchy)
            {
                return blueUnits[i];
            }

        }

        //for (int i = 0; i < gUnits.Count; i++)
        //{
        //    if (!gUnits[i].activeInHierarchy)
        //    {
        //        return gUnits[i];
        //    }
        //}
        return null;
    }
    public GameObject RedUnitsDel()
    {
        for (int i = 0; i < redUnits.Count; i++)
        {
            if (!redUnits[i].activeInHierarchy)
            {
                return redUnits[i];
            }
        }
        return null;
    }
    public GameObject BlueUnitsDel()
    {
        for (int i = 0; i < blueUnits.Count; i++)
        {
            if (!blueUnits[i].activeInHierarchy)
            {
                return blueUnits[i];
            }
        }
        return null;
    }    
}
