using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkSync : MonoBehaviour {
    PhotonView pv;
    Transform sendPosition;
    Transform healthVarPosition;
    AIUnit unitAi;
    UnitState unitSt;
    GameObject myActive;

    Quaternion curRot;
    Vector3 curPos;
    Vector3 healthvarPos;
    
    float curHP;

	// Use this for initialization
	void Start () {
        pv = GetComponent<PhotonView>();
        sendPosition = transform;
        unitAi = GetComponent<AIUnit>();
        unitSt = GetComponent<UnitState>();
        healthVarPosition = unitAi.healthUI;
        myActive = transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (!pv.isMine)
        {
            if (myActive.activeInHierarchy)
            {
                sendPosition.position = Vector3.Lerp(sendPosition.position, curPos, Time.deltaTime * 10f);
                sendPosition.rotation = Quaternion.Slerp(sendPosition.rotation, curRot, Time.deltaTime * 10f);
                healthVarPosition.position = Vector3.Lerp(healthVarPosition.position, healthvarPos, Time.deltaTime * 10f);
                unitSt.pHealth = curHP;            
            }
        }
	}
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //포지션
            stream.SendNext(sendPosition.position);
            //로테이션
            stream.SendNext(sendPosition.rotation);

            //HP ui
            stream.SendNext(unitSt.pHealth);
            if(healthVarPosition != null)
            stream.SendNext(healthVarPosition.position);
                                  
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();            
            curHP = (float)stream.ReceiveNext();
            healthvarPos = (Vector3)stream.ReceiveNext();
        }
    }
}
