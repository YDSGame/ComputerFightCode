//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//public class UnitsActions : MonoBehaviour {
//    CharacterState myState;
//    CharacterState attackedEnemy;
//    bool isRedUnit = false;
//    bool attackingNow;
//    Collider[] EnemyCheck;
//    GameObject enemytoAttack;
//    GameObject movingTarget;
//    GameObject hitByOb;
//    GameObject rayhit;
//    float curPatrolTime = 3f;
//    float patrolTime = 3f;
//    NavMeshAgent myNav;

//    void Start () {
//        myState = this.GetComponent<CharacterState>();
//        myNav = GetComponent<NavMeshAgent>();
//        myNav.speed = myState.pSpeed;
//        myNav.stoppingDistance = myState.pAttackRange;
//        if (this.gameObject.tag == "RedTeam")
//        {
//            isRedUnit = true;
//            movingTarget = GameObject.FindGameObjectWithTag("BlueNex");
//        }
//        else
//        {
//            movingTarget = GameObject.FindGameObjectWithTag("RedNex");
//        }
//        //print(movingTarget);
//        if(enemytoAttack == null)
//        {
//            //print("적이없다");
//        }
//    }
    
//    // Update is called once per frame
//    void Update () {
//        if(myState.pHealth >= 0 && myState.estate != CharacterState.eState.Dead)
//        {
//            //print("피가 0이 아니다");
//            switch (myState.estate)
//            {
//                case CharacterState.eState.GoNexus:
//                    {                        
//                        if (isRedUnit)
//                        {
//                            //print("적 발견2");
//                            UnitMove(movingTarget);
//                            UnitLookAt(movingTarget);
//                            EnemyCheck = Physics.OverlapSphere(transform.position, 20f, 1 << LayerMask.NameToLayer("BlueUnit"));
//                            //print(EnemyCheck.Length);
//                        }
//                        else if (!isRedUnit)
//                        {
//                            //print("적 발견2");
//                            UnitMove(movingTarget);
//                            UnitLookAt(movingTarget);
//                            EnemyCheck = Physics.OverlapSphere(transform.position, 20f, 1 << LayerMask.NameToLayer("RedUnit"));
//                            //print(EnemyCheck.Length);
//                        }
//                        //for (int i = 0; i < EnemyCheck.Length; i++)
//                        //{
//                        //    if (i != EnemyCheck.Length - 1)
//                        //    {
//                        //        if (Vector3.Distance(transform.position, EnemyCheck[i].transform.position) > Vector3.Distance(transform.position, EnemyCheck[i + 1].transform.position))
//                        //        {
//                        //            enemytoAttack = EnemyCheck[i + 1].gameObject;
//                        //            attackedEnemy = enemytoAttack.GetComponent<CharacterState>();
//                        //        }
//                        //    }
//                        //    else
//                        //    {
//                        //        enemytoAttack = EnemyCheck[i].gameObject;
//                        //        attackedEnemy = enemytoAttack.GetComponent<CharacterState>();
//                        //    }
//                        //}
//                        if(EnemyCheck.Length >= 1)
//                        {
//                            if (!hitByOb)
//                            {
//                                enemytoAttack = EnemyCheck[0].gameObject;
//                                attackedEnemy = enemytoAttack.GetComponent<CharacterState>();
//                                movingTarget = enemytoAttack;
//                            }
//                            myState.estate = CharacterState.eState.GoAttack;
//                            return;
//                        }                                                                           
//                        else
//                        {
//                            GotoNexus();
//                        }
//                        break;
//                    }
//                case CharacterState.eState.GoAttack:
//                    {                        
//                        if (myState.estate != CharacterState.eState.Dead)
//                        {
//                            if (attackedEnemy.estate != CharacterState.eState.Dead)
//                            {
//                                if (Vector3.Distance(transform.position,enemytoAttack.transform.position) > myState.pAttackRange)
//                                {
//                                    if (rayhit == null)
//                                    {
//                                        UnitLookAt(movingTarget);
//                                        UnitMove(movingTarget);
//                                    }
//                                    else
//                                    {
//                                        UnitMoveAround();
//                                    }
                                    
//                                    patrolTime -= Time.deltaTime;
//                                    if(patrolTime < 0)
//                                    {
//                                        GotoNexus();
//                                        patrolTime = curPatrolTime;
//                                        return;
//                                    }
//                                }
//                                else
//                                {
//                                    myState.estate = CharacterState.eState.Attacking;
//                                    return;
//                                }
//                            }
//                            else
//                            {
//                                GotoNexus();                                                            
//                            }

//                        }
//                        break;
//                    }
//                case CharacterState.eState.Attacking:
//                    {
//                        //print(this.name + "가 공격중이다!!");
//                        if (myState.estate != CharacterState.eState.Dead)
//                        {
//                            //print(1);
//                            //if (Vector3.Distance(transform.position, enemytoAttack.transform.position) >= myState.pAttackRange)
//                            //{
//                            //    GotoNexus();
//                            //}
//                            if (enemytoAttack != null && Vector3.Distance(transform.position, enemytoAttack.transform.position) > 10)
//                            {
//                                //print(2);
//                                myState.estate = CharacterState.eState.GoNexus;
                                
//                            }
//                            else if (Vector3.Distance(transform.position, enemytoAttack.transform.position) <= myState.pAttackRange)
//                            {
//                                //print(3);
//                                if (!attackingNow)
//                                {
//                                    //print(4);
//                                    attackingNow = true;
//                                    InvokeRepeating("UnitAttack", 0, myState.pAttackSpeed);
//                                }
//                                else if (attackedEnemy.pHealth <= 0)
//                                {
//                                    //print(5);
//                                    attackedEnemy.estate = CharacterState.eState.Dead;
//                                }
//                                else
//                                {
//                                    GotoNexus();
//                                }
//                            }
//                            else
//                            {
//                                GotoNexus();
//                            }
//                        }
//                        else if (enemytoAttack == null && attackedEnemy.estate == CharacterState.eState.Dead)
//                        {
//                            //print(6);
//                            GotoNexus();
//                        }

//                        break;
//                    }                
//                default:
//                    {
//                        break;
//                    }
//            }
//        }
//        if (myState.pHealth <= 0)
//        {
//            myState.gameObject.SetActive(false);
//            myState.estate = CharacterState.eState.Dead;
//            hitByOb = null;
//            myState.pHealth = myState.pMaxHealth;
//            return;
//        }        
//    }
//    void UnitMove(GameObject mTarget)
//    {
//        //print(this.name + "가 "+mTarget +"에게 간다");
//        //print(myState.pSpeed);
//        //transform.position = Vector3.MoveTowards(transform.position, mTarget.transform.position, myState.pSpeed * Time.deltaTime);
        

//    }
//    void UnitMoveAround()
//    {
//        print("돌아가라");
//        Quaternion moveAround = Quaternion.LookRotation(transform.right);
//        transform.rotation = Quaternion.Slerp(transform.rotation, moveAround, 100f * Time.deltaTime);
//        transform.position = Vector3.MoveTowards(transform.position, transform.forward, myState.pSpeed * Time.deltaTime);        
        
//    }
//    void UnitLookAt(GameObject mTarget)
//    {
//        //print(this.name + "가 " + mTarget + "를 본다");
//        Quaternion lookTarget = Quaternion.LookRotation(mTarget.transform.position - transform.position);
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, 10f * Time.deltaTime);
//    }
//    void GotoNexus()
//    {
//        if (this.gameObject.tag == "RedTeam")
//        {
//            movingTarget = GameObject.FindGameObjectWithTag("BlueNex");
//        }
//        else
//        {
//            movingTarget = GameObject.FindGameObjectWithTag("RedNex");
//        }
//        enemytoAttack = null;
//        attackedEnemy = null;
//        hitByOb = null;
//        attackingNow = false;
//        myState.estate = CharacterState.eState.GoNexus;
//        return;
//    }
//    void UnitAttack()
//    {       
//        if (hitByOb)
//        {
//            enemytoAttack = hitByOb;
//            attackedEnemy = enemytoAttack.GetComponent<CharacterState>();
//            movingTarget = enemytoAttack;
//        }
//        if (myState.estate != CharacterState.eState.Dead)
//        {
//            //myState.estate = CharacterState.eState.Attacking;
//            if(enemytoAttack == null)
//            {
//                GotoNexus();
//                CancelInvoke();
                
//                return;
//            }
//            if (Vector3.Distance(transform.position, enemytoAttack.transform.position) > myState.pAttackRange)
//            {
//                GotoNexus();
//                CancelInvoke();
               
//                return;
//            }
//            else if (attackedEnemy.pHealth >= 0)
//            {
//                if (enemytoAttack.GetComponent<UnitsActions>().hitByOb != null)
//                {
//                    enemytoAttack.GetComponent<UnitsActions>().hitByOb = this.gameObject;
//                }
//                attackedEnemy.pHealth = attackedEnemy.pHealth - myState.pPower;
//            }
//            if (attackedEnemy.pHealth <= 0 || attackedEnemy.estate == CharacterState.eState.Dead)
//            {
//                if (enemytoAttack.GetComponent<UnitsActions>().hitByOb)
//                {
//                    enemytoAttack.GetComponent<UnitsActions>().hitByOb = null;
//                }
//                GotoNexus();
//                CancelInvoke();
                                
//                return;
//            }
//        }
//        else
//        {
//            enemytoAttack = null;
//            attackedEnemy = null;
//            attackingNow = false;
//            return;
//        }        
//    }
//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    print("콜리션");
//    //    if (isRedUnit)
//    //    {
//    //        if (collision.gameObject.tag == "RedTeam")
//    //        {
//    //            print("레드저장");
//    //            rayhit = collision.gameObject;
//    //        }
//    //    }
//    //    else if(!isRedUnit)
//    //    {
//    //        if (collision.gameObject.tag == "BlueTeam")
//    //        {
//    //            print("블루저장");
//    //            rayhit = collision.gameObject;
//    //        }
//    //    }
//    //}
//    private void OnCollisionExit(Collision collision)
//    {
//        rayhit = null;
//    }
//}
