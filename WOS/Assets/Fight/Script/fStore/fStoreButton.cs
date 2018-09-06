using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fStoreButton : MonoBehaviour {
    public fBuild m_cBuild;
    fspecial m_cSpecial;
    public Text m_cText;
    // Use this for initialization
    void Start()
    {
        m_cBuild = GetComponent<fBuild>();
    }


    public void SetText(fBuild build)
    {
        m_cBuild = build;
        m_cText.text = build.Name;
    }
    public void SetText(fspecial Special)
    {
        m_cSpecial = Special;
        m_cText.text = "<color=#ff0000>" + Special.Name + "</color>";
    }
}
