using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Airspeed1 : MonoBehaviour {

    public float maxAirspeed = 30f;
    public float airspeed = 30f;
    public Text AirspeedTextMoving;
    public GameObject airspeedBar;

    // Use this for initialization
    void Start()
    {
        AirspeedTextMoving.text = airspeed.ToString();
        InvokeRepeating("ReduceAirspeed", 0f, 0.2f);
    }

    void ReduceAirspeed()
    {
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
        float calc_airspeed = myAirspeed / maxAirspeed;
        if(calc_airspeed == 0)
        {
            airspeedBar.transform.localScale = new Vector3(1f, 0.001f, 1f);
            //AirspeedTextMoving.transform.localScale = new Vector3(1f, 1000f, 1f);
        }
        else
        {
            airspeedBar.transform.localScale = new Vector3(1f, calc_airspeed, 1f);
            //AirspeedTextMoving.transform.localScale = new Vector3(1f, (1 / calc_airspeed), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
