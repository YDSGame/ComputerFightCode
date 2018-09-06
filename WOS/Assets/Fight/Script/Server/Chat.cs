using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using Photon;
public class Chat : Photon.MonoBehaviour {

    
    public List<string> ChatList = new List<string>();
    public Text ChatBox;
    public InputField input;
    public ScrollRect scroll;
    int MessageLimit = 20;
    int idx = 0;
    public void SendButton()
    {
        
        //char[] chr = new char[] { (char)39 };
        //string s_idx = idx.
        string currentMsg = input.text;
        Send(PhotonTargets.All, currentMsg);  //버튼 클릭하면 내용을 가져와 전송
        input.text = string.Empty;
        input.Select();
     //   ChatBox.text = ToStringMessages();
        scroll.velocity = new Vector2(0, 110); // 채팅창 갱신
    
    }
    public void ChatClear() // 겜방에 들어가고 나갈때 채팅 초기화 
    {
        ChatList.Clear();
        ChatBox.text = "";
    }
    public void OnChatUpdate()
    {

    }
    void Send(PhotonTargets _target, string _msg)
    {
        photonView.RPC("SendMsg", _target, _msg);
    }
    [PunRPC]
    void SendMsg(string _msg,PhotonMessageInfo _Info)
    {
         AddChatBox(string.Format("{0}:{1}", _Info.sender, _msg));
        //AddChatBox("[" + _Info.sender + "]" + _msg);
    }
    public string ToStringMessages()
    {
        StringBuilder text = new StringBuilder();
        for (int i =0; i<this.ChatList.Count; i++)
        {
            text.AppendLine(string.Format("{0}", this.ChatList[i]));

        }
        return text.ToString();
    }

    void AddChatBox(string _msg)
    {
        string chat = ChatBox.text;
        chat += string.Format("\n{0}", _msg);
        ChatBox.text = chat;
        //  this.ChatList.Add(_msg);
        //  this.TrunCateMessage();
    }

    public void TrunCateMessage()
    {
        if(this.MessageLimit <=0 || this.ChatList.Count <= this.MessageLimit)
        {
            return;
        }
        int excessCount = this.ChatList.Count - this.MessageLimit;
        this.ChatList.RemoveRange(0, excessCount);
       
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SendButton();
        }

    }
}
