using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nettest : MonoBehaviour {

    PhotonView pv;    
    public GameObject rotation;
    Transform sendPosition;
    Transform sendPosition2;
    UnitAIUp unitAi;
    UnitState unitSt;
    
    Quaternion curRot; //= Quaternion.identity;
    Vector3 curPos;// = Vector3.zero;
    GameObject gunnerEffect;
    Image gunnerShoot;
    bool hpBar;
    bool isDead;
    int unitDie;
    bool isGunnerEffect;
    float curHP;
   
    // Use this for initialization
    void Start () {   
        pv = GetComponent<PhotonView>();
        sendPosition = GetComponent<Transform>(); //참조해서 계속 가져온다.
        sendPosition2 = transform.GetChild(0).GetComponent<Transform>();
        unitAi = GetComponent<UnitAIUp>();
        unitSt = GetComponent<UnitState>();
        
        //effect = unitAi.flash.activeSelf;
        //curHP = unitSt.pHealth;        
          
        // transform.position= 값만 복사.
	}
	
	// Update is called once per frame
	void Update () {

        if (!pv.isMine)
        {   
            //포지션 동기화
            sendPosition.position = Vector3.Lerp(sendPosition.position, curPos, Time.deltaTime * 3f);
            sendPosition2.rotation = Quaternion.Slerp(sendPosition2.rotation, curRot, Time.deltaTime * 1f);
            //unitAi.healthUI.gameObject.SetActive(hpBar);            
            unitSt.pHealth = curHP;
            transform.GetChild(0).gameObject.SetActive(isDead);
        }

	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //포지션
            stream.SendNext(sendPosition.position);
            stream.SendNext(sendPosition2.rotation);


            //HP ui
            stream.SendNext(unitSt.pHealth);

            //죽으면 렌더링 끔/살았으면 렌더링 킴
            stream.SendNext(transform.GetChild(0).gameObject.activeInHierarchy);
            //stream.SendNext(unitAi.healthUI.gameObject.activeSelf);
            //이펙트
            //stream.SendNext(unitAi.flash.activeInHierarchy);
           
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            //hpBar = (bool)stream.ReceiveNext();
            curHP = (float)stream.ReceiveNext();
            isDead = (bool)stream.ReceiveNext();
            //isGunnerEffect = (bool)stream.ReceiveNext();
        }
    }
}
