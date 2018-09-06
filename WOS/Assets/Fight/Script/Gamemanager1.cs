using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager1 : Photon.MonoBehaviour
{
    static Gamemanager1 Instance = null; // 싱글톤
    public fNexus m_Nexus;    
    public fBlueTeam m_cBlueTeam; // 블루팀
    public List<fNode> BlueNode = new List<fNode>(); // 블루 플레이어의 노드(땅)
    public List<fNode> RedNode = new List<fNode>(); //  레드 플레이어의 노드(땅)
    public fRedTeam m_cRedTeam; // 레드팀
    public fGUIMANAGER m_cGUIMANAGER; // GUI 매니저
    public fBuildManager m_cBulildManager; // 빌드매니저 
    public fStoreManager m_cStoreManager; // 상점 매니저
    public SpecialUse m_cSpecialuse; // 스킬사용
    public Transform[] max;
    public Transform[] min;
    bool gameOver = false;
    PhotonView pv;
    int roomPlayer;
    public float f_CountTime;
    float f_beforeTime;
    int readyPlayer;
    bool gameStart;
    // Use this for initialization
    private void Awake()
    {
        f_beforeTime = f_CountTime;
        PhotonNetwork.isMessageQueueRunning = true;
        Instance = this;
        pv = this.GetComponent<PhotonView>();
    }
    private void Start()
    {
        //PhotonNetwork.autoCleanUpPlayerObjects = false;
        if (PhotonNetwork.isMasterClient)
        {
            readyPlayer++;
        }
        else
        {
            pv.RPC("SendReady", PhotonTargets.MasterClient);
        }
    }
    public static Gamemanager1 GetInstance()
    {
        return Instance;
    }
    

    IEnumerator  InsTime()
    {        
        if (PhotonNetwork.isMasterClient)
        {
            while (f_CountTime > 0) //마스터가 하도록 바꿔 줘야함
            {
                m_cGUIMANAGER.t_InsTimeText.text = " " + ((int)f_CountTime);
                yield return new WaitForSeconds(1f);
                f_CountTime--;
                pv.RPC("CountDown", PhotonTargets.Others, f_CountTime);
                yield return null;
            }
        }
        if (f_CountTime <= 0)
        {
            //StartCoroutine("BlueUnitIns");
            //StartCoroutine("RedUnitIns");
            if (PhotonNetwork.isMasterClient)
            {
                BlueUnitsInsRPC();
                RedUnitsInsRPC();
                //print("소환");

            }
            f_CountTime = f_beforeTime;
            StartCoroutine("InsTime");
        }
    }
    [PunRPC]
    public void CountDown(float time)
    {
        m_cGUIMANAGER.t_InsTimeText.text = " " + ((int)time);        
    }
    void BlueUnitsInsRPC()
    {
        //Debug.Log("소환2");
        for (int i = 0; i < m_cBlueTeam.cPlayer.BearBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[1].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.BlueBearIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    //m_Nexus.blueBears[unitIndex].transform.position = setposition;
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 1, unitIndex, setposition, true);
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.ELBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[2].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.BlueElephantIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 2, unitIndex, setposition, true);
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.RabbitBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[3].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.BlueBunnyIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 3, unitIndex, setposition, true);
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.GunBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[4].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.BlueGunnerIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 4, unitIndex, setposition, true);
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.DogBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[5].Amount; j++)
            {
                int unitIndex = m_Nexus.BlueDogIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 5, unitIndex, setposition, true);
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.SheepBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[6].Amount; j++)
            {
                int unitIndex = m_Nexus.BlueSheepIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 6, unitIndex, setposition, true);
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.SpaceBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[7].Amount; j++)
            {
                int unitIndex = m_Nexus.BlueGirlIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 7, unitIndex, setposition, true);
                }
            }
        }
    }
    void RedUnitsInsRPC()
    {
        for (int i = 0; i < m_cRedTeam.cPlayer.BearBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[1].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.RedBearIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    //m_Nexus.redBears[unitIndex].transform.position = setposition;
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 1, unitIndex, setposition, false);
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.ELBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[2].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.RedElephantIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 2, unitIndex, setposition, false);
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.RabbitBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[3].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.RedBunnyIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 3, unitIndex, setposition, false);
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.GunBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[4].Amount; j++)
            {
                //GameObject unit = m_Nexus.BlueBear();
                int unitIndex = m_Nexus.RedGunnerIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 4, unitIndex, setposition, false);
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.DogBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[5].Amount; j++)
            {
                int unitIndex = m_Nexus.RedDogIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 5, unitIndex, setposition, false);
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.SheepBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[6].Amount; j++)
            {
                int unitIndex = m_Nexus.RedSheepIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 6, unitIndex, setposition, false);
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.SpaceBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[7].Amount; j++)
            {
                int unitIndex = m_Nexus.RedGirlIndex();
                if (unitIndex != -1)
                {
                    float randomX = Random.Range(-40f, 40f);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    Vector3 setposition = new Vector3(randomX, 0, randomZ);
                    pv.RPC("SetUnitPosition", PhotonTargets.All, 7, unitIndex, setposition, false);
                }
            }
        }
    }
    [PunRPC]
    void SendReady()
    {
        readyPlayer++;
    }
    [PunRPC]
    void SetUnitPosition(int _Name, int _index, Vector3 _position, bool _isBlue)
    {
        GameObject unitIns = null;
        //print(_index);
        if (_isBlue) //블루팀
        {
            if (_Name == 1)
            {
                unitIns = m_Nexus.blueBears[_index].gameObject;
                //Debug.Log("곰이다");
            }
            else if (_Name == 2)
            {
                unitIns = m_Nexus.blueElephants[_index].gameObject;
                //Debug.Log("코끼리다");
            }
            else if (_Name == 3)
            {
                unitIns = m_Nexus.blueBunnies[_index].gameObject;
                //Debug.Log("토끼다");
            }
            else if (_Name == 4)
            {
                unitIns = m_Nexus.blueGunners[_index].gameObject;
                //Debug.Log("거너다");
            }
            else if (_Name == 5)
            {
                unitIns = m_Nexus.blueDogs[_index].gameObject;
            }
            else if(_Name == 6)
            {
                unitIns = m_Nexus.blueSheeps[_index].gameObject;
            }
            else if (_Name == 7)
            {
                unitIns = m_Nexus.blueGirls[_index].gameObject;
            }
            unitIns.layer = LayerMask.NameToLayer("BlueUnit");
            //Debug.Log("레이어지정");
        }
        else
        {
            if (_Name == 1)
            {
                unitIns = m_Nexus.redBears[_index].gameObject;
            }
            else if (_Name == 2)
            {
                unitIns = m_Nexus.redElephants[_index].gameObject;
            }
            else if (_Name == 3)
            {
                unitIns = m_Nexus.redBunnies[_index].gameObject;
            }
            else if (_Name == 4)
            {
                unitIns = m_Nexus.redGunners[_index].gameObject;
            }
            else if (_Name == 5)
            {
                unitIns = m_Nexus.redDogs[_index].gameObject;
            }
            else if (_Name == 6)
            {
                unitIns = m_Nexus.redSheeps[_index].gameObject;
            }
            else if (_Name == 7)
            {
                unitIns = m_Nexus.redGirls[_index].gameObject;
            }
            unitIns.layer = LayerMask.NameToLayer("RedUnit");
        }
        if (unitIns != null)
        {
            UnitState unitSt = unitIns.GetComponent<UnitState>();
            unitSt.pHealth = unitSt.pMaxHealth;
            unitSt.estate = UnitState.eState.Move;
            unitIns.transform.GetChild(0).gameObject.SetActive(true);
            unitIns.GetComponent<AIUnit>().healthUI.gameObject.SetActive(true);
            unitIns.GetComponent<CapsuleCollider>().enabled = true;
            StartCoroutine(unitIns.GetComponent<AIUnit>().Action());
        }
    }

    [PunRPC]
    void GameStart()
    {
        gameStart = true;

        if (PhotonNetwork.isMasterClient)
        {
            m_Nexus.NetWorkPooling(min, max);
        }
        StartCoroutine("JellyUp");
        StartCoroutine("InsTime");
    }

    void OnOwnershipRequest(object[] viewAndPlayer)
    {
        print("마스터 나감 소유권 양도");
        //PhotonView view = viewAndPlayer[0] as PhotonView;
        //PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;

        //Debug.Log("OnOwnershipRequest(): Player " + requestingPlayer + " requests ownership of: " + view + ".");
        for (int i = 0; i < viewAndPlayer.Length; i++)
        {
            PhotonView view = viewAndPlayer[i] as PhotonView;
            GameObject oner = PhotonView.Find(view.viewID).gameObject;
            if (oner.GetComponent<UnitState>() != null)
            {
                if (oner.GetComponent<UnitState>().eName != UnitState.eCharacterName.Nexus)
                {
                    if (oner.activeInHierarchy)
                    {
                        oner.SetActive(true);
                        oner.GetComponent<UnitState>().estate = UnitState.eState.Move;
                    }
                }
            }
        }
    }

    IEnumerator GameOver()
    {
        while (!gameOver)
        {
            if (m_Nexus.BlueNex.GetComponent<UnitState>().pHealth <= 0)
            {
                print("레드팀 승리");
                pv.RPC("Victory", PhotonTargets.All, PunTeams.Team.red);
                gameOver = true;
            }
            else if (m_Nexus.RedNex.GetComponent<UnitState>().pHealth <= 0)
            {
                print("블루팀 승리");
                pv.RPC("Victory", PhotonTargets.All, PunTeams.Team.blue);
                gameOver = true;
            }
            yield return null;
        }
    }
    [PunRPC]
    void Victory(PunTeams.Team _team)
    {
        print("승리 팀 : " + _team);
        switch (_team) //이긴팀 이름
        {
            case PunTeams.Team.none:
                break;
            case PunTeams.Team.red:
                {
                    if(PhotonNetwork.player.GetTeam() == PunTeams.Team.red) //내가 이긴팀이면
                    {
                        m_cGUIMANAGER.result.SetActive(true);
                        m_cGUIMANAGER.t_result.text = "승리";
                        m_cGUIMANAGER.BuildButton.SetActive(false);
                        m_cGUIMANAGER.MyBuildList.SetActive(false);                        
                    }
                    else //내가 진팀이면
                    {
                        m_cGUIMANAGER.result.SetActive(true);
                        m_cGUIMANAGER.t_result.text = "패배";
                        m_cGUIMANAGER.BuildButton.SetActive(false);
                        m_cGUIMANAGER.MyBuildList.SetActive(false);                        
                    }
                }
                break;
            case PunTeams.Team.blue:
                {
                    if (PhotonNetwork.player.GetTeam() == PunTeams.Team.red) //내가 진팀이면
                    {
                        m_cGUIMANAGER.result.SetActive(true);
                        m_cGUIMANAGER.t_result.text = "패배";
                        m_cGUIMANAGER.BuildButton.SetActive(false);
                        m_cGUIMANAGER.MyBuildList.SetActive(false);                        
                    }
                    else //내가 이긴팀이면
                    {
                        m_cGUIMANAGER.result.SetActive(true);
                        m_cGUIMANAGER.t_result.text = "승리";
                        m_cGUIMANAGER.BuildButton.SetActive(false);
                        m_cGUIMANAGER.MyBuildList.SetActive(false);
                    }
                }
                break;
        }
    }
  public  void GameEnd()
    {
        PhotonNetwork.isMessageQueueRunning = false;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
    private void Update()
    {
        if (readyPlayer == PhotonNetwork.room.MaxPlayers && PhotonNetwork.isMasterClient && !gameStart)
        {
            pv.RPC("GameStart", PhotonTargets.All);
           // StartCoroutine("GameOver");
        }
        StartCoroutine("GameOver");
        //f_CountTime -= Time.deltaTime;
        //m_cGUIMANAGER.t_InsTimeText.text = " " + ((int)f_CountTime+1); // 여기서 시간이 0이되면 풀링 
        //if(f_CountTime < 0) // 0이되면 다시 카운트 다운 
        //{
        //    //스폰 구현 (빌딩 정보 가져오는거
        //    //건물 개수  = m_cBlueTeam.cPlayer.BearBuild
        //    //유닛 개수 = m_cBulildManager.GetBuildlist()[0].Amount;
        //    for (int i = 0; i < m_cBlueTeam.cPlayer.BearBuild; i++)
        //    {
        //        for (int j = 0; j < m_cBulildManager.GetBuildlist()[0].Amount; j++)
        //        {

        //        }
        //    }
        //    f_CountTime = 30;
        //}        
        JellyText();

        if (NetWorkConnect.ins.isblueTeam)
        {
            if (m_cBlueTeam.cPlayer.JellyTime == true) //젤리 쿨타임 확인
            {
                m_cBlueTeam.cPlayer.JellyIns -= Time.deltaTime;
            }
            else
            {
                m_cBlueTeam.cPlayer.JellyIns = 0;
            }
        }
        if (NetWorkConnect.ins.isRedTeam)
        {
            if (m_cRedTeam.cPlayer.JellyTime == true) //젤리 쿨타임 확인
            {
                m_cRedTeam.cPlayer.JellyIns -= Time.deltaTime;
            }
            else
            {
                m_cRedTeam.cPlayer.JellyIns = 0;
            }
        }
    }
    void JellyText()
    {
        if (NetWorkConnect.ins.isblueTeam)
        {
            m_cGUIMANAGER.t_JellyText.text = "Jelly : " + m_cBlueTeam.cPlayer.Jelly;
        }
        if (NetWorkConnect.ins.isRedTeam)
        {

            m_cGUIMANAGER.t_JellyText.text = "Jelly : " + m_cRedTeam.cPlayer.Jelly;
        }
    }
    IEnumerator JellyUp()
    {
        while (true)
        {
            if (NetWorkConnect.ins.isblueTeam)
            {
                if (m_cBlueTeam.cPlayer.JellyPlus == 0)  // 젤리 건물 갯수
                {
                    if (m_cBlueTeam.cPlayer.JellyTime == true) // 젤리샀을때 쿨타임이 true면 
                    {
                        if (m_cBlueTeam.cPlayer.JellyIns < 0) // 젤리쿨타임 시간초를 받아오는데 0이크면 젤리 쿨타임은 false
                        {
                            m_cBlueTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cBlueTeam.cPlayer.Jelly += 1;  // 젤리들어오는 갯수 
                    }
                }
                if (m_cBlueTeam.cPlayer.JellyPlus == 1)
                {
                    if (m_cBlueTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cBlueTeam.cPlayer.JellyIns < 0)
                        {
                            m_cBlueTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cBlueTeam.cPlayer.Jelly += 2;
                    }
                }
                if (m_cBlueTeam.cPlayer.JellyPlus == 2)
                {
                    if (m_cBlueTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cBlueTeam.cPlayer.JellyIns < 0)
                        {
                            m_cBlueTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cBlueTeam.cPlayer.Jelly += 3;
                    }
                }
                if (m_cBlueTeam.cPlayer.JellyPlus == 3)
                {
                    if (m_cBlueTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cBlueTeam.cPlayer.JellyIns < 0)
                        {
                            m_cBlueTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cBlueTeam.cPlayer.Jelly += 4;
                    }
                }
                if (m_cBlueTeam.cPlayer.JellyPlus == 4)
                {
                    if (m_cBlueTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cBlueTeam.cPlayer.JellyIns < 0)
                        {
                            m_cBlueTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cBlueTeam.cPlayer.Jelly += 5;
                    }
                }
                if (m_cBlueTeam.cPlayer.JellyPlus == 5)
                {
                    if (m_cBlueTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cBlueTeam.cPlayer.JellyIns < 0)
                        {
                            m_cBlueTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cBlueTeam.cPlayer.Jelly += 6;
                    }
                }
                yield return new WaitForSeconds(0.095f);
            }
            if (NetWorkConnect.ins.isRedTeam)
            {
                if (m_cRedTeam.cPlayer.JellyPlus == 0)  // 젤리 건물 갯수
                {
                    if (m_cRedTeam.cPlayer.JellyTime == true) // 젤리샀을때 쿨타임이 true면 
                    {
                        if (m_cRedTeam.cPlayer.JellyIns < 0) // 젤리쿨타임 시간초를 받아오는데 0이크면 젤리 쿨타임은 false
                        {
                            m_cRedTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cRedTeam.cPlayer.Jelly += 1;  // 젤리들어오는 갯수 
                    }
                }
                if (m_cRedTeam.cPlayer.JellyPlus == 1)
                {
                    if (m_cRedTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cRedTeam.cPlayer.JellyIns < 0)
                        {
                            m_cRedTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cRedTeam.cPlayer.Jelly += 2;
                    }
                }
                if (m_cRedTeam.cPlayer.JellyPlus == 2)
                {
                    if (m_cRedTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cRedTeam.cPlayer.JellyIns < 0)
                        {
                            m_cRedTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cRedTeam.cPlayer.Jelly += 3;
                    }
                }
                if (m_cRedTeam.cPlayer.JellyPlus == 3)
                {
                    if (m_cRedTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cRedTeam.cPlayer.JellyIns < 0)
                        {
                            m_cRedTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cRedTeam.cPlayer.Jelly += 4;
                    }
                }
                if (m_cRedTeam.cPlayer.JellyPlus == 4)
                {
                    if (m_cRedTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cRedTeam.cPlayer.JellyIns < 0)
                        {
                            m_cRedTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cRedTeam.cPlayer.Jelly += 5;
                    }
                }
                if (m_cRedTeam.cPlayer.JellyPlus == 5)
                {
                    if (m_cRedTeam.cPlayer.JellyTime == true)
                    {
                        if (m_cRedTeam.cPlayer.JellyIns < 0)
                        {
                            m_cRedTeam.cPlayer.JellyTime = false;
                        }
                    }
                    else
                    {
                        m_cRedTeam.cPlayer.Jelly += 6;
                    }
                }
                yield return new WaitForSeconds(0.095f);
            }
        }
    }


    [PunRPC]
    public void BlueBuildUp(int num) // 블루팀  건물갯수 네트워크 
    {
        if (num == 1)
        {
            m_cBlueTeam.cPlayer.BearBuild += 1;
        }
        if (num == 2)
        {
            m_cBlueTeam.cPlayer.ELBuild += 1;
        }
        if (num == 3)
        {
            m_cBlueTeam.cPlayer.RabbitBuild += 1;
        }
        if (num == 4)
        {
            m_cBlueTeam.cPlayer.GunBuild += 1;

        }
        if (num == 5)
        {
            m_cBlueTeam.cPlayer.DogBuild += 1;
        }
        if (num == 6)
        {
            m_cBlueTeam.cPlayer.SheepBuild += 1;

        }
        if (num == 7)
        {
            m_cBlueTeam.cPlayer.SpaceBuild += 1;
        }
        if (num == 8)
        {
            m_cBlueTeam.cPlayer.ClownBuild += 1;
        }
    }
    [PunRPC]
    public void RedBuildUp(int num) // 레드팀 건물갯수 네트워크 
    {
        if (num == 1)
        {
            m_cRedTeam.cPlayer.BearBuild += 1;
        }
        if (num == 2)
        {
            m_cRedTeam.cPlayer.ELBuild += 1;
        }
        if (num == 3)
        {
            m_cRedTeam.cPlayer.RabbitBuild += 1;
        }
        if (num == 4)
        {
            m_cRedTeam.cPlayer.GunBuild += 1;

        }
        if(num == 5)
        {
            m_cRedTeam.cPlayer.DogBuild += 1;
        }
        if (num == 6)
        {
            m_cRedTeam.cPlayer.SheepBuild += 1;
        }
        if (num == 7)
        {
            m_cRedTeam.cPlayer.SpaceBuild += 1;
        }
        if (num == 8)
        {
            m_cRedTeam.cPlayer.ClownBuild += 1;
        }
    }
    [PunRPC]
    void BlueBuildCheck(int i, int p) // 블루팀 노드에 건물 만들었는지 네트워크 
    {
        BlueNode[i].gBuildCreat = m_cStoreManager.cStoreBuy.prefarb_Build[p];
        BlueNode[i].BlueBuildCreat();
    }
    [PunRPC]
    void RedBuildCheck(int i, int p) // 레드팀 노드에 건물 만들었는지 네트워크 
    {        
        RedNode[i].gBuildCreat = m_cStoreManager.cStoreBuy.prefarb_Build[p];
        RedNode[i].RedBuildCreat();
    }

    [PunRPC]
    void RedMsg(string text)
    {
       m_cGUIMANAGER.t_Notice2.SetActive(true);
        m_cGUIMANAGER.text_Notice2.text = text;
        StartCoroutine("Wait");
    }
    [PunRPC]
    void BlueMsg(string text)
    {
        m_cGUIMANAGER.t_Notice2.SetActive(true);
        m_cGUIMANAGER.text_Notice2.text = text;
        StartCoroutine("Wait");
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        m_cGUIMANAGER.t_Notice2.SetActive(false);
    }
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()); //연결상태
        GUILayout.Label(PhotonNetwork.playerName); // 내 뷰아이디 확인
        GUILayout.Label(PhotonNetwork.player.GetTeam().ToString()); //내 팀 확인
    }
    //테스트때 만든 것 들 (안씀)
    void BlueUnitIns()
    {
        for (int i = 0; i < m_cBlueTeam.cPlayer.BearBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[1].Amount; j++)
            {
                GameObject unit = m_Nexus.BlueBear();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.ELBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[2].Amount; j++)
            {
                GameObject unit = m_Nexus.BlueElephant();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.RabbitBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[3].Amount; j++)
            {
                GameObject unit = m_Nexus.BlueBunny();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
        for (int i = 0; i < m_cBlueTeam.cPlayer.GunBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[4].Amount; j++)
            {
                GameObject unit = m_Nexus.RedBunny();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[0].position.z, max[0].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
    }
    void RedUnitIns()
    {
        for (int i = 0; i < m_cRedTeam.cPlayer.BearBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[1].Amount; j++)
            {
                GameObject unit = m_Nexus.RedBear();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.ELBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[2].Amount; j++)
            {
                GameObject unit = m_Nexus.RedElephant();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }

        for (int i = 0; i < m_cRedTeam.cPlayer.RabbitBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[3].Amount; j++)
            {
                GameObject unit = m_Nexus.RedBunny();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
        for (int i = 0; i < m_cRedTeam.cPlayer.GunBuild; i++)
        {
            for (int j = 0; j < m_cBulildManager.GetBuildlist()[4].Amount; j++)
            {
                GameObject unit = m_Nexus.RedGunner();
                if (unit != null)
                {
                    float randomX = Random.Range(-45, 45);
                    float randomZ = Random.Range(min[1].position.z, max[1].position.z);
                    unit.transform.position = new Vector3(randomX, 0, randomZ);
                    unit.SetActive(true);
                    unit.GetComponent<UnitState>().pHealth = unit.GetComponent<UnitState>().pMaxHealth;
                    unit.GetComponent<UnitState>().estate = UnitState.eState.Move;
                }
            }
        }
    }
}
