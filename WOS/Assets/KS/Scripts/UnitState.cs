using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class UnitState : MonoBehaviour
{    
    public float pMaxHealth;
    
   // [HideInInspector]
    public float pHealth;
    public float pPower;
    public float pSpeed;
    public float pAttackSpeed;
    public float pAttackRange;
    public float pSight;
    public float pdef;
    public float pRange;
    public enum eAttackType { Milee, Distance, Range, RangeDistance};
    public enum eCharacterName { None =-1,Nexus,Bunny, Gunner, Big, Bear, Dog, Sheep, Girl, Clown};
    public enum eState { Move, GoAttack,Attacking,EnemyCheck, Dead};    

    public eAttackType eType;
    public eCharacterName eName;
    public eState estate;

    public float BleedingTime = 0;
    public bool isBledding = false;
   
    private void Start()
    {
        pHealth = pMaxHealth;        
    }      
}

