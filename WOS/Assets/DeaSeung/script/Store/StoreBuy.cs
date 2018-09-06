using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBuy : MonoBehaviour {
    public GameObject[] prefarb_Build;
    public Build m_cBuild;
    public BuildManager m_cBuildM;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuyClick()
    {
  
            m_cBuildM = Gamemanager.GetInstance().cBuildManager.GetComponent<BuildManager>();
           // m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStoreBuildlist.m_prefabButton.GetComponent<Build>(); // 이렇게사용하니 한개의 구매밖에 못함
        //   m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStorePanel.cStoreBuy.m_cBuild; << 필요가없음 판넬에서 선택했을때부터 설정해주면되니까
            Player cPlayer = Gamemanager.GetInstance().cPlayer.GetComponent<Player>();
            switch (m_cBuild.BuildName)
            {
            case Build.eBuildName.BEAR:
                {
                    for (int i = 0; i < Gamemanager.GetInstance().Node.Count; i++)
                    {
                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[0].JellyPrice)
                        {
                            Debug.Log("베어의땅 구매완료");

                            Gamemanager.GetInstance().Node[i].gBuildCreat = prefarb_Build[0];
                            Gamemanager.GetInstance().Node[i].BuildCreat();
                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[0].JellyPrice;
                            print("i" + i);


                        }
                        else
                        {
                            Debug.Log("베어의땅 젤리부족");

                        }
                        break;
                    }
                }
                break;
            case Build.eBuildName.GUN:
                {
                    for (int i = 0; i < Gamemanager.GetInstance().Node.Count; i++)
                    {
                        if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[1].JellyPrice)
                        {
                            Debug.Log("장난감 총 구매완료");
                            Gamemanager.GetInstance().Node[i].gBuildCreat = prefarb_Build[1];
                            Gamemanager.GetInstance().Node[i].BuildCreat();
                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[1].JellyPrice;
                        }
                        else
                        {
                            Debug.Log("장난감 총 젤리부족");

                        }
                        break;
                    }
                }
                break;
            case Build.eBuildName.JELLY:
                {

                    if (cPlayer.Jelly >= m_cBuildM.GetBuildlist()[2].JellyPrice)
                    {
                        if (cPlayer.JellyPlus < 5)
                        {
                            Debug.Log("젤리구매완료");
                            cPlayer.Jelly = cPlayer.Jelly - m_cBuildM.GetBuildlist()[2].JellyPrice;
                            cPlayer.JellyPlus += 1;

                        }
                        if(cPlayer.JellyPlus == 1)
                        {
                            cPlayer.JellyTime = true;
                            cPlayer.JellyIns = 15;
                        }
                        if (cPlayer.JellyPlus == 2)
                        {
                            cPlayer.JellyTime = true;
                            Gamemanager.GetInstance().cPlayer.JellyIns += 20;
                        }
                        else
                        {
                            Debug.Log("더이상 젤리 사용 불가능");
                        }
                        if (cPlayer.JellyPlus == 3)
                        {
                            Debug.Log("필살기 사용가능");
                        }
                    }
                    else
                    {
                        Debug.Log("장난감 총 젤리부족");

                    }

                }
                 break;


        }
    
    }
}
