using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OOBscript : MonoBehaviour {

	public bool danger = false;
	public Text HUDdanger;
    GameObject sancho;
    AsteroidSpawner asteroidSpawner;

	// Use this for initialization
	void Start () {
		HUDdanger = GetComponent<Text> ();
        sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
    }
	
	// Update is called once per frame
	void Update () {
        if (asteroidSpawner.curAsteroids != 0)
        {
            if (danger)
            {
                HUDdanger.text = "Turn Around!";
            }
            else
            {
                HUDdanger.text = "SAFE";
            }
        } else
        {
            HUDdanger.text = "Return to space station";
        }
	}
}
