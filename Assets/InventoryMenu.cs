using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour {


    Image background;
    Image repairMoney;
    Image arrowOpen;
    Image arrowClose;
    Image invalid;
    Image repair;
    TankObject tank;

    public bool open = false;
    GameObject GM;
    GameManager GMScript;

    public Sprite[] weaponLogos = new Sprite[8];
    public GameObject invWeaponLogo;
    public GameObject invWeaponAmmoCount;
    public GameObject infiniteImage;

    public void OpenClose() {
        GM = GameObject.Find("GameManager");        //GameObject.Find to get GameManager
        GMScript = GM.GetComponent<GameManager>();	//GetComponent to access GameManager script inside object
        background = GameObject.Find("InventoryBackground").GetComponent<Image>();
        arrowOpen = GameObject.Find("MenuOpen").GetComponent<Image>();
        arrowClose = GameObject.Find("MenuClose").GetComponent<Image>();
        invalid = GameObject.Find("InvalidItems").GetComponent<Image>();
        repairMoney = GameObject.Find("RepairMoney").GetComponent<Image>();

        if (open){
            background.enabled = false;
            arrowOpen.enabled = true;
            arrowClose.enabled = false;
            open = false;
            invalid.enabled = false;
            repairMoney.enabled = false;

            GameObject.Find("Repair").GetComponent<Image>().enabled = false;
            GameObject.Find("Money").GetComponent<Text>().text = "";
            GameObject.Find("amtRepair").GetComponent<Text>().text = "";
            GameObject.Find("1INF").GetComponent<Image>().enabled = false;

            for (int i = 1; i <= 8; i++)
            {
                GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Image>().enabled = false;
                GameObject.Find("amt".Insert(3, i.ToString())).GetComponent<Text>().text = "";
                GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Button>().interactable = false;
            }
            int hi = GMScript.tankInfos[GMScript.playerTurn].activeWeapon;
            GameObject.Find(hi.ToString().Insert(1, "H")).GetComponent<Image>().enabled = false;

            Debug.Log("Inventory menu closed");
        } else {
            background.enabled = true;
            arrowOpen.enabled = false;
            arrowClose.enabled = true;
            invalid.enabled = true;
            repairMoney.enabled = true;
            open = true;

            ChangeValue(GameObject.Find("Money").GetComponent<Text>(), GMScript.tankInfos[GMScript.playerTurn].getFunds(), "$");
            Image a = GameObject.Find("1A").GetComponent<Image>();
            a.enabled = true;
            GameObject.Find("1INF").GetComponent<Image>().enabled = true;

            for (int i = 2; i <= 8; i++)
            {
                a = GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Image>();
                a.enabled = true;
                if (GMScript.tankInfos[GMScript.playerTurn].getAmmo()[i] > 0)
                {
                    GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Button>().interactable = true;
                    GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Image>().color = new Color32(255,255,255,255);
                    ChangeValue(GameObject.Find("amt".Insert(3, i.ToString())).GetComponent<Text>(), GMScript.tankInfos[GMScript.playerTurn].getAmmo()[i], "x");
                } else
                {
                    GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Button>().interactable = false;
                    GameObject.Find(i.ToString().Insert(1, "A")).GetComponent<Image>().color = new Color32(103, 105,105,255);
                }
            }

            int hi = GMScript.tankInfos[GMScript.playerTurn].activeWeapon;
            GameObject.Find(hi.ToString().Insert(1, "H")).GetComponent<Image>().enabled = true;

            if (GMScript.tankInfos[GMScript.playerTurn].getAmmo()[0] > 0) {
                GameObject.Find("Repair").GetComponent<Image>().enabled = true;
                ChangeValue(GameObject.Find("amtRepair").GetComponent<Text>(), GMScript.tankInfos[GMScript.playerTurn].getAmmo()[0], "x");
            } else {
                ChangeValue(GameObject.Find("amtRepair").GetComponent<Text>(), 0, "x");
            }

            Debug.Log("Inventory menu open");
        }
    }

    public void ChangeValue(Text obj, int newValue, string start){
        if (newValue < 10 && !obj.name.Equals("Money")) {
            obj.text = start.Insert(1, "0").Insert(2, newValue.ToString());
        } else {
            obj.text = start.Insert(1, newValue.ToString());
        }
    }

    public void BackgroundClick()
    {

    }

    // check for shortcut 
    void Update() {
        GM = GameObject.Find("GameManager");        //GameObject.Find to get GameManager
        GMScript = GM.GetComponent<GameManager>();	//GetComponent to access GameManager script inside object
        if (Input.anyKeyDown) {

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && GMScript.tankInfos[GMScript.playerTurn].fuel == 0)
            {
                Image error = GameObject.Find("nFuel").GetComponent<Image>();
                StartCoroutine(NoAmmo(error, 1));
            }

            int key;
            if (int.TryParse(Input.inputString, out key)) {
                if (key < 9 && key > 0) {
                    WeaponShortcutSelect(key);
                }
            }
        }
    }


    public void WeaponShortcutSelect(int i)
    {
        int hi = GMScript.tankInfos[GMScript.playerTurn].activeWeapon;
        GameObject.Find(hi.ToString().Insert(1, "H")).GetComponent<Image>().enabled = false;
        if (i == 1)
        {
            Debug.Log("weapon " + i.ToString() + " selected");
            GMScript.tankInfos[GMScript.playerTurn].setActiveWeapon(i);
            open = true;
            OpenClose();
        } else
        {
            if (GMScript.tankInfos[GMScript.playerTurn].getAmmo()[i] > 0)
            {
                Debug.Log("weapon " + i.ToString() + " selected");
                GMScript.tankInfos[GMScript.playerTurn].setActiveWeapon(i);
                open = true;
                OpenClose();

            }
            else
            {
                Debug.Log("No more of weapon " + i.ToString());
                Image error = GameObject.Find("n".Insert(1, i.ToString())).GetComponent<Image>();
                StartCoroutine(NoAmmo(error, 1));
            }

        }
    }

    public void WeaponSelect(int i) {
        if (i == 0)
        {
            GMScript.repairTank();
            open = true;
            OpenClose();
        } else
        {
            int hi = GMScript.tankInfos[GMScript.playerTurn].activeWeapon;
            GameObject.Find(hi.ToString().Insert(1, "H")).GetComponent<Image>().enabled = false;
            if (i == 1)
            {
                Debug.Log("weapon " + i.ToString() + " selected");
                GMScript.tankInfos[GMScript.playerTurn].setActiveWeapon(i);
                open = true;
                OpenClose();
            }
            else
            {
                if (GMScript.tankInfos[GMScript.playerTurn].getAmmo()[i] > 0)
                {
                    Debug.Log("weapon " + i.ToString() + " selected");
                    GMScript.tankInfos[GMScript.playerTurn].setActiveWeapon(i);
                    open = true;
                    OpenClose();
                }
            }
        }
    }

    IEnumerator NoAmmo(Image img, float delay){
        img.enabled = true;
        yield return new WaitForSeconds(delay);
        img.enabled = false;
    }

    public void WeaponLogoChange(int i, int j)
    {
        // Change weapon logo bottom left
        invWeaponLogo.GetComponent<Image>().sprite = weaponLogos[i-1];

        // Change ammo count if not default weapon
        if (i > 1)
        {
            if (!invWeaponAmmoCount.activeSelf)
                invWeaponAmmoCount.SetActive(enabled);
            if (infiniteImage.activeSelf)
                infiniteImage.SetActive(false);
            invWeaponAmmoCount.GetComponent<TextMeshProUGUI>().text = "x" + j;
        }
        // Enable infinite ammo image if default weapon
        else
        {
            invWeaponAmmoCount.SetActive(false);
            infiniteImage.SetActive(true);
        }
    }

}
