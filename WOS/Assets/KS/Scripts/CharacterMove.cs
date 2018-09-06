//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterMove : MonoBehaviour {

//    CharacterState ch; //현재 스크립트가 바인딩 되어있는 놈의 캐릭터 클래스
//    CharacterState enemy;
//    GameObject pBnexus; //현재 오브젝트가 이동할 방향
//    GameObject pRnexus;//현재 오브젝트가 이동할 방향
//    Collider[] pCharacters; //캐릭터들의 집합
//    Collider[] pEnemies; //캐릭터들 중에 적들의 집합
//    float[] pDistance; //적들과 나의 거리의 집합
//    GameObject eEnemy; //가장 가까운 적
//	// Use this for initialization
//	void Start () {
//        ch = this.GetComponent<CharacterState>(); //나의 캐릭터 클래스를 초기화
//        pRnexus = GameObject.FindGameObjectWithTag("RedNex"); //이동할 방향 초기화
//        pBnexus = GameObject.FindGameObjectWithTag("BlueNex"); //이동할 방향 초기화        
//        //pCharacters = new Collider[20];
//        //pEnemies = new Collider[20]; //체크할 수 초기화
//        //pDistance = new float[20];//체크할 수 초기화
//    }
	
//	// Update is called once per frame
//	void Update () {        
//        Actions(); //행동 메소드
//        //DeadCheck();
//    }
//    void DeadCheck()
//    {
//        if(ch.estate != CharacterState.eState.Dead)
//        {
//            if (ch.pHealth < 0.1f)
//            {
//                ch.estate = CharacterState.eState.Dead;
//            }
//            else
//            {
//                return;
//            }

//        }
//    }
//    void Moving(GameObject mTarget) //이동 메소드
//    {
//        //print("이놈을 보고 간다" + mTarget);
//        transform.position = Vector3.MoveTowards(transform.position, mTarget.transform.position, ch.pSpeed * Time.deltaTime);
//        //transform.rotation = Quaternion.RotateTowards(transform.rotation, mTarget.transform.rotation, 100 * Time.deltaTime);
//    }
//    void LookTarget(GameObject mTarget) //로테이션 메소드
//    {
//        Quaternion target = Quaternion.LookRotation(mTarget.transform.position - transform.position);
//        transform.rotation = Quaternion.Slerp(transform.rotation, target, 10 * Time.deltaTime);
//        //transform.LookAt(mTarget.transform);
//    }
//    IEnumerator GoAttack(GameObject mTarget) //공격 메소드 (수정 요함)
//    {        
//        ch.estate = CharacterState.eState.GoAttack;
//        CharacterState cTarget = mTarget.GetComponent<CharacterState>();
                     
//        while(cTarget.estate != CharacterState.eState.Attacking && ch.estate != CharacterState.eState.Dead)
//        {            
//            //print("공격하러가는");
//            //print(this.gameObject.name +"가"+mTarget.name+" : 공격중");
//            cTarget.pHealth = cTarget.pHealth - ch.pPower;
//            if(cTarget.estate != CharacterState.eState.Attacking)
//            {
//                yield return new WaitForSeconds(ch.pAttackSpeed);                        
//            }
//            if(cTarget.pHealth < 0.1f)
//            {
//                yield return eEnemy = null;
//            }
//        }             
//        print(this.name + "가 "+cTarget.name + "를 죽였다");
        
//        if (ch.estate != CharacterState.eState.Dead)
//        {
//            ch.estate = CharacterState.eState.GoNexus;
//        }
//        yield return eEnemy = null;
//    }
    
//    void Actions()
//    {
//        if(ch.estate != CharacterState.eState.Dead &&ch.estate != CharacterState.eState.GoAttack)
//        {
//            if(eEnemy != null)
//            {
//                print(this.name +"의 적은 "+eEnemy);
                
//            }
//            int mIndex = 0; //적들의 집합중 가장 가까운 거리에 있는 녀석의 인덱스를 알아냄,초기화
//            int mEqurls = 0; //나를 포함 같은 태그를 가진놈들의 수,초기화
//            //가상의 10크기의 공을 그리고 그 공안에 들어오는 CharacterLayer를 가진 오브젝트들을 배열에 저장
//            pCharacters = Physics.OverlapSphere(transform.position, 100f, 1 << LayerMask.NameToLayer("Character"),
//                                                                                                    QueryTriggerInteraction.Ignore);
//            pEnemies =new Collider[pCharacters.Length];
//            pDistance = new float[pCharacters.Length];
//            if(pCharacters != null &&eEnemy ==null && pCharacters.Length >=2)//나이외의 오브젝트가 있다면
//            {
//                float mMin = 100f; //거리 걸러내기위함(초기화)
            
//                for (int i = 0 , j = 0; i < pCharacters.Length; i++)
//                {
//                    //print(this.gameObject.name+" , " + pCharacters[i].gameObject.name);
//                    //print(this.gameObject.name +"가 닿은 수 : "+ pCharacters.Length);
//                    if (this.gameObject.tag == "BlueTeam" && pCharacters[i].gameObject != this.gameObject
//                        && pCharacters[i].gameObject.tag =="RedTeam")
//                    {
//                        pEnemies[j] = pCharacters[i];
//                        pDistance[j] = Vector3.Distance(transform.position, pEnemies[j].transform.position);
//                        //print(this.gameObject.name + "닿은놈 : " +pEnemies[j]);
//                        //print("닿은놈 거리 :" + pDistance[j]);
//                        j++;
//                    }
//                    else if(this.gameObject.tag == "RedTeam" && pCharacters[i].gameObject != this.gameObject
//                        && pCharacters[i].gameObject.tag == "BlueTeam")
//                    {
//                        pEnemies[j] = pCharacters[i];
//                        pDistance[j] = Vector3.Distance(transform.position, pEnemies[j].transform.position);
//                        //print(this.gameObject.name + "닿은놈 : " + pEnemies[j]);
//                        //print("닿은놈 거리 :" + pDistance[j]);
//                        j++;
//                    }
//                    else //나를 포함 나와같은 태그의 수
//                    {
//                        //print("나와 같은놈");
//                        mEqurls++;
//                    }
                
//                }
//                if (pEnemies[0] != null) //적들의 집합의 0번째 인덱스의 데이터가 null이면 이후 인덱스도 모두 null이기 때문에 검사할 필요없음
//                {
//                    //모든 캐릭터들의 집합의 길이에서 나를포함한 같은 태그의 수를 빼 pEnemies의 데이터가 있는 인덱스 들만 찾음.
//                    for (int i = 0; i < pCharacters.Length- mEqurls; i++)
//                    {
//                        //print("적수"+pEnemies.Length);
//                        //print("거리배열" + pDistance.Length);
//                        if(mMin > pDistance[i]) //거리들의 집합중 가장 낮은 거리에 있는 적의 인덱스를 찾음
//                        {
//                            mIndex = i;
//                            mMin = pDistance[mIndex];
//                        }                
//                    }
//                    //print(this.gameObject.name +"인덱스" + mIndex);
//                    //print("발견오브젝트" + pEnemies[mIndex]);
//                            //찾았다면 해당 인덱스의 오브젝트를 적으로 삼음
//                    eEnemy = pEnemies[mIndex].gameObject;
//                    enemy = eEnemy.GetComponent<CharacterState>();
//                }
//            }
//            else
//            {
//                eEnemy = null;
//                mEqurls++;
//            }
//            //print(this.gameObject.name+ pCharacters.Length + ":" + mEqurls);
//            if(pCharacters.Length ==mEqurls)//값이 없으면 접촉하고 있지 않다
//            {
//                //print(this.gameObject.name + " 접촉물 없음");
//                eEnemy = null;  
//                if (this.gameObject.tag == "BlueTeam")//접촉물이 없기때문에 계속해서 넥서스를 바라보며 향해 간다
//                {
//                    Moving(pRnexus);
//                    LookTarget(pRnexus);
//                }
//                else
//                {
//                    Moving(pBnexus);
//                    LookTarget(pBnexus);
//                }           
//            }
//            if(eEnemy != null) //제일 가까운 적이 존재한다면
//            {               
//                LookTarget(eEnemy); //적을 바라보고
//                if(Vector3.Distance(this.transform.position,eEnemy.transform.position) > ch.pAttackRange) //5만큼 떨어진 거리만큼 이동
//                {
//                    Moving(eEnemy);
//                }
//                else //이동이 완료 되었으면 공격
//                {
//                    print(this.gameObject.name+ " 접촉함");                        
//                        StartCoroutine(GoAttack(eEnemy));                    
//                }                
//            }           
//        }
//    }    
//}
