using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour {
    //버튼클릭용

    public Build m_cBuild;
    public BuildManager m_cBuildManger;
    public StoreBuildlist cBuildlist;
   // public StoreBuy cStoreBuy;
    public Image m_cImage;
    public Text m_cText;
    public Text m_cJellyText;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Set(Build.eBuildName eBuild)
    {
        Build cBuild = Gamemanager.GetInstance().cBuildManager.GetBuild(eBuild);

        if(m_cImage.sprite)
        {
            Destroy(m_cImage.sprite);
        }
        Sprite sprite = Resources.Load<Sprite>("Tex/" + cBuild.Image);

        m_cText.text = cBuild.Comment;

    }
    public void Click()
    {
        m_cBuildManger = Gamemanager.GetInstance().cBuildManager.GetComponent<BuildManager>();
        m_cText = Gamemanager.GetInstance().cStoreMnager.cStorePanel.m_cText;
        m_cJellyText = Gamemanager.GetInstance().cStoreMnager.cStorePanel.m_cJellyText;
        switch (m_cBuild.BuildName)
        {
            case Build.eBuildName.BEAR:
                {
                   
                   
                    Debug.Log("베어클릭");

                    m_cText.text = m_cBuildManger.GetBuildlist()[0].Comment;
                    m_cJellyText.text = "젤리 :"+m_cBuildManger.GetBuildlist()[0].Jellyvaule+ "필요";
                    Gamemanager.GetInstance().cStoreMnager.cStoreBuy.m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStoreBuildlist.GetBtnlist()[0].GetComponent<Build>();
                   // Gamemanager.GetInstance().cStoreMnager.cStoreBuy.m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStoreBuildlist.m_prefabButton.GetComponent<Build>();
                    break;
                }
            case Build.eBuildName.GUN:
                {
                    Debug.Log("건클릭");
                    m_cText.text = m_cBuildManger.GetBuildlist()[1].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[1].Jellyvaule +"필요";
                    Gamemanager.GetInstance().cStoreMnager.cStoreBuy.m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStoreBuildlist.GetBtnlist()[1].GetComponent<Build>();
                    //Gamemanager.GetInstance().cStoreMnager.cStoreBuy.m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStoreBuildlist.m_prefabButton.GetComponent<Build>();
                    break;
                }
            case Build.eBuildName.JELLY:
                {
                    Debug.Log("젤리클릭");
                    m_cText.text = m_cBuildManger.GetBuildlist()[2].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[2].Jellyvaule + "필요";
                    Gamemanager.GetInstance().cStoreMnager.cStoreBuy.m_cBuild = Gamemanager.GetInstance().cStoreMnager.cStoreBuildlist.GetBtnlist()[2].GetComponent<Build>();
                }
                break;
        }
    }
}
