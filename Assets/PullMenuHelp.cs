using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PullMenuHelp : MonoBehaviour {

    bool helpOpen = false;
    Image bulletHelp;
    Image circleHelp;
    Image background;
    Image control;
    Image win;
    Image points;
    Image weapon;
    Image fuel;
    Image upgrades;
    Image repair;
    Image wind;

    Image infoControl;
    Image infoWin;
    Image infoPoints;
    Image infoWeapon;
    Image infoFuel;
    Image infoUpgrades;
    Image infoRepair;
    Image infoWind;
    Image settings;

    Image HControl;
    Image HWin;
    Image HPoints;
    Image HWeapon;
    Image HFuel;
    Image HUpgrades;
    Image HRepair;
    Image HWind;

	Image menuFocus;

    public void Start()
    {
        GameObject GM = GameObject.Find("GameManager");        //GameObject.Find to get GameManager
        GameManager GMScript = GM.GetComponent<GameManager>();	//GetComponent to access GameManager script inside object
        HelpButtonOpen();
        HelpButtonClose();
    }

	public void Update(){
		if (helpOpen) {
			ColorBlock cb;
			Color32 normal = new Color32 (255, 255, 255, 100);

			// Control button reset
			cb = HControl.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HControl.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Win conditions reset
			cb = HWin.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HWin.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Points conditions reset
			cb = HPoints.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HPoints.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Weapon reset
			cb = HWeapon.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HWeapon.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Fuel resets
			cb = HFuel.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HFuel.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Upgrades resets
			cb = HUpgrades.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HUpgrades.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Repair resets
			cb = HRepair.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HRepair.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			// Wind resets
			cb = HWind.transform.gameObject.GetComponentInParent<Button> ().colors;
			cb.normalColor = normal;
			HWind.transform.gameObject.GetComponentInParent<Button> ().colors = cb;

			if (menuFocus != null) {
				cb = menuFocus.transform.gameObject.GetComponentInParent<Button> ().colors;
				cb.normalColor = new Color32 (255, 255, 255, 255);
				menuFocus.transform.gameObject.GetComponentInParent<Button> ().colors = cb;
			}
		}
	}

    public void HelpButtonOpen()
    {
        settings = GameObject.Find("SettingsClosed").GetComponent<Image>();
        settings.enabled = false;

        circleHelp = GameObject.Find("HelpOpen").GetComponent<Image>();
        bulletHelp = GameObject.Find("HelpClosed").GetComponent<Image>();
        background = GameObject.Find("HelpBackground").GetComponent<Image>();

        //get menu choices
        control = GameObject.Find("ControlChoice").GetComponent<Image>();
        win = GameObject.Find("WinChoice").GetComponent<Image>();
        points = GameObject.Find("PointsChoice").GetComponent<Image>();
        weapon = GameObject.Find("WeaponsChoice").GetComponent<Image>();
        fuel = GameObject.Find("FuelChoice").GetComponent<Image>();
        upgrades = GameObject.Find("UpgradesChoice").GetComponent<Image>();
        repair = GameObject.Find("RepairChoice").GetComponent<Image>();
        wind = GameObject.Find("WindChoice").GetComponent<Image>();

        //get menu contents
        infoControl = GameObject.Find("ControlInfo").GetComponent<Image>();
        infoWin = GameObject.Find("WinInfo").GetComponent<Image>();
        infoPoints = GameObject.Find("PointsInfo").GetComponent<Image>();
        infoWeapon = GameObject.Find("WeaponsInfo").GetComponent<Image>();
        infoFuel = GameObject.Find("FuelInfo").GetComponent<Image>();
        infoUpgrades = GameObject.Find("UpgradesInfo").GetComponent<Image>();
        infoRepair = GameObject.Find("RepairInfo").GetComponent<Image>();
        infoWind = GameObject.Find("WindInfo").GetComponent<Image>();

        HControl = GameObject.Find("ControlH").GetComponent<Image>();
        HWin = GameObject.Find("WinH").GetComponent<Image>();
        HPoints = GameObject.Find("PointsH").GetComponent<Image>();
        HWeapon = GameObject.Find("WeaponsH").GetComponent<Image>();
        HFuel = GameObject.Find("FuelH").GetComponent<Image>();
        HUpgrades = GameObject.Find("UpgradesH").GetComponent<Image>();
        HRepair = GameObject.Find("RepairsH").GetComponent<Image>();
        HWind = GameObject.Find("WindH").GetComponent<Image>();

        //get menu highlights
        control = GameObject.Find("ControlChoice").GetComponent<Image>();
        win = GameObject.Find("WinChoice").GetComponent<Image>();
        points = GameObject.Find("PointsChoice").GetComponent<Image>();
        weapon = GameObject.Find("WeaponsChoice").GetComponent<Image>();
        fuel = GameObject.Find("FuelChoice").GetComponent<Image>();
        upgrades = GameObject.Find("UpgradesChoice").GetComponent<Image>();
        repair = GameObject.Find("RepairChoice").GetComponent<Image>();
        wind = GameObject.Find("WindChoice").GetComponent<Image>();

        circleHelp.enabled = true;
        bulletHelp.enabled = false;
        background.enabled = true;
        control.enabled = true;
        win.enabled = true;
        points.enabled = true;
        weapon.enabled = true;
        fuel.enabled = true;
        upgrades.enabled = true;
        repair.enabled = true;
        wind.enabled = true;


        HControl.enabled = true;
        HWin.enabled = true;
        HPoints.enabled = true;
        HWeapon.enabled = true;
        HFuel.enabled = true;
        HUpgrades.enabled = true;
        HRepair.enabled = true;
        HWind.enabled = true;

        GameObject.Find("ControlH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("WinH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("PointsH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("WeaponsH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("FuelH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("UpgradesH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("RepairsH").GetComponent<Button>().OnDeselect(null);
        GameObject.Find("WindH").GetComponent<Button>().OnDeselect(null);


        ControlClick();

        Debug.Log("Help menu open");
        helpOpen = true;
    }

    public void HelpButtonClose()
    {
        settings.enabled = true;
        circleHelp.enabled = false;
        bulletHelp.enabled = true;
        background.enabled = false;
        control.enabled = false;
        win.enabled = false;
        points.enabled = false;
        weapon.enabled = false;
        fuel.enabled = false;
        upgrades.enabled = false;
        repair.enabled = false;
        wind.enabled = false;

        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = false;

        HControl.enabled = false;
        HWin.enabled = false;
        HPoints.enabled = false;
        HWeapon.enabled = false;
        HFuel.enabled = false;
        HUpgrades.enabled = false;
        HRepair.enabled = false;
        HWind.enabled = false;

        Debug.Log("Help menu closed");
        helpOpen = false;
    }

    public void ControlClick()
    {
        infoControl.enabled = true;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = false;

		menuFocus = HControl;

        Debug.Log("Controls selected");
    }

    public void WinClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = true;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = false;

		menuFocus = HWin;

        Debug.Log("How to win selected");
    }

    public void PointsClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = true;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = false;

		menuFocus = HPoints;

        Debug.Log("Points selected");
    }

    public void WeaponClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = true;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = false;

		menuFocus = HWeapon;

        Debug.Log("Weapons selected");
    }

    public void FuelClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = true;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = false;

		menuFocus = HFuel;

        Debug.Log("Fuel selected");
    }

    public void UpgradesClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = true;
        infoRepair.enabled = false;
        infoWind.enabled = false;

		menuFocus = HUpgrades;

        Debug.Log("Upgrades selected");
    }

    public void RepairClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = true;
        infoWind.enabled = false;

		menuFocus = HRepair;

        Debug.Log("Repair selected");
    }

    public void WindClick()
    {
        infoControl.enabled = false;
        infoWin.enabled = false;
        infoPoints.enabled = false;
        infoWeapon.enabled = false;
        infoFuel.enabled = false;
        infoUpgrades.enabled = false;
        infoRepair.enabled = false;
        infoWind.enabled = true;

		menuFocus = HWind;

        Debug.Log("Wind selected");
    }

    public void BackgroundClick()
    {

    }

}
