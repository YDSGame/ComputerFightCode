using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueNexus : MonoBehaviour {
    public static BlueNexus insNexus;

    public GameObject[] characters;
    public GameObject poolStarter;
    public List<GameObject> gUnits;
    
    int poolCount = 30;
    private void Awake()
    {
        insNexus = this;
    }
    // Use this for initialization
    void Start()
    {        
        gUnits = new List<GameObject>();
        ObjectPool();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ObjectPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            for (int j = 0; j < characters.Length; j++)
            {
                GameObject unit = (GameObject)Instantiate(characters[j]);
                unit.transform.parent = poolStarter.transform;
                unit.transform.rotation = poolStarter.transform.rotation;
                unit.SetActive(false);
                unit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
                gUnits.Add(unit);
            }
        }
    }
   public GameObject UnitsIns()
    {
        for (int i = 0; i < gUnits.Count; i++)
        {
            if (!gUnits[i].activeInHierarchy)
            {
                return gUnits[i];
            }
        }
        return null;
    }
    public GameObject UnitsDel()
    {
        for (int i = 0; i < gUnits.Count; i++)
        {
            if (!gUnits[i].activeInHierarchy && gUnits[i].GetComponent<UnitState>().estate == UnitState.eState.Dead )
            {
                return gUnits[i];
            }
        }
        return null;
    }
}
