using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Airspeed1 : MonoBehaviour {

    public float maxAirspeed = 30f;
    public float airspeed = 30f;
    public Text AirspeedTextMoving;
    public Image airspeedBar;

    // Use this for initialization
    void Start()
    {

        airspeedBar = GameObject.Find("AirspeedBarFill").GetComponent<Image>();
        airspeedBar.fillAmount = 0f;

        AirspeedTextMoving = GameObject.Find("AirspeedText").GetComponent<Text>();
        AirspeedTextMoving.text = airspeed.ToString();
        InvokeRepeating("ReduceAirspeed", 0f, 0.2f);
    }

    void ReduceAirspeed()
    {
        airspeedBar.fillAmount = airspeed / 100;
        airspeed -= 1f;
        if (airspeed < 0f)
        {
            airspeed = 30f;
        }
        SetAirspeedBar(airspeed);
        AirspeedTextMoving.text = airspeed.ToString();
    }

    public void SetAirspeedBar(float myAirspeed)
    {
        //myAltitude is value 0, 1
        airspeedBar.fillAmount = myAirspeed / maxAirspeed;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
