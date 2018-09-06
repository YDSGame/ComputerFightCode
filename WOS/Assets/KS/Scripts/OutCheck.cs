using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OutCheck : MonoBehaviour {

    public void OnPhotonPlayerDisconnected(PhotonPlayer other) //다른 유저가 접속이 끊기면 불린다.
    {
        print(other.ID);
        string a = other.ToString();
        char[] chr = new char[] {(char)39};
        string[] str =  a.Split(chr);
        int i = int.Parse(str[1]);
        
        if (PhotonNetwork.isMasterClient)
        {
            if (other != null)                
            NetWorkConnect.ins.ReMoveList(i);
        }
    }
}
