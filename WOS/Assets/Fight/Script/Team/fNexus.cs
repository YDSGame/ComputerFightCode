using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fNexus : MonoBehaviour
{
    PhotonView pv;
    //플레이어당 최대 풀링 수 ( 곰 = 30, 거너 = 25, 토끼의숲 = 150, 코끼리 = 30)
    public static fNexus insNexus; //넥서스 자신 
    public GameObject fightWolrd; //풀링될 곳(부모)
    [Header("RedTeam")]
    public GameObject[] redCharacters; //레드 유닛
    public Transform[] redWayPoints; //레드 유닛 이동경로
    
    [Header("BlueTeam")]
    public GameObject[] blueCharacters; //블루 유닛
    public Transform[] blueWayPoints; //블루 유닛 이동경로


    [HideInInspector] public List<GameObject> redBears; //곰풀링
    [HideInInspector] public List<GameObject> redGunners; //거너풀링
    [HideInInspector] public List<GameObject> redBunnies; //버니풀링
    [HideInInspector] public List<GameObject> redElephants;//코끼리 풀링
    [HideInInspector] public List<GameObject> redDogs;
    [HideInInspector] public List<GameObject> redSheeps;
    [HideInInspector] public List<GameObject> redGirls;


    public List<GameObject> blueBears;
    [HideInInspector] public List<GameObject> blueGunners;
    [HideInInspector] public List<GameObject> blueBunnies;
    [HideInInspector] public List<GameObject> blueElephants;
    [HideInInspector] public List<GameObject> blueDogs;
    [HideInInspector] public List<GameObject> blueSheeps;
    [HideInInspector] public List<GameObject> blueGirls;

    public GameObject RedNex;
    public GameObject BlueNex;
    //135,150,450,90,150,150,45 *2
    //풀링 수
    int bearPool = 45; 
    int gunnerPool = 50; 
    int bunnyPool = 150; 
    int elephantPool = 30;
    int dogPool = 50;
    int sheepPool = 50;
    int GirlPool = 50;
    private void Awake()
    {
        pv = this.GetComponent<PhotonView>();
        insNexus = this;
    }
    // Use this for initialization
    void Start()
    {               
        //if(PhotonNetwork.isMasterClient)
        //NetWorkPooling(); //풀링시작
    }
    public void NetWorkPooling(Transform[] _min, Transform[] _max)
    {        
        for (int i = 0; i < bearPool; i++)
        {                         
            float randomXB = Random.Range(-40f,40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueBear", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueBear", Vector3.zero, Quaternion.identity, 0 ,null);
            int id = blueUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id ,i, blueSetPosition);
            //blueBears.Add(blueUnits);
                
            //GameObject redUnits = PhotonNetwork.Instantiate("RedBear", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedBear", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            //print("아이디 :  " + id2);
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redBears.Add(redUnits);                        
        }
        for (int i = 0; i < elephantPool; i++)
        {            
            float randomXB = Random.Range(-40f, 40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueBig", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueBig", Vector3.zero, Quaternion.identity, 0, null);

            int id = blueUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id, i, blueSetPosition);
            //blueElephants.Add(blueUnits);

            //GameObject redUnits = PhotonNetwork.Instantiate("RedBig", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedBig", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redElephants.Add(redUnits);            
        }
        for (int i = 0; i < bunnyPool; i++)
        {          
            float randomXB = Random.Range(-40f, 40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueBunny", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueBunny", Vector3.zero, Quaternion.identity, 0, null);
            int id = blueUnits.GetComponent<PhotonView>().viewID;                
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id, i, blueSetPosition);
            //blueBunnies.Add(blueUnits);

            //GameObject redUnits = PhotonNetwork.Instantiate("RedBunny", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedBunny", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redBunnies.Add(redUnits);            
        }
        for (int i = 0; i < gunnerPool; i++)
        {            
            float randomXB = Random.Range(-40f, 40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueGunner", Vector3.zero, Quaternion.identity, 0, null);
            int id = blueUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id, i, blueSetPosition);
            //blueGunners.Add(blueUnits);

            //GameObject redUnits = PhotonNetwork.Instantiate("RedGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedGunner", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redGunners.Add(redUnits);            
        }
        for (int i = 0; i < dogPool; i++)
        {           
            float randomXB = Random.Range(-40f, 40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueDog", Vector3.zero, Quaternion.identity, 0, null);
            int id = blueUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id, i, blueSetPosition);
            //blueGunners.Add(blueUnits);

            //GameObject redUnits = PhotonNetwork.Instantiate("RedGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedDog", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redGunners.Add(redUnits);            
        }
        for (int i = 0; i < sheepPool; i++)
        {
            float randomXB = Random.Range(-40f, 40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueSheep", Vector3.zero, Quaternion.identity, 0, null);
            int id = blueUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id, i, blueSetPosition);
            //blueGunners.Add(blueUnits);

            //GameObject redUnits = PhotonNetwork.Instantiate("RedGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedSheep", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redGunners.Add(redUnits);            
        }
        for (int i = 0; i < GirlPool; i++)
        {
            float randomXB = Random.Range(-40f, 40f);
            float randomZB = Random.Range(_min[0].position.z, _max[0].position.z);
            float randomXR = Random.Range(-40f, 40f);
            float randomZR = Random.Range(_min[1].position.z, _max[1].position.z);
            Vector3 blueSetPosition = new Vector3(randomXB, 0, randomZB);
            Vector3 redSetPosition = new Vector3(randomXR, 0, randomZR);
            //GameObject blueUnits = PhotonNetwork.Instantiate("BlueGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject blueUnits = PhotonNetwork.InstantiateSceneObject("BlueGirl", Vector3.zero, Quaternion.identity, 0, null);
            int id = blueUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddBlueTeamList", PhotonTargets.All, id, i, blueSetPosition);
            //blueGunners.Add(blueUnits);

            //GameObject redUnits = PhotonNetwork.Instantiate("RedGunner", Vector3.zero, Quaternion.identity, 0);
            GameObject redUnits = PhotonNetwork.InstantiateSceneObject("RedGirl", Vector3.zero, Quaternion.identity, 0, null);
            int id2 = redUnits.GetComponent<PhotonView>().viewID;
            pv.RPC("AddRedTeamList", PhotonTargets.All, id2, i, redSetPosition);
            //redGunners.Add(redUnits);            
        }
    }//풀링 완료 했고 리스트 추가후 애들한테 뿌려라
    [PunRPC]
    void AddBlueTeamList(int _id , int _name, Vector3 _setPosition)
    {
        //print(_id);
        GameObject added = PhotonView.Find(_id).gameObject;
        //print(added);
        switch (added.GetComponent<UnitState>().eName)
        {
            case UnitState.eCharacterName.Bunny:
                {
                    blueBunnies.Add(added);
                }
                break;
            case UnitState.eCharacterName.Gunner:
                {
                    blueGunners.Add(added);
                }
                break;
            case UnitState.eCharacterName.Big:
                {
                    blueElephants.Add(added);
                }
                break;
            case UnitState.eCharacterName.Bear:
                {
                    blueBears.Add(added);
                }
                break;
            case UnitState.eCharacterName.Dog:
                {
                    blueDogs.Add(added);
                }
                break;
            case UnitState.eCharacterName.Sheep:
                {
                    blueSheeps.Add(added);
                }                
                break;
            case UnitState.eCharacterName.Girl:
                {
                    blueGirls.Add(added);
                }
                break;
        }
        added.name = added.name + _name;
        AIUnit addUnitAi = added.GetComponent<AIUnit>();
        added.transform.parent = fightWolrd.transform;
        added.transform.rotation = Quaternion.LookRotation(Vector3.forward);        
        //added.GetComponent<UnitState>().estate = UnitState.eState.Dead;
        addUnitAi.wayPoints = new Transform[blueWayPoints.Length];
        added.transform.position = _setPosition;
        added.layer = LayerMask.NameToLayer("Default");
        for (int i = 0; i < addUnitAi.wayPoints.Length; i++)
        {
            addUnitAi.wayPoints[i] = blueWayPoints[i];
        }
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                addUnitAi.healthUI = Instantiate(addUnitAi.healthBar, c.transform).transform;
                addUnitAi.healthUI.name = "HealthBar : " + addUnitAi.name;
                addUnitAi.healthSlider = addUnitAi.healthUI.GetChild(0).GetComponent<Image>();                
                break;
            }
        }        
        addUnitAi.healthUI.gameObject.SetActive(false);
        added.GetComponent<CapsuleCollider>().enabled = false;
        added.transform.GetChild(0).gameObject.SetActive(false);
    }

    [PunRPC]
    void AddRedTeamList(int _id, int _name ,Vector3 _setPosition)
    {
        //print(_id);
        GameObject added = PhotonView.Find(_id).gameObject;
        //print(added);
        switch (added.GetComponent<UnitState>().eName)
        {
            case UnitState.eCharacterName.Bunny:
                {
                    redBunnies.Add(added);
                }
                break;
            case UnitState.eCharacterName.Gunner:
                {
                    redGunners.Add(added);
                }
                break;
            case UnitState.eCharacterName.Big:
                {
                    redElephants.Add(added);
                }
                break;
            case UnitState.eCharacterName.Bear:
                {
                    redBears.Add(added);
                }
                break;
            case UnitState.eCharacterName.Dog:
                {
                    redDogs.Add(added);
                }
                break;
            case UnitState.eCharacterName.Sheep:
                {
                    redSheeps.Add(added);
                }
                break;
            case UnitState.eCharacterName.Girl:
                {
                    redGirls.Add(added);
                }
                break;
        }
        added.name = added.name + _name;
        AIUnit addUnitAi = added.GetComponent<AIUnit>();
        added.transform.parent = fightWolrd.transform;
        added.transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        //added.GetComponent<UnitState>().estate = UnitState.eState.Dead;
        addUnitAi.wayPoints = new Transform[redWayPoints.Length];
        added.transform.position = _setPosition;
        added.layer = LayerMask.NameToLayer("Default");
        for (int i = 0; i < addUnitAi.wayPoints.Length; i++)
        {
            addUnitAi.wayPoints[i] = redWayPoints[i];
        }
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                addUnitAi.healthUI = Instantiate(addUnitAi.healthBar, c.transform).transform;
                addUnitAi.healthUI.name = "HealthBar : " + addUnitAi.name;
                addUnitAi.healthSlider = addUnitAi.healthUI.GetChild(0).GetComponent<Image>();
                break;
            }
        }                
        addUnitAi.healthUI.gameObject.SetActive(false);
        added.GetComponent<CapsuleCollider>().enabled = false;
        added.transform.GetChild(0).gameObject.SetActive(false);
    }   
    public int BlueBearIndex()
    {
        for (int i = 0; i < blueBears.Count; i++)
        {
            if (!blueBears[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                //print("꺼져있는 놈 : "+i);
                return i;
            }
        }
        return -1;
    }
    public int BlueBunnyIndex()
    {
        for (int i = 0; i < blueBunnies.Count; i++)
        {
            if (!blueBunnies[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int BlueElephantIndex()
    {
        for (int i = 0; i < blueElephants.Count; i++)
        {
            if (!blueElephants[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int BlueGunnerIndex()
    {
        for (int i = 0; i < blueGunners.Count; i++)
        {
            if (!blueGunners[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int BlueDogIndex()
    {
        for (int i = 0; i < blueDogs.Count; i++)
        {
            if (!blueDogs[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int BlueSheepIndex()
    {
        for (int i = 0; i < blueSheeps.Count; i++)
        {
            if (!blueSheeps[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int BlueGirlIndex()
    {
        for (int i = 0; i < blueGirls.Count; i++)
        {
            if (!blueGirls[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedBearIndex()
    {
        for (int i = 0; i < redBears.Count; i++)
        {
            if (!redBears[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedBunnyIndex()
    {
        for (int i = 0; i < redBunnies.Count; i++)
        {
            if (!redBunnies[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedElephantIndex()
    {
        for (int i = 0; i < redElephants.Count; i++)
        {
            if (!redElephants[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedGunnerIndex()
    {
        for (int i = 0; i < redGunners.Count; i++)
        {
            if (!redGunners[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedDogIndex()
    {
        for (int i = 0; i < redDogs.Count; i++)
        {
            if (!redDogs[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedSheepIndex()
    {
        for (int i = 0; i < redSheeps.Count; i++)
        {
            if (!redSheeps[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
    public int RedGirlIndex()
    {
        for (int i = 0; i < redGirls.Count; i++)
        {
            if (!redGirls[i].transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }

    //테스트 때 만들었던 것들
    public GameObject RedBear() //레드 곰 생성
    {
        for (int i = 0; i < redBears.Count; i++) //레드 베어 풀링 숫자만큼
        {
            if (!redBears[i].activeInHierarchy)//하이라키가 꺼진 놈 찾아서 리턴시킴(한번만)
            {
                return redBears[i];
            }
        }                 
        return null;
    }
    public GameObject RedBunny()
    {
        for (int i = 0; i < redBunnies.Count; i++)
        {
            if (!redBunnies[i].activeInHierarchy)
            {
                return redBunnies[i];
            }
        }
        return null;
    }
    public GameObject RedElephant()
    {
        for (int i = 0; i < redElephants.Count; i++)
        {
            if (!redElephants[i].activeInHierarchy)
            {
                return redElephants[i];
            }
        }
        return null;
    }
    public GameObject RedGunner()
    {
        for (int i = 0; i < redGunners.Count; i++)
        {
            if (!redGunners[i].activeInHierarchy)
            {
                return redGunners[i];
            }
        }
        return null;
    }
    public GameObject BlueBear()
    {
        for (int i = 0; i < blueBears.Count; i++)
        {
            if (!blueBears[i].activeInHierarchy)
            {
                return blueBears[i];
            }
        }
        return null;
    }
    public GameObject BlueBunny()
    {
        for (int i = 0; i < blueBunnies.Count; i++)
        {
            if (!blueBunnies[i].activeInHierarchy)
            {
                return blueBunnies[i];
            }
        }
        return null;
    }
    public GameObject BlueElephant()
    {
        for (int i = 0; i < blueElephants.Count; i++)
        {
            if (!blueElephants[i].activeInHierarchy)
            {
                return blueElephants[i];
            }
        }
        return null;
    }
    public GameObject BlueGunner()
    {
        for (int i = 0; i < blueGunners.Count; i++)
        {
            if (!blueGunners[i].activeInHierarchy)
            {
                return blueGunners[i];
            }
        }
        return null;
    }
    
    void UnitsPool()
    {
        int index = 0; //풀링 순서
        for (int i = 0; i < bearPool * 4; i++) //곰 풀링 수만큼 레드와 블루 풀링
        {
            GameObject redUnit = redCharacters[index]; //생성

            redUnit.gameObject.name = redUnit.gameObject.name + i; //이름 정의
            redUnit.transform.parent = fightWolrd.transform; //부모 설정
            redUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(Vector3.forward); //바라보는 방향
            //redUnit.GetComponent<UnitAIUp>().wayPoint = poolBlueStarter;
            redUnit.SetActive(false); //꺼둠
            redUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead; //죽음 상태로
            redUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length]; //유닛의 웨이포인트의 크기를 설정
            for (int j = 0; j < redUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++) //넥서스가 가진 웨이포인트를 유닛에게 전달
            {
                redUnit.GetComponent<UnitAIUp>().wayPoints[j] = redWayPoints[j].gameObject;
            }
            redBears.Add(redUnit); //레드 곰 리스트에 넣어줌(관리하기위함)

            GameObject blueUnit = (GameObject)Instantiate(blueCharacters[index]); //위와 같음
            blueUnit.gameObject.name = blueUnit.gameObject.name + i;
            blueUnit.transform.parent = fightWolrd.transform;
            blueUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(-Vector3.forward);
            //blueUnit.GetComponent<UnitAIUp>().wayPoint = poolRedStarter;
            blueUnit.SetActive(false);
            blueUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            blueUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < blueUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                blueUnit.GetComponent<UnitAIUp>().wayPoints[j] = blueWayPoints[j].gameObject;
            }
            blueBears.Add(blueUnit);
        }
        index++;
        for (int i = 0; i < elephantPool * 4; i++)
        {
            GameObject redUnit = (GameObject)Instantiate(redCharacters[index]);
            redUnit.gameObject.name = redUnit.gameObject.name + i;
            redUnit.transform.parent = fightWolrd.transform;
            redUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(Vector3.forward);
            //redUnit.GetComponent<UnitAIUp>().wayPoint = poolBlueStarter;
            redUnit.SetActive(false);
            redUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            redUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < redUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                redUnit.GetComponent<UnitAIUp>().wayPoints[j] = redWayPoints[j].gameObject;
            }
            redElephants.Add(redUnit);

            GameObject blueUnit = (GameObject)Instantiate(blueCharacters[index]);
            blueUnit.gameObject.name = blueUnit.gameObject.name + i;
            blueUnit.transform.parent = fightWolrd.transform;
            blueUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(-Vector3.forward);
            //blueUnit.GetComponent<UnitAIUp>().wayPoint = poolRedStarter;
            blueUnit.SetActive(false);
            blueUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            blueUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < blueUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                blueUnit.GetComponent<UnitAIUp>().wayPoints[j] = blueWayPoints[j].gameObject;
            }
            blueElephants.Add(blueUnit);
        }
        index++;
        for (int i = 0; i < bunnyPool * 4; i++)
        {
            GameObject redUnit = (GameObject)Instantiate(redCharacters[index]);
            redUnit.gameObject.name = redUnit.gameObject.name + i;
            redUnit.transform.parent = fightWolrd.transform;
            redUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(Vector3.forward);
            //redUnit.GetComponent<UnitAIUp>().wayPoint = poolBlueStarter;
            redUnit.SetActive(false);
            redUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            redUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < redUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                redUnit.GetComponent<UnitAIUp>().wayPoints[j] = redWayPoints[j].gameObject;
            }
            redBunnies.Add(redUnit);

            GameObject blueUnit = (GameObject)Instantiate(blueCharacters[index]);
            blueUnit.gameObject.name = blueUnit.gameObject.name + i;
            blueUnit.transform.parent = fightWolrd.transform;
            blueUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(-Vector3.forward);
            //blueUnit.GetComponent<UnitAIUp>().wayPoint = poolRedStarter;
            blueUnit.SetActive(false);
            blueUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            blueUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < blueUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                blueUnit.GetComponent<UnitAIUp>().wayPoints[j] = blueWayPoints[j].gameObject;
            }
            blueBunnies.Add(blueUnit);
        }
        index++;
        for (int i = 0; i < gunnerPool * 4; i++)
        {
            GameObject redUnit = (GameObject)Instantiate(redCharacters[index]);
            redUnit.gameObject.name = redUnit.gameObject.name + i;
            redUnit.transform.parent = fightWolrd.transform;
            redUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(Vector3.forward);
            //redUnit.GetComponent<UnitAIUp>().wayPoint = poolBlueStarter;
            redUnit.SetActive(false);
            redUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            redUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < redUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                redUnit.GetComponent<UnitAIUp>().wayPoints[j] = redWayPoints[j].gameObject;
            }
            redGunners.Add(redUnit);

            GameObject blueUnit = (GameObject)Instantiate(blueCharacters[index]);
            blueUnit.gameObject.name = blueUnit.gameObject.name + i;
            blueUnit.transform.parent = fightWolrd.transform;
            blueUnit.transform.GetChild(0).rotation = Quaternion.LookRotation(-Vector3.forward);
            //blueUnit.GetComponent<UnitAIUp>().wayPoint = poolRedStarter;
            blueUnit.SetActive(false);
            blueUnit.GetComponent<UnitState>().estate = UnitState.eState.Dead;
            blueUnit.GetComponent<UnitAIUp>().wayPoints = new GameObject[redWayPoints.Length];
            for (int j = 0; j < blueUnit.GetComponent<UnitAIUp>().wayPoints.Length; j++)
            {
                blueUnit.GetComponent<UnitAIUp>().wayPoints[j] = blueWayPoints[j].gameObject;
            }
            blueGunners.Add(blueUnit);
        }        
    }
}
