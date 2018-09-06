//#define DEBUG_MODE


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotNetAI : MonoBehaviour
{
    UnitState myState; //내 상태
    Rigidbody myRigidBody; //내 물리
    //CapsuleCollider myCapsuleCollider;//내 캡슐콜라이더
    GameObject myActive; //내 죽음 정보 (자식이 사라지면 죽음으로 판단)
    SoundAndEffect mySoundAndEffect;//이펙트 사운드 담아두는 클래스
    AudioSource myAudioSource; //실제 사운드 실행
    public Sight3D mySight; //내 시야
    
    int wayPointIndex; //현재 웨이포인트 상태
    bool attackCooltime = false; //공격 쿨타임
    bool imBlue; //팀정함
    public GameObject target; //타겟
    public Transform wayPoint; //웨이포인트
    public Transform[] wayPoints; //갈곳들
    float bleedingTime;  //출혈 시간 
    float defaultAR; //넥서스공격시 공격사거리 늘려줌
    bool attackingNexus = false; //넥서스 공격중/공격하러갈때
    public List<Collider> sightTargets = null; //시야범위체크용
    public List<Collider> attackAreaTargets = null; //공격범위체크용
    public List<Collider> rangeDistanceTargets = null; //원거리공격범위체크용(스플래쉬 공격용)
    //회피 이동 관련
    Transform myTransform; //내 정보
    string obstacle; //아군 레이어
    Vector3 moveDir; //내 움직일 방향
    Vector3 movePos; //움직일 양
    public float rayWidth; //레이 오른쪽 왼쪽 폭
    public float rotationSpeed; //회전속도 (6이 적당함)
    public float avoidRange; //장애물 인지거리
    public float sensivity; //장애물 피하는 감도

   

    // Use this for initialization
    void Start()
    {
        GetMyComponent(); //정보 가져오기
        SetMyStates(); //가져온 정보를 세팅하기        
        StartCoroutine(Action());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate() //고정된 프레임만큼 실행
    {
        //if (pv.isMine)
        //    myTransform.position = movePos;
        ////myRigidBody.velocity = movePos; //실제 움직임처리(RigidBody의 속도를 방향와거리를 가진 벡터로 더해준다)        
    }
    public IEnumerator Bleeding(float _BleedingTime)
    {
        bleedingTime = _BleedingTime;
        if (!myState.isBledding)
        {
            myState.isBledding = true;
            while (bleedingTime >= 0)
            {
                myState.pHealth -= 1; // 1초당 1씩 체력 감소
                bleedingTime--;
                yield return new WaitForSeconds(1f);
            }
            myState.isBledding = false;
        }
        yield return null;
    }
    
    void SetMyStates() //스타트때 세팅해야할 정보 모음
    {
        defaultAR = myState.pAttackRange;
        //rayWidth = myCapsuleCollider.radius; //나의 몸집의 절반
        if (CompareTag("BlueTeam")) //팀결정
        {
            imBlue = true;
            obstacle = "BlueUnit"; //아군결정
        }
        else
        {
            obstacle = "RedUnit";
        }
        mySight.sightViewDistance = myState.pSight; //시야체크 클래스에 내 시야거리정보를 넘긴다.
        mySight.attackAreaViewDistance = myState.pAttackRange; //시야체크 클래스에 내 공격거리 정보를 넘긴다.
        //foreach (Canvas c in FindObjectsOfType<Canvas>()) //체력바
        //{
        //    if (c.renderMode == RenderMode.WorldSpace) //월드스페이스인 캔버스 찾음
        //    {
        //        healthUI = Instantiate(healthBar, c.transform).transform; //체력바 생성후 캔버스에 그려주기
        //        healthUI.name = "HealthBar : " + name; //체력바 이름
        //        healthSlider = healthUI.GetChild(0).GetComponent<Image>(); //슬라이딩 할 이미지
        //        break;
        //    }
        //}
        
        wayPoint = wayPoints[0]; //웨이포인트 초기화        
    }

    void GetMyComponent() //스타트시 가져와야할 정보 모음
    {
        myState = GetComponent<UnitState>();
        myRigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        mySight = GetComponent<Sight3D>(); //주석이유 : 넥서스에서 생성시에 겟해줌
        //myCapsuleCollider = GetComponent<CapsuleCollider>();
        mySoundAndEffect = GetComponent<SoundAndEffect>();
        myAudioSource = GetComponent<AudioSource>();
        myActive = transform.GetChild(0).gameObject;        
    }
    void Moving(Transform _target)
    {
        myTransform.Translate(transform.forward * myState.pSpeed * Time.deltaTime, Space.World); //현재내가 보는 방향을 월드기준으로 움직인다        
    }
    void WayPointCheck()
    {
        if (Mathf.Abs(transform.position.z - wayPoint.transform.position.z) <= 2f) //웨이포인트와 나의 거리의 Z축의 절대값을 비교하여 1보다 가까워지면
        {
            if (wayPointIndex < wayPoints.Length - 1) //현재 웨이포인트를 다음으로 찍어준다.
            {
                wayPointIndex++;
                //print(indexWay);
                wayPoint = wayPoints[wayPointIndex];
                //Debug.Log("웨이포인트 변경");
            }
            else
            {
                if (imBlue)
                {
                    wayPoint = Gamemanager1.GetInstance().m_Nexus.RedNex.transform;
                }
                else
                {
                    wayPoint = Gamemanager1.GetInstance().m_Nexus.BlueNex.transform;
                }
            }
        }
    }

    public IEnumerator Action() //행동
    {
        while (myActive.activeInHierarchy)
        {
            //Debug.Log("행동하라");
            DeadCheck(); //죽음 체크 메서드
            //WayPointCheck(); //웨이포인트 갱신

         

            switch (myState.estate) //나의 상태 패턴
            {
                case UnitState.eState.Move: //무브상태일때
                    {
                        if (target == null)
                        {
                            myRigidBody.isKinematic = false; //밀려나는 상태
                            AvoidMove(wayPoint); //장애물을 피해서 웨이포인트까지 이동하라                        
                            LookRotation(moveDir); //장애물을 바라보지않으며 웨이포인트를 바라봐라
                            SightTargetCheck();
                            Moving(wayPoint);
                        }
                        else
                        {
                            myState.estate = UnitState.eState.GoAttack;
                        }
                        //Debug.Log("움직이는중");
                    }
                    break;
                case UnitState.eState.GoAttack: //공격하러 가는 상태
                    {
                        if (target != null)
                        {
                            myRigidBody.isKinematic = false; //밀려나는 상태                           
                            Moving(target.transform);
                            if (target.CompareTag("Nexus")) //공격하러 가는 오브젝트가 넥서스라면
                            {
                                if (myState.pAttackRange <= 7f)
                                {
                                    attackingNexus = true;
                                    myState.pAttackRange = 6f;
                                    SightTargetCheck();
                                }
                                else
                                {
                                    myState.pAttackRange = defaultAR;
                                }
                            }
                            if (Vector3.Distance(transform.position, target.transform.position) <= myState.pAttackRange) //사정거리 안에 들어왔다면
                            {
                                myState.estate = UnitState.eState.Attacking; //공격중인 상태로
                            }
                            else //사정거리 안에 들어오지 않았다면
                            {
                                AvoidMove(target.transform); //장애물을 피하며 이동하라
                                LookRotation(moveDir); //타겟을 바라보기
                            }
                            if (Vector3.Distance(transform.position, target.transform.position) > myState.pSight) //타겟이 시야범위에 벗어났다면
                            {
                                target = null; //타겟팅 취소
                                myState.estate = UnitState.eState.EnemyCheck; //무브상태로
                            }
                        }
                    }
                    break;
                case UnitState.eState.Attacking: //공격중인 상태
                    {
                        if (target != null)
                        {
                            myRigidBody.isKinematic = true; //물리(충돌)적용받지 않음 : 밀려나지 않는 상태
                            LookRotation(target.transform.position - myTransform.position); //공격중 일땐 바라만 본다.
                            if (Vector3.Distance(transform.position, target.transform.position) <= myState.pAttackRange)//사정거리 안에 들어왔다면
                            {
                                if (target.CompareTag("Nexus")) //넥서스를 공격중이라면
                                {
                                    attackingNexus = true;
                                    Attack(); //공격한다.
                                    SightTargetCheck();//공격중 적군이 있다면(시야범위 체크 메서드)
                                }
                                else
                                {
                                    Attack();
                                }
                                if (target.GetComponent<UnitState>().estate == UnitState.eState.Dead || target.GetComponent<UnitState>().pHealth <= 0) //깎은뒤 타겟이 죽었다면
                                {
                                    //공격중이 아니다
                                    target = null;
                                    myState.estate = UnitState.eState.EnemyCheck; //내 공격범위에 적이 있는지 체크한다                                
                                }
                            }
                            else //사정거리 밖으로 벗어났다면
                            {
                                myState.estate = UnitState.eState.GoAttack; //공격하러 가는 상태로
                            }
                        }
                    }
                    break;
                case UnitState.eState.EnemyCheck: //공격범위에 적이 있는지 체크중인 상태
                    {
                        AttackSightTargetCheck(); //공격범위 체크 메서드
                    }
                    break;
                case UnitState.eState.Dead: //죽음 상태
                    {
                        myRigidBody.isKinematic = false;
                        target = null;
                      
                        //myActive.SetActive(false);
                        float randomX;
                        float randomZ;
                        if (imBlue)
                        {
                            randomX = Random.Range(-40f, 40f);
                            randomZ = Random.Range(Gamemanager1.GetInstance().min[0].position.z, Gamemanager1.GetInstance().max[0].position.z);
                        }
                        else
                        {
                            randomX = Random.Range(-40f, 40f);
                            randomZ = Random.Range(Gamemanager1.GetInstance().min[1].position.z, Gamemanager1.GetInstance().max[1].position.z);
                        }
                        Vector3 mySetPosition = new Vector3(randomX, 0, randomZ); //죽었으니 스폰지점 랜덤 지정
                        gameObject.layer = LayerMask.NameToLayer("Default"); //죽었으니 타겟 지정이 안되도록 레이어 초기화
                        //pv.RPC("SendDead", PhotonTargets.All, pv.viewID, mySetPosition); //모두에게 보냄
                    }
                    break;
            }
            yield return null;
        }
        yield return null;
    }

    [PunRPC]
    void UnitsEffectRPC(int _ID)
    {
        GameObject unit = PhotonView.Find(_ID).gameObject;
        StartCoroutine(unit.transform.GetComponent<AIUnit>().UnitsEffect());
    }
    public IEnumerator UnitsEffect()
    {
        switch (myState.eName)
        {
            case UnitState.eCharacterName.Bunny:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
            case UnitState.eCharacterName.Gunner:
                {
                    if (mySoundAndEffect.flash != null)
                    {
                        mySoundAndEffect.flash.SetActive(true);
                    }
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                    yield return new WaitForSeconds(0.2f);
                    if (mySoundAndEffect.flash != null)
                    {
                        mySoundAndEffect.flash.SetActive(false);
                    }
                }
                break;
            case UnitState.eCharacterName.Big:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
            case UnitState.eCharacterName.Bear:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
            case UnitState.eCharacterName.Dog:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
            case UnitState.eCharacterName.Sheep:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
            case UnitState.eCharacterName.Girl:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
            case UnitState.eCharacterName.Clown:
                {
                    if (myAudioSource.clip != null)
                    {
                        myAudioSource.PlayOneShot(myAudioSource.clip);
                    }
                }
                break;
        }
        yield return null;
    }
    void SightTargetCheck() //시야범위 체크 메서드
    {
        sightTargets = mySight.FindVisibleTargets(); //타겟을 체킹하라
        if (sightTargets.Count > 0) //타겟들이 들어왔다면
        {
            SetTarget(sightTargets); //걸러낸다.
        }
    }
    void AttackSightTargetCheck() //공격범위 체크 메서드
    {
        attackAreaTargets = mySight.FindAttackTarget(); //공격범위안 체크
        if (attackAreaTargets.Count > 0)//내 공격범위안에 적이 있다면
        {
            SetTarget(attackAreaTargets);//타겟을 걸러낸다
        }
        else //적이 없다면
        {
            //Debug.Log("ddd");
            myState.estate = UnitState.eState.Move; //무브상태로 간다
        }
    }
    void Attack() //공격 메서드
    {
        if (!attackCooltime) //쿨타임만큼 
        {
            StartCoroutine(AttackType());//어택타입에 맞게 공격한다.
            //pv.RPC("UnitsEffectRPC", PhotonTargets.All, pv.viewID);
            attackCooltime = true;
        }
    }
    void LookRotation(Vector3 _lookAt) //장애물 회피 회전
    {
        Quaternion look = Quaternion.LookRotation(_lookAt);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, look, Time.deltaTime * rotationSpeed);
    }
    private void AvoidMove(Transform _target) //장애물 회피 이동
    {
        //Debug.Log("이동중");
        moveDir = (_target.position - myTransform.position).normalized;
        //Debug.Log("방향" + moveDir);
        RaycastHit hit;//Ray                                               폭
        Vector3 leftRayPos = myTransform.position - (myTransform.right * rayWidth);
        Vector3 rightRayPos = myTransform.position + (myTransform.right * rayWidth);
        //                    현재포지션 + 살짝 들어올림                       나의 앞            hit된 오브젝트, 레이거리(회피판별거리)        
        if (Physics.Raycast(myTransform.position + new Vector3(0, 0.5f, 0), myTransform.forward, out hit, avoidRange))
        {
#if DEBUG_MODE
            Debug.Log("레이어" + hit.transform.gameObject.layer);
            Debug.Log("나는 : " + hit.transform.gameObject);
            Debug.Log("옵스 : " + LayerMask.NameToLayer(obstacle));
#endif
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer(obstacle)) //hit된 놈중 아군이면
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.GetComponent<UnitState>().estate == UnitState.eState.Attacking || //어택중이거나 적체크중이거나 공격하러 가는중이면
                   hit.transform.GetComponent<UnitState>().estate == UnitState.eState.EnemyCheck ||
                  (myState.estate != UnitState.eState.GoAttack && hit.transform.GetComponent<UnitState>().estate == UnitState.eState.GoAttack))
                {
                    moveDir += hit.normal * sensivity; //움직이는 방향에 hit된 놈의Nomal(직각)받아서 감도 곱해줌
                }
            }
        }
        if (Physics.Raycast(leftRayPos + new Vector3(0, 0.5f, 0), myTransform.forward, out hit, avoidRange))
        {
#if DEBUG_MODE
            Debug.Log("레이어" + hit.transform.gameObject.layer);
            Debug.Log("나는 : " + hit.transform.gameObject);
            Debug.Log("옵스 : " + LayerMask.NameToLayer(obstacle));
#endif
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer(obstacle))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.GetComponent<UnitState>().estate == UnitState.eState.Attacking ||
                   hit.transform.GetComponent<UnitState>().estate == UnitState.eState.EnemyCheck ||
                  (myState.estate != UnitState.eState.GoAttack && hit.transform.GetComponent<UnitState>().estate == UnitState.eState.GoAttack))
                {
                    moveDir += hit.normal * sensivity;
                }
            }
        }
        else if (Physics.Raycast(rightRayPos + new Vector3(0, 0.5f, 0), myTransform.forward, out hit, avoidRange))
        {
#if DEBUG_MODE
            Debug.Log("레이어" + hit.transform.gameObject.layer);
            Debug.Log("나는 : " + hit.transform.gameObject);
            Debug.Log("옵스 : " + LayerMask.NameToLayer(obstacle));
#endif
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer(obstacle))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.GetComponent<UnitState>().estate == UnitState.eState.Attacking ||
                   hit.transform.GetComponent<UnitState>().estate == UnitState.eState.EnemyCheck ||
                   (myState.estate != UnitState.eState.GoAttack && hit.transform.GetComponent<UnitState>().estate == UnitState.eState.GoAttack))
                {
                    moveDir += hit.normal * sensivity;
                }
            }
        }

        else //씬뷰에 그리기
        {
#if DEBUG_MODE
            Debug.DrawRay(leftRayPos+new Vector3(0, 0.5f, 0), myTransform.forward * avoidRange, Color.red);
            Debug.DrawRay(myTransform.position + new Vector3(0, 0.5f, 0), myTransform.forward * avoidRange, Color.red);
            Debug.DrawRay(rightRayPos+ new Vector3(0, 0.5f, 0), myTransform.forward * avoidRange, Color.red);
#endif
        }
        movePos = myTransform.forward * myState.pSpeed * 2f; //이동계산
        movePos.y = myRigidBody.velocity.y; //이동계산중 y축만 속도값을 받아줌 (점프값)       
    }
    void SetTarget(List<Collider> targets) //들어온 타겟들 걸러내기
    {
        if (targets.Count == 1) //타겟이 하나라면
        {
            target = targets[0].gameObject; //그냥 지정해준다.            
        }
        else //타겟이 하나 이상일때
        {
            if (targets.Count > 0) //한번더 체크
            {
                int index = 0; //제일 가까운 오브젝트의 인덱스 저장용
                int next = 0;
                for (int i = 0; i < targets.Count; i++)
                {
                    if (!targets[i].CompareTag("Nexus") && targets[i].GetComponent<UnitState>().estate != UnitState.eState.Dead || targets[i].GetComponent<UnitState>().pHealth > 0) //넥서스를 제외시킨다.
                    { //넥서스를 제외한 현재인덱스의 거리와 다음인덱스의 거리값 비교해서 거리가 적은 것을 가져온다.
                        next = i + 1; //다음 오브젝트
                        if (next < targets.Count)
                        {
                            if (Vector3.Distance(transform.position, targets[i].transform.position) >
                               Vector3.Distance(transform.position, targets[next].transform.position))
                            {
                                index = i;
                            }
                        }
                    }
                }
                target = targets[index].gameObject; //타겟 지정
                Debug.Log("타겟 갱신 : " + target.name + "인덱스 : " + index + target.tag);
            }
            attackingNexus = false;
        }
        if (target.GetComponent<UnitState>().estate == UnitState.eState.Dead || target.GetComponent<UnitState>().pHealth <= 0)
        {
            target = null;
            myState.estate = UnitState.eState.Move;
        }
        else
        {
            if (!attackingNexus)
            {
                myState.estate = UnitState.eState.GoAttack; //공격하러 가는 상태로            
            }
        }
    }
    void DeadCheck() //죽음 체크
    {
        if (myState.pHealth <= 0)
        {
            myState.estate = UnitState.eState.Dead;
        }
    }
    public IEnumerator AttackType() //유닛의 공격타입에 따라 공격 형태,데미지를 바꿈
    {
#if DEBUG_MODE
        Debug.Log("공격했다");
#endif
        switch (myState.eType)
        {
            case UnitState.eAttackType.Range: //범위공격
                {
                    attackAreaTargets = mySight.FindAttackTarget();
                    for (int i = 0; i < attackAreaTargets.Count; i++)
                    {
                        if (myState.pPower > attackAreaTargets[i].GetComponent<UnitState>().pdef)
                        {
                            attackAreaTargets[i].GetComponent<UnitState>().pHealth =
                             attackAreaTargets[i].GetComponent<UnitState>().pHealth <= myState.pPower - attackAreaTargets[i].GetComponent<UnitState>().pdef ?
                             0 : attackAreaTargets[i].GetComponent<UnitState>().pHealth - myState.pPower - attackAreaTargets[i].GetComponent<UnitState>().pdef;
                        }
                        else
                        {
                            attackAreaTargets[i].GetComponent<UnitState>().pHealth--;
                        }
                    }
                    yield return new WaitForSeconds(myState.pAttackSpeed);
                    attackCooltime = false;
                }
                break;
            case UnitState.eAttackType.RangeDistance://원거리 범위 공격
                {
                    rangeDistanceTargets = mySight.FindAttackTargets(target.transform, myState.pRange);

                    if (rangeDistanceTargets.Count > 0) // null이 아니라면  방어력에 상관없이 적들에게 데미지를 줌
                    {
                        for (int i = 0; i < rangeDistanceTargets.Count; i++)
                        {
                            if (myState.pPower > rangeDistanceTargets[i].GetComponent<UnitState>().pdef)
                            {
                                rangeDistanceTargets[i].GetComponent<UnitState>().pHealth =
                                rangeDistanceTargets[i].GetComponent<UnitState>().pHealth <= myState.pPower ?
                                0 : rangeDistanceTargets[i].GetComponent<UnitState>().pHealth - myState.pPower;
                            }
                            else
                            {
                                rangeDistanceTargets[i].GetComponent<UnitState>().pHealth--;
                            }
                        }
                    }
                    yield return new WaitForSeconds(myState.pAttackSpeed);
                    attackCooltime = false;
                }
                break;
            default:
                {
                    switch (myState.eName)
                    {   //코끼리에게 5배의 데미지를 준다.
                        case UnitState.eCharacterName.Bunny:
                            {
                                if (myState.pPower > target.GetComponent<UnitState>().pdef)
                                {
                                    if (target.GetComponent<UnitState>().eName == UnitState.eCharacterName.Big)
                                    {
                                        target.GetComponent<UnitState>().pHealth =
                                        target.GetComponent<UnitState>().pHealth <= ((myState.pPower * 5) - target.GetComponent<UnitState>().pdef) ?
                                        0 : target.GetComponent<UnitState>().pHealth - (myState.pPower * 5) - target.GetComponent<UnitState>().pdef;
                                    }
                                    else
                                    {
                                        target.GetComponent<UnitState>().pHealth =
                                        target.GetComponent<UnitState>().pHealth <= myState.pPower - target.GetComponent<UnitState>().pdef ?
                                        0 : target.GetComponent<UnitState>().pHealth - myState.pPower - target.GetComponent<UnitState>().pdef;
                                    }
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().pHealth--;
                                }
                                yield return new WaitForSeconds(myState.pAttackSpeed);
                                attackCooltime = false;
                            }
                            break;
                        //코끼리에게 공격력의 절반만 입히며,토끼에게는 두배의 데미지를 준다.
                        case UnitState.eCharacterName.Gunner:
                            {
                                if (myState.pPower > target.GetComponent<UnitState>().pdef)
                                {
                                    if (target.GetComponent<UnitState>().eName == UnitState.eCharacterName.Big)//절반
                                    {
                                        target.GetComponent<UnitState>().pHealth =
                                        target.GetComponent<UnitState>().pHealth <= ((myState.pPower / 2) - target.GetComponent<UnitState>().pdef) ?
                                        0 : target.GetComponent<UnitState>().pHealth - (myState.pPower / 2) - target.GetComponent<UnitState>().pdef;
                                        print((myState.pPower / 2) - target.GetComponent<UnitState>().pdef);
                                    }
                                    else if (target.GetComponent<UnitState>().eName == UnitState.eCharacterName.Bunny)//두배
                                    {
                                        target.GetComponent<UnitState>().pHealth =
                                            target.GetComponent<UnitState>().pHealth <= ((myState.pPower * 2) - target.GetComponent<UnitState>().pdef) ?
                                            0 : target.GetComponent<UnitState>().pHealth - (myState.pPower * 2) - target.GetComponent<UnitState>().pdef;
                                    }
                                    else
                                    {
                                        target.GetComponent<UnitState>().pHealth =
                                        target.GetComponent<UnitState>().pHealth <= (myState.pPower - target.GetComponent<UnitState>().pdef) ?
                                        0 : target.GetComponent<UnitState>().pHealth - myState.pPower - target.GetComponent<UnitState>().pdef;
                                    }
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().pHealth--;
                                }

                                yield return new WaitForSeconds(myState.pAttackSpeed);
                                attackCooltime = false;
                            }
                            break;
                        case UnitState.eCharacterName.Dog:
                            {
                                if (!target.CompareTag("Nexus"))
                                {
                                    StartCoroutine(target.GetComponent<AIUnit>().Bleeding(3f));
                                }

                                if (myState.pPower > target.GetComponent<UnitState>().pdef)
                                {
                                    target.GetComponent<UnitState>().pHealth =
                                    target.GetComponent<UnitState>().pHealth <= (myState.pPower - target.GetComponent<UnitState>().pdef) ?
                                    0 : target.GetComponent<UnitState>().pHealth - (myState.pPower - target.GetComponent<UnitState>().pdef);
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().pHealth--;
                                }
                                yield return new WaitForSeconds(myState.pAttackSpeed);
                                attackCooltime = false;
                            }
                            break;

                        case UnitState.eCharacterName.Sheep:
                            {
                                int randomDamage = Random.Range((int)myState.pPower, 40);
                                if (randomDamage > target.GetComponent<UnitState>().pdef)
                                {
                                    target.GetComponent<UnitState>().pHealth =
                                    target.GetComponent<UnitState>().pHealth <= (randomDamage - target.GetComponent<UnitState>().pdef) ?
                                    0 : target.GetComponent<UnitState>().pHealth - (randomDamage - target.GetComponent<UnitState>().pdef);
                                    yield return new WaitForSeconds(myState.pAttackSpeed);
                                    attackCooltime = false;
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().pHealth--;
                                }
                            }
                            break;

                        default:
                            {    //타겟의 현재 체력이 (나의 공격력-타겟의 방어력)한 값보다 작다면 타겟의 체력을 0으로, 
                                 //아니라면 타겟의 현재 체력에서 나의공격력-타겟방어력 만큼 -시킴
                                 // if(target.GetComponent<UnitState>().pHealth <= (myState.pPower - target.GetComponent<UnitState>().pdef))
                                 //{
                                 //    target.GetComponent<UnitState>().pHealth = 0;
                                 //}
                                 //else
                                 //{
                                 //    target.GetComponent<UnitState>().pHealth = target.GetComponent<UnitState>().pHealth-(myState.pPower - target.GetComponent<UnitState>().pdef);
                                 //} //이 내용을 ?로 간결화 함
                                if (myState.pPower > target.GetComponent<UnitState>().pdef)
                                {
                                    target.GetComponent<UnitState>().pHealth =
                                    target.GetComponent<UnitState>().pHealth <= (myState.pPower - target.GetComponent<UnitState>().pdef) ?
                                    0 : target.GetComponent<UnitState>().pHealth - (myState.pPower - target.GetComponent<UnitState>().pdef);
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().pHealth--;
                                }
                                yield return new WaitForSeconds(myState.pAttackSpeed);
                                attackCooltime = false;
                            }
                            break;
                    }
                    break;
                }
        }
        yield return null;
    }
}