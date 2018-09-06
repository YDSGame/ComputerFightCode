//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.AI;

//public class UnitAbility : MonoBehaviour {

//    Animator myanim;
//    CharacterState myState;
//    NavMeshAgent myNav;
//    GameObject target;
//    Collider[] setTarget;
//    bool isRedUnit = false;
//    float attackTime;
//    float curatkTime = 0;
//    float setTargetTime;
//    float curSetTarget = 3f;
    

//    // Use this for initialization
//    void Start () {
//        myState = GetComponent<CharacterState>();
//        myNav = GetComponent<NavMeshAgent>();
//        myanim = GetComponent<Animator>();
        
//        myNav.speed = myState.pSpeed;
//        myNav.stoppingDistance = myState.pAttackRange;
//        attackTime = myState.pAttackSpeed;
//        setTargetTime = curSetTarget;

//        if (this.gameObject.tag == "RedTeam")
//        {
//            isRedUnit = true;
//            target = GameObject.FindGameObjectWithTag("BlueNex");
//        }
//        else
//        {
//            target = GameObject.FindGameObjectWithTag("RedNex");
//        }
//        myState.estate = CharacterState.eState.GoNexus;
//        if(myState.estate == CharacterState.eState.GoNexus)
//        {
//            myNav.SetDestination(target.transform.position);            
//        }
       
//    }
//	void UnitAttack(int damage)
//    {
//        if(target != null)
//        {
//            CharacterState atkTarget = target.GetComponent<CharacterState>();        
//            atkTarget.pHealth = damage >= atkTarget.pHealth ? 0 : atkTarget.pHealth - damage;
//        }
//        //int realDamage = def >= damage ? 1 : damage - def; //데미지가 방어력보다 작다면 1을 그렇지 않다면 방어력을 뺀 공격력을
//        //hp = realDamage >= hp ? 0 : hp - realDamage; //정해진 공격력보다 hp가 작을경우 hp는 0이되고 높을경우엔 해당 공격력 만큼 뺀다.
//    }
//    void UnitRook()
//    {
//        Quaternion rooking = Quaternion.LookRotation(target.transform.position - transform.position);
//        transform.rotation = Quaternion.Slerp(transform.rotation, rooking, 10f * Time.deltaTime);
//    }
//    public void GoNexus()
//    {
//        myNav.ResetPath();
//        setTarget = null;
//        target = null;
//        if (this.gameObject.tag == "RedTeam")
//        {
//            isRedUnit = true;
//            target = GameObject.FindGameObjectWithTag("BlueNex");
//        }
//        else
//        {
//            target = GameObject.FindGameObjectWithTag("RedNex");
//        }
//        if(myState.estate != CharacterState.eState.Dead)
//        {
//            myState.estate = CharacterState.eState.GoNexus;        
//        }
//    }
//    void ChangeState()
//    {
//        if (myState.estate != CharacterState.eState.Dead)
//        {
//            if (curatkTime > 0 && myState.estate != CharacterState.eState.Attacking)
//            {
//                curatkTime = curatkTime - Time.deltaTime;
//            }
//            switch (myState.estate)
//            {
//                case CharacterState.eState.GoNexus:
//                    {
                        
//                        myNav.SetDestination(target.transform.position);
//                        //myNav.Move(target.transform.position);
                        
//                        if (isRedUnit)
//                        {
//                            setTarget = Physics.OverlapSphere(transform.position, 50f, 1 << LayerMask.NameToLayer("BlueUnit"));
//                        }
//                        else
//                        {
//                            setTarget = Physics.OverlapSphere(transform.position, 50f, 1 << LayerMask.NameToLayer("RedUnit"));
//                        }
//                        for (int i = 0; i < setTarget.Length; i++)
//                        {
//                            if(i != setTarget.Length - 1)
//                            {
//                                if (Vector3.Distance(transform.position,setTarget[i].transform.position)> Vector3.Distance(transform.position, setTarget[i+1].transform.position))
//                                {
//                                    target = setTarget[i + 1].gameObject;
//                                    myState.estate = CharacterState.eState.GoAttack;

//                                }
//                                else
//                                {
//                                    target = setTarget[i].gameObject;
//                                }
//                            }                            
//                        }
//                        //if (setTarget.Length>=1)
//                        //{
//                        //    target = setTarget[0].gameObject;
//                        //    print(this.name + "가 " + target.name + "공격");
//                        //}

//                        break;
//                    }
//                case CharacterState.eState.GoAttack:
//                    {
//                        myNav.SetDestination(target.transform.position);
//                        setTargetTime -= Time.deltaTime;
//                        if (myNav.remainingDistance <= myNav.stoppingDistance)
//                        {
//                            myState.estate = CharacterState.eState.Attacking;
//                        }
//                        if (target.GetComponent<CharacterState>().estate == CharacterState.eState.Dead)
//                        {
//                            GoNexus();
//                        }
//                        //if (setTargetTime < 0)
//                        //{
//                        //    print("다시정해");
//                        //    setTargetTime = curSetTarget;
//                        //    GoNexus();
//                        //}
//                        break;
//                    }
//                case CharacterState.eState.Attacking:
//                    {
//                        CharacterState targetState = target.GetComponent<CharacterState>();
//                        if (myNav.remainingDistance >= myNav.stoppingDistance)
//                        {
//                            GoNexus();
//                        }
//                        else
//                        {
//                            UnitRook();
//                            if (curatkTime > 0)
//                            {
//                                curatkTime = curatkTime - Time.deltaTime;
//                            }
//                            else
//                            {
//                                UnitAttack((int)myState.pPower);
//                                curatkTime = attackTime;
//                            }
//                        }
//                        if (targetState.estate == CharacterState.eState.Dead)
//                        {
//                            GoNexus();
//                        }

//                        break;
//                    }
                
//                default:
//                    {

//                        break;
//                    }
//            }

//        }

//    }
//    void DeadCheck()
//    {
//        if(myState.pHealth == 0)
//        {
//            GoNexus();
//            this.gameObject.SetActive(false);
//            myState.estate = CharacterState.eState.Dead;
//        }
//    }
//    // Update is called once per frame
//    void Update () {
//        ChangeState();
//        DeadCheck();
//    }
//}