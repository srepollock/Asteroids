using UnityEngine;
using System.Collections;

public class Eliptical_movement : MonoBehaviour {

	public Vector3 center = new Vector3(0, 0, 0);
    public float radiusA = 10f;
    public float radiusB = 15f;
    public float speed = 100f;
    public float rtilt = 0f;
    public float atilt_phase = 0f;
    public float atilt_severity = 0f;
    public float angle;
     
    // Use this for initialization
    void Start () {
     
    }
     
    // Update is called once per frame
    void Update () {
		angle += speed * Time.deltaTime;
		if (angle >= 360f) {
			angle = 0;
		}
		
    	transform.position = center + new Vector3(0f + (radiusA * MCos(angle) * MCos(rtilt)) - (radiusB * MSin(angle) * MSin(rtilt)), 
    											  MCos(angle * atilt_phase) * atilt_severity,
                        				 	 	  0f + (radiusA * MCos(angle) * MSin(rtilt)) + (radiusB * MSin(angle) * MCos(rtilt)));
    }

    float MCos(float value)	{
	    return Mathf.Cos(Mathf.Deg2Rad * value);
	}

	float MSin(float value)	{
	    return Mathf.Sin(Mathf.Deg2Rad * value);
	}
}
