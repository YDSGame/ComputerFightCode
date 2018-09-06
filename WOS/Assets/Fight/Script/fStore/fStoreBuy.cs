using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fStoreBuy : MonoBehaviour {

    public GameObject[] prefarb_Build; // 건물 프리팹 넣는곳
    public fBuild m_cBuild; // 건물 enum을 쓸려고 만든거
   // public fspecial m_cSpecial; 
    public fspecial fspecial; // 버튼클릭시 스페셜기술 enum을 쓸려고 만들었는곳
    public fBuildManager m_cBuildM; // 빌드리스트 가지고오기위한 클래스 변수 
    public Text c_Text;

    public bool b_Build = false;
    public bool b_Special = false;

    PhotonView PV;  //포톤뷰
     public SpecialUse m_cSpecialuse;
    // Use this for initialization
    void Start()
    {
        PV = Gamemanager1.GetInstance().GetComponent<PhotonView>(); // 게임매니저 받아옴 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyClick()
    {

        m_cBuildM = Gamemanager1.GetInstance().m_cBulildManager.GetComponent<fBuildManager>();
        m_cSpecialuse = Gamemanager1.GetInstance().m_cSpecialuse.GetComponent<SpecialUse>();
        fPlayer cPlayer; // 플레이어의 노드를 접근하기위해서 만든 멤버변수 
        if (NetWorkConnect.ins.isblueTeam) // 내가 블루팀일떄 
        {

           cPlayer = Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GetComponent<fPlayer>(); // 플레이어의 노드를 접근하기위해서 만든 멤버변수 
            if (b_Build == true)
            {
                if (cPlayer.Nodes < 70)
                {
                    switch (m_cBuild.BuildName)
                    {

                        case fBuild.eBuildName.JELLY:
                            {

                                if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[0].JellyPrice)
                                {
                                    if (cPlayer.JellyPlus < 5)
                                    {
                                        Debug.Log("젤리구매완료");
                                        cPlayer.b_Jelly = true;
                                        cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[0].JellyPrice;
                                        cPlayer.JellyPlus += 1;
                                        c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                        StartCoroutine("Wait");
                                        if (cPlayer.JellyPlus == 1) // 젤리에따라서 쿨타임 증가 
                                        {
                                            cPlayer.JellyTime = true;
                                            cPlayer.JellyIns = 15;
                                        }
                                        if (cPlayer.JellyPlus == 2)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyIns += 20;
                                        }
                                        if (cPlayer.JellyPlus == 3)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyIns += 25;
                                        }
                                        if (cPlayer.JellyPlus == 4)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyIns += 30;
                                        }
                                        if (cPlayer.JellyPlus == 5)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyIns += 35;
                                        }
                                    }

                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리매입불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    if (cPlayer.JellyPlus == 3)
                                    {
                                        Debug.Log("필살기 사용가능");
                                    }
                                }
                                else
                                {
                                    Debug.Log("장난감 총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }

                            }
                            break;
                        case fBuild.eBuildName.BEAR:
                            {                                                                  //지을 수 있는 건물 최대 수
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.BearBuild < 15)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++) //for문을 쓴 이유는 리스트에 들어가서 하나하나 체크하기위해서 
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[1].JellyPrice)
                                        {
                                            Debug.Log("베어의땅 구매완료");

                                            // Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[0]; //프리팹들어갈꺼
                                            // Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 0); // 블루팀 건물을 체크해서 모든 플레이어에게 보내준다. 
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[1].JellyPrice;
                                            print("i" + i);
                                            cPlayer.Nodes += 1;
                                            PV.RPC("BlueBuildUp", PhotonTargets.All, 1); // 건물을 만들었다는것을 모든 플레이어에게 보내준다. 
                                                                                         // Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.BearBuild += 1;

                                            //c_Text.text = "<color=#ff0000>" + 10 + "</color>";
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("베어의땅 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.EL:
                            {

                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ELBuild < 30)
                                {
                                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyPlus >= 2)
                                    {

                                        for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++) //for문을 쓴 이유는 리스트에 들어가서 하나하나 체크하기위해서 
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[2].JellyPrice)
                                            {
                                                Debug.Log("코끼리뿔 구매완료");

                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[2];
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 2);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[2].JellyPrice;
                                                print("i" + i);
                                                cPlayer.Nodes += 1;
                                                PV.RPC("BlueBuildUp", PhotonTargets.All, 2);
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ELBuild += 1;
                                                //c_Text.text = "<color=#ff0000>" + 10 + "</color>";
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("베어의땅 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ELBuild >= 30)
                                    {
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";

                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물1개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }

                            }
                            break;
                        case fBuild.eBuildName.RABBIT:
                            {
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.RabbitBuild < 30)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++) //for문을 쓴 이유는 리스트에 들어가서 하나하나 체크하기위해서 
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[3].JellyPrice)
                                        {
                                            Debug.Log("토끼의집 구매완료");

                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[3];
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 3);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[3].JellyPrice;
                                            print("i" + i);
                                            cPlayer.Nodes += 1;
                                            PV.RPC("BlueBuildUp", PhotonTargets.All, 3);
                                            // Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.RabbitBuild += 1;
                                            //c_Text.text = "<color=#ff0000>" + 10 + "</color>";
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("베어의땅 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.GUN:
                            {
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild < 25)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++)
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[4].JellyPrice)
                                        {
                                            Debug.Log("장난감 총 구매완료");
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 1);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[4].JellyPrice;
                                            cPlayer.Nodes += 1;
                                            PV.RPC("BlueBuildUp", PhotonTargets.All, 4);
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild += 1;
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("장난감 총 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");

                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.DOG:
                            {
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild < 15)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++)
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[5].JellyPrice)
                                        {
                                            Debug.Log("장난감 총 구매완료");
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 4);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[5].JellyPrice;
                                            cPlayer.Nodes += 1;
                                            PV.RPC("BlueBuildUp", PhotonTargets.All, 5);
                                            //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild += 1;
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("장난감 총 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");

                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.SHEEP:
                            {
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SheepBuild < 25)
                                {
                                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyPlus >= 1)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++)
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[6].JellyPrice)
                                            {
                                                Debug.Log("장난감 총 구매완료");
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 5);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[6].JellyPrice;
                                                cPlayer.Nodes += 1;
                                                PV.RPC("BlueBuildUp", PhotonTargets.All, 6);
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild += 1;
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("장난감 총 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");

                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SheepBuild >= 25)
                                    {
                                        Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물1개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }
                            }
                            break;
                        case fBuild.eBuildName.SPACE:
                            {
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SpaceBuild < 25)
                                {
                                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyPlus >= 2)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++)
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[7].JellyPrice)
                                            {
                                                Debug.Log("장난감 총 구매완료");
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 6);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[7].JellyPrice;
                                                cPlayer.Nodes += 1;
                                                PV.RPC("BlueBuildUp", PhotonTargets.All, 7);
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild += 1;
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("장난감 총 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");

                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SpaceBuild >= 25)
                                    {
                                        Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물2개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }
                            }
                            break;
                        case fBuild.eBuildName.CLOWN:
                            {
                                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ClownBuild < 1)
                                {
                                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyPlus >= 3)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().BlueNode.Count; i++)
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[8].JellyPrice)
                                            {
                                                Debug.Log("장난감 총 구매완료");
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("BlueBuildCheck", PhotonTargets.All, i, 7);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[8].JellyPrice;
                                                cPlayer.Nodes += 1;
                                                PV.RPC("BlueBuildUp", PhotonTargets.All, 8);
                                                //Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild += 1;
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("장난감 총 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");

                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ClownBuild >= 1)
                                    {
                                        Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물3개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }
                            }
                            break;
                        default:
                            break;

                    }
                }
                else
                {
                    Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice.SetActive(true);
                    Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice.GetComponent<Text>().text = "건물을 더이상 짓을수없습니다.";

                }
            }
            if (b_Special == true)
            {
                switch (fspecial.BuildName)
                {

                    case fspecial.eBuildName.HEAING:
                        {

                            cPlayer = Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GetComponent<fPlayer>();
                            if (cPlayer.JellyPlus >= 4)
                            {
                                if (cPlayer.Jelly >= m_cBuildM.GetFspecialsList()[0].JellyPrice)
                                {

                                    Debug.Log("젤리구매완료");
                                    cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetFspecialsList()[0].JellyPrice;
                                    m_cSpecialuse.BlueHealing();
                                    Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice2.SetActive(true);
                                    Gamemanager1.GetInstance().m_cGUIMANAGER.text_Notice2.text = "필살기를 사용하였습니다.";
                                    PV.RPC("RedMsg", PhotonTargets.Others, "블루팀에서 필살기를 시전하였습니다.");
                                    StartCoroutine("Wait2");
                                    c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                    StartCoroutine("Wait");


                                }
                                else
                                {
                                    Debug.Log("신의총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }
                            }
                            else
                            {
                                c_Text.text = "<color=#ff0000>" + "젤리공장 4개필요!!" + "</color>";
                                StartCoroutine("Wait");
                            }
                           
                        } break;
                    case fspecial.eBuildName.DESTORY:
                        {

                            cPlayer = Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GetComponent<fPlayer>();
                            if (cPlayer.JellyPlus >= 4)
                            {
                                if (cPlayer.Jelly >= m_cBuildM.GetFspecialsList()[1].JellyPrice)
                                {

                                    Debug.Log("젤리구매완료");
                                    cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetFspecialsList()[1].JellyPrice;
                                    m_cSpecialuse.RedDestory();
                                    c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                    StartCoroutine("Wait");


                                }
                                else
                                {
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }
                            }
                            else
                            {
                                c_Text.text = "<color=#ff0000>" + "젤리공장 4개필요!!" + "</color>";
                                StartCoroutine("Wait");
                            }

                        } break;
                    case fspecial.eBuildName.MINDCONTROLL:
                        {

                            cPlayer = Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GetComponent<fPlayer>();
                            if (cPlayer.JellyPlus >= 4)
                            {
                                if (cPlayer.Jelly >= m_cBuildM.GetFspecialsList()[2].JellyPrice)
                                {

                                    Debug.Log("젤리구매완료");
                                    cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetFspecialsList()[2].JellyPrice;
                                    c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                    StartCoroutine("Wait");


                                }
                                else
                                {
                                    Debug.Log("신의총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }
                            }
                            else
                            {
                                c_Text.text = "<color=#ff0000>" + "젤리공장 4개필요!!" + "</color>";
                                StartCoroutine("Wait");
                            }

                        }
                        break;
                }
            }
        }
        //레드
        if (NetWorkConnect.ins.isRedTeam)
        {

            cPlayer = Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GetComponent<fPlayer>();
            if (b_Build == true)
            {
                if (cPlayer.Nodes < 70)
                {
                    switch (m_cBuild.BuildName)
                    {

                        case fBuild.eBuildName.JELLY:
                            {

                                if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[0].JellyPrice)
                                {
                                    if (cPlayer.JellyPlus < 5)
                                    {
                                        Debug.Log("젤리구매완료");
                                        cPlayer.b_Jelly = true;
                                        cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[0].JellyPrice;
                                        cPlayer.JellyPlus += 1;
                                        c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                        StartCoroutine("Wait");
                                        if (cPlayer.JellyPlus == 1) // 젤리에따라서 쿨타임 증가 
                                        {
                                            cPlayer.JellyTime = true;
                                            cPlayer.JellyIns = 15;
                                        }
                                        if (cPlayer.JellyPlus == 2)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyIns += 20;
                                        }
                                        if (cPlayer.JellyPlus == 3)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyIns += 25;
                                        }
                                        if (cPlayer.JellyPlus == 4)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyIns += 30;
                                        }
                                        if (cPlayer.JellyPlus == 5)
                                        {
                                            cPlayer.JellyTime = true;
                                            Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyIns += 35;
                                        }
                                    }

                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리매입불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    if (cPlayer.JellyPlus == 3)
                                    {
                                        Debug.Log("필살기 사용가능");
                                    }
                                }
                                else
                                {
                                    Debug.Log("장난감 총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }

                            }
                            break;
                        case fBuild.eBuildName.BEAR:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.BearBuild < 15)
                                {
                                    Debug.Log("곰만들자");
                                    for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++) //for문을 쓴 이유는 리스트에 들어가서 하나하나 체크하기위해서 
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[1].JellyPrice)
                                        {
                                            Debug.Log("베어의땅 구매완료");

                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[0]; //프리팹들어갈꺼
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("RedBuildCheck", PhotonTargets.All, i, 0);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[1].JellyPrice;
                                            print("i" + i);
                                            cPlayer.Nodes += 1;
                                            PV.RPC("RedBuildUp", PhotonTargets.All, 1);
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.BearBuild += 1;
                                            //c_Text.text = "<color=#ff0000>" + 10 + "</color>";
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("베어의땅 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.EL:
                            {

                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ELBuild < 30)
                                {
                                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyPlus >= 2)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++) //for문을 쓴 이유는 리스트에 들어가서 하나하나 체크하기위해서 
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[2].JellyPrice)
                                            {
                                                Debug.Log("코끼리뿔 구매완료");

                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[2];
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("RedBuildCheck", PhotonTargets.All, i, 2);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[2].JellyPrice;
                                                print("i" + i);
                                                cPlayer.Nodes += 1;
                                                PV.RPC("RedBuildUp", PhotonTargets.All, 2);
                                                // Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ELBuild += 1;
                                                //c_Text.text = "<color=#ff0000>" + 10 + "</color>";
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("베어의땅 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ELBuild >= 30)
                                    {
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물2개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }

                            }
                            break;
                        case fBuild.eBuildName.RABBIT:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.RabbitBuild < 30)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++) //for문을 쓴 이유는 리스트에 들어가서 하나하나 체크하기위해서 
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[3].JellyPrice)
                                        {
                                            Debug.Log("토끼의집 구매완료");

                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[3];
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("RedBuildCheck", PhotonTargets.All, i, 3);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[3].JellyPrice;
                                            print("i" + i);
                                            cPlayer.Nodes += 1;
                                            PV.RPC("RedBuildUp", PhotonTargets.All, 3);
                                            // Gamemanager1.GetInstance().m_cRedTeam.cPlayer.RabbitBuild += 1;
                                            //c_Text.text = "<color=#ff0000>" + 10 + "</color>";
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("베어의땅 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.GUN:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild < 25)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++)
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[4].JellyPrice)
                                        {
                                            Debug.Log("장난감 총 구매완료");
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("RedBuildCheck", PhotonTargets.All, i, 1);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[4].JellyPrice;
                                            cPlayer.Nodes += 1;
                                            PV.RPC("RedBuildUp", PhotonTargets.All, 4);
                                            //  Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild += 1;
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("장난감 총 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");

                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.DOG:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild < 15)
                                {
                                    for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++)
                                    {
                                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[5].JellyPrice)
                                        {
                                            Debug.Log("강아지 구매완료");
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                            //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                            PV.RPC("RedBuildCheck", PhotonTargets.All, i, 4);
                                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[5].JellyPrice;
                                            cPlayer.Nodes += 1;
                                            PV.RPC("RedBuildUp", PhotonTargets.All, 5);
                                            //  Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild += 1;
                                            c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                            StartCoroutine("Wait");
                                        }
                                        else
                                        {
                                            Debug.Log("장난감 총 젤리부족");
                                            c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                            StartCoroutine("Wait");

                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                    c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                    StartCoroutine("Wait");
                                }
                            }
                            break;
                        case fBuild.eBuildName.SHEEP:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SheepBuild < 25)
                                {
                                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyPlus >= 1)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++)
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[6].JellyPrice)
                                            {
                                                Debug.Log("양 구매완료");
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("RedBuildCheck", PhotonTargets.All, i, 5);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[6].JellyPrice;
                                                cPlayer.Nodes += 1;
                                                PV.RPC("RedBuildUp", PhotonTargets.All, 6);
                                                //  Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild += 1;
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("장난감 총 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");

                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SheepBuild >= 25)
                                    {
                                        Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물1개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }
                            }
                            break;
                        case fBuild.eBuildName.SPACE:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SpaceBuild < 25)
                                {
                                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyPlus >= 2)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++)
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[7].JellyPrice)
                                            {
                                                Debug.Log("양 구매완료");
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("RedBuildCheck", PhotonTargets.All, i, 6);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[7].JellyPrice;
                                                cPlayer.Nodes += 1;
                                                PV.RPC("RedBuildUp", PhotonTargets.All, 7);
                                                //  Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild += 1;
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("장난감 총 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");

                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SpaceBuild >= 25)
                                    {
                                        Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물2개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }
                            }
                            break;
                        case fBuild.eBuildName.CLOWN:
                            {
                                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ClownBuild < 1)
                                {
                                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyPlus >= 3)
                                    {
                                        for (int i = 0; i < Gamemanager1.GetInstance().RedNode.Count; i++)
                                        {
                                            if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[8].JellyPrice)
                                            {
                                                Debug.Log("양 구매완료");
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].gBuildCreat = prefarb_Build[1];
                                                //Gamemanager1.GetInstance().m_cRedTeam.cPlayer.Node[i].BuildCreat();
                                                PV.RPC("RedBuildCheck", PhotonTargets.All, i, 7);
                                                cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[8].JellyPrice;
                                                cPlayer.Nodes += 1;
                                                PV.RPC("RedBuildUp", PhotonTargets.All, 8);
                                                //  Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild += 1;
                                                c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                                StartCoroutine("Wait");
                                            }
                                            else
                                            {
                                                Debug.Log("장난감 총 젤리부족");
                                                c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                                StartCoroutine("Wait");

                                            }
                                            break;
                                        }
                                    }
                                    else if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ClownBuild >= 1)
                                    {
                                        Debug.Log("건물을 더이상 짓을수 없습니다. ");
                                        c_Text.text = "<color=#ff0000>" + "건설 불가!" + "</color>";
                                        StartCoroutine("Wait");
                                    }
                                    else
                                    {
                                        c_Text.text = "<color=#ff0000>" + "젤리건물3개필요" + "</color>";
                                        StartCoroutine("Wait");
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice.SetActive(true);
                    Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice.GetComponent<Text>().text = "건물을 더이상 짓을수없습니다.";

                }
            }
            if (b_Special == true)
            {
                switch (fspecial.BuildName)
                {

                    case fspecial.eBuildName.HEAING:
                        {

                            cPlayer = Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GetComponent<fPlayer>();
                            if (cPlayer.JellyPlus >= 4)
                            {
                                if (cPlayer.Jelly >= m_cBuildM.GetFspecialsList()[0].JellyPrice)
                                {

                                    Debug.Log("젤리구매완료");
                                    cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetFspecialsList()[0].JellyPrice;
                                    m_cSpecialuse.BlueHealing();
                                    Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice2.SetActive(true);
                                    Gamemanager1.GetInstance().m_cGUIMANAGER.text_Notice2.text = "필살기를 사용하였습니다.";
                                    PV.RPC("BlueMsg", PhotonTargets.Others, "레드팀에서 필살기를 시전하였습니다.");
                                    StartCoroutine("Wait2");
                                    c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                    StartCoroutine("Wait");


                                }
                                else
                                {
                                    Debug.Log("신의총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }
                            }
                            else
                            {
                                c_Text.text = "<color=#ff0000>" + "젤리공장 4개필요!!" + "</color>";
                                StartCoroutine("Wait");
                            }

                        }
                        break;
                    case fspecial.eBuildName.DESTORY:
                        {

                            cPlayer = Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GetComponent<fPlayer>();
                            if (cPlayer.JellyPlus >= 4)
                            {
                                if (cPlayer.Jelly >= m_cBuildM.GetFspecialsList()[1].JellyPrice)
                                {

                                    Debug.Log("젤리구매완료");
                                    cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetFspecialsList()[1].JellyPrice;
                                    c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                    StartCoroutine("Wait");


                                }
                                else
                                {
                                    Debug.Log("신의총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }
                            }
                            else
                            {
                                c_Text.text = "<color=#ff0000>" + "젤리공장 4개필요!!" + "</color>";
                                StartCoroutine("Wait");
                            }

                        }
                        break;
                    case fspecial.eBuildName.MINDCONTROLL:
                        {

                            cPlayer = Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GetComponent<fPlayer>();
                            if (cPlayer.JellyPlus >= 4)
                            {
                                if (cPlayer.Jelly >= m_cBuildM.GetFspecialsList()[2].JellyPrice)
                                {

                                    Debug.Log("젤리구매완료");
                                    cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetFspecialsList()[2].JellyPrice;
                                    c_Text.text = "<color=#ff0000>" + "구매완료!!" + "</color>";
                                    StartCoroutine("Wait");


                                }
                                else
                                {
                                    Debug.Log("신의총 젤리부족");
                                    c_Text.text = "<color=#ff0000>" + "젤리부족!!" + "</color>";
                                    StartCoroutine("Wait");

                                }
                            }
                            else
                            {
                                c_Text.text = "<color=#ff0000>" + "젤리공장 4개필요!!" + "</color>";
                                StartCoroutine("Wait");
                            }

                        }
                        break;
                }
            }
        }
     
    }
   public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        c_Text.text = "구매";
    }
    public IEnumerator Wait2()
    {
        yield return new WaitForSeconds(3.0f);
        Gamemanager1.GetInstance().m_cGUIMANAGER.t_Notice2.SetActive(false);
    }
}
