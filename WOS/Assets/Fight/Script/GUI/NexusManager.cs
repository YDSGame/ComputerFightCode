using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NexusManager : MonoBehaviour {

    PhotonView pv;
    public GameObject heathsbar;
    public Transform viewTarget;
    public Image healthSlider; // 테스트용
    public float x, y, z;
    public Transform healthUI;
    
    UnitState NexusState;
    Transform cam;
    float curHp;
    private void Awake()
    {
        NexusState = GetComponent<UnitState>();
        pv = GetComponent<PhotonView>();
    }
    // Use this for initialization
    void Start () {
        
        foreach (Canvas c in FindObjectsOfType<Canvas>()) 
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                healthUI = Instantiate(heathsbar, c.transform).transform;
                healthSlider = healthUI.GetChild(0).GetComponent<Image>();
                break;
            }
        }
        cam = Camera.main.transform;
        healthUI.gameObject.SetActive(true);
        healthUI.position = viewTarget.position;
        healthUI.localScale = new Vector3(x, y, z);
        healthUI.transform.forward = -cam.forward; // 체력바 -앞에
    }
	
	// Update is called once per frame
	void Update () {
       
        healthSlider.fillAmount = NexusState.pHealth / NexusState.pMaxHealth;
        if (!pv.isMine)
        {
            NexusState.pHealth = curHp;
        }
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(NexusState.pHealth);           
        }
        else
        {
            curHp = (float)stream.ReceiveNext();
        }
    }
}
