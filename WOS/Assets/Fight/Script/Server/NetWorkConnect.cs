using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client;
using Hashtable = ExitGames.Client.Photon.Hashtable;
// 0.01 버젼에서는 채팅 개선 
// 0.02 버젼에서는 게임방 팀 나누기 
// 0.03 버젼에서는 게임 실행
// 0.04 버젼에서는 건물 동기화 및 채팅창 갱신

public class NetWorkConnect : Photon.MonoBehaviour
{
    public static NetWorkConnect ins;    
    public Text m_cText; // 버젼
    string str_Version = "0.04";
    public GameObject PlayerName;
    public Transform PlayerList;
    public List<Transform> BlueTeamList;
    public List<Transform> RedTeamList;    
    string myname;
    enum myRoom { CHAT, GAME }
    myRoom myroom;
    int roomnum;
    bool team;
    public Text m_cRoomNumber;
    public List<int> pid = new List<int>(); // 플레이어 들어오는인원
    int myID;
    public bool isblueTeam;
    public bool isRedTeam;
    public GameObject m_cCountDown;

    public AudioSource LobbyAudio;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        ins = this;
        myroom = myRoom.CHAT;
        //로깅 레벨 이름설정에서 현재 세팅으로 서버연결
        PhotonNetwork.ConnectUsingSettings(str_Version);        
        m_cText.text = "현재버젼  : " + str_Version;
        //부가적으로 로깅레벨과 이름세팅
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);           
    }
    void OnGui()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    //방 생성 연결
    void OnJoinedLobby()
    {
        //print("내 아이디: " + PhotonNetwork.playerName);
        //Debug.Log("로비");
        switch (myroom)
        {
            case myRoom.CHAT:
                {
                    //print("대기실");
                    Hashtable cus = new Hashtable() { { "ChatRoom",0 } }; //"Chatroom"키에 해당하며 해당 값을 가진다.
                    PhotonNetwork.JoinRandomRoom(cus,100);//해당 해쉬테이블값을 비교해서 그값을 가진 룸을 찾는다. (있다면 - 들어간다, 없다면 - return)
                }
                break;
            case myRoom.GAME:
                {
                    RoomOptions roomOptions = new RoomOptions();

                    if (roomnum == 1)
                    {
                        //print("1:1방 들어가자");                       
                        Hashtable cus = new Hashtable() { { "GameRoom", 1 } };                                                                                                                 
                        PhotonNetwork.JoinRandomRoom(cus,2);                                                
                    }
                    else if (roomnum == 2)
                    {
                        //print("2:2방 들어가자");
                        Hashtable cus = new Hashtable() { { "GameRoom", 2 } };                       
                        PhotonNetwork.JoinRandomRoom(cus, 4);
                    }
                    else if (roomnum == 3)
                    {
                        //print("3:3방 만들자");
                        Hashtable cus = new Hashtable() { { "GameRoom", 3 } };
                        PhotonNetwork.JoinRandomRoom(cus, 6);
                        //roomOptions.IsOpen = true; // 공개방 설정
                        //roomOptions.IsVisible = true; // 리스트 공개
                        //roomOptions.CleanupCacheOnLeave = true; // 방 떠나면 정보 날리기
                        //PhotonNetwork.CreateRoom("3:3", roomOptions, TypedLobby.Default);                       
                    }
                    break;
                }
        }
        // PhotonNetwork.JoinRoom("ChatRoom");
    }    
    void OnPhotonRandomJoinFailed()
    {
        //Debug.Log("방 실패해서 방을 개설");
        switch (myroom)
        {
            case myRoom.CHAT:
                {
                    CreatChatRoom();
                }
                break;
            case myRoom.GAME:
                {
                    //Debug.Log("게임을 찾지못해서 게임방 개설");
                    CreatGameRoom();                    
                }
                break;
        }
    }
    void CreatChatRoom()
    {
        Hashtable cus = new Hashtable() { { "ChatRoom", 0 } };
        RoomOptions myop = new RoomOptions();
        myop.CustomRoomProperties = new Hashtable();
        myop.CustomRoomPropertiesForLobby = new string[1];
        myop.CustomRoomPropertiesForLobby[0] = "ChatRoom";
        myop.CustomRoomProperties.Add("ChatRoom", 0);                    
        myop.IsOpen = true;
        myop.IsVisible = true;
        myop.MaxPlayers = 100;
        PhotonNetwork.CreateRoom(null, myop, TypedLobby.Default);        
    }

    [PunRPC]
    void CreatGameRoom()
    {                
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true; // 공개방 설정
        roomOptions.IsVisible = true; // 리스트 공개
        roomOptions.CleanupCacheOnLeave = true; // 방 떠나면 정보 날리기

        if (roomnum == 1)
        {
            //print("1:1방 만들자");
            Hashtable cus = new Hashtable() { { "GameRoom", 1 } };
            roomOptions.CustomRoomProperties = new Hashtable();
            roomOptions.CustomRoomPropertiesForLobby = new string[1];
            roomOptions.CustomRoomPropertiesForLobby[0] = "GameRoom";
            roomOptions.CustomRoomProperties.Add("GameRoom", 1);
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
        }
        else if (roomnum == 2)
        {
            //print("2:2방 만들자");
            Hashtable cus = new Hashtable() { { "GameRoom", 2 } };
            roomOptions.CustomRoomProperties = new Hashtable();
            roomOptions.CustomRoomPropertiesForLobby = new string[1]; // 개수 할당
            roomOptions.CustomRoomPropertiesForLobby[0] = "GameRoom";
            roomOptions.CustomRoomProperties.Add("GameRoom", 2);
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
        }
        else if (roomnum == 3)
        {
            //print("3:3방 만들자");
            Hashtable cus = new Hashtable() { { "GameRoom", 3 } };            
            roomOptions.CustomRoomProperties = new Hashtable();
            roomOptions.CustomRoomPropertiesForLobby = new string[1]; // 개수 할당
            roomOptions.CustomRoomPropertiesForLobby[0] = "GameRoom";
            roomOptions.CustomRoomProperties.Add("GameRoom", 3);            
            roomOptions.MaxPlayers = 6;
            PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
        }
        //TypedLobby lobby = new TypedLobby(); // 로비 타입 설정
        //lobby.Name = "Chat Lobby"; // 로비이름
        //lobby.Type = LobbyType.Default;// 로비는 일반적인 로비
        //이로비에 이옵션으로 채팅방이라는 방을 만들기
        //PhotonNetwork.CreateRoom("ChatRoom", roomOptions, TypedLobby.Default);
    }
   
    void OnJoinedRoom()  // 방 생성
    {
        switch (myroom)
        {
            case myRoom.CHAT:
                {
                    //Debug.Log("채팅방 접속");                   
                    pid.Clear();
                    PhotonNetwork.automaticallySyncScene = false;
                    if (PhotonNetwork.isMasterClient)
                    {
                        PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0); //생성
                        myID = PlayerName.GetComponent<PhotonView>().viewID; //아이디 저장
                        myname = myID.ToString(); //내 닉네임 저장
                        PlayerName.GetComponent<Text>().text = myname; //텍스트오브젝트 아이디 설정
                        PhotonNetwork.playerName = myID.ToString(); //플레이어 이름 지정                                                             
                        PlayerName.transform.SetParent(PlayerList); //자식으로 들어감
                        pid.Add(PlayerName.GetComponent<PhotonView>().viewID);                        
                    }
                    else
                    {
                        PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0); //생성
                        myID = PlayerName.GetComponent<PhotonView>().viewID; //아이디 저장
                        myname = myID.ToString(); //내 닉네임 저장
                        PlayerName.GetComponent<Text>().text = myname; //텍스트오브젝트 아이디 설정
                        PhotonNetwork.playerName = myID.ToString(); //플레이어 이름 지정                                           
                        photonView.RPC("NameSet", PhotonTargets.Others, myID, myname);                        
                        photonView.RPC("SendListToMaster", PhotonTargets.MasterClient, myID);
                        photonView.RPC("SetList", PhotonTargets.MasterClient);
                    }                    
                }
                break;
            case myRoom.GAME:
                {
                    //print("게임방 접속"); 
                    pid.Clear();
                    PhotonNetwork.automaticallySyncScene = true;
                    if (PhotonNetwork.isMasterClient)
                    {
                        PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0); //생성
                        myID = PlayerName.GetComponent<PhotonView>().viewID; //아이디 저장
                        myname = myID.ToString(); //내 닉네임 저장
                        PlayerName.GetComponent<Text>().text = myname; //텍스트오브젝트 아이디 설정
                        PhotonNetwork.playerName = myID.ToString(); //플레이어 이름 지정            
                        if(roomnum == 1)
                        {
                            PlayerName.transform.SetParent(BlueTeamList[0]); //바꿈
                        }
                        else if(roomnum == 2)
                        {
                            PlayerName.transform.SetParent(BlueTeamList[1]); //바꿈
                        }
                        else if (roomnum == 3)
                        {
                        PlayerName.transform.SetParent(BlueTeamList[2]); //바꿈
                        }
                        pid.Add(PlayerName.GetComponent<PhotonView>().viewID);                        
                    }
                    else
                    {
                        PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0); //생성
                        myID = PlayerName.GetComponent<PhotonView>().viewID; //아이디 저장
                        myname = myID.ToString(); //내 닉네임 저장
                        PlayerName.GetComponent<Text>().text = myname; //텍스트오브젝트 아이디 설정
                        PhotonNetwork.playerName = myID.ToString(); //플레이어 이름 지정                      
                        photonView.RPC("NameSet", PhotonTargets.Others, myID, myname);
                        //PlayerName.transform.SetParent(BlueTeamList); //바꿈
                        photonView.RPC("SendListToMaster", PhotonTargets.MasterClient, PlayerName.GetComponent<PhotonView>().viewID);
                        photonView.RPC("SetList", PhotonTargets.MasterClient);
                    }
                    //if(BlueTeamList.GetChildCount() == 1)
                    //{
                    //    team = true;
                    //    //photonView.RPC("PlayerListview", PhotonTargets.All, PlayerName.GetComponent<PhotonView>().viewID, myname, team);
                    //}
                    //else
                    //{
                    //    //PlayerName.transform.SetParent(RedTeamList); //바꿈
                    //    team = false;
                    //    //photonView.RPC("PlayerListview", PhotonTargets.All, PlayerName.GetComponent<PhotonView>().viewID, myname, team);
                    //}
                }
                break;
        }
    }
    [PunRPC]
    void SendListToMaster(int _otherID)  // 방장에게 아이디 넘버를 넘김
    {
        if (PhotonNetwork.isMasterClient)
        {
            pid.Add(_otherID);
        }// 방장에게 다른 플레이어 목록을 추가한다.
    }
    [PunRPC]
    public void ReMoveList(int _OutID) // 아이디 지움 
    {
        pid.Remove(_OutID);
    }
    [PunRPC]
    void SendListToClient(int _otherID)  // 리스트 추가 
    {
        pid.Add(_otherID); //방장이아닌애들에게 리스트를 넘겨줌
    }
    [PunRPC]
    void ResetPlayerList() // 리스트 초기화 
    {
        pid.Clear();
    }
    [PunRPC]
    void SetList()
    {
        if (PhotonNetwork.isMasterClient) // 내가 마스터 클라이언트라면
        {
            photonView.RPC("ResetPlayerList", PhotonTargets.Others); // 다른 타겟에게 리스트 초기화를 해준다.
            for (int i = 0; i < pid.Count; i++)   // 플레이어수 만큼 반복문을 돈다.
            {
                photonView.RPC("SendListToClient", PhotonTargets.Others, pid[i]);  // 플레이어리스트이 i만큼 다른 플레이어에에게 플레이어의 리스트를 넘겨준다. 
            }
            photonView.RPC("SetTeam", PhotonTargets.All); // 모든 플레이어는 팀을 나눈다.
           
        }       
    }
    void GameRoomCheck() // 게임방 체크 
    {
        if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
        {
            if (pid.Count == 2)
            {
                m_cCountDown.SetActive(true);
                photonView.RPC("GameCount", PhotonTargets.All);
                //if(PhotonNetwork.isMasterClient)
                //{
                //    StartCoroutine("GameCount");  //  방장이 시간관련 코루틴 불러옴
                //}

            }
        }
    }
    [PunRPC]
    IEnumerator GameCount() // 게임 카운트
    {
        int i = 5;

        for (i = 5; i >= 0; i--)
        {
            if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
            {
                m_cCountDown.GetComponent<Text>().text = " " + i;
                //  photonView.RPC("OhterPlayerCount", PhotonTargets.All,i); // 게임방 플레이어들에게 다 보내줌 텍스트를

                yield return new WaitForSeconds(1f);
                if (i == 0 && PhotonNetwork.isMasterClient)   // 시간이 0 이고 방장이라면 
                {
                    //m_cCountDown.SetActive(false);
                    PhotonNetwork.isMessageQueueRunning = false; // 나머지 플레이어가 같은 장면 올때까지 false 로 한다. 
                    PhotonNetwork.LoadLevel(1);
                }
                PhotonNetwork.room.IsOpen = false; // 방잠김
            }
            else
            {
                m_cCountDown.SetActive(false);
                PhotonNetwork.room.IsOpen = true;
                break;
            }
        }
    }
    [PunRPC]
    void OhterPlayerCount(int i) // 카운트다운 텍스트 받아서 처리용
    {
        m_cCountDown.GetComponent<Text>().text = " " + i;
        if (i == 0)
        {
            m_cCountDown.SetActive(false);
            PhotonNetwork.isMessageQueueRunning = false; // 나머지 플레이어가 같은 장면 올때까지 false 로 한다.
            PhotonNetwork.LoadLevel(1);
        }
    }
    [PunRPC]
    void SetTeam()  //팀나누기
    {
        switch (myroom)
        {
            case myRoom.CHAT:
                {
                    for (int i = 0; i < pid.Count; i++) 
                    {
                        PhotonView.Find(pid[i]).gameObject.transform.SetParent(PlayerList); // 플레이어를 찾는다.
                        PhotonView.Find(pid[i]).gameObject.transform.position = Vector3.zero;
                    }
                }
                break;
            case myRoom.GAME:
                {
                    if (roomnum==1)
                    {
                        for (int i = 0; i < pid.Count; i++)
                        {
                            if (i % 2 == 0)//BlueTeam
                            {
                                PhotonView.Find(pid[i]).gameObject.transform.SetParent(BlueTeamList[0]); // 뷰아이디 찾아서 블루팀에 넣는다.
                                if (PhotonView.Find(pid[i]).isMine)
                                {
                                    isblueTeam = true;
                                    PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
                                }
                            }
                            else //RedTeam
                            {
                                PhotonView.Find(pid[i]).gameObject.transform.SetParent(RedTeamList[0]);
                                if (PhotonView.Find(pid[i]).isMine)
                                {
                                    isRedTeam = true;
                                    PhotonNetwork.player.SetTeam(PunTeams.Team.red);
                                }
                            }
                            GameRoomCheck();
                        }
                    }
                    else if (roomnum == 2)
                    {
                        for (int i = 0; i < pid.Count; i++)
                        {
                            if (i % 2 == 0)//BlueTeam
                            {
                                PhotonView.Find(pid[i]).gameObject.transform.SetParent(BlueTeamList[1]);
                                if (PhotonView.Find(pid[i]).isMine)
                                {
                                    isblueTeam = true;
                                    PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
                                }
                            }
                            else //RedTeam
                            {
                                PhotonView.Find(pid[i]).gameObject.transform.SetParent(RedTeamList[1]);
                                if (PhotonView.Find(pid[i]).isMine)
                                {

                                    isRedTeam = true;
                                    PhotonNetwork.player.SetTeam(PunTeams.Team.red);
                                }
                            }
                        }
                    }
                    else if (roomnum == 3)
                    {
                        for (int i = 0; i < pid.Count; i++)
                        {
                            if (i % 2 == 0)//BlueTeam
                            {
                                PhotonView.Find(pid[i]).gameObject.transform.SetParent(BlueTeamList[2]);
                                if (PhotonView.Find(pid[i]).isMine)
                                {
                                    isblueTeam = true;
                                    PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
                                }
                            }
                            else //RedTeam
                            {
                                PhotonView.Find(pid[i]).gameObject.transform.SetParent(RedTeamList[2]);
                                if (PhotonView.Find(pid[i]).isMine)
                                {
                                    isRedTeam = true;
                                    PhotonNetwork.player.SetTeam(PunTeams.Team.red);
                                }
                            }
                        }
                    }
                }
                break;
        }
    }
    [PunRPC]
    void NameSet(int _othersID, string _otherName)  // 플레이어 이름 
    {
        PhotonView.Find(_othersID).gameObject.GetComponent<Text>().text = " " + _otherName;
        PhotonView.Find(_othersID).gameObject.name = _otherName;
    }
    public void OnPhotonPlayerConnected(PhotonPlayer other) // 다른 유저가 접속했을 때 (다른 유저가 OnJoined후 내가 불러온다)
    {       
        photonView.RPC("NameSet", PhotonTargets.Others, PlayerName.GetComponent<PhotonView>().viewID, myname);  
    }
    [PunRPC] //바꿈
    void PlayerListview(int _othersID, string _otherName ,bool _team)
    {
        switch (myroom)
        {
            case myRoom.CHAT:
                {
                    PhotonView.Find(_othersID).gameObject.GetComponent<Text>().text = " " + _otherName;
                    PhotonView.Find(_othersID).gameObject.transform.SetParent(PlayerList);
                }
                break;
            case myRoom.GAME:
                {
                    if (_team)
                    {
                        PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0);
                        PlayerName.GetComponent<Text>().text = " " + PhotonNetwork.playerName;
                        PlayerName.transform.SetParent(BlueTeamList[0]); //바꿈
                        PhotonView.Find(_othersID).gameObject.GetComponent<Text>().text = " " + _otherName;
                        PhotonView.Find(_othersID).gameObject.transform.SetParent(BlueTeamList[0]);
                    }
                    else
                    {
                        PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0);
                        PlayerName.GetComponent<Text>().text = " " + PhotonNetwork.playerName;
                        PlayerName.transform.SetParent(RedTeamList[0]); //바꿈
                        PhotonView.Find(_othersID).gameObject.GetComponent<Text>().text = " " + _otherName;
                        PhotonView.Find(_othersID).gameObject.transform.SetParent(RedTeamList[0]);
                    }                   
                }
                break;
        }        //PlayerName = PhotonNetwork.Instantiate("PlayerName", PlayerList.position, Quaternion.identity, 0);
        //PlayerName.transform.SetParent(PlayerList);
    }

    //버튼 사용
    public void OneGameRoom()
    {
        roomnum = 1;
        myroom = myRoom.GAME;
        PhotonNetwork.LeaveRoom();
        //roomOptions.maxPlayers = 2;
        //PhotonNetwork.CreateRoom("1:1", roomOptions, TypedLobby.Default);
    }
    public void TwoGameRoom()
    {
        roomnum = 2;
        myroom = myRoom.GAME;
        PhotonNetwork.LeaveRoom();
    }
    public void ThreeGameRoom()
    {
        roomnum = 3;
        myroom = myRoom.GAME;
        PhotonNetwork.LeaveRoom();
    }
    public void RoomExit()
    {
        myroom = myRoom.CHAT;
        //photonView.RPC("ReMoveList", PhotonTargets.MasterClient, myID);
        //photonView.RPC("SetList", PhotonTargets.MasterClient);
        
        m_cCountDown.SetActive(false);
        PhotonNetwork.player.SetTeam(PunTeams.Team.none);
        PhotonNetwork.LeaveRoom();
    }
 
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()); //연결상태
        GUILayout.Label(PhotonNetwork.playerName); // 내 뷰아이디 확인
        GUILayout.Label(PhotonNetwork.player.GetTeam().ToString()); //내 팀 확인
    }

}