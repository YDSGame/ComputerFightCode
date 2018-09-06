using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class RedTeam : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    public GameObject BlueNex;
    public GameObject Target = null;
    public Unit unit;
    public NavMeshAgent nav;
    public Collider[] Enemy = null;
    public LayerMask mask;
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        unit = this.GetComponent<Unit>();
        nav.speed = unit.nMoveSpd;
        unit.eState = Unit.State.Move;
        StartCoroutine("UnitState");
        StartCoroutine("Dead");

    }
    IEnumerator UnitState()
    {
        int Dis;
        while (unit.eState != Unit.State.DIE)
        {
            switch (unit.eState)
            {
                case Unit.State.Move:
                    {
                        nav.isStopped = false;
                        if (Target == null)
                        {

                            nav.SetDestination(BlueNex.transform.position);
                            Enemy = Physics.OverlapSphere(transform.position, unit.fSight, mask);

                            if (Enemy.Length != 0)
                            {
                                Target = Enemy[0].gameObject;
                                unit.eState = Unit.State.Tracking;
                                yield return null;
                            }

                        }
                        //if (unit.nHp <= 0)
                        //{
                        //    Debug.Log("레드유닛죽음");
                        //    unit.eState = Unit.State.DIE;
                        //    yield return null;
                        //}
                        break;
                    }
                case Unit.State.Tracking:
                    {
                        nav.isStopped = false;
                        if (Target != null)
                        {
                            Dis = (int)Vector3.Distance(transform.position, Target.transform.position);
                            nav.SetDestination(Target.transform.position);
                            if (Dis <= unit.fADRange)
                            {

                                unit.eState = Unit.State.ATTACK;
                                yield return null;
                            }
                        }
                        //if (Target.GetComponent<Unit>().nHp <= 0)
                        //{
                        //    Target.GetComponent<Unit>().eState = Unit.State.DIE;
                        //    Target.gameObject.SetActive(false);
                        //    unit.eState = Unit.State.Move;
                        //    yield return Target = null;
                        //}
                        //if (unit.nHp <= 0)
                        //{
                        //    Debug.Log("레드유닛죽음");
                        //    unit.eState = Unit.State.DIE;
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
                                Debug.Log("추적해라");
                                Target.GetComponent<Unit>().nHp = Target.GetComponent<Unit>().nHp - (unit.nAD - Target.GetComponent<Unit>().nArmor);
                                yield return new WaitForSeconds(unit.fAttackSpd);
                            }
                            else if (Dis >= unit.fADRange)
                            {
                                unit.eState = Unit.State.Tracking;
                                yield return null;
                            }
                        }
                        if (Target.GetComponent<Unit>().nHp <= 0)
                        {
                            //Target.GetComponent<Unit>().eState = Unit.State.DIE;
                            //Target.gameObject.SetActive(false);
                            unit.eState = Unit.State.Move;
                            yield return Target = null;
                        }
                        //if (unit.nHp <= 0)
                        //{
                        //    Debug.Log("레드유닛죽음");
                        //    unit.eState = Unit.State.DIE;
                        //    yield return null;
                        //}
                    }
                    break;
                case Unit.State.DIE:
                    {
                        if (unit.eState == Unit.State.DIE)
                        {
                            unit.GetComponent<GameObject>().SetActive(false);
                            yield return null;
                        }
                    }
                    break;


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
            if (unit.nHp <= 0)
            {
                unit.eState = Unit.State.DIE;
                this.gameObject.SetActive(false);
                Target = null;
                Enemy = null;
                yield return null;
            }
            yield return null;
        }
    }
    //시야 그리기
    private void OnDrawGizmos()
    {
        if (debugMode)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, unit.fADRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, unit.fSight);
        }


    }

   
}

