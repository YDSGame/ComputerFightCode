using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SecenManager : MonoBehaviour {
    public GameObject g_Menu; // 메뉴
    public GameObject g_Type; // 대결타입
    public GameObject g_One; // 1대1
    public GameObject g_two; // 2대2
    public GameObject g_Three; // 3대3
    public GameObject Next; // 다음
    public GameObject pre; // 이전
    public GameObject GameShow; // 게임 설명
    public List<GameObject> Images = new List<GameObject>();
    public AudioSource LobbyAudio;
    public int i;
    // public GameObject g_Chat; // 채팅방
    public void GameClose()
    {
        GameShow.SetActive(false);
        Images[i].SetActive(false);
        i = 0;
    }
    public void GameOpen()
    {
        GameShow.SetActive(true);
        Images[i].SetActive(true);

    }
    public void NextImages()
    {
        Images[i+1].SetActive(true);
        Images[i].SetActive(false);
        i++;
    }
    public void PreImages()
    {
        Images[i - 1].SetActive(true);
        Images[i].SetActive(false);
        i--;
    }
    public void GameGo()
    {
        g_Type.SetActive(true);
        g_Menu.SetActive(false);
      //  g_Chat.SetActive(false);
      //  SceneManager.LoadScene(0);
    }
    public void ChatGo()
    {        
        g_Menu.SetActive(false);
       // g_Chat.SetActive(true);
    }
    public void ChatExit()
    {
        g_Menu.SetActive(true);
       // g_Chat.SetActive(false);
    }
    public void clickExit()
    {
        g_Type.SetActive(false);
        g_Menu.SetActive(true);
       // g_Chat.SetActive(true);
    }
    public void OneVsOne()
    {
        g_Type.SetActive(false);
        g_One.SetActive(true);
    }
    public void OneVsOneExit()
    {
        g_Menu.SetActive(true);
        g_One.SetActive(false);
       // g_Chat.SetActive(true);
    }
    public void TwoVsTwo()
    {
        g_Type.SetActive(false);
        g_two.SetActive(true);
    }
    public void TwoVsTwoExit()
    {
        g_Type.SetActive(true);
        g_two.SetActive(false);
    }
    public void ThreeVSThree()
    {
        g_Type.SetActive(false);
        g_Three.SetActive(true);
    }
    public void ThreeVSThreeExit()
    {
        g_Type.SetActive(true);
        g_Three.SetActive(false);
    }
}
