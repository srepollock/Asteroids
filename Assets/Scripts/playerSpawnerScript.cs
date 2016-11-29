using UnityEngine;
using System.Collections;

public class playerSpawnerScript : MonoBehaviour {

    public GameObject[] playerObjects;

	// Use this for initialization
	void Start () {
        Instantiate(playerObjects[PlayerPrefs.GetInt("selectedShip")], transform.position, transform.rotation); 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
