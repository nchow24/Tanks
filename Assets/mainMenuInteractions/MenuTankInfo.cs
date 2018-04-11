using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* menuTankInfo : MonoBehaviour
 * 
 * Class that stores the tank info to be passed into the game
 * TODO:
 *  - Pass info into game
 */

[System.Serializable]
public class MenuTankInfo : MonoBehaviour {

	private MenuHandler menu;
	private Animator boxAnim;

	[SerializeField]
    private string player;
	[SerializeField]
    private InputField username;
	[SerializeField]
	public string status;
	[SerializeField]
    private Image tank;
	[SerializeField]
    private TankChooser tankChooser;
	[SerializeField]
    private TankStatus tankStatus;
    
	public Color32 getTankColour() { return tankChooser.getTankColour (); }
	public string getTankStatus(){ return status; }
	public string getPlayerName(){
		// Array is: placeholder, name
		Text[] inputs = username.GetComponentsInChildren<Text> ();
		if (inputs [1].text.Equals ("")) {
			return inputs [0].text;
		} else {
			return inputs [1].text;
		}
	}

	void Start () {
		menu = GameObject.Find ("_MenuHandler").GetComponent<MenuHandler> ();
		boxAnim = GameObject.Find ("Two Player Requirement Box").GetComponent<Animator> ();

        tankChooser = ScriptableObject.CreateInstance<TankChooser>();
        tankStatus = ScriptableObject.CreateInstance<TankStatus>();

        player = transform.name;
        username = gameObject.GetComponentInChildren<InputField>();
		status = transform.Find("PlayerStatus").gameObject.GetComponentInChildren<Text>().text;
        tank = gameObject.GetComponent<Image>();

        tankChooser.initialize(player, status, tank);
		tankStatus.initialize(transform.Find("PlayerStatus").gameObject.GetComponentInChildren<Text>(), player);
    }

	void Update(){
		username = gameObject.GetComponentInChildren<InputField>();
		status = transform.Find("PlayerStatus").gameObject.GetComponentInChildren<Text>().text;
	}

    public void onUpPress()
    {
        tankChooser.incrementColour();
    }

    public void onDownPress()
    {
        tankChooser.decrementColour();
    }

    public void onPlayerTypeChange()
    {
		if ((menu.getNumPlayers () - 1) < 2 && status.Equals("PLAYER")) {
			boxAnim.SetTrigger("lessThanTwo");
		} else {
			tankStatus.increment ();
			status = tankStatus.getStatus ();
			tankStatus.setPlayerBoxColour ();
			if (tankStatus.getStatus ().Equals ("NONE")) {
				username.interactable = false;
			} else {
				username.interactable = true;
			}
			tankChooser.updateTankStatus (status);
		}
    }

    public void updateUsername()
    {
        gameObject.GetComponentInParent<AudioSource>().Play();
    }
}
