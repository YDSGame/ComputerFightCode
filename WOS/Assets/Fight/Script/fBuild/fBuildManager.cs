using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fBuildManager : MonoBehaviour {

    List<fBuild> m_cBuildList = new List<fBuild>();
    List<fspecial> m_cSpecials = new List<fspecial>();
    public fBuild m_cBuild;

    private void Awake()
    {
        //건물리스트
        m_cBuildList.Add(new fBuild("젤리공장", "젤리가 더 빨리들어옴", fBuild.eBuildName.JELLY, 500));
        m_cBuildList.Add(new fBuild("베어의땅", "장난감 곰 2마리소환", fBuild.eBuildName.BEAR, 300, 2));
        m_cBuildList.Add(new fBuild("코끼리뿔", "코끼리 1  마리소환", fBuild.eBuildName.EL, 500, 1));
        m_cBuildList.Add(new fBuild("토끼의숲", "야생토끼 5 마리소환", fBuild.eBuildName.RABBIT, 150, 5));
        m_cBuildList.Add(new fBuild("장난감총", "장난감 총 1마리소환", fBuild.eBuildName.GUN, 300, 1));
        m_cBuildList.Add(new fBuild("들개집", "사냥개  2마리소환", fBuild.eBuildName.DOG, 220, 2));
        m_cBuildList.Add(new fBuild("목장", "양 3마리소환", fBuild.eBuildName.SHEEP, 420, 3));
        m_cBuildList.Add(new fBuild("우주선", "미래소녀 2마리소환", fBuild.eBuildName.SPACE, 500, 2));
        m_cBuildList.Add(new fBuild("서커스", "광대(영웅) 필드에서 1마리소환", fBuild.eBuildName.CLOWN, 700, 1));
        //필살기 리스트
        m_cSpecials.Add(new fspecial("신의축복", "아군유닛체력회복", fspecial.eBuildName.HEAING, 4500));
        m_cSpecials.Add(new fspecial("소멸", "적기지에있는 적을소멸", fspecial.eBuildName.DESTORY, 5500));
        m_cSpecials.Add(new fspecial("권능의손", "적기지에있는 적을 마인드컨트롤", fspecial.eBuildName.MINDCONTROLL, 6000));

    }
    public fBuild GetBuild(fBuild.eBuildName eBuildName)  // Get빌드 이넘으로 보낼때
    {
        return m_cBuildList[(int)eBuildName];
    }
    public fBuild GetBuild(int idx) // Get빌드 리스트를 인덱스로 보낼때
    {
        return m_cBuildList[idx];
    }
    public int BuildSize() // 건물 리스트 사이즈 크기 
    {
        return m_cBuildList.Count;
    }
    public List<fBuild> GetBuildlist() // Get건물 리스트 
    {
        return m_cBuildList;
    }
    //필살기 
    public fspecial GetFspecial(fspecial.eBuildName eSpecialName)
    {
        return m_cSpecials[(int)eSpecialName];
    }
    public fspecial GetFspecial(int idx)
    {
        return m_cSpecials[idx];
    }
    public int SpecialSize()
    {
        return m_cSpecials.Count;
    }
    public List<fspecial> GetFspecialsList()
    {
        return m_cSpecials;
    }

}
