using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fspecial : MonoBehaviour {

    public Text BuildText;
    public int JellyPrice; // 젤리 가격
    string m_strName; //  빌드이름
    string m_strComment; // 빌드 내용
    string m_strImage;  // 빌드 사진
    public int m_cAmount; // 소환양
    public enum eBuildName { NONE = -1,HEAING,DESTORY,MINDCONTROLL };  // 건물 enum
    public eBuildName BuildName; // 건물 enum을 멤버변수로 

    public string Name
    {
        get { return m_strName; }
        set { m_strName = value; }
    }
    public string Image
    {
        get { return m_strImage; }
        //  set { m_strImage = value; }
    }
    public string Comment
    {
        get { return m_strComment; }
        set { m_strComment = value; }
    }
    public int Jellyvaule
    {
        get { return JellyPrice; }
        set { JellyPrice = value; }
    }
    public eBuildName BuildNamy
    {
        get { return BuildName; }
        set { BuildName = value; }

    }
    public int Amount
    {
        get { return m_cAmount; }
        set { m_cAmount = value; }
    }

    public fspecial(string name, string comment, eBuildName buildName, int Jelly, int aMount)
    {
        SetItem(name, comment, buildName, Jelly, aMount);
    }
    public fspecial(string name, string comment, eBuildName buildName, int Jelly)
    {
        SetUse(name, comment, buildName, Jelly);
    }
    public void SetItem(string name, string comment, eBuildName buildName, int Jelly, int m_Amount)
    {
        Name = name;
        Comment = comment;
        BuildName = buildName;
        Jellyvaule = Jelly;
        Amount = m_Amount;
    }
    public void SetUse(string name, string comment, eBuildName buildName, int Jelly)
    {
        Name = name;
        Comment = comment;
        BuildName = buildName;
        Jellyvaule = Jelly;

    }
}
