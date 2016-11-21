using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OOBscript : MonoBehaviour {

	public bool danger = false;
	public Text HUDdanger;
    public Image warningImage;
    GameObject sancho;
    AsteroidSpawner asteroidSpawner;

    float minAlpha = 0.2f;
    float maxAlpha = 0.5f;
    bool increasing = true;
    Color32 redWarning = new Color32(255, 0, 0, 100);
    Color32 blueWarning = new Color32(19, 128, 255, 100);

    // Use this for initialization
    void Start () {
		HUDdanger = GetComponent<Text> ();
        sancho = GameObject.Find("Sancho");
        asteroidSpawner = sancho.GetComponent<AsteroidSpawner>();
        warningImage.canvasRenderer.SetAlpha(0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (asteroidSpawner.curAsteroids != 0)
        {
            if (danger)
            {
                HUDdanger.text = "Turn Around!";
                //Debug.Log("alpha: " + warningImage.color.a);
                flashRed();
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

    void flashRed()
    {
        bool performingAction = false;
        warningImage.GetComponent<Image>().color = redWarning;
        warningImage.CrossFadeAlpha(maxAlpha + 0.1f, 3f, false);
        if (increasing)
        {
            if (!performingAction)
            {
                warningImage.CrossFadeAlpha(0.6f, 1f, false);
                performingAction = true;
                //Debug.Log("up renderer alpha: " + warningImage.canvasRenderer.GetAlpha());
            }
            if (warningImage.canvasRenderer.GetAlpha() >= maxAlpha)
            {
                increasing = false;
                performingAction = false;
                //Debug.Log("up Done");
            }
        }
        else
        {
            if (!performingAction)
            {
                warningImage.CrossFadeAlpha(0.0f, 1f, false);
                performingAction = true;
                //Debug.Log("down renderer alpha: " + warningImage.canvasRenderer.GetAlpha());
            }
            if (warningImage.canvasRenderer.GetAlpha() <= minAlpha)
            {
                increasing = true;
                performingAction = false;
                //Debug.Log("down Done");
            }
        }

    }

    void flashBlue()
    {
        bool performingAction = false;
        warningImage.GetComponent<Image>().color = blueWarning;
        warningImage.CrossFadeAlpha(0.6f, 3f, false);
        if (increasing)
        {
            if (!performingAction)
            {
                warningImage.CrossFadeAlpha(0.6f, 1f, false);
                performingAction = true;
                //Debug.Log("up renderer alpha: " + warningImage.canvasRenderer.GetAlpha());
            }
            if (warningImage.canvasRenderer.GetAlpha() >= 0.5f)
            {
                increasing = false;
                performingAction = false;
                //Debug.Log("up Done");
            }
        }
        else
        {
            if (!performingAction)
            {
                warningImage.CrossFadeAlpha(0.0f, 1f, false);
                performingAction = true;
                //Debug.Log("down renderer alpha: " + warningImage.canvasRenderer.GetAlpha());
            }
            if (warningImage.canvasRenderer.GetAlpha() <= 0.2f)
            {
                increasing = true;
                performingAction = false;
                //Debug.Log("down Done");
            }
        }
    }
}
