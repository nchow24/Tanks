using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour {

	static readonly Color blueColor = new Color (102/255f,188/255f,231/255f);
	static readonly Color emptyColor = new Color (209/255f,198/255f,190/255f);
	static readonly Color disabledColor = new Color (92/255f,87/255f,80/255f);
	const int WEAPON_COUNT = 9;

	static readonly WeaponInfo[] WEAPON_INFO = new WeaponInfo[] {
		new WeaponInfo("REPAIR KIT", "Repair kits are used to patch up any damages to your tank.", 1000),
		new WeaponInfo("SMALL MISSILE","This tiny missile packs a punch.",0),
		new WeaponInfo("LARGE MISSILE", "The large missile packs a mean punch.", 500),
		new WeaponInfo("ARMOR PIERCING", "This quick tiny missile will pierce through the strongest of armors.", 1250),
		new WeaponInfo("FRAG", "Surprise your enemies with an explosion.", 1500),
		new WeaponInfo("EXPLOSIVE", "Frag? Pshhh. Give em a taste of this one!", 3000),
		new WeaponInfo("INCENDIARY","Did someone say fire?!",2500),
		new WeaponInfo("BALL","Don't be square, buy a ball.",3000),
		new WeaponInfo("HELI-STRIKE","Send an helicopter to drop some fire on your enemies!",4500),
	};

	static readonly WeaponInfo[] MISC_INFO = new WeaponInfo[] {
		new WeaponInfo("ARMOR","Add armor to your tank to protect yourself from damage.",1000),
		new WeaponInfo("FUEL","You'll need this to move around!",500)
	};

	GameObject GM;
	GameManager GMScript;

	public Text fundsText;
	public Text playerNameText;
	public Text w1InvCount; // Current inventory count
	public Text w1PurCount;
	public Text w2InvCount;
	public Text w2PurCount;
	public Text repairInvCountText;
	public Text repairPurCountText;
	public Text descTitle;
	public Text descBody;
	public Text descWeapCost;
	public Text totalCartCost;
	public Text afterPurchaseFund;

	public Transform weaponsPanel; // (0) weaponBox0, (1) weap1, ... weapN -> (1) WpnLogo, (2) title, (3) curAmount, (4) purAmt, (5) addBtn, (6) minusBtn
	public Transform armorPanel; // (0) title, (1) pills, (2) addBtn, (3) minusBtn
	public Transform fuelPanel; // (0) title, (1) pills, (2) addBtn, (3) minusBtn
	public Transform repairPanel; // (2) curAmt, (3) purchAmt, (4) minusBtn, (5) addBtn

	public Button addArmorBtn;
	public Button minusArmorBtn;
	public Button addFuelBtn;
	public Button minusFuelBtn;
	public Button addRepairBtn;
	public Button minusRepairBtn;

	// Inventory count while buying
	private int[] ammoPurchaseCount;
	private int armorPurchaseCount;
	private int fuelPurchaseCount;
	private int totalPurchaseAmount;
	private int funds; // Current funds

    // Prefabs
    public GameObject confirmBoxPrefab;
    private GameObject confirmBoxPrefabClone;
    public GameObject errorMessagePrefab;
    private GameObject errorMessagePrefabClone;
	public GameObject clearCartBoxPrefab;
	private GameObject clearCartBoxPrefabClone;

	private GameObject[] animations = new GameObject[8];

	// TODO: WHEN COMBINING AND REFACTORING
	// Replace fake data with game manager objects. 
	// Confirm all the prices and descriptions

	// TODO: for DESCRIPTIONS
	// Add a swapping sprite for logos

	// TODO: 
	// Make buttons clickable when disabled,
	// Show error dialog.

	private tankInfo info;

	int curPlayerIndex = 0;
	public tankInfo curTank;

	public class WeaponInfo {
		public string name;
		public string description;
		public int cost;
		public WeaponInfo() {
			this.name = "Default Name";
			this.description = "Default Description";
			this.cost = 100;
		}
		public WeaponInfo(string name, string description, int cost) {
			this.name = name;
			this.description = description;
			this.cost = cost;
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Start()");
		GM = GameObject.Find("GameManager");
		GMScript = GM.GetComponent<GameManager>();

        Debug.Log(GMScript.currentRound);

        /** IMPORTANT: Always set curTank to set the shop for */
        setNextPlayer ();

		// Start with the first active
		setButtons ();

		// Locates all the animation objects
		initAnimations ();
	}

	public void setNextPlayer() {
		Debug.Log("Setting up next player: " + curPlayerIndex);
//		 info = new tankInfo();
//		 curTank = info; // This is for testing
//		 initFakeData ();

		if (curPlayerIndex >= GMScript.numPlayers) {
			Debug.Log ("Error setting up next player");
			return;
		}
        curTank = GMScript.tankInfos [curPlayerIndex];
		
		initPlayerShop();
		curPlayerIndex++; // Increment for next player if they exist
	}

	private void initFakeData() {
		// Fake data
		ammoPurchaseCount = new int[WEAPON_COUNT]; // [0] repair, [1] large missle, [2] armor pen
		armorPurchaseCount = 0;
		fuelPurchaseCount = 0;
		totalPurchaseAmount = 0;
		info.name = "Alex";
		info.ammo = new int[WEAPON_COUNT]; // need to initialize ammo bc it isn't initialized in constructor +1 for repair
		info.ammo [0] = 4; // 0 is repair count
		info.ammo [1] = 99; // SMALL MISSILE
		info.ammo [2] = 15; // Large Missile
		info.ammo [3] = 30; // Armor Pen
		info.ammo [4] = 35; // Frag
		info.ammo [5] = 98; // Explosive
		info.ammo [6] = 97; // incendiary
		info.ammo [7] = 45; // ball
		info.ammo [8] = 95; // Heli
		info.funds = 99999;
		funds = info.funds;
		info.fuelLevel = 120;
		info.armorLevel = 3;
	}
	
	// Update is called once per frame
	void Update () {
        int afterPurchase = funds - totalPurchaseAmount;

        // Disable weapon + btn if not enough funds
        for (int i = 1; i < weaponsPanel.childCount; i++) {
			int childIndex = i;
			int weaponIndex = i + 1;

			Button weaponAddBtn = weaponsPanel.GetChild (childIndex).GetChild (4).GetComponent<Button> ();
			if (afterPurchase >= WEAPON_INFO [weaponIndex].cost && (ammoPurchaseCount [weaponIndex] + curTank.ammo [weaponIndex]) < 99)
				weaponAddBtn.interactable = true;
			else {
				// Debug.Log ("UPDATE()... Add button at index " + childIndex + " has been disabled");	
				weaponAddBtn.interactable = false;
			}
		}

		// repair button price logic
		if (afterPurchase >= WEAPON_INFO [0].cost && (ammoPurchaseCount [0] + curTank.ammo [0]) < 99)
			addRepairBtn.interactable = true;
		else 
			addRepairBtn.interactable = false;

		// armor button price logic
		if (afterPurchase >= MISC_INFO [0].cost && (curTank.armorLevel + armorPurchaseCount < 5))
			addArmorBtn.interactable = true;
		else
			addArmorBtn.interactable = false;

		// fuel button price logic
		if (afterPurchase >= MISC_INFO [1].cost && ((curTank.fuelLevel-100)/20 + fuelPurchaseCount < 5))
			addFuelBtn.interactable = true;
		else
			addFuelBtn.interactable = false;

		// Purchase/Cart Logic
		totalCartCost.text = totalPurchaseAmount == 0 ? "$0" : "-$" + totalPurchaseAmount.ToString();
		afterPurchaseFund.text = "$" + (funds - totalPurchaseAmount).ToString();
	}

	// Sets current player info for the shop
	private void initPlayerShop() {
		Debug.Log("InitShop");

		playerNameText.text = curTank.name;
		fundsText.text = "$" + curTank.funds.ToString();
		totalCartCost.text = "$0";
        
		// set tank color
		GameObject.Find("Tank").GetComponent<Image>().color = curTank.GetComponent<SpriteRenderer>().color;

		funds = curTank.funds;
		ammoPurchaseCount = new int[WEAPON_COUNT];
		armorPurchaseCount = 0;
		fuelPurchaseCount = 0;
		totalPurchaseAmount = 0;

		// Set default description when nothing is updated
		setDescription ("","Please select an item to see more information!", 0);
//		foreach (GameObject a in animations) {
//			a.SetActive (false);
//		}

		// Set up weapons ammo and buttons
		for (int i = 1; i < weaponsPanel.childCount; i++) {
			int curIndex = i; // Weird glitch where if you don't do this, you get index out of bounds....
			Transform weaponBox = weaponsPanel.GetChild(curIndex);
			weaponBox.GetChild (2).GetComponent<Text>().text = curTank.ammo[i+1] == 0 ? "0" : "x" + curTank.ammo[i+1];
			weaponBox.GetChild (5).GetComponent<Button>().interactable = false;
			weaponBox.GetChild (3).GetComponent<Text>().text = "0"; // Set purchase amt to 0
		}

		// Setup repair section
		minusRepairBtn.interactable = false;
		repairInvCountText.text = curTank.ammo[0] == 0 ? "0" : "x" + curTank.ammo [0];
		repairPurCountText.text = "0";

		// Setup fuel/armor sections
		setupPills (fuelPanel.GetChild(1), (curTank.fuelLevel-100)/20);
		minusFuelBtn.interactable = false;

		setupPills (armorPanel.GetChild(1), (curTank.armorLevel));
		minusArmorBtn.interactable = false;
	}

	void setButtons() {
		for (int i = 0; i < weaponsPanel.childCount; i++) {
			int childIndex = i; // Weird glitch where if you don't do this, you get index out of bounds....
			int weaponIndex = i+1;
			Transform weaponBox = weaponsPanel.GetChild(childIndex);

			Button addBtn = weaponBox.GetChild (4).GetComponent<Button>(); // Add btn
			Button minusBtn = weaponBox.GetChild (5).GetComponent<Button>(); // Minus btn
			addBtn.onClick.AddListener (() => onWeaponUpdate (childIndex, 1));
			minusBtn.onClick.AddListener (() => onWeaponUpdate (childIndex, -1));

			weaponBox.GetComponent<Button>().onClick.AddListener(() => onSelect('w', weaponIndex));
		}

		addRepairBtn.onClick.AddListener (() => onRepairUpdate (1));
		minusRepairBtn.onClick.AddListener (() => onRepairUpdate (-1));

		addFuelBtn.onClick.AddListener (() => onFuelUpdate (1));
		minusFuelBtn.onClick.AddListener (() => onFuelUpdate (-1));

		addArmorBtn.onClick.AddListener (() => onArmorUpdate (1));
		minusArmorBtn.onClick.AddListener (() => onArmorUpdate (-1));

		// add onclick listeners for highlighted boxes to update description
		repairPanel.GetComponent<Button>().onClick.AddListener(() => onSelect('r', 0));
		armorPanel.GetComponent<Button>().onClick.AddListener(() => onSelect('a', 0));
		fuelPanel.GetComponent<Button>().onClick.AddListener(() => onSelect('f', 0));
	}

	public void onWeaponUpdate(int childIndex, int value) {
//		Debug.Log("Updating weapon... weapon index:" + childIndex);
		int weaponIndex = childIndex + 1;

		if (curTank.ammo.Length < weaponIndex) {
			Debug.Log("Error: Weapon index too large." + weaponIndex);
			return;
		}

		// Assume enough funding else the button would be disabled
		ammoPurchaseCount [weaponIndex] += value;
		totalPurchaseAmount += WEAPON_INFO [weaponIndex].cost * value; // will be negative if adding
		weaponsPanel.GetChild(childIndex).GetChild(3).GetComponent<Text>().text = ammoPurchaseCount[weaponIndex] == 0 ? "0" : "+" + ammoPurchaseCount[weaponIndex].ToString();

		// Button will be disabled based on amount
		if ((ammoPurchaseCount [weaponIndex] + curTank.ammo [weaponIndex]) < 99)
			weaponsPanel.GetChild (childIndex).GetChild (4).GetComponent<Button> ().interactable = true;
		else {
			weaponsPanel.GetChild (childIndex).GetChild (4).GetComponent<Button> ().interactable = false;
//			Debug.Log ("Weapon Index:" + weaponIndex + " has been disabled bc too much");
		}

		if (ammoPurchaseCount [weaponIndex] > 0)
			weaponsPanel.GetChild (childIndex).GetChild (5).GetComponent<Button> ().interactable = true;
		else {
			weaponsPanel.GetChild (childIndex).GetChild (5).GetComponent<Button> ().interactable = false;
//			Debug.Log ("Weapon Index:" + weaponIndex + " has been disabled bc too little");
		}
		
	}

	public void onRepairUpdate(int value) {
//		Debug.Log ("On Repair Update");

		ammoPurchaseCount [0] += value;
		totalPurchaseAmount += WEAPON_INFO [0].cost * value;
		repairPurCountText.text = ammoPurchaseCount[0] == 0 ? "0" : "+" + ammoPurchaseCount [0].ToString();

		// Inventory logic is done here, price logic is done in update()
		if ((ammoPurchaseCount[0] + curTank.ammo[0]) >= 99 )
			addRepairBtn.interactable = false;
		else
			addRepairBtn.interactable = true;
		
		if (ammoPurchaseCount[0] <= 0)
			minusRepairBtn.interactable = false;
		else
			minusRepairBtn.interactable = true;
	}

	/** Need to change 5 pill colors depending on inventory and purchase amount and money.. */
	public void onArmorUpdate(int value) {
//		Debug.Log ("On armor pill Update");

		if ((armorPurchaseCount == 0 && value == -1) || (armorPurchaseCount == 5 && value == 1)) {
			Debug.Log ("Error: You're trying to add/subtract when there is none/too much armor purchase count.");
			return;
		}

		armorPurchaseCount += value;
		totalPurchaseAmount += MISC_INFO[0].cost * value;
		Transform pills = armorPanel.GetChild(1);

		if (value == 1)
			pills.GetChild (curTank.armorLevel-1 + armorPurchaseCount).GetComponent<Image> ().color = blueColor;
		else
			pills.GetChild(curTank.armorLevel-1 + armorPurchaseCount + 1).GetComponent<Image> ().color = emptyColor;

		// Logic for disabling add/minus buttons. Must work with the global update condition.
		if (curTank.armorLevel + armorPurchaseCount < pills.childCount)
			addArmorBtn.interactable = true;
		else 
			addArmorBtn.interactable = false;

		if (armorPurchaseCount > 0)
			minusArmorBtn.interactable = true;
		else
			minusArmorBtn.interactable = false;
	}

	/** Need to change 5 pill colors depending on inventory and purchase amount and money.. */
	public void onFuelUpdate(int value) {
//		Debug.Log ("On some pill Update");

		if ((fuelPurchaseCount == 0 && value == -1) || (fuelPurchaseCount == 5 && value == 1)) {
			Debug.Log ("Error: You're trying to add/subtract when there is none/too much fuel purchase count.");
			return;
		}

		fuelPurchaseCount += value;
		totalPurchaseAmount += MISC_INFO[1].cost * value;
		Transform pills = fuelPanel.GetChild(1);

        int pillCount = (curTank.fuelLevel - 100)/20;

        if (value == 1)
			pills.GetChild (pillCount-1 + fuelPurchaseCount).GetComponent<Image> ().color = blueColor;
		else
			pills.GetChild(pillCount-1 + fuelPurchaseCount + 1).GetComponent<Image> ().color = emptyColor;

		// Logic for disabling add/minus buttons. Must work with the global update condition.
		if (pillCount + fuelPurchaseCount < pills.childCount)
			addFuelBtn.interactable = true;
		else 
			addFuelBtn.interactable = false;

		if (fuelPurchaseCount > 0)
			minusFuelBtn.interactable = true;
		else
			minusFuelBtn.interactable = false;
	}

	private void setupPills(Transform pills, int defaultCount) {
		if (pills.childCount != 5)
			return;

		for (int i = 0; i < pills.childCount; i++) {
			pills.GetChild (i).GetComponent<Image>().color = i < defaultCount ? disabledColor : emptyColor;
		}
	}

	/** On selects, update descriptions
      * inputs char c
      * c = w, r, a, f for weapons, repair, armor, fuel
      * Index is used for weapons, 0 if it doesn't exist
      * Added 's' for small missile...
	  */
	public void onSelect(char c, int weaponIndex) {
		switch(c) {
		case 'w':
			setDescription (WEAPON_INFO [weaponIndex].name, WEAPON_INFO [weaponIndex].description, WEAPON_INFO [weaponIndex].cost);
			int childIndex = weaponIndex - 1;
			setAnimation (childIndex);
			break;
		case 'r':
			setDescription(WEAPON_INFO[0].name, WEAPON_INFO[0].description, WEAPON_INFO[0].cost);
			break;
		case 'a':
			setDescription(MISC_INFO[0].name, MISC_INFO[0].description, MISC_INFO[0].cost);
			break;
		case 'f':
			setDescription(MISC_INFO[1].name, MISC_INFO[1].description, MISC_INFO[1].cost);
			break;
		default:
			Debug.Log ("onSelect(): DEFAULT switch staement. Wrong parameters or something");
			return;
		}

		if (c != 'w')
			turnOffAnimations ();
	}

	public void onDeselect() {
		setDescription ("","Please select an item to see more information!", 0);
		turnOffAnimations ();
	}

	private void setDescription(string title, string description, int cost) {
		descTitle.text = title;
		descBody.text = description;
		descWeapCost.text = cost == 0 ? "" : "COST: $" + cost.ToString();
	}


	// Confirmation Buttons
	public void onPlayAndGoClick() {
//		Debug.Log("You clicked play and go.");
		if (confirmBoxPrefabClone != null) return;

        confirmBoxPrefabClone = Instantiate(confirmBoxPrefab);
        confirmBoxPrefabClone.transform.SetParent(GameObject.Find("Canvas").transform); // UI Elements need to be in Canvas to be displayed
        confirmBoxPrefabClone.transform.localScale = new Vector2(1.5f, 1.5f);
        confirmBoxPrefabClone.transform.localPosition = new Vector2(0f,0f);
    }

	public void saveChanges() {
//		Debug.Log ("saveChanges()");

		// Save repair and weapon purchases
		for (int i = 0; i < ammoPurchaseCount.Length; i++) {
			int curIndex = i;
			curTank.ammo [curIndex] += ammoPurchaseCount [curIndex];
		}

		// Save armor/fuel
		curTank.armorLevel += armorPurchaseCount;
        curTank.maxHealth = 100 + 40 * curTank.armorLevel;
		curTank.fuelLevel += fuelPurchaseCount * 20;

		// Save new funding
		curTank.funds -= totalPurchaseAmount;
	}

	public void onClearCartConfirmation() {
		Debug.Log("You clicked clear cart.");
		if (clearCartBoxPrefabClone != null) return;
		clearCartBoxPrefabClone = Instantiate(clearCartBoxPrefab);
		clearCartBoxPrefabClone.transform.SetParent(GameObject.Find("Canvas").transform); // UI Elements need to be in Canvas to be displayed
		clearCartBoxPrefabClone.transform.localScale = new Vector2(1.5f, 1.5f);
		clearCartBoxPrefabClone.transform.localPosition = new Vector2(0f,0f);
	}

	public void onClearCart() {
		Debug.Log("onClearCart()");
		initPlayerShop ();
	}
		
    // If invalid click (e.g. tried to deduct purchase but already at zero)
    public void onInvalidClick()
    {
        //Destroys current error message if it already exists
        if (errorMessagePrefabClone != null)
            Destroy(errorMessagePrefabClone);

        errorMessagePrefabClone = Instantiate(errorMessagePrefab);
        errorMessagePrefabClone.transform.SetParent(GameObject.Find("Canvas").transform);   // Needs to be in Canvas to be displayed
        errorMessagePrefabClone.transform.localScale = new Vector2(1f, 1f);
        errorMessagePrefabClone.transform.position = Input.mousePosition;

        // Error message dissapears after 3s
        Destroy(errorMessagePrefabClone, 3f);

        // Note: Error message is deleted when clicked on
        // Can also be deleted when mouse cursor is moved, but this is commented out for now
        // That code is found in ShopErrorMessage.cs

    }

	public bool isMorePlayers() {
		return curPlayerIndex < GMScript.numPlayers;
	}

	// Helper method to connect all animations' gameobjects
	private void initAnimations(){
		for (int i = 1; i <= 8; i++) {
			string temp = "anim" + i.ToString ();
			animations [i - 1] = GameObject.Find (temp);
			animations [i - 1].SetActive (false);
		}
		// Animation 0 should always be running, it is the farthest back
//		animations [0].SetActive (true);
	}

	// Helper method to turn on a specific animation
	private void setAnimation(int index){
		for (int i = 0; i < weaponsPanel.childCount; i++) {
			animations [i].SetActive (false);
		}
		animations [index].SetActive (true);
	}

	private void turnOffAnimations() {
		foreach (GameObject a in animations) {
			a.SetActive (false);
		}

	}
}
