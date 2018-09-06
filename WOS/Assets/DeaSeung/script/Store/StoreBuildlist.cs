using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreBuildlist : MonoBehaviour
{
    List<GameObject> buttonlist = new List<GameObject>(); // 구매버튼 확인용
    //버튼생성용
    public GameObject m_prefabButton; // 프리팹버튼
    public GameObject m_Buildlist; // 빌드리스트 
    private Build cBuild;
    public Text m_cText;
    private BuildManager m_cBuild;
    // private Build.eBuildName BuildName;

    // Use this for initialization
    void Start()
    {
        m_cBuild = Gamemanager.GetInstance().cBuildManager.GetComponent<BuildManager>();
        for (int i =0; i<m_cBuild.BuildSize(); i++)
        {
            GameObject button = Instantiate(m_prefabButton) as GameObject; // 위에서 버튼을 만들어서
            StoreBuntton sbtn = button.GetComponent<StoreBuntton>(); // 얘는 // 그 버튼의 StoreButton 을 가져온건데 // 여기서는 button 의 컴포넌트를 가져왔고
            button.transform.parent = m_Buildlist.transform;
            sbtn.SetText(m_cBuild.GetBuild(i));
            cBuild = button.GetComponent<Build>();
            cBuild.BuildName = m_cBuild.GetBuildlist()[i].BuildName;
            buttonlist.Add(button);
        }
        Debug.Log("건물수 : " + m_cBuild.BuildSize());
        Debug.Log("생성된 : " + buttonlist.Count);
    }

    public List<GameObject> GetBtnlist()
    {
        return buttonlist;
    }


}










































//public void Initialize(StorePanel storePanel)
//{
//    m_cBuild = Gamemanager.GetInstance().cBuildManager.GetComponent<BuildManager>();
//    //  cBuildInven = GetComponent<BuildInventory>();
//    // m_cPanel = GetComponent<StorePanel>();
//    Debug.Log("빌드건물 : " + m_cBuild.BuildSize());

//    for (int i = 0; i < m_cBuild.BuildSize(); i++)
//    {
//        Debug.Log(i);
//        Debug.Log("버튼생성 : " + m_cBuild.BuildSize());
//        GameObject button = Instantiate(m_prefabButton) as GameObject;
//        StoreBuntton sBtn = button.GetComponent<StoreBuntton>();
//        StorePanel sPanel = button.GetComponent<StorePanel>();
//        button.transform.parent = m_Buildlist.transform;
//        sBtn.SetText(m_cBuild.GetBuild(i));
//        cBuild = button.GetComponent<Build>();
//        //            Debug.Log(m_cBuild.GetBuild(i).BuildName + ", " + m_cBuild.BuildSize());
//        cBuild.BuildName = m_cBuild.GetBuild(i).BuildName;
//        //            Debug.Log("-");

//        button.transform.SetParent(m_Buildlist.transform);
//       // cBuildInven.GetComponent<StorePanel>().m_cText = m_cComent;
//        //m_cText = storePanel.m_cText;
//        //button.transform.localScale = new Vector3(1, 1, 1);
//        //button.transform.localPosition = new Vector3(0, 0, 0);
//         Button cBtn = sBtn.GetComponent<Button>();
//        //cBtn.onClick.AddListener(() => cBuildInven.SetPanel(m_cBuild.GetBuild(i).BuildName));
//        cBtn.onClick.AddListener(() => cBuildInven.SetPanel(cBuild.BuildName));


//    }
//}
//public void addBuild(Build.eBuildName eBuildName)
//{
//    GameObject objItemButton = Instantiate(m_prefabButton) as GameObject;
//    StoreBuntton cStoreBtn = objItemButton.GetComponent<StoreBuntton>();
//    Build cBuild = Gamemanager.GetInstance().cBuildManager.GetBuild(eBuildName);
//    cStoreBtn.SetText();
//    objItemButton.transform.SetParent(m_Buildlist.transform);
//    objItemButton.transform.localScale = new Vector3(1, 1, 1);
//    objItemButton.transform.localPosition = new Vector3(0, 0, 0);
//    Button cBtn = cStoreBtn.GetComponent<Button>();
//}
