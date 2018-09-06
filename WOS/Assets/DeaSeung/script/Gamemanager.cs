using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour {
    static Gamemanager Instance = null;
    public Player cPlayer;
    public GUIManger cGUIManger;
    public StoreManager cStoreMnager;
    public BuildManager cBuildManager;
    public List<Node> Node = new List<Node>();
    public Unit unit;
    public Nexus Rednex;
    public Nexus Bluenex;
    public BlueTeam blueTeam;
    public RedTeam redTeam;
    // Use this for initialization
    private void Awake()
    {
        Instance = this;
    }
    public static Gamemanager GetInstance()
    {
        return Instance;
    }
    void Start () {
        StartCoroutine("JellyUp");
        
    }
	
	// Update is called once per frame
	void Update () {
        Text();
    
	}
    void Text()
    {
        cGUIManger.t_JellyText.text = "Jelly : " + cPlayer.Jelly +" / "+cPlayer.JellyPlus;
    }
    IEnumerator JellyUp()
    {
        while (true)
        {
            if (cPlayer.JellyPlus == 0)
            {
                cPlayer.Jelly += 1;
               
            }
            if (cPlayer.JellyPlus == 1)
            {
                
                cPlayer.Jelly += 2;

            }
            if (cPlayer.JellyPlus == 2)
            {

                cPlayer.Jelly += 3;

            }
            if (cPlayer.JellyPlus == 3)
            {

                cPlayer.Jelly += 4;

            }
            if (cPlayer.JellyPlus == 5)
            {

                cPlayer.Jelly += 6;

            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
