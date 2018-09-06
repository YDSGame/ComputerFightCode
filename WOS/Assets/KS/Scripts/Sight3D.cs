using System.Collections.Generic;
using UnityEngine;

public class Sight3D : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    [SerializeField] private bool drawSightCircle;
    [SerializeField] private bool drawSightLine;
    [SerializeField] private bool drawAttackAreaCircle;
    [SerializeField] private bool drawAttackAreaLine;
    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float sightViewAngle;    //시야각
    [SerializeField] public float sightViewDistance; //시야거리
    [Range(0f, 360f)]
    [SerializeField] private float attackAreaViewAngle;    //공격범위
    [SerializeField] public float attackAreaViewDistance; //시야거리

    [SerializeField] private LayerMask TargetMask;    //Enemy 레이어마스크 지정을 위한 변수
    [SerializeField] public LayerMask ObstacleMask;  //Obstacle 레이어마스크 지정 위한 변수(아군)

    [HideInInspector] public Collider[] sightTargets;
    [HideInInspector] public Collider[] attackTargets;

    private Transform _transform;
    void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    //private void Start()
    //{
    //    sightViewDistance = GetComponentInParent<UnitState>().pSight;

    //    attackAreaViewDistance = GetComponentInParent<UnitState>().pAttackRange;
    //}
    //void Update()
    //{      
    //    //FindVisibleTargets();   //Enemy인지 Obstacle인지 판별
    //    //FindAttackTarget();
    //}

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        //좌우 회전값 갱신
        angleInDegrees += transform.eulerAngles.y;
        //경계 벡터값 반환
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    private void OnDrawGizmos() //디버그용
    {
        if (debugMode)
        {
            Vector3 sightLeftBoundary = DirFromAngle(-sightViewAngle / 2);
            Vector3 sightRightBoundary = DirFromAngle(sightViewAngle / 2);
            Vector3 attackLeftBoundary = DirFromAngle(-attackAreaViewAngle / 2);
            Vector3 attackRightBoundary = DirFromAngle(attackAreaViewAngle / 2);
            Vector3 originPos = transform.position;
            //Sight
            if (sightViewAngle != 360)
            {
                if (drawSightLine)
                {
                    Debug.DrawRay(originPos, sightLeftBoundary * sightViewDistance, Color.green);
                    Debug.DrawRay(originPos, sightRightBoundary * sightViewDistance, Color.green);
                }
            }
            if (drawSightCircle)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(originPos, sightViewDistance);
            }

            //AttackArea
            if (attackAreaViewAngle != 360)
            {
                if (drawAttackAreaLine)
                {
                    Debug.DrawRay(originPos, attackLeftBoundary * attackAreaViewDistance, Color.green);
                    Debug.DrawRay(originPos, attackRightBoundary * attackAreaViewDistance, Color.green);
                }
            }
            if (drawAttackAreaCircle)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(originPos, attackAreaViewDistance);
            }
        }
    }

    public List<Collider> FindVisibleTargets()
    {
        //시야거리 내에 존재하는 정한 레이어에 해당하는 모든 오브젝트 받아오기
        sightTargets = Physics.OverlapSphere(_transform.position, sightViewDistance, TargetMask);
        List<Collider> filter = new List<Collider>();
        for (int i = 0; i < sightTargets.Length; i++)
        {
            //print(targets[i]);                      
            Transform target = sightTargets[i].transform;

            //타겟까지의 단위벡터
            Vector3 dirToTarget = (target.position - _transform.position).normalized;

            //_transform.forward와 dirToTarget은 모두 단위벡터이므로 내적값은 두 벡터가 이루는 각의 Cos값과 같다.
            //내적값이 시야각/2의 Cos값보다 크면 시야에 들어온 것이다.
            //if (Mathf.Cos(Vector3.Dot(_transform.forward, dirToTarget)) < Mathf.Cos((sightViewAngle / 2))) //안에 들어왔을때
            //if (Vector3.Angle(_transform.forward, dirToTarget) < sightViewAngle/2)
            if (Vector3.Dot(_transform.forward, dirToTarget) > Mathf.Cos((sightViewAngle / 2) * Mathf.Deg2Rad)) //안에 들어왔을때
            {
                //float distToTarget = Vector3.Distance(_transform.position, target.position);

                //if (!Physics.Raycast(_transform.position, dirToTarget, distToTarget, ObstacleMask))
                //{
                //    if (drawSightLine)
                //        Debug.DrawLine(_transform.position, target.position, Color.red);
                //}
                filter.Add(sightTargets[i]);
            }          
        }
        return filter;
    }
    public List<Collider> FindAttackTarget()
    {
        //시야거리 내에 존재하는 정한 레이어에 해당하는 모든 오브젝트 받아오기
        attackTargets = Physics.OverlapSphere(_transform.position, attackAreaViewDistance, TargetMask);
        List<Collider> filter = new List<Collider>();
        for (int i = 0; i < attackTargets.Length; i++)
        {
            //print(targets[i]);            
            Transform target = attackTargets[i].transform;

            //타겟까지의 단위벡터
            Vector3 dirToTarget = (target.position - _transform.position).normalized;

            //_transform.forward와 dirToTarget은 모두 단위벡터이므로 내적값은 두 벡터가 이루는 각의 Cos값과 같다.
            //내적값이 시야각/2의 Cos값보다 크면 시야에 들어온 것이다.
            if (Vector3.Dot(_transform.forward, dirToTarget) > Mathf.Cos((attackAreaViewAngle / 2) * Mathf.Deg2Rad))
            //if (Vector3.Angle(_transform.forward, dirToTarget) < sightViewAngle/2)
            {
                //float distToTarget = Vector3.Distance(_transform.position, target.position);

                //if (!Physics.Raycast(_transform.position, dirToTarget, distToTarget, ObstacleMask))
                //{
                //    if (drawAttackAreaLine)
                //    Debug.DrawLine(_transform.position, target.position, Color.red);
                //}
                filter.Add(sightTargets[i]);
            }          
        }
        return filter;
    }
    public List<Collider> FindAttackTargets(Transform _target, float _attackRange)
    {
        //시야거리 내에 존재하는 정한 레이어에 해당하는 모든 오브젝트 받아오기
        attackTargets = Physics.OverlapSphere(_target.position, _attackRange, TargetMask);
        List<Collider> filter = new List<Collider>();

        for (int i = 0; i < attackTargets.Length; i++)
        {
            //print(targets[i]);

            Transform target = attackTargets[i].transform;

            //타겟까지의 단위벡터
            Vector3 dirToTarget = (target.position - _transform.position).normalized;

            //_transform.forward와 dirToTarget은 모두 단위벡터이므로 내적값은 두 벡터가 이루는 각의 Cos값과 같다.
            //내적값이 시야각/2의 Cos값보다 크면 시야에 들어온 것이다.



            //if (!Physics.Raycast(_transform.position, dirToTarget, distToTarget, ObstacleMask))
            //{
            //    if (drawAttackAreaLine)
            //    Debug.DrawLine(_transform.position, target.position, Color.red);
            //}       
            filter.Add(attackTargets[i]);
        }
        return filter;
    }
}

