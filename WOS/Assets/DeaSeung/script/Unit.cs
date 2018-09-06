using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
    //유닛 스텟
    public int nHp;
    public int nAD;
    public int nArmor;
    public int nMoveSpd;
    public float fADRange;
    public float fSight;
    public float fAttackSpd;
    public enum State { IDLE,Move,Tracking,ATTACK,DIE};
    public enum eAttackType { NEAR, DISTANCE, RANGE};
    public enum eCharacterName { Bunny, Gunner, Big, Bear };
    public float fInsTime;
    public eAttackType eType;
    public eCharacterName eName;
    public State eState;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
