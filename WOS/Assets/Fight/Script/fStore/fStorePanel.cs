using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fStorePanel : MonoBehaviour
{
    //버튼클릭용

    public fBuild m_cBuild; // enum의 체크용
    public fspecial fspecial; // 스페셜기술 체크용
    public fBuildManager m_cBuildManger; // 건물리스트 가지고오는 용도 
    public Text m_cState;
    public Text m_cText;
    public Text m_cJellyText;
    public GameObject[] Units;
    public void Click()
    {
        m_cBuildManger = Gamemanager1.GetInstance().m_cBulildManager.GetComponent<fBuildManager>();
        m_cText = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.m_cText;
        m_cJellyText = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.m_cJellyText;
        m_cState = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.m_cState;
        Units = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.Units;
        switch (m_cBuild.BuildName)
        {
            case fBuild.eBuildName.JELLY:
                {
                    //Debug.Log("젤리클릭");
                    m_cState.text = "젤리건물을 건설하면 젤리 공급이 빨라집니다.";
                    m_cText.text = m_cBuildManger.GetBuildlist()[0].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[0].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[0].GetComponent<fBuild>();
                }
                break;
            case fBuild.eBuildName.BEAR:
                {


                    //Debug.Log("베어클릭");

                    m_cState.text = "이름 : 곰 \n체력 : " +
                   (int)Units[0].GetComponent<UnitState>().pHealth + "\n공격 :" +
                   (int)Units[0].GetComponent<UnitState>().pPower + "\n방어력 :" +
                   (int)Units[0].GetComponent<UnitState>().pdef + "\n공격속도 : 빠름\n특징 : 없음";
                    m_cText.text = m_cBuildManger.GetBuildlist()[1].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[1].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[1].GetComponent<fBuild>(); // 판넬에서부터 구매버튼이랑 연결
                    break;
                }
            case fBuild.eBuildName.EL:
                {
                    //Debug.Log("코끼리클릭");
                    m_cState.text = "이름 : 코끼리 \n체력 : " +
                      (int)Units[1].GetComponent<UnitState>().pHealth + "\n공격 :" +
                      (int)Units[1].GetComponent<UnitState>().pPower + "\n방어력 :" +
                     (int)Units[1].GetComponent<UnitState>().pdef + "\n공격속도 : 매우느림\n특징 : 광역어택";
                    m_cText.text = m_cBuildManger.GetBuildlist()[2].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[2].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[2].GetComponent<fBuild>();
                    break;
                }
            case fBuild.eBuildName.RABBIT:
                {
                    //Debug.Log("토끼클릭");
                    m_cState.text = "이름 : 미친토끼 \n체력 : " +
                      (int)Units[2].GetComponent<UnitState>().pHealth + "\n공격 :" +
                      (int)Units[2].GetComponent<UnitState>().pPower + "\n방어력 :" +
                     (int)Units[2].GetComponent<UnitState>().pdef + "\n공격속도 : 매우빠름\n특징 : 없음";
                    m_cText.text = m_cBuildManger.GetBuildlist()[3].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[3].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[3].GetComponent<fBuild>();
                    break;
                }

            case fBuild.eBuildName.GUN:
                {
                    //Debug.Log("건클릭");
                    m_cState.text = "이름 : 사냥꾼 \n체력 : " +
                      (int)Units[3].GetComponent<UnitState>().pHealth + "\n공격 :" +
                      (int)Units[3].GetComponent<UnitState>().pPower + "\n방어력 :" +
                     (int)Units[3].GetComponent<UnitState>().pdef + "\n공격속도 : 보통\n특징 : 원거리";
                    m_cText.text = m_cBuildManger.GetBuildlist()[4].Comment; // 멘트 
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[4].Jellyvaule + "필요"; // 젤리 
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[4].GetComponent<fBuild>(); // 구매 
                    break;
                }
            case fBuild.eBuildName.DOG:
                {
                    //Debug.Log("개클릭");
                    m_cState.text = "이름 : 광견 \n체력 : " +
                      (int)Units[4].GetComponent<UnitState>().pHealth + "\n공격 :" +
                      (int)Units[4].GetComponent<UnitState>().pPower + "\n방어력 :" +
                     (int)Units[4].GetComponent<UnitState>().pdef + "\n공격속도 : 느림\n특징 : 출혈효과";
                    m_cText.text = m_cBuildManger.GetBuildlist()[5].Comment; // 멘트 
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[5].Jellyvaule + "필요"; // 젤리 
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[5].GetComponent<fBuild>(); // 구매 
                    break;
                }
            case fBuild.eBuildName.SHEEP:
                {
                    //Debug.Log("개클릭");
                    m_cState.text = "이름 : 양 \n체력 : " +
                      (int)Units[5].GetComponent<UnitState>().pHealth + "\n공격 :" +
                      (int)Units[5].GetComponent<UnitState>().pPower + "\n방어력 :" +
                     (int)Units[5].GetComponent<UnitState>().pdef + "\n공격속도 : 느림\n특징 : 크리티컬";
                    m_cText.text = m_cBuildManger.GetBuildlist()[6].Comment; // 멘트 
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[6].Jellyvaule + "필요"; // 젤리 
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[6].GetComponent<fBuild>(); // 구매 
                    break;
                }
            case fBuild.eBuildName.SPACE:
                {
                    //Debug.Log("개클릭");
                    //m_cState.text = "이름 : 양 \n체력 : " +
                    //  (int)Units[5].GetComponent<UnitState>().pHealth + "\n공격 :" +
                    //  (int)Units[5].GetComponent<UnitState>().pPower + "\n방어력 :" +
                    // (int)Units[5].GetComponent<UnitState>().pdef + "\n공격속도 : 느림\n특징 : 크리티컬";
                    m_cText.text = m_cBuildManger.GetBuildlist()[7].Comment; // 멘트 
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[7].Jellyvaule + "필요"; // 젤리 
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[7].GetComponent<fBuild>(); // 구매 
                    break;
                }
            case fBuild.eBuildName.CLOWN:
                {
                    //Debug.Log("개클릭");
                    //m_cState.text = "이름 : 양 \n체력 : " +
                    //  (int)Units[5].GetComponent<UnitState>().pHealth + "\n공격 :" +
                    //  (int)Units[5].GetComponent<UnitState>().pPower + "\n방어력 :" +
                    // (int)Units[5].GetComponent<UnitState>().pdef + "\n공격속도 : 느림\n특징 : 크리티컬";
                    m_cText.text = m_cBuildManger.GetBuildlist()[8].Comment; // 멘트 
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetBuildlist()[8].Jellyvaule + "필요"; // 젤리 
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.m_cBuild = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetBtnlist()[8].GetComponent<fBuild>(); // 구매 
                    break;
                }

        }
    }
    public void SpecialClick()
    {
        m_cBuildManger = Gamemanager1.GetInstance().m_cBulildManager.GetComponent<fBuildManager>();
        m_cText = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.m_cText;
        m_cJellyText = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.m_cJellyText;
        m_cState = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.m_cState;
        Units = Gamemanager1.GetInstance().m_cStoreManager.cStorePanel.Units;
        switch (fspecial.BuildName)
        {

            case fspecial.eBuildName.HEAING:
                {
                    //Debug.Log("젤리클릭");
                    m_cState.text = "<color=#ff0000>"+"신의 축복"+ "</color>";
                    m_cText.text = m_cBuildManger.GetFspecialsList()[0].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetFspecialsList()[0].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.fspecial = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetSpeciallist()[0].GetComponent<fspecial>();
                } break;
            case fspecial.eBuildName.DESTORY:
                {
                    //Debug.Log("젤리클릭");
                    m_cState.text = "<color=#ff0000>"+"소멸" + "</color>";
                    m_cText.text = m_cBuildManger.GetFspecialsList()[1].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetFspecialsList()[1].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.fspecial = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetSpeciallist()[1].GetComponent<fspecial>();
                }
                break;
            case fspecial.eBuildName.MINDCONTROLL:
                {
                    //Debug.Log("젤리클릭");
                    m_cState.text = "<color=#ff0000>" + "권능의손" + "</color>";
                    m_cText.text = m_cBuildManger.GetFspecialsList()[2].Comment;
                    m_cJellyText.text = "젤리 :" + m_cBuildManger.GetFspecialsList()[2].Jellyvaule + "필요";
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Special = true;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.b_Build = false;
                    Gamemanager1.GetInstance().m_cStoreManager.cStoreBuy.fspecial = Gamemanager1.GetInstance().m_cStoreManager.cStoreBuildlist.GetSpeciallist()[2].GetComponent<fspecial>();
                }
                break;
        }
    }
}