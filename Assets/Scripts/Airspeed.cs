using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Airspeed : MonoBehaviour {

    public float maxAirspeed = 30f;
    public float airspeed = 30f;
    public Text AirspeedTextMoving;
    public GameObject airspeedBar;

    // Use this for initialization
    private void dataUpdater(DataStruct newData)
    {
        airspeed = (float) newData.speedData;
    }

    void Start()
    {
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataUpdater);
    }



    public void SetAirspeedBar(float myAirspeed)
    {
        //myAltitude is value 0, 1
        float calc_airspeed = myAirspeed / maxAirspeed;
        if(calc_airspeed == 0)
        {
            airspeedBar.transform.localScale = new Vector3(1f, 0.001f, 1f);
            AirspeedTextMoving.transform.localScale = new Vector3(1f, 1000f, 1f);
        }
        else
        {
            airspeedBar.transform.localScale = new Vector3(1f, calc_airspeed, 1f);
            AirspeedTextMoving.transform.localScale = new Vector3(1f, (1 / calc_airspeed), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAirspeedBar(airspeed);
        AirspeedTextMoving.text = airspeed.ToString() + " kph";
    }
}
