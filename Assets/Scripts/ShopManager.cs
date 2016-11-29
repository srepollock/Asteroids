using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public GameObject[] buyingButtons;

	// Use this for initialization
	void Start () {
        for(int i = 0; i < buyingButtons.Length; i++)
        {
            buyingButtons[i].active = false;
        }

        buyingButtons[1].active = true;
        buyingButtons[2].active = true;
        buyingButtons[3].active = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void bodyShop()
    {
        for (int i = 0; i < buyingButtons.Length; i++)
        {
            buyingButtons[i].active = true;
        }

        buyingButtons[1].active = false;
        buyingButtons[2].active = false;
        buyingButtons[3].active = false;
    }

    public void goBack()
    {
        for(int i = 0; i < buyingButtons.Length; i++)
        {
            buyingButtons[i].active = false;
        }

        buyingButtons[1].active = true;
        buyingButtons[2].active = true;
        buyingButtons[3].active = true;
    }

    public void podPlayerSelect()
    {
        PlayerPrefs.SetInt("selectedShip", 0);
    }

    public void tankPlayerSelect()
    {
        PlayerPrefs.SetInt("unlockedTankPlayer", 1);
        PlayerPrefs.SetInt("selectedShip", 1);
    }

    public void assaultPlayerSelect()
    {
        PlayerPrefs.SetInt("unlockedAssaultPlayer", 1);
        PlayerPrefs.SetInt("selectedShip", 2);
    }
}
