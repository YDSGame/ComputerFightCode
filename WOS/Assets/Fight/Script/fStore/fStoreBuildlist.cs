using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fStoreBuildlist : MonoBehaviour {

    List<GameObject> buttonlist = new List<GameObject>(); // 구매버튼 확인용
    List<GameObject> Speciallist = new List<GameObject>(); // 필살기
    //버튼생성용
    public GameObject m_prefabButton; // 프리팹버튼
    public GameObject m_SprefabButton; // 프리팹버튼
    public GameObject m_Buildlist; // 빌드리스트 
    public GameObject m_Speciallist; // 필살기 리스트
    private fBuild cBuild;
    private fspecial cSpecial;
    private fBuildManager m_cBuild;
 //   private fBuildManager m_cSpecial;
    void Start()
    {
        m_cBuild = Gamemanager1.GetInstance().m_cBulildManager;
       // m_cSpecial = Gamemanager1.GetInstance().m_cBulildManager;
        //Debug.Log("건물수 : " + m_cBuild.BuildSize());
        for (int i = 0; i < m_cBuild.BuildSize(); i++)
        {
           
            GameObject button = Instantiate(m_prefabButton) as GameObject;
            fStoreButton sbtn = button.GetComponent<fStoreButton>(); //버튼의 겟 컴포넌트 필요함 꼭 중요 없으면 생성도안됨
            button.transform.parent = m_Buildlist.transform;
            sbtn.SetText(m_cBuild.GetBuildlist()[i]); 
            cBuild = button.GetComponent<fBuild>();
            cBuild.BuildName = m_cBuild.GetBuildlist()[i].BuildName;
            buttonlist.Add(button);
        } 
        for(int i=0; i<m_cBuild.SpecialSize(); i++)
        {
            //Debug.Log("필살기개수"+m_cBuild.SpecialSize());
            GameObject button = Instantiate(m_SprefabButton) as GameObject;
            fStoreButton sbtn = button.GetComponent<fStoreButton>(); //버튼의 겟 컴포넌트 필요함 꼭 중요 없으면 생성도안됨
            button.transform.parent = m_Buildlist.transform;
            sbtn.SetText(m_cBuild.GetFspecialsList()[i]);
            cSpecial = button.GetComponent<fspecial>();
            cSpecial.BuildName = m_cBuild.GetFspecialsList()[i].BuildName;
            Speciallist.Add(button);
        }

        //Debug.Log("생성된 : " + buttonlist.Count);
    }
   
    public List<GameObject> GetBtnlist()
    {
        return buttonlist;
    }
    public List<GameObject> GetSpeciallist()
    {
        return Speciallist;
    }
}
