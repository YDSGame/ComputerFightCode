using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus1 : MonoBehaviour
{    
    public static Nexus1 insNexus;    
    public GameObject[] redCharacters;
    public GameObject[] blueCharacters;    
    public GameObject poolRedStarter;
    public GameObject poolBlueStarter;    
    [HideInInspector] public List<GameObject> redUnits;
    [HideInInspector] public List<GameObject> blueUnits;    
    public int poolCount = 5;
    private void Awake()
    {
        insNexus = this;
    }
    // Use this for initialization
    void Start()
    {
        poolRedStarter = GM1.ins.redStartpos;
        poolBlueStarter = GM1.ins.blueStartPos;
        
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
                unit.transform.rotation = poolRedStarter.transform.rotation;
                unit.transform.position = poolRedStarter.transform.position;
               // unit.GetComponent<RedTeam>().BlueNex = poolBlueStarter;   
                unit.SetActive(false);
                unit.GetComponent<Unit>().eState = Unit.State.DIE;
                redUnits.Add(unit);
            }

            for (int j = 0; j < blueCharacters.Length; j++)
            {
                GameObject unit = (GameObject)Instantiate(blueCharacters[j]);
                unit.gameObject.name = unit.gameObject.name + i;
                unit.transform.parent = poolBlueStarter.transform;
                unit.transform.rotation = poolBlueStarter.transform.rotation;
              //  unit.GetComponent<BlueTeam>().RedNex = poolRedStarter;
                unit.SetActive(false);
                unit.GetComponent<Unit>().eState = Unit.State.DIE;
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
