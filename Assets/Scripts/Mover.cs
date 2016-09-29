using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed;
    public float lifetime = 10f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0) {
            Destroy(gameObject);
        }
	}
}
