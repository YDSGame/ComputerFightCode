using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MakeRoom : Photon.MonoBehaviour {
    public string version = "0.1";
    public bool GameRoom = false; // 게임 방 생성 유무
    public List<PhotonPlayer> playerList = new List<PhotonPlayer>();
    public List<PhotonPlayer> myPlayerList;
    public Transform[] teamtran;
    public GameObject playerPrefab;
    public Text m_cText; // 버젼
    GameObject myName;


    //DS
    public bool ChatRoom = false; // 채팅 방 생성 유무
    private void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }

        m_cText.text = "현재버젼  : " + version;
    }
    int maxPlayer;

    public void OnClickedMatchingStartButton(int _maxPlayer)
    {
        GameRoom = true;
        maxPlayer = _maxPlayer;
        PhotonNetwork.JoinRandomRoom(); // 방이있는지 없는지 확인해준다고한다.
        
    }
    void OnPhotonRandomJoinFailed()
    {
        print("방이없어서 방 생성");
        CreateRoom();
        //if (GameRoom == true)
        //{
        //    print("게임방생성");
        //    string _roomName = "Room_" + Random.Range(0, 999).ToString("000");

        //    RoomOptions roomOptions = new RoomOptions();
        //    roomOptions.IsOpen = true;
        //    roomOptions.IsVisible = true;
        //    roomOptions.MaxPlayers = (byte)maxPlayer;
        //    print("총 인원 : " + maxPlayer);
        //    PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
        //}
        //if (ChatRoom == true)
        //{
        //    Debug.Log("채팅방 생성");
        //    RoomOptions roomOptions = new RoomOptions(); //방옵션 설정
        //    roomOptions.IsOpen = true; //공개방 설정
        //    roomOptions.IsVisible = true; // 리스트 공개
        //                                  //option.cleanupCacheOnLeave = true; // 방 떠나면 정보 날리기

        //    TypedLobby lobby = new TypedLobby(); //로비 타입설정
        //    lobby.Name = "Chat Lobby"; // 로비이름
        //    lobby.Type = LobbyType.Default;// 로비는 일반적인 로비

        //    //이로비에 이옵션으로 채팅방이라는 방을 만들기

        //    PhotonNetwork.CreateRoom("ChatRoom", roomOptions, lobby);
        //}
    }
    void OnJoinedLobby()
    {
        print("로비에들어옴");
       
    }
     void OnJoinedRoom()
    {
        if (GameRoom == true)
        {
            Debug.Log("게임방 참가 ");
            myName = PhotonNetwork.Instantiate("PlayerName", teamtran[0].position, Quaternion.identity, 0);
            myName.transform.SetParent(teamtran[0]);
        }
    }
    public void CreateRoom()
    {
        if (GameRoom == true)
        {
            print("게임방생성");
            string _roomName = "Room_" + Random.Range(0, 999).ToString("000");

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = (byte)maxPlayer;
            print("총 인원 : " + maxPlayer);
            PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
        }
        if (ChatRoom == true)
        {
            Debug.Log("채팅방 생성");
            RoomOptions roomOptions = new RoomOptions(); //방옵션 설정
            roomOptions.IsOpen = true; //공개방 설정
            roomOptions.IsVisible = true; // 리스트 공개
                                          //option.cleanupCacheOnLeave = true; // 방 떠나면 정보 날리기

            TypedLobby lobby = new TypedLobby(); //로비 타입설정
            lobby.Name = "Chat Lobby"; // 로비이름
            lobby.Type = LobbyType.Default;// 로비는 일반적인 로비

            //이로비에 이옵션으로 채팅방이라는 방을 만들기

            PhotonNetwork.CreateRoom("ChatRoom", roomOptions, lobby);
        }
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        print("Create Room Failed = " + codeAndMsg[1]);
    }
    // Use this for initialization
    void Start () {
        
            
        StartCoroutine("GameStartCheck");
        
	}
	IEnumerator GameStartCheck()
    {
        print("게임체크");
        while (true)
        {
            myPlayerList = PhotonNetwork.playerList.ToList();
            print("List :" + myPlayerList.Count);
            if(PhotonNetwork.room !=null &&PhotonNetwork.isMasterClient  &&PhotonNetwork.room.playerCount == PhotonNetwork.room.MaxPlayers)
            {
                PhotonNetwork.room.IsOpen = false;
                print("게임시작하자");
            }
            yield return null;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI()
    {
        //화면 좌측 상단에 접속 과정에 대한 로그 출력
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    //DS
    public void OnClickedMatchingStartButton()
    {
        ChatRoom = true;
        PhotonNetwork.JoinRoom("ChatRoom");
    }
    public void ChatExit()
    {
        ChatRoom = false;
        PhotonNetwork.LeaveRoom();
    }
}
