using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

    public static MenuHandler instance = null;

    [SerializeField]
	private GameManager gameManager;
	private MenuTankInfo[] menuTankInfos = new MenuTankInfo[4];
	[SerializeField]
	private int totalPlayers;
	[SerializeField]
	private int numRounds;

    [SerializeField]
    public string[] names;
    [SerializeField]
    public Color32[] colours;

    public void Start()
	{
		menuTankInfos[0] = GameObject.Find("P1").GetComponent<MenuTankInfo>();
		menuTankInfos[1] = GameObject.Find("P2").GetComponent<MenuTankInfo>();
		menuTankInfos[2] = GameObject.Find("P3").GetComponent<MenuTankInfo>();
		menuTankInfos[3] = GameObject.Find("P4").GetComponent<MenuTankInfo>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager> ();
    }

    public void Update(){
        totalPlayers = getNumPlayers();
        numRounds = NumberRounds.getRounds();

        gameManager.numPlayers = totalPlayers;
        gameManager.totalRounds = numRounds;
		if (GameObject.Find ("Terrain Type") != null) {
			gameManager.map = GameObject.Find("Terrain Type").GetComponent<TerrainType>().getTerrainType();
		}
	}

    // Exit button interaction
    public void exit()
    {
        Application.Quit();
    }

	public int getNumPlayers(){
		int totalPlayers = 0;
		for (int i = 0; i < menuTankInfos.Length; i++) {
			if (menuTankInfos [i].getTankStatus ().Equals ("PLAYER")) {
				totalPlayers++;
			}
		}
		return totalPlayers;
	}

	//public void initTanks(string [] name, bool [] isbot, Color32[] colours)
	public void gameSetup(){
        names = new string[totalPlayers];
        bool[] isbot = { false, false, false, false };
        colours = new Color32[totalPlayers];

		int index = 0;
		for (int i = 0; i < menuTankInfos.Length; i++)
        {
			if (menuTankInfos [i].getTankStatus ().Equals ("PLAYER")) {
				names [index] = menuTankInfos [i].getPlayerName ();
				colours [index] = menuTankInfos [i].getTankColour ();
				index++;
			}
        }
	}
}