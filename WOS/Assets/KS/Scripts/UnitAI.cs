//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class UnitAI : MonoBehaviour {
//    CharacterState myState;
//    CharacterState targetState;
//    NavMeshAgent myNav;
//    Sight3D mySight;
//    public GameObject wayPoint;
//    public GameObject target;

//    public Collider[] sightTargets;
//    public Collider[] attackAreaTargets;
//    public bool imDead =false;
//    float follwingTime = 3f;
//    float curFollwing;    
//	// Use this for initialization
//	void Start () {
//        myState = GetComponent<CharacterState>();
//        myNav = GetComponent<NavMeshAgent>();
//        mySight = GetComponent<Sight3D>();
//        myNav.speed = myState.pSpeed;
//        //myNav.stoppingDistance = myState.pAttackRange;
//        StartCoroutine("Action");
//        StartCoroutine("DeadCheck");
//        curFollwing = follwingTime;        
//    }
//    private void Update()
//    {
//        SpawnCheck();
//    }
//    public IEnumerator Action()
//    {
//        while(myState.estate != CharacterState.eState.Dead)
//        {
//            switch (myState.estate)
//            {
//                case CharacterState.eState.Move: 
//                    {
//                        if(target == null)
//                        {
//                            //print("적이없다 움직이자");
//                            myNav.SetDestination(wayPoint.transform.position);
//                        }
//                        if(target != null)
//                        {                           
//                            myNav.SetDestination(target.transform.position);                          
//                        }                        
//                        myNav.avoidancePriority = 51;
//                        myNav.isStopped = false;
                                                                   
//                        yield return new WaitForSeconds(0.25f);
//                        //print("시야 타겟체크");
//                        sightTargets = mySight.FindVisibleTargets();
//                        if(sightTargets.Length > 0)
//                        {
//                            //print("타겟 발견");
//                            target = sightTargets[0].gameObject;
//                            targetState = target.GetComponent<CharacterState>();
//                            StartCoroutine("TargetInAttackArea");
//                        }
//                        break;
//                    }
//                case CharacterState.eState.GoAttack:
//                    {
//                        //print(this.gameObject.name + " -1");
//                        if(target != null)//타겟이 존재하면
//                        {
//                            if(targetState.estate == CharacterState.eState.Dead)
//                            {
//                                StartCoroutine("EnemyCheck");
//                                yield return null;
//                            }
//                            //print(this.gameObject.name + " -2");
//                            //print(target);
//                            //myNav.SetDestination(target.transform.position); //쫓아감
//                            if(target != null)
//                            {
//                                if(Vector3.Distance(transform.position,target.transform.position) <= myState.pAttackRange) //공격사정거리 안에 들어옴
//                                {
//                                    //print(this.gameObject.name + " -3");
//                                    myNav.isStopped = true; //나의 움직임을 멈춤
//                                    myNav.velocity = Vector3.zero;
//                                    //myNav.Stop(true);
//                                    myNav.avoidancePriority = 49; //우선순위를 올림
//                                    StartCoroutine("RookTarget"); //적을 바라봄
//                                    attackAreaTargets = mySight.FindAttackTarget(); //공격범위에 해당하는 적들을 받아옴
//                                    if(attackAreaTargets.Length > 0) //적들이 있다면
//                                    {
//                                        //print(this.gameObject.name + " -4");
//                                        target = attackAreaTargets[0].gameObject;
//                                        targetState = target.GetComponent<CharacterState>();
//                                        for (int i = 0; i < attackAreaTargets.Length; i++)
//                                        {
//                                            if(target == attackAreaTargets[i].gameObject) //타겟으로 삼고있는 놈을 공격
//                                            {
//                                                if(targetState.estate != CharacterState.eState.Dead) //타겟이 죽지않았다면
//                                                {
//                                                    //print(this.gameObject.name + "공격중!!!");
//                                                    targetState.pHealth = targetState.pHealth <= myState.pPower ? 0 : targetState.pHealth - myState.pPower;
//                                                    yield return new WaitForSeconds(myState.pAttackSpeed);
//                                                }
//                                                if(targetState.estate == CharacterState.eState.Dead) //타겟이 죽었다면
//                                                {
//                                                    StartCoroutine("EnemyCheck");
//                                                }
//                                            }
                                    
//                                        }
//                                    }
//                                }
//                                else //공격 사정거리안에 들어오지 않았다면 공격행동을 멈추고 다시 쫓아감
//                                {
//                                    //print(this.gameObject.name + " -5");
//                                    //print("사정거리안에 들어오지 않고있다");
//                                    myNav.avoidancePriority = 50; //우선순위를 낮춤
//                                    myNav.isStopped = false; //움직임
//                                    curFollwing = curFollwing - Time.deltaTime;
//                                    if(curFollwing < 0)
//                                    {
//                                        //print("시간초과");
//                                        StartCoroutine("EnemyCheck");
//                                        curFollwing = follwingTime;
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                StartCoroutine("MoveOn");
//                            }
//                        }
//                        else
//                        {
//                            //print(this.gameObject.name+" -6");
//                            StartCoroutine("EnemyCheck");
//                        }                       
//                        break;
//                    }               
//                default:
//                    {                        
//                        break;
//                    }                
//            }            
//            yield return null;
//        }
//        yield return null;
//    }
//    public IEnumerator EnemyCheck()
//    {
//        myState.estate = CharacterState.eState.EnemyCheck;
//        //print("적 체크한다");
//        bool enemy = false;
//        //attackAreaTargets = mySight.FindAttackTarget();
//        if(attackAreaTargets.Length > 0)
//        {
//            if(attackAreaTargets[0].gameObject != target)
//            {
//                target = attackAreaTargets[0].gameObject;
//                targetState = target.GetComponent<CharacterState>();
//                enemy = true;
//                myState.estate = CharacterState.eState.GoAttack;
//            }       
//        }
//        if(!enemy) //적이 없다면
//        {
//            //print("적이 공격범위에 없다");
//            StartCoroutine("SightCheck"); //시야범위로 체크한다.
//        }
//        yield return null;
//    }
//    public IEnumerator SightCheck()
//    {
//        //print("시야 체크한다");        
//        sightTargets = mySight.FindVisibleTargets();//시야에 있는 오브젝트 재정의        
//        if(sightTargets.Length > 0) //시야에 적들이 있다면
//        {            
//            target = sightTargets[0].gameObject; //타겟을 정한다
//            targetState = target.GetComponent<CharacterState>();
//            myState.estate = CharacterState.eState.GoAttack; //공격하러간다.
//            //myNav.avoidancePriority = 50;
//            //myNav.isStopped = false;
//        }
//        else //시야에 적들이 없다면
//        {
//            //print("시야에 적이 없다");
//            StartCoroutine("MoveOn");
//        }
//        yield return null;
//    }
//   public IEnumerator DeadCheck()
//    {
//        while(myState.estate != CharacterState.eState.Dead)
//        {
//            if(myState.pHealth == 0)
//            {
//                StopAllCoroutines();
//                this.gameObject.SetActive(false);
//                target = null;
//                targetState = null;
//                myState.estate = CharacterState.eState.Dead;
//                imDead = true;
//            }
//            yield return null;
//        }
//        yield return null;
//    }
//    public void SpawnCheck()
//    {
//        if (imDead && this.gameObject.activeInHierarchy)
//        {
//            StartCoroutine("Action");
//            StartCoroutine("DeadCheck");
//            imDead = false;
//            print("ReSpawn");
//        }
//    }
//    IEnumerator TargetInAttackArea()
//    {
//        if(Vector3.Distance(transform.position,target.transform.position) <= myState.pAttackRange)
//        {
//            myState.estate = CharacterState.eState.GoAttack;
//        }
//        yield return null;
//    }
//    IEnumerator RookTarget()
//    {
//        while (myNav.isStopped)
//        {
//            if(target != null)
//            {
//                Quaternion lookat = Quaternion.LookRotation(target.transform.position - transform.position);
//                transform.rotation = Quaternion.Slerp(transform.rotation, lookat, 10 * Time.deltaTime);
//            }
//            yield return null;
//        }
//        yield return null;
//    }
    
//    IEnumerator MoveOn()
//    {
//        target = null;
//        targetState = null;
//        //sightTargets = null;
//        //attackAreaTargets = null;
//        //myNav.isStopped = false;
//        myState.estate = CharacterState.eState.Move;
//        yield return null;
//    }
//}
