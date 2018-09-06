using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fOnMouseUI : MonoBehaviour {
    public GameObject BuildInven;
    public Text c_Text;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))

        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                BuildInven.SetActive(false);
                if (this.CompareTag("State"))
                {
                    this.gameObject.GetComponent<Image>().enabled = true;
                    this.gameObject.GetComponentInChildren<Text>().enabled = true;
                }
                if (this.CompareTag("Build"))
                {
                    c_Text.text = "구매";
                }
            }

        }

       // UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
      
    }
    public void Click()
    {
        BuildInven.SetActive(true);
        if(this.CompareTag("State"))
        {
            this.gameObject.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponentInChildren<Text>().enabled = false;
        }
    }
}
