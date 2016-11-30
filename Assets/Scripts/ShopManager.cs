using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    // AudioSource for buying items.
    AudioSource buyItemSFX;

    // PlayerScore helper class
    PlayerScore ps;

    // Array of buttons
    public GameObject[] buyingButtons;

    // Prices
    public int tankPlayerPrice = 5;
    public int assaultPlayerPrice = 5;

    // Use this for initialization
    void Start () {
        buyItemSFX = this.GetComponent<AudioSource>();
        ps = this.GetComponent<PlayerScore>();
        initialButtonSetup();
    }
	
	// Update is called once per frame
	void Update () {
        displayCurrency();
    }

    public void initialButtonSetup()
    {
        for (int i = 0; i < buyingButtons.Length; i++)
        {
            buyingButtons[i].active = false;
        }

        buyingButtons[1].active = true;
        buyingButtons[2].active = true;
        buyingButtons[3].active = true;
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
        initialButtonSetup();
    }

    public void podPlayerSelect()
    {
        Debug.Log("Selected Pod");
        PlayerPrefs.SetInt("selectedShip", 0);
    }

    public void tankPlayerSelect()
    {
        if (PlayerPrefs.GetInt("tankPlayer") == 0 && ps.SpendScore(tankPlayerPrice))
        {
            Debug.Log("Purchase and selected Tank");
            PlayerPrefs.SetInt("tankPlayer", 1);
            PlayerPrefs.SetInt("selectedShip", 1);
            buyItem();
        } else
        {
            Debug.Log("Selected Tank");
            PlayerPrefs.SetInt("selectedShip", 1);
        }
    }

    public void assaultPlayerSelect()
    {
        if (PlayerPrefs.GetInt("assaultPlayer") == 0 && ps.SpendScore(assaultPlayerPrice))
        {
            Debug.Log("Purchase and selected Assault");
            PlayerPrefs.SetInt("assaultPlayer", 1);
            PlayerPrefs.SetInt("selectedShip", 2);
            buyItem();
        }
        else
        {
            Debug.Log("Selected Assault");
            PlayerPrefs.SetInt("selectedShip", 2);
        }
    }

    public void displayCurrency()
    {
        GameObject currencytextobj = GameObject.Find("CurrencyDisplay");
        Text currencytext = currencytextobj.GetComponent<Text>();
        currencytext.text = "You have $" + PlayerScore.GetPlayerCurrency();
    }

    public void buyItem()
    {
        buyItemSFX.Play();
    }
}
