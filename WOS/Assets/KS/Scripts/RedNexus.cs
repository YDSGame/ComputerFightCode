using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNexus : MonoBehaviour {
    public enum nexusTeam {RED,BLUE}
    public static RedNexus insNexus;
    public GameObject[] characters;
    public GameObject[] redCharacters;
    public GameObject[] blueCharacters;
    public GameObject poolStarter;
    public GameObject poolRedStarter;
    public GameObject poolBlueStarter;
    public List<GameObject> gUnits;
    [HideInInspector]public List<GameObject> redUnits;
    [HideInInspector]public List<GameObject> blueUnits;
    public nexusTeam nexusColor;
    [HideInInspector]
    public int poolCount = 30;
    private void Awake()
    {
        insNexus = this;
    }
    // Use this for initialization
    void Start()
    {
        gUnits = new List<GameObject>();
        ObjectPool();
        //print(gUnits.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ObjectPool()
    {        
        for (int i = 0; i < poolCount; i++)
        {           
                for (int j = 0; j < redCharacters.Length; j++)
                {
                    GameObject unit = (GameObject)Instantiate(redCharacters[j]);
                    unit.transform.parent = poolRedStarter.transform;
                    unit.transform.rotation = poolRedStarter.transform.rotation;
                    unit.SetActive(false);
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
                    redUnits.Add(unit);
                }
            
                for (int j = 0; j < blueCharacters.Length; j++)
                {
                    GameObject unit = (GameObject)Instantiate(blueCharacters[j]);
                    unit.transform.parent = poolBlueStarter.transform;
                    unit.transform.rotation = poolBlueStarter.transform.rotation;
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
            if (!redUnits[i].activeInHierarchy && redUnits[i].GetComponent<UnitState>().estate == UnitState.eState.Dead)
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
            if (!blueUnits[i].activeInHierarchy && blueUnits[i].GetComponent<UnitState>().estate == UnitState.eState.Dead)
            {
                return blueUnits[i];
            }
        }
        return null;
    }
    public GameObject UnitsDel()
    {
        if (nexusColor == nexusTeam.RED)
        {
            for (int i = 0; i < redUnits.Count; i++)
            {
                if (!redUnits[i].activeInHierarchy && redUnits[i].GetComponent<UnitState>().estate == UnitState.eState.Dead)
                {
                    return redUnits[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < blueUnits.Count; i++)
            {
                if (!blueUnits[i].activeInHierarchy && blueUnits[i].GetComponent<UnitState>().estate == UnitState.eState.Dead)
                {
                    return blueUnits[i];
                }
            }
        }
        //for (int i = 0; i < gUnits.Count; i++)
        //{
        //    if (!gUnits[i].activeInHierarchy && gUnits[i].GetComponent<UnitState>().estate == UnitState.eState.Dead)
        //    {
        //        return gUnits[i];
        //    }
        //}
        return null;
    }
}
