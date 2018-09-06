using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fGUIMANAGER : MonoBehaviour {
    public Text t_JellyText;  // 젤리양 
    public Text t_InsTimeText; // 소환 시간 
    public GameObject m_prefabText; // 텍스트 프리팹
    public GameObject Creat; // 생성위치 
    public Text JellyFactory;
    public Text t_whereTeam;
    public GameObject t_Notice;
    public GameObject BuildButton;
    public GameObject MyBuildList;
    public GameObject result;
    public Text t_result;
    public Text DeathMatchText;
    public GameObject t_Notice2;
    public Text text_Notice2;

    Text bearText;
    Text ELText;
    Text RabbitText;
    Text t_Gun;
    Text t_Dog;
    Text t_Sheep;
    
    // Use this for initialization
    void Start () {
        StartCoroutine("situation");
        t_whereTeam.text = PhotonNetwork.player.GetTeam().ToString();
	}
		
    IEnumerator situation() // 내 현재 건물 상황 
    {
        while (true)
        {
            if (NetWorkConnect.ins.isblueTeam)
            {
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.BearBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_BearBuild == false)
                    {
                        GameObject bText = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        bearText = bText.GetComponent<Text>();
                        bText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_BearBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ELBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_ELBuild == false)
                    {
                        GameObject EText = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        ELText = EText.GetComponent<Text>();
                        ELText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_ELBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.RabbitBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_RabbitBuild == false)
                    {
                        GameObject Rabittext = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        RabbitText = Rabittext.GetComponent<Text>();
                        Rabittext.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_RabbitBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild == 1)
                {


                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_GunBuild == false)
                    {
                        GameObject GunText = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        t_Gun = GunText.GetComponent<Text>();
                        GunText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_GunBuild = true;
                    }
                }
                if(Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.DogBuild == 1)
                {
                 
                    if(Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_DogBuild == false)
                    {
                        GameObject DogText = Instantiate(m_prefabText) as GameObject;
                        t_Dog = DogText.GetComponent<Text>();
                        DogText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_DogBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SheepBuild == 1)
                {

                    if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_SheepBuild == false)
                    {
                        GameObject SheepText = Instantiate(m_prefabText) as GameObject;
                        t_Sheep = SheepText.GetComponent<Text>();
                        SheepText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_SheepBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_Jelly == true)
                {
                    JellyFactory.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[0].Name + " : " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.JellyPlus;
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_BearBuild == true)
                {
                    bearText.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[1].Name + ": " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.BearBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[1].Amount * Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.BearBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_ELBuild == true)
                {
                    ELText.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[2].Name + ": " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ELBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[2].Amount * Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.ELBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_RabbitBuild == true)
                {
                    RabbitText.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[3].Name + ": " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.RabbitBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[3].Amount * Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.RabbitBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_GunBuild == true)
                {
                    t_Gun.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[4].Name + ": " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[4].Amount * Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.GunBuild + "EA";
                }
                if(Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_DogBuild == true)
                {
                    t_Dog.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[5].Name + ": " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.DogBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[5].Amount * Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.DogBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.b_SheepBuild == true)
                {
                    t_Sheep.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[6].Name + ": " + Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SheepBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[6].Amount * Gamemanager1.GetInstance().m_cBlueTeam.cPlayer.SheepBuild + "EA";
                }
                yield return null;
            }


            if(NetWorkConnect.ins.isRedTeam)
            {
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.BearBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_BearBuild == false)
                    {
                        GameObject bText = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        bearText = bText.GetComponent<Text>();
                        bText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_BearBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ELBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_ELBuild == false)
                    {
                        GameObject EText = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        ELText = EText.GetComponent<Text>();
                        ELText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_ELBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.RabbitBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_RabbitBuild == false)
                    {
                        GameObject Rabittext = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        RabbitText = Rabittext.GetComponent<Text>();
                        Rabittext.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_RabbitBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild == 1)
                {
                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_GunBuild == false)
                    {
                        GameObject GunText = Instantiate(m_prefabText) as GameObject;
                        //  fsituation BearText = bText.GetComponent<fsituation>();
                        t_Gun = GunText.GetComponent<Text>();
                        GunText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_GunBuild = true;
                    }
                }
                if(Gamemanager1.GetInstance().m_cRedTeam.cPlayer.DogBuild == 1)
                {
                    
                    if(Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_DogBuild == false)
                    {
                        GameObject DogText = Instantiate(m_prefabText) as GameObject;
                        t_Dog = DogText.GetComponent<Text>();
                        DogText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_DogBuild = true;
                    }
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SheepBuild == 1)
                {

                    if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_SheepBuild == false)
                    {
                        GameObject SheepText = Instantiate(m_prefabText) as GameObject;
                        t_Sheep = SheepText.GetComponent<Text>();
                        SheepText.transform.parent = Creat.transform;
                        Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_SheepBuild = true;
                    }
                }

                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_Jelly == true)
                {
                    JellyFactory.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[0].Name + " : " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.JellyPlus;
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_BearBuild == true)
                {
                    bearText.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[1].Name + ": " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.BearBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[1].Amount * Gamemanager1.GetInstance().m_cRedTeam.cPlayer.BearBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_ELBuild == true)
                {
                    ELText.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[2].Name + ": " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ELBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[2].Amount * Gamemanager1.GetInstance().m_cRedTeam.cPlayer.ELBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_RabbitBuild == true)
                {
                    RabbitText.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[3].Name + ": " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.RabbitBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[3].Amount * Gamemanager1.GetInstance().m_cRedTeam.cPlayer.RabbitBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_GunBuild == true)
                {
                    t_Gun.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[4].Name + ": " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[4].Amount * Gamemanager1.GetInstance().m_cRedTeam.cPlayer.GunBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_DogBuild == true)
                {
                    t_Dog.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[5].Name + ": " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.DogBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[5].Amount * Gamemanager1.GetInstance().m_cRedTeam.cPlayer.DogBuild + "EA";
                }
                if (Gamemanager1.GetInstance().m_cRedTeam.cPlayer.b_SheepBuild == true)
                {
                    t_Sheep.text = Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[6].Name + ": " + Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SheepBuild + "B" + "/" + Gamemanager1.GetInstance().m_cBulildManager.GetBuildlist()[6].Amount * Gamemanager1.GetInstance().m_cRedTeam.cPlayer.SheepBuild + "EA";
                }
                yield return null;
            }
        }
    }

    [PunRPC]
    void RedMsg(Text text)
    {
        t_Notice2.SetActive(true);
        t_Notice2.GetComponent<Text>().text = text.text;
        StartCoroutine("Wait");
    }
    [PunRPC]
    void BlueMsg(Text text)
    {
        t_Notice2.SetActive(true);
        t_Notice2.GetComponent<Text>().text = text.text;
        StartCoroutine("Wait");
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        t_Notice2.SetActive(false);
    }
}
