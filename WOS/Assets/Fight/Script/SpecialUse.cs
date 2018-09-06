using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialUse : MonoBehaviour {
    public GameObject Center;

    AIUnit AIUnit;
    private void Start()
    {

    }

    public void BlueHealing()
    {
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.blueBears.Count; i++)  //  블루베어 힐링
            {
                if (Gamemanager1.GetInstance().m_Nexus.blueBears[i].activeInHierarchy == true)
                {
                    Debug.Log("체력회복");
                    Gamemanager1.GetInstance().m_Nexus.blueBears[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.blueBears[i].GetComponent<UnitState>().pMaxHealth;
                }
            }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.blueBunnies.Count; i++)  // 블루토끼 힐
        {
            if (Gamemanager1.GetInstance().m_Nexus.blueBunnies[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.blueBunnies[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.blueBunnies[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.blueDogs.Count; i++) //블루강아지 힐
        {
            if (Gamemanager1.GetInstance().m_Nexus.blueDogs[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.blueDogs[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.blueDogs[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.blueElephants.Count; i++) //코끼리 힐
        {
            if (Gamemanager1.GetInstance().m_Nexus.blueElephants[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.blueElephants[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.blueElephants[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.blueSheeps.Count; i++) //블루 양 힐
        {
            if (Gamemanager1.GetInstance().m_Nexus.blueSheeps[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.blueSheeps[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.blueSheeps[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.blueGunners.Count; i++) //블루 거너 힐
        {
            if (Gamemanager1.GetInstance().m_Nexus.blueGunners[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.blueGunners[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.blueGunners[i].GetComponent<UnitState>().pMaxHealth;
            }
        }

    }
    public void RedHealing()
    {
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redBears.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redBears[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.redBears[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.redBears[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redBunnies.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redBunnies[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.redBunnies[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.redBunnies[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redDogs.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redDogs[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.redDogs[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.redDogs[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redElephants.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redElephants[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.redElephants[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.redElephants[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redSheeps.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redSheeps[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.redSheeps[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.redSheeps[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redGunners.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redGunners[i].activeInHierarchy == true)
            {
                Debug.Log("체력회복");
                Gamemanager1.GetInstance().m_Nexus.redGunners[i].GetComponent<UnitState>().pHealth = Gamemanager1.GetInstance().m_Nexus.redGunners[i].GetComponent<UnitState>().pMaxHealth;
            }
        }
    }

    public void BlueDestory()
    {

    }
    public void RedDestory()
    {
        for (int i = 0; i < Gamemanager1.GetInstance().m_Nexus.redBears.Count; i++)
        {
            if (Gamemanager1.GetInstance().m_Nexus.redBears[i].activeInHierarchy == true)
            {
             
                if(Gamemanager1.GetInstance().m_Nexus.redBears[i].GetComponent<AIUnit>().wayPoint == Gamemanager1.GetInstance().m_Nexus.redBears[i].GetComponent<AIUnit>().wayPoints[0])
                {
                    Debug.Log("레드베어 체력0");
                    Gamemanager1.GetInstance().m_Nexus.redBears[i].GetComponent<UnitState>().pHealth = 0;
                }
            }
        }
    }

}
