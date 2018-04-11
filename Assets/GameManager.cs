using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
    public GameObject pausedMenuPrefab;
    private GameObject pausedMenuPrefabClone;
    public GameObject introTutPrefab;
    private GameObject introTutPrefabClone;

    // For menu
	public int numPlayers;
	public int totalRounds;
	public string map;
	public bool mapInstantiated = false;
	public bool launchedFromMenu = false;
	public int currentRound;
	public string[] names;
	public Color32[] colours;
	public GameObject[] tanks;
    public tankInfo[] tankInfos;
	public int playerTurn;
    private AudioSource source;
    public float windSpeed = 0;				// A negative value indicates left, positive right
	public int tanksLeft = 0;		// Variable to indicate number of tanks on the map
	public bool tanksInit = false;		// Variable to indicate whether the tanks have been initialized

	[SerializeField]
	public GameObject tankPrefab;

	public bool movementEnabled = true;		// Variable to disable movement after shooting
    public bool round1 = true;
    private bool turnEnded = false;

    public void Start()
    {
        tankInfos = new tankInfo[4];
    }

    void Awake () {
		if (instance == null) 
			instance = this;
		else if (instance != null)
			Destroy(gameObject);
		DontDestroyOnLoad(transform.gameObject);
        currentRound = 1;
        Application.targetFrameRate = 60;
	}

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (pausedMenuPrefabClone == null)
            {
                pausedMenuPrefabClone = Instantiate(pausedMenuPrefab, GameObject.Find("Canvas").transform);
            }
            else
            {
                Destroy(pausedMenuPrefabClone);
            }
        }
    }

	// Method to exit game from the menu
	public void exitGame(){
		Application.Quit ();
	}

	// Method to setup game from the main menu
	public void gameSetup(){
		launchedFromMenu = true;
		MenuTankInfo[] menuTankInfos = new MenuTankInfo[4];
		menuTankInfos[0] = GameObject.Find("P1").GetComponent<MenuTankInfo>();
		menuTankInfos[1] = GameObject.Find("P2").GetComponent<MenuTankInfo>();
		menuTankInfos[2] = GameObject.Find("P3").GetComponent<MenuTankInfo>();
		menuTankInfos[3] = GameObject.Find("P4").GetComponent<MenuTankInfo>();

		names = new string[numPlayers];
		bool[] isbot = { false, false, false, false };
		colours = new Color32[numPlayers];

		int index = 0;
		for (int i = 0; i < menuTankInfos.Length; i++)
		{
			if (menuTankInfos [i].getTankStatus ().Equals ("PLAYER")) {
				names [index] = menuTankInfos [i].getPlayerName ();
				colours [index] = menuTankInfos [i].getTankColour ();
				index++;
			}
		}

        SceneManager.UnloadSceneAsync("Main Menu");
	}

	// Method to initialize the array of tanks, to be used when the player clicks Yes on the setup menu (so only once per game)
	public void initTanks(string [] name, bool [] isbot, Color32 [] colours){
		tanks = new GameObject[numPlayers];
        tankInfos = new tankInfo[4];
        for (int i = 0; i < numPlayers; i++) {
            // Have to initialize this or it doens't work
            tanks [i] = Instantiate (tankPrefab,new Vector3(0,0, 0),Quaternion.identity,this.transform);
			tanks [i].GetComponent<Player> ().enabled = false;
            tanks [i].AddComponent<tankInfo>();
            tanks[i].GetComponent<SpriteRenderer>().color = colours[i];
            tanks[i].transform.Find("Turret").GetComponent<SpriteRenderer>().color = colours[i];
            tankInfos [i] = tanks [i].GetComponent<tankInfo> ();
			tankInfos [i].isBot = isbot[i];
			tankInfos [i].name = name[i];
			tankInfos [i].tankid = i;
			tankInfos [i].maxHealth = 100;
			tankInfos [i].score = 0;
			tankInfos [i].funds = 0;

			tankInfos [i].ammo = new int[9];
			tankInfos [i].ammo [0] = 99;

			for (int j = 0; j < 9; j++) {
				tankInfos [i].ammo [j] = 0;
			}

			// Ammo indexed at 0 represents repairs
			tankInfos [i].ammo[0] = 1;
			tankInfos [i].ammo[1] = 99;
			tankInfos [i].ammo[2] = 5;

			tankInfos [i].fuelLevel = 100;
			tankInfos[i].maxFuel = tankInfos [i].fuelLevel;
			tankInfos[i].fuel = tankInfos[i].maxFuel;
            
            //tankInfos [i].repairCount = 1;
			tankInfos [i].armorLevel = 0;

			tankInfos [i].colour = colours [i];
		}
		tanksInit = true;
	}

    public void DeleteTanks()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            Destroy(tanks[i]);
            Destroy(tankInfos[i]);
        }
    }

	public void playAgain(){
		/*string[] testNames = new string[4];
		bool[] bots = new bool[4];
		Color32[] testColours = new Color32[4];

		for (int i = 0; i < numPlayers; i++) {
			testNames [i] = tankInfos [i].name;
			bots [i] = tankInfos [i].isBot;
			testColours [i] = tankInfos [i].colour;
		}

		initTanks (testNames, bots, testColours);
        mapInstantiated = false;
        launchedFromMenu = false;
        tanksInit = false;*/
        for (int i = 0; i < numPlayers; i++)
        {
            tankInfos[i].fuelLevel = 100;
            tankInfos[i].maxFuel = tankInfos[i].fuelLevel;
            tankInfos[i].fuel = tankInfos[i].maxFuel;
            tankInfos[i].armorLevel = 0;

            tankInfos[i].score = 0;
            tankInfos[i].funds = 0;
            for (int j = 0; j < 9; j++)
            {
                if (j == 0)
                {
                    tankInfos[i].ammo[j] = 1;
                }
                else if (j == 1)
                {
                    tankInfos[i].ammo[j] = 99;
                }
                else if (j == 2)
                {
                    tankInfos[i].ammo[j] = 5;
                }
                else
                {
                    tankInfos[i].ammo[j] = 0;
                }
            }
        }
        currentRound = 1;
	}

	// Used for the beginning of every round
	public void initRound(){
		this.transform.SetParent (GameObject.Find("Canvas").transform);
		setTanksActive (true);
		setActiveMap ();
		for (int i = 0; i < numPlayers; i++) {
			GameObject terrain = GameObject.FindGameObjectWithTag("Terrain");
            Vector3 mapPosition = terrain.transform.position;
			tanks[i].transform.SetPositionAndRotation(new Vector3(mapPosition.x + (8-numPlayers) * i- (numPlayers % 2) -3 * numPlayers/2, mapPosition.y, 0), Quaternion.identity);
            
			tankInfos [i].health = tankInfos [i].maxHealth;

			tankInfos [i].activeWeapon = 1;

			tankInfos [i].pos = new Vector3 (i + mapPosition.x, mapPosition.y, 0);
			tankInfos [i].armAngle = new Vector3(0,0,0);
			tankInfos [i].health = tankInfos [i].maxHealth;

			tankInfos [i].maxFuel = tankInfos [i].fuelLevel;
			tankInfos [i].fuel = tankInfos [i].maxFuel;
		}
		tanksLeft = numPlayers;

		for (int i = 0; i < numPlayers; i++) {
			tanks [i].GetComponent<SpriteRenderer> ().material.color = new Color32 (255, 255, 255, 255);
		}

        // Updates ammo count bottom left
        tankInfos[playerTurn].setActiveWeapon(tankInfos[playerTurn].activeWeapon);
		windSpeed = (windSpeed * 2 + (Random.value * 200.0f - 100.0f)) / 3;
        if (currentRound == 1)
        {
            introTutPrefabClone = Instantiate(introTutPrefab, GameObject.Find("Canvas").transform);
        }
    }

	public void setActiveMap(){
		if (map.Equals ("DESERT") && !mapInstantiated) {
			GameObject.Find ("Middleground - Desert").SetActive (true);
			GameObject.Find ("Background - Desert").SetActive (true);
			GameObject.Find ("Middleground - Mountain").SetActive (false);
			GameObject.Find ("Background - Mountain").SetActive (false);
			mapInstantiated = true;

			GameObject.Find ("Player Name HUD").GetComponentInChildren<TMP_Text>().color = new Color32(0,0,0,255);
			GameObject.Find ("Power Value").GetComponentInChildren<TMP_Text>().color = new Color32(0,0,0,255);
			GameObject.Find ("Turret Angle Text").GetComponentInChildren<TMP_Text>().color = new Color32(0,0,0,255);
            GameObject.Find ("Power Text").GetComponent<TMP_Text>().color = new Color32(0, 0, 0, 255);
            foreach (TMP_Text tempText in GameObject.Find("Health").GetComponentsInChildren<TMP_Text>())
            {
                tempText.color = new Color32(0, 0, 0, 255);
            }
            GameObject.Find("Line").GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
		if (!mapInstantiated){
			GameObject.Find ("Middleground - Mountain").SetActive (true);
			GameObject.Find ("Background - Mountain").SetActive (true);
			GameObject.Find ("Middleground - Desert").SetActive (false);
			GameObject.Find ("Background - Desert").SetActive (false);
			mapInstantiated = true;

            GameObject.Find("Player Name HUD").GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("Power Value").GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("Turret Angle Text").GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("Power Text").GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
            foreach (TMP_Text tempText in GameObject.Find("Health").GetComponentsInChildren<TMP_Text>())
            {
                tempText.color = new Color32(255, 255, 255, 255);
            }
            GameObject.Find("Line").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
	}

	// Used for keeping the tankInfos array up to date with the tank information
	public void updateTank(){
		for (int i = 0; i < numPlayers; i++) {
			// Boolean variable representing if it is the current player's turn
			bool currentTurn = (playerTurn == tankInfos [i].tankid);
			//if (!tanks [i].activeSelf) {
			//	break;
			//}
			TextMeshProUGUI[] allTextMeshPros = tanks[i].GetComponentsInChildren<TextMeshProUGUI>();
			foreach (TextMeshProUGUI tempText in allTextMeshPros) {
				if (tempText.name == "Score") {
					tempText.text = tankInfos [i].score.ToString() + " pts";
				}
				if (tempText.name == "PlayerName") {
					tempText.text = tankInfos [i].name.ToString();
				}
			}

			if (map.Equals ("DESERT")) {
				foreach (TextMeshProUGUI tempText in allTextMeshPros) {
					tempText.color = new Color32 (0, 0, 0, 255);
				}
			}

			// Updates Location
			tankInfos [i].pos = tanks [i].transform.position;

			// Disables Player script if not player's turn or if movement is disabled, enables otherwise
			if (!currentTurn || !movementEnabled) {
				tanks [i].GetComponent<Player> ().enabled = false;
                tanks[i].GetComponentInChildren<CannonWinston>().enabled = false;
			} else {
				tanks [i].GetComponent<Player> ().enabled = true;
                tanks[i].GetComponentInChildren<CannonWinston>().enabled = true;
            }

			//Image health = tanks[i].GetComponent<Image>();
			//int percent1 = tankInfos [i].health / tankInfos [i].maxHealth;
			//health.GetComponent<Image> ().fillAmount = percent1;

			Transform[] allChildren = tanks [i].GetComponentsInChildren<Transform> ();
			foreach (Transform child in allChildren) {
				if (child.name == "Turret") {
					// Updates Turret Angle
					tankInfos [i].armAngle = child.transform.localEulerAngles;
					// Disables turret script if not player's turn,enables otherwise
					if(!currentTurn){
						child.GetComponent<Turret> ().enabled = false;
					} else {
						child.GetComponent<Turret> ().enabled = true;
					}
				}
				// Disables renderer of Triangle object if not player's turn
				if (child.name == "Triangle") {
					if (!currentTurn) {
						child.GetComponent<Image> ().enabled = false;
					} else {
						child.GetComponent<Image> ().enabled = true;
					}
				}
				if (child.name == "Mask") {
					float percent = (float)tankInfos [i].health / (float)tankInfos [i].maxHealth;
					child.GetComponent<Image> ().fillAmount = percent;
				}
			}
		}
	}

	public void destroyTank(int i){
		if (!tankInfos[i].destroyingTank) {
			// "Locks" the act of destroying the tank
			tankInfos[i].destroyingTank = true;
			Animator myAnimator = tanks [i].transform.Find ("explosion").GetComponent<Animator> ();
			myAnimator.SetTrigger ("explode");
            source = GameObject.Find("explosion").GetComponent<AudioSource>();
            source.PlayOneShot(source.GetComponent<AudioClip>());
			StartCoroutine (setInvisible (i));
			tankInfos [playerTurn].score += 1000;
            tankInfos[playerTurn].funds += 1000;
            StartCoroutine(setTankActiveDelay(i, false));
		}
	}

	public IEnumerator setInvisible(int i){
		yield return new WaitForSeconds (0.5f);
		tanks [i].GetComponent<SpriteRenderer> ().material.color = new Color32 (0, 0, 0, 0);
	}

	public IEnumerator setTankActiveDelay (int i, bool active){
		yield return new WaitForSeconds (0.5f);
		tanks [i].SetActive (active);
		tanksLeft -= 1;
		if (tanksLeft == 1)
		{
			Invoke("endRound", 2.0f);
		}
		tankInfos[i].destroyingTank = false;
	}

	public void setTanksActive(bool active){
		for (int i = 0; i < numPlayers; i++) {
			tanks [i].SetActive (active);
		}
	}

	public void nextTurn(){
		playerTurn = (playerTurn + 1) % numPlayers;
		while (!tanks[playerTurn].activeSelf)
			playerTurn = (playerTurn + 1) % numPlayers;
		float temp = Random.value * 200.0f - 100.0f;
		windSpeed = (windSpeed * 2 + temp) / 3;
		movementEnabled = true;
        // Updates ammo count bottom left
        tankInfos[playerTurn].setActiveWeapon(tankInfos[playerTurn].activeWeapon);
        turnEnded = false;
    }

    public void repairTank()
    {
        source = tanks[playerTurn].GetComponent<AudioSource>();
        if (tankInfos[playerTurn].ammo[0] >= 1)
        {

            tankInfos[playerTurn].health = tankInfos[playerTurn].maxHealth;
            tankInfos[playerTurn].ammo[0] -= 1;
            source.PlayOneShot(source.clip);
        }
    }

    public void endRound(){
		mapInstantiated = false;
		tanksLeft = 0;
		tankInfos [playerTurn].score += 2000;
        tankInfos[playerTurn].funds += 2000;
        setTanksActive(false);
		this.transform.SetParent (null);
		DontDestroyOnLoad(transform.gameObject);
		SceneManager.LoadScene ("RoundResults");
	}

    public void projDestroyed()
    {
        if (!turnEnded)
        {
            turnEnded = true;
            Invoke("nextTurn", 1f);
        }
    }

    public void projDestroyed(float i)
    {
        if (!turnEnded)
        {
            turnEnded = true;
            Invoke("nextTurn", i);
        }
    }
}
