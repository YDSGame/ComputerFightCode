using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public static GM ins;
    public Nexus gm_Nexus;
    Vector3 redStart;
    Vector3 blueStart;
    public GameObject redStartpos;
    public GameObject blueStartPos;
    int unitCount;
    private void Awake()
    {
        ins = this;
    }
    // Use this for initialization
    void Start () {
        redStart = redStartpos.transform.position;
        blueStart = blueStartPos.transform.position;
        unitCount = Nexus.insNexus.poolCount;
        
	}
    private void OnGUI()
    {
        if(GUI.Button(new Rect(450, 10, 70, 40), "RedTeam"))
        {           
            RedTeamSpawn();
        }
        if (GUI.Button(new Rect(450, 50, 70, 40), "BlueTeam"))
        {
            BlueTeamSpawn();
        }
    }
    void RedTeamSpawn()
    {
        float randomX = -47;
        float randomZ = 0;
        for (int i = 0; i < unitCount; i++)
        {
            for (int j = 0; j < Nexus.insNexus.redCharacters.Length; j++)
            {
                if (randomX > 47)
                {
                    randomX = -47;
                }
                if (redStartpos.transform.childCount < 32)
                {
                    randomX = ((unitCount * 3f) / 2f) * (-1f);
                }
                GameObject unit = Nexus.insNexus.RedUnitsIns();
                if (unit == null) return;        
                unit.transform.position = new Vector3(redStart.x + randomX, redStart.y, redStart.z + randomZ);
                UnitState cUnit = unit.GetComponent<UnitState>();
                unit.SetActive(true);
                cUnit.pHealth = cUnit.pMaxHealth;
                cUnit.estate = UnitState.eState.Move;
                randomX += 3;
            }
            randomZ -= 0.5f;
        }
    }
    void BlueTeamSpawn()
    {
        float randomX =-47;
        float randomZ = 0;
        for (int i = 0; i < unitCount; i++)
        {            
            for (int j = 0; j < Nexus.insNexus.blueCharacters.Length; j++)
            {
                if (randomX > 47)
                {
                    randomX = -47;
                }                
                if (blueStartPos.transform.childCount < 32)
                {
                    randomX = ((unitCount * 3f) / 2f)*(-1f);
                }
                GameObject unit = Nexus.insNexus.BlueUnitsIns();
                if (unit == null) return;
                unit.transform.position = new Vector3(blueStart.x+randomX,blueStart.y,blueStart.z+ randomZ);
                UnitState cUnit = unit.GetComponent<UnitState>();
                unit.SetActive(true);
                cUnit.pHealth = cUnit.pMaxHealth;
                cUnit.estate = UnitState.eState.Move;
                randomX+= 3;

            }
                randomZ +=0.5f;
        }
    }
    void RedTeamDeadCheck()
    {
        GameObject unit = Nexus.insNexus.RedUnitsDel();
        if (unit == null) return;
        unit.transform.position = redStart;
        //unit.SetActive(false);
        UnitState cUnit = unit.GetComponent<UnitState>();
        cUnit.estate = UnitState.eState.Dead;        
    }
    void BlueTeamDeadCheck() 
    {
        GameObject unit = Nexus.insNexus.BlueUnitsDel(); //죽은 오브젝트를 리턴받는다.
        if (unit == null) return;
        unit.transform.position = blueStart; //죽은 오브젝트를 스폰지점으로 귀환시킨다.
        //unit.SetActive(false); //죽은 오브젝트를 비활성화 시킨다.
        //죽은 오브젝트를 풀할때까지 기다리는 상태로 만든다.
        UnitState cUnit = unit.GetComponent<UnitState>();
        cUnit.estate = UnitState.eState.Dead;        
    }
    void DeadZoneCheck()
    {
        GameObject unit = DeadZone.ins.deadOB();
        if (unit == null) return;
        unit.GetComponent<UnitState>().pHealth = 0;
    }
    // Update is called once per frame
    void Update () {            
        DeadZoneCheck();
        RedTeamDeadCheck();
        BlueTeamDeadCheck();
        if (Input.GetKey(KeyCode.R))
        {
            RedTeamSpawn();
            BlueTeamSpawn();
        }
    }
}
