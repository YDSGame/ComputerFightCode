using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class BlueTeam : MonoBehaviour {

    [SerializeField] private bool debugMode;
    public GameObject RedNex;
    public GameObject Target = null;
    public Unit unit;
    public NavMeshAgent nav;
    public Collider[] Enemy = null;
    public LayerMask mask;

	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
        nav.speed = unit.nMoveSpd;
        unit.eState = Unit.State.Move;
        StartCoroutine("UnitState");
        StartCoroutine("Dead");

    }
	
	// Update is called once per frame
	void Update () {
     
       
    }
    //IEnumerator Move()
    //{
      
    //        if (Target == null)
    //        {
    //            nav.SetDestination(RedNex.transform.position);
    //            Enemy = Physics.OverlapSphere(transform.position, unit.fSight, mask);
    //            print(Enemy.Length);
    //            if (Enemy.Length != 0)
    //            {
    //                Target = Enemy[0].gameObject;
    //                unit.eState = Unit.State.Tracking;
    //                // yield return StartCoroutine("UnitState");
    //                yield return null;
    //            }

    //            yield return null;
    //        }
    //        yield return null;
            
        

    //    //if (Target != null)
    //    //{

    //    //    //while (Target.GetComponent<Unit>().eState != Unit.State.DIE)
    //    //    //  {

    //    //    //nav.SetDestination(Target.transform.position);
    //    //    unit.eState = Unit.State.ATTACK;
    //    //    //StartCoroutine("UnitState");
    //    //    print("타겟에게 가는중");
    //    //    //nav.SetDestination(Target.transform.position);

    //    //    yield return StartCoroutine("UnitState");
    //    //    //  }
    //    //}
     

    //}
    //IEnumerator Attack(GameObject mEnemy)
    //{
   
    //    Unit cEnemy = mEnemy.GetComponent<Unit>();
    //    int Dis = (int)Vector3.Distance(transform.position, cEnemy.transform.position);
    //    print("적유닛과 나의 거리 :" + Dis);
    //    int DisRange = (int)unit.fADRange;
    //    while (unit.eState == Unit.State.ATTACK && cEnemy.eState != Unit.State.DIE)
    //    {
    //        if (Dis <= unit.fADRange)
    //        {
    //            cEnemy.nHp = cEnemy.nHp - (unit.nAD - cEnemy.nArmor);
    //            yield return new WaitForSeconds(unit.fAttackSpd);
    //        }
    //        else if (Dis >= unit.fADRange)
    //        {
    //            unit.eState = Unit.State.Tracking;
    //            yield return null;
    //        }
    //        yield return null;
    //    }
    //    if (cEnemy.nHp <= 0.1f)
    //    {
           
    //        yield return mEnemy = null;
    //    }
    //}
    IEnumerator UnitState()
    {
        int Dis;
       while(unit.eState != Unit.State.DIE)
        {
            switch (unit.eState)
            {
              
                case Unit.State.Move:
                    {
                        nav.isStopped = false;
                        if(Target == null)
                        {
                                
                                nav.SetDestination(RedNex.transform.position);
                                Enemy = Physics.OverlapSphere(transform.position, unit.fSight, mask);
                            

                            if (Enemy.Length != 0)
                         {
                                Target = Enemy[0].gameObject;
                                unit.eState = Unit.State.Tracking;
                                yield return null;
                         }
                          //if(unit.nHp <= 0.1f)
                          //  {
                          //      Debug.Log("블루유닛죽음");
                          //      unit.eState = Unit.State.DIE;
                          //      Target.GetComponent<Unit>().eState = Unit.State.DIE;
                          //      Target.gameObject.SetActive(false);
                          //      Enemy = null;
                          //      yield return null;
                          //  }
                        }
                        break;
                    }
                case Unit.State.Tracking:
                    {
                        if (Target != null)
                        {
                            nav.isStopped = false;
                            Dis = (int)Vector3.Distance(transform.position, Target.transform.position);
                            nav.SetDestination(Target.transform.position);
                            Debug.Log("유닛시야에들어옴");
                            if (Dis <= unit.fADRange)
                            {
                                
                                unit.eState = Unit.State.ATTACK;
                                yield return new WaitForSeconds(0.2f);
                            }
                            if (Target.GetComponent<Unit>().nHp <= 0)
                            {
                                Debug.Log("적 유닛 죽음");
                                Target.GetComponent<Unit>().eState = Unit.State.DIE;
                                Target.gameObject.SetActive(false);
                                unit.eState = Unit.State.Move;
                                yield return Target = null;
                            }
                        }
                        //if (unit.nHp <= 0.1f)
                        //{
                        //    Debug.Log("블루유닛죽음");
                        //    unit.eState = Unit.State.DIE;
                        //    Enemy = null;

                        //    yield return null;
                        //}
                    }
                    break;
                case Unit.State.ATTACK:
                    {
                        if (Target.GetComponent<Unit>().eState != Unit.State.DIE)
                        {
                            Dis = (int)Vector3.Distance(transform.position, Target.transform.position);
                            nav.SetDestination(Target.transform.position + new Vector3(0, 0, -(float)unit.fADRange));
                            if (Dis <= unit.fADRange)
                            {
                                nav.isStopped = true;
                                Debug.Log("공격범위안들어옴");
                                Target.GetComponent<Unit>().nHp = Target.GetComponent<Unit>().nHp - (unit.nAD - Target.GetComponent<Unit>().nArmor);
                                yield return new WaitForSeconds(unit.fAttackSpd);
                            }
                            else if(Dis >= unit.fADRange)
                            {
                                nav.isStopped = false;
                                unit.eState = Unit.State.Tracking;
                                yield return null;
                            }
                        }
                        if(Target.GetComponent<Unit>().nHp <= 0.1f)
                        {
                            Debug.Log("적 유닛 죽음");
                            Target.GetComponent<Unit>().eState = Unit.State.DIE;
                            Target.gameObject.SetActive(false);
                            unit.eState = Unit.State.Move;
                            yield return Target = null;
                        }
                        //if (unit.nHp <= 0.1f)
                        //{
                        //    Debug.Log("블루유닛죽음");
                        //    unit.eState = Unit.State.DIE;
                        //  //  unit.GetComponent<GameObject>().SetActive(false);
                        //    Enemy = null;
                        //    yield return null;
                        //}
                    }
                    break;
                //case Unit.State.DIE:
                //    {
                //        if (unit.eState == Unit.State.DIE)
                //        {
                //            unit.GetComponent<GameObject>().SetActive(false);
                //            yield return null;
                //            if (unit.GetComponent<GameObject>().activeInHierarchy == true)
                //            {
                //                unit.GetComponent<GameObject>().SetActive(false);
                //                yield return null;
                //            }
                //        }

                   // }
                default:
                    {
                        break;
                    }
            }
            yield return null;
        }
        yield return null; ;
    }
    public IEnumerator Dead()
    {
        while (unit.eState != Unit.State.DIE)
        {
            if (unit.nHp <= 0.1f)
            {
                StopAllCoroutines();
                this.gameObject.SetActive(false);
                Target = null;
                unit.eState = Unit.State.DIE;
            }
            yield return null;
        }
        yield return null;
    }
    //시야 그리기
    private void OnDrawGizmos()
            {
        if (debugMode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(unit.transform.position, unit.fADRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(unit.transform.position, unit.fSight);

        }
            }

            //IEnumerator EnemyCheck()
            //{

            //    Enemy = Physics.OverlapSphere(unit.transform.position, ViewDistance, LayerMask.NameToLayer("RedUnit"));
            //    Target = Enemy[0].gameObject;
            //    unit.eState = Unit.State.ATTACK;
            //    if(Target.GetComponent<Unit>().eState == Unit.State.DIE)
            //    {
            //        Target = null;
            //        Enemy = Physics.OverlapSphere(unit.transform.position, ViewDistance, LayerMask.NameToLayer("RedUnit"));
            //        Target = Enemy[0].gameObject;

            //        yield return null;
            //    }

            //}
        }
