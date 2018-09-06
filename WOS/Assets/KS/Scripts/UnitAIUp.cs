using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class UnitAIUp : MonoBehaviour
{
    UnitState myState; //내 상태
    GameObject myLooking;
    //UnitState targetState;
    NavMeshAgent myNav; //내 NavMesh
    Sight3D mySight;//내 시야 및 공격 범위의 적들 체크
    public GameObject[] wayPoints;
    public GameObject wayPoint; //이동 포인트
    public GameObject target; //이동 중 타겟발견
    [HideInInspector] public List<Collider> sightTargets = null; //시야에 들어온 적 체크
    [HideInInspector] public List<Collider> attackAreaTargets = null; //공격하는 순간 그 공격 범위의 적 체크
    [HideInInspector] public List<Collider> rangeDistanceTargets = null;
    [HideInInspector] public bool imDead = true; //내가 죽었는지
    bool imBlue;
    public bool attackNexus;
    bool isSpawn = false;
    float follwingTime = 3f; //적을 공격하러 갈때 공격하지 못하고 움직이는 시간이 지나면 다른 적을 쫓음(초기화)
    float curFollwing;
    bool imAttacking;
    float nexus = 0;
    int indexWay;
    bool setdes;

    //NetWork
    PhotonView pv;
    PhotonTransformView ptv;

    //Gunner Effect
    public GameObject flash;
    public Sprite[] flashSprites;
    public SpriteRenderer[] spriteRenderers;
    float flashTime = 0.05f;
    public AudioSource attackSound;

    //UI
    public GameObject healthBar;
    public Transform viewTarget;
    [HideInInspector] public Image healthSlider;
    Transform cam;
    [HideInInspector] public Transform healthUI;
    

    //float bleedingTime = 1f;
    //float curbleedingTime = 0f;
    //float bleedingCount = 3;

    // Use this for initialization
    void Start()
    {
        if (CompareTag("BlueTeam"))
        {
            imBlue = true;
        }
        pv = GetComponent<PhotonView>();
        ptv = GetComponent<PhotonTransformView>();
        //foreach (Canvas c in FindObjectsOfType<Canvas>())
        //{
        //    if (c.renderMode == RenderMode.WorldSpace)
        //    {
        //        healthUI = Instantiate(healthBar, c.transform).transform;
        //        healthUI.name = "HealthBar : "+ name;
        //        healthSlider = healthUI.GetChild(0).GetComponent<Image>();                
        //        break;
        //    }
        //}
        myState = GetComponent<UnitState>(); //내 상태 받아옴
        if (PhotonNetwork.isMasterClient)
        {
            myLooking = transform.GetChild(0).gameObject;//내 오브젝트의 자식의 x번째를 가져온다.
            //healthSlider = healthBar.GetComponent<Image>();
            myNav = GetComponent<NavMeshAgent>();//내 NavMesh받아옴
            mySight = transform.GetChild(0).GetComponent<Sight3D>();//내 시야정보 받아옴
            myNav.speed = myState.pSpeed;//내 스피드
                                         //myNav.stoppingDistance = myState.pAttackRange;
            //print(wayPoints[0]);
            //wayPoint = wayPoints[indexWay];
            //체력바

            //StartCoroutine("Action"); //행동개시
            //StartCoroutine("DeadCheck"); //내 죽음 체크
            //StartCoroutine("NextWayPoint"); //다음 웨이포인트 체크

            curFollwing = follwingTime; //따라다니는 시간 초기화        
        }
            cam = Camera.main.transform;
            healthUI.transform.forward = -cam.forward;        
            healthUI.gameObject.SetActive(false);        
    }
    IEnumerator NextWayPoint()
    {
        if (Mathf.Abs(transform.position.z - wayPoint.transform.position.z) < 1.5f) //다음 웨이포인트 찍어주는 놈.
        {
            if (indexWay < wayPoints.Length - 1)
            {
                indexWay++;
                //print(indexWay);
                wayPoint = wayPoints[indexWay];
                myNav.SetDestination(wayPoint.transform.position);
            }
            else if (indexWay == wayPoints.Length - 1)
            {
                if (transform.CompareTag("BlueTeam"))
                {
                    wayPoint = Gamemanager1.GetInstance().m_Nexus.RedNex.gameObject;
                }
                else
                {
                    wayPoint = Gamemanager1.GetInstance().m_Nexus.BlueNex.gameObject;
                }
                myNav.SetDestination(wayPoint.transform.position);
            }
        }
        yield return null;
    }
    //UnitState.eState
    IEnumerator GunnerShoot()
    {
        flash.SetActive(true);
        attackSound.PlayOneShot(attackSound.clip);
        //attackSound.Play();
        int flashSpriteIndex = Random.Range(0, flashSprites.Length);
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = flashSprites[flashSpriteIndex];
        }
        yield return new WaitForSeconds(flashTime);
        flash.SetActive(false);
        //attackSound.Stop();
    }
   public IEnumerator Action()
    {
        wayPoint = wayPoints[0];        
        while (!imDead)
        {            
            switch (myState.estate)
            {
                case UnitState.eState.Move: //움직임
                    {
                        myNav.isStopped = false; //움직일 수 있다
                        myNav.avoidancePriority = 51; ////우선순위 지정
                        if (!setdes)
                        {
                            myNav.SetDestination(wayPoint.transform.position); //웨이포인트로
                            setdes = true;
                        }
                        StartCoroutine(LookingAt(wayPoint));
                        sightTargets = mySight.FindVisibleTargets(); //내 시야안에 적이 있는지 체크                    
                        StartCoroutine(SightTarget(sightTargets)); //시야안의 적중 가까운놈 찾음
                        StartCoroutine(NextWayPoint());
                        //yield return new WaitForSeconds(0.25f);
                    }
                    break;
                case UnitState.eState.GoAttack:
                    {
                        StartCoroutine(LookingAt(target));
                        StartCoroutine(NextWayPoint());
                        if (!imAttacking) //내가 어택중이 아니라면
                        {
                            //myNav.avoidancePriority = 50;
                            if (!setdes)
                            {
                                myNav.SetDestination(target.transform.position); //타겟으로 간다
                                setdes = true;
                            }
                        }
                        if (Vector3.Distance(transform.position, target.transform.position) <= myState.pAttackRange) //타겟이 내 공격사정거리 내에 들어오면
                        {
                            if (attackNexus) //넥서스를 공격중이다
                            {
                                sightTargets = mySight.FindVisibleTargets(); //내 시야안에 넥서스 이외의 적이 있는지 체크
                                StartCoroutine(SightTarget(sightTargets));
                                //Debug.Log(Vector3.Distance(transform.position, target.transform.position - new Vector3(0, 0, target.GetComponent<CapsuleCollider>().radius)));
                            }
                            imAttacking = true; //공격중이다
                            //myNav.avoidancePriority = 49; //우선순위
                            myNav.isStopped = true; //공격중이라 움직일 수 없게
                            myNav.velocity = Vector3.zero; //미끄러짐 방지
                            //StartCoroutine("RookTarget"); //타겟을 바라본다(NavMesh가 Stop이면 타겟을 바라보지 않기 때문에 필요함)
                            //타겟의 체력이 내 공격력보다 작다면 타겟의 피를0으로 아니면 내 공격력 만큼 깎음
                            if (myState.eType == UnitState.eAttackType.Distance)
                            {
                                myNav.avoidancePriority = 49; //우선순위
                            }
                            StartCoroutine("UnitsEffect");
                            StartCoroutine("AttackType");
                            //target.GetComponent<UnitState>().pHealth = target.GetComponent<UnitState>().pHealth <= myState.pPower ?
                            //0 : target.GetComponent<UnitState>().pHealth - myState.pPower;
                            if (target.GetComponent<UnitState>().estate == UnitState.eState.Dead) //깎은뒤 타겟이 죽었다면
                            {
                                imAttacking = false; //공격중이 아니다
                                myState.estate = UnitState.eState.EnemyCheck; //내 공격범위에 적이 있는지 체크한다
                                target = null;
                                setdes = false;
                            }
                            else
                            {
                                yield return new WaitForSeconds(myState.pAttackSpeed); //타겟이 죽지 않았다면 내 공격 속도뒤에 공격을 반복
                            }
                        }
                        else //타겟이 사정거리에 들어오지 않으면
                        {
                            if (target.GetComponent<UnitState>().estate == UnitState.eState.Dead) //타겟이 죽었다면
                            {
                                imAttacking = false; //공격중이 아니다
                                target = null;
                                myState.estate = UnitState.eState.EnemyCheck; //공격범위안 적 체크
                                setdes = false;
                            }
                            imAttacking = false;
                            curFollwing = curFollwing - Time.deltaTime; //따라가는 시간
                            myNav.isStopped = false; //따라가라
                            if (curFollwing < 0)
                            {
                                curFollwing = follwingTime; //초기화
                                myState.estate = UnitState.eState.Move;//적을 따라가지 못하니 웨이포인트로 가라
                                setdes = false;
                            }
                        }
                    }
                    break;
                case UnitState.eState.EnemyCheck:
                    {
                        attackAreaTargets = mySight.FindAttackTarget(); //내 공격범위에 적이 있는지 체크
                        if (attackAreaTargets.Count > 0)
                        {
                            int index = 0;
                            for (int i = 0; i < attackAreaTargets.Count; i++)
                            {
                                if (Vector3.Distance(transform.position, attackAreaTargets[index].transform.position) > Vector3.Distance(transform.position, attackAreaTargets[i].transform.position))
                                {
                                    index = i;
                                }
                            }
                            target = attackAreaTargets[index].gameObject; //제일 가까운 놈
                            myState.estate = UnitState.eState.GoAttack;                            
                        }
                        else
                        {
                            target = null;                            
                            myState.estate = UnitState.eState.Move;
                        }
                    }
                    break;
                case UnitState.eState.Dead: //내가 죽으면
                    { //내 모든 정보를 초기화
                        target = null;
                        sightTargets = null;
                        attackAreaTargets = null;
                        setdes = false;
                        imDead = true; //내가 죽었다  
                        imAttacking = false; //어택중이 아님 (죽었으니)
                        attackNexus = false; //넥서스를 공격하고 있지 않음
                        //targetState = null;                        
                        isSpawn = false; //죽었으니 스폰되어있지 않음
                        transform.GetChild(0).gameObject.SetActive(false); //죽었으니 렌더링 끄기
                        healthUI.gameObject.SetActive(false); //죽었으니 체력바 숨기기
                        //this.gameObject.SetActive(false);
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
                        pv.RPC("SendDead", PhotonTargets.All, pv.viewID, mySetPosition); //모두에게 보냄
                        //this.gameObject.SetActive(false); //나를 없앰                        
                    }
                    break;
            }
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.isMasterClient) //로컬만 처리함
        {
            SpawnCheck();
            BleedingAttack();
        }
        //print(healthSlider.fillAmount);
        healthSlider.fillAmount = myState.pHealth / myState.pMaxHealth;        
        healthUI.position = viewTarget.position; // 움직여서 스토킹
        //healthUI.forward = -cam.forward;
        
    }
    public void SpawnCheck()
    {
        if (!imDead && !isSpawn) //죽지 않았으며 스폰되어있지 않다면
        {
            //transform.GetChild(0).gameObject.SetActive(true);
            print("스폰해");
            isSpawn = true; //스폰되었다
            indexWay = 0; //스폰되었으니 가진 웨이포인트중 첫번째를 웨이포인트로 초기화
            wayPoint = wayPoints[indexWay].gameObject; //웨이포인트 지정
            myState.estate = UnitState.eState.Move; //내 상태를 움직임 상태로 바꿈
            //StartCoroutine("Action"); //움직여라
            //StartCoroutine("NextWayPoint");
            StartCoroutine("DeadCheck"); //나의 죽음을 체크하라
            //healthUI.gameObject.SetActive(true);            
            //print("ReSpawn");
            pv.RPC("SendLive", PhotonTargets.All, pv.viewID); //모두에게 나의 스폰을 알린다(숨겨진 체력바를 살림)

            switch (myState.eName) //공격 이펙트 초기화
            {
                case UnitState.eCharacterName.Bunny:
                    {

                    }
                    break;
                case UnitState.eCharacterName.Gunner:
                    {
                        flash.SetActive(false);
                    }
                    break;
                case UnitState.eCharacterName.Big:
                    {

                    }
                    break;
                case UnitState.eCharacterName.Bear:
                    {
                        
                    }
                    break;
            }
        }
    }
    IEnumerator SightTarget(List<Collider> sight) //시야 체크
    {
        if (sight.Count == 1) //적이 하나만 있다면 넥서스이거나 적이니 가까운 적을 계산 필요없음
        {
            target = sight[0].gameObject;
            if (target.CompareTag("Nexus")) //타겟이 넥서스라면 공격도중 적유닛이 나오면 적유닛을 공격하도록 하기위함
            {                
                nexus = target.transform.localScale.x / 2; //넥서스를 때릴 때 중앙에 닿아야 때리려 하기 때문에 값을 더해줌
                attackNexus = true; //넥서스를 공격중이다
            }
            else
            {
                nexus = 0; //넥서스를 공격하는것이 아니기 때문에 공격사거리 값을 더해줄 필요 없음
                attackNexus = false; //넥서스를 공격중이 아니다
            }
            myState.estate = UnitState.eState.GoAttack; //어택 상태로
            setdes = false;
        }
        else
        {
            if (sight.Count > 0) //시야안의 적들이 있다면
            {
                //target = sight[0].gameObject;
                int index = 0; //가까운놈의 인덱스

                for (int i = 0; i < sightTargets.Count; i++)
                {
                    if (!sight[i].CompareTag("Nexus"))//넥서스를 제외한 나머지
                    { //현재 인덱스의 타겟과 다음인덱스의 타겟의 거리를 비교해서 작은 쪽을 index에 저장
                        if (Vector3.Distance(transform.position, sightTargets[index].transform.position) > Vector3.Distance(transform.position, sightTargets[i].transform.position))
                        { //가까운놈 체크
                            index = i;
                        }
                    }
                }
                target = sightTargets[index].gameObject; //가까운놈 찾아서 타겟으로 만듬
                nexus = 0; //넥서스가 아니기 때문에 거리값 더해 줄 필요 없음
                attackNexus = false; //넥서스를 공격 중이 아님
                myState.estate = UnitState.eState.GoAttack; //어택 상태로                
            }
        }
        yield return null;
    }
    
    IEnumerator LookingAt(GameObject _target) //타겟을 받아 그 타겟을 바라보게 함
    {   //Quaternion ->rotation정보를 가지는 클래스(안에는 rotation을 이용한 메소드가 많음 Lerp,Slerp등등)
        Quaternion lookat = Quaternion.LookRotation(_target.transform.position - myLooking.transform.position);//바라보아야 하는 방향Vector3로 Quaternion을 저장
        myLooking.transform.rotation = Quaternion.Slerp(myLooking.transform.rotation, lookat, 10f * Time.deltaTime);//내 보는 방향을 바라봐야하는 회전값으로 보간        
        yield return null;
    }

    [PunRPC]
    void SendDead(int _ID, Vector3 _Setposition) //모두에게 죽음을 알림
    {
        GameObject deadUnit = PhotonView.Find(_ID).gameObject; //게임오브젝트를 찾음
        deadUnit.GetComponent<UnitAIUp>().healthUI.gameObject.SetActive(false); //찾은 놈의 체력바를 숨김
        //deadUnit.transform.GetChild(0).gameObject.SetActive(false);
        //deadUnit.GetComponent<UnitAIUp>().imDead = true;
        deadUnit.transform.position = _Setposition;         //리스폰 될 지점을 세팅해줌
        //PhotonView.Find(_ID).gameObject.SetActive(false);
        //PhotonView.Find(_ID).GetComponent<UnitAIUp>().healthUI.gameObject.SetActive(false);
    }
    [PunRPC]
    void SendLive(int _ID) //모두에게 리스폰 됨을 알림
    {
        GameObject liveUnit = PhotonView.Find(_ID).gameObject; //게임 오브젝트를 찾음
        print(liveUnit);
        liveUnit.GetComponent<UnitAIUp>().healthUI.gameObject.SetActive(true);   //체력바를 켜줌     
    }
    public IEnumerator DeadCheck() //죽음을 체크함
    {
        while (myState.estate != UnitState.eState.Dead) //내가 죽지 않은 동안 반복함
        {
            if (myState.pHealth <= 0) //피가 0이면 죽음처리를 해줘야 하기 때문에
            {
                myState.estate = UnitState.eState.Dead; //피가 0 이하기 때문에 죽음상태
                //StopAllCoroutines();                
            }
            //healthSlider.fillAmount = myState.pHealth / myState.pMaxHealth;
            yield return null;
        }
        yield return null;
    }
    void BleedingAttack() // 출혈공격
    {
        if (myState.isBledding == true) // 캐릭터상태가 블리딩 상태면
        {
          //  Debug.Log("출혈효과 실행");
            myState.pHealth -= Time.deltaTime; // 1초당 1씩 체력 감소
            myState.BleedingTime -= Time.deltaTime; // 그리고 블리딩 시간3초니까 -1초
            if(myState.BleedingTime <= 0 || myState.estate == UnitState.eState.Dead)
            {
              //  Debug.Log("출혈효과 끝");
                myState.BleedingTime = 0f;
                myState.isBledding = false;
            }
            //if(myState.estate == UnitState.eState.Dead)
            //{
            //    myState.BleedingTime = 0f;
            //    myState.isBledding = false;
            //}
        }
        
        //if(Time.time > curbleedingTime) //현재 시간과 비교한다  현재시간이 더 크다면
        //{
        //    curbleedingTime = Time.time + bleedingTime; 출혈 타임을 현재시간 + 출혈시간으로 바꿔줌.
        //    bleedingCount--; //출혈 카운트를 --시킴
        //    myState.pHealth--; //내 HP깎음 (출혈 카운트가 0이 아니라면 다시 위의 과정을 반복 현재시간과 현재시간+출혈시간을 계산하여 현재시간이 더 커지면 실행)
        //}
        //if(bleedingCount <= 0 || myState.estate == UnitState.eState.Dead)//출혈 카운트가 0이거나 내가 죽었다면
        //{
        //    bleedingCount = bleedingTime;
        //    curbleedingTime = 0f;
        //    myState.isBledding = true;
        //}
    }
    public IEnumerator AttackType() //유닛의 공격타입에 따라 공격 형태,데미지를 바꿈
    {
        switch (myState.eType)
        {
            case UnitState.eAttackType.Range: //범위공격
                {
                    attackAreaTargets = mySight.FindAttackTarget();
                    for (int i = 0; i < attackAreaTargets.Count; i++)
                    {
                        attackAreaTargets[i].GetComponent<UnitState>().pHealth =
                        attackAreaTargets[i].GetComponent<UnitState>().pHealth <= myState.pPower - attackAreaTargets[i].GetComponent<UnitState>().pdef ?
                        0 : attackAreaTargets[i].GetComponent<UnitState>().pHealth - myState.pPower - attackAreaTargets[i].GetComponent<UnitState>().pdef;
                    }
                }
                break;
            case UnitState.eAttackType.RangeDistance://원거리 범위 공격
                {
                    rangeDistanceTargets = mySight.FindAttackTargets(target.transform, myState.pRange);

                    if(rangeDistanceTargets.Count > 0) // null이 아니라면  방어력에 상관없이 적들에게 데미지를 줌
                    {
                        for (int i = 0; i < rangeDistanceTargets.Count; i++)
                        {
                            rangeDistanceTargets[i].GetComponent<UnitState>().pHealth =
                            rangeDistanceTargets[i].GetComponent<UnitState>().pHealth <= myState.pPower ?
                            0 : rangeDistanceTargets[i].GetComponent<UnitState>().pHealth - myState.pPower;
                        }
                    }
                }
                break;
            default:
                {
                    switch (myState.eName)
                    {   //코끼리에게 5배의 데미지를 준다.
                        case UnitState.eCharacterName.Bunny:
                            {
                                if (target.GetComponent<UnitState>().eName == UnitState.eCharacterName.Big)
                                {
                                    target.GetComponent<UnitState>().pHealth = 
                                    target.GetComponent<UnitState>().pHealth <= ((myState.pPower * 5) - target.GetComponent<UnitState>().pdef)?
                                    0 : target.GetComponent<UnitState>().pHealth - (myState.pPower * 5) - target.GetComponent<UnitState>().pdef;
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().pHealth =
                                    target.GetComponent<UnitState>().pHealth <= myState.pPower - target.GetComponent<UnitState>().pdef ?
                                    0 : target.GetComponent<UnitState>().pHealth - myState.pPower - target.GetComponent<UnitState>().pdef;
                                }
                            }
                            break;
                        //코끼리에게 공격력의 절반만 입히며,토끼에게는 두배의 데미지를 준다.
                        case UnitState.eCharacterName.Gunner:
                            {
                                if(target.GetComponent<UnitState>().eName == UnitState.eCharacterName.Big)//절반
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
                            break;
                        case UnitState.eCharacterName.Dog:
                            {
                                if (target.GetComponent<UnitState>().isBledding == false)
                                {
                                    target.GetComponent<UnitState>().BleedingTime = 3f; // 타겟이 블리딩 타임 3초를 가진다.
                                    target.GetComponent<UnitState>().isBledding = true; // 블리딩상태를 트루로 한다.
                                }
                                else
                                {
                                    target.GetComponent<UnitState>().BleedingTime = 3f; // 타겟이 블리딩 타임 3초를 가진다.
                                }
                                target.GetComponent<UnitState>().pHealth =
                             target.GetComponent<UnitState>().pHealth <= (myState.pPower - target.GetComponent<UnitState>().pdef) ?
                             0 : target.GetComponent<UnitState>().pHealth - (myState.pPower - target.GetComponent<UnitState>().pdef);
                            }
                            break;

                        case UnitState.eCharacterName.Sheep:
                            {
                                int randomDamage = Random.Range((int)myState.pPower, 40);
                                target.GetComponent<UnitState>().pHealth = 
                                target.GetComponent<UnitState>().pHealth <= (randomDamage - target.GetComponent<UnitState>().pdef) ?
                                0 : target.GetComponent<UnitState>().pHealth - (randomDamage - target.GetComponent<UnitState>().pdef);

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
                                target.GetComponent<UnitState>().pHealth =
                                target.GetComponent<UnitState>().pHealth <= (myState.pPower - target.GetComponent<UnitState>().pdef) ?
                                0 : target.GetComponent<UnitState>().pHealth - (myState.pPower - target.GetComponent<UnitState>().pdef);
                            }
                            break;
                    }
                    break;
                }
        }
        yield return null;
    }
    [PunRPC]
    void GunnerShootSound(int _ID)
    {
        GameObject gunner = PhotonView.Find(_ID).gameObject;
        gunner.GetComponent<UnitAIUp>().StartCoroutine(GunnerShoot());
        //StartCoroutine(GunnerShoot());
    }
    public IEnumerator UnitsEffect()
    {
        switch (myState.eName)
        {
            case UnitState.eCharacterName.Bunny:
                {

                }
                break;
            case UnitState.eCharacterName.Gunner:
                {
                    //StartCoroutine("GunnerShoot");
                    pv.RPC("GunnerShootSound", PhotonTargets.All, pv.viewID);
                }
                break;
            case UnitState.eCharacterName.Big:
                {

                }
                break;
            case UnitState.eCharacterName.Bear:
                {

                }
                break;
        }
            yield return null;
    }


    //테스트때 썼던 것들 (지금은 안씀)
    IEnumerator RookTarget() //타겟을 바라봄
    {
        while (myNav.isStopped)
        {
            if (target != null)
            {
                Quaternion lookat = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookat, 10 * Time.deltaTime);
            }
            yield return null;
        }
        yield return null;
    }
}
