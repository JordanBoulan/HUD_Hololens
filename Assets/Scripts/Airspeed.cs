using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This class updates the airpseed display with data rcieved from the Pi
public class Airspeed : MonoBehaviour {

    public float maxAirspeed = 30f; //Max airspeed of the display
    public float airspeed = 30f;    //Current airspeed, set to 30 to initialize
    public Text AirspeedTextMoving; //Moving text anchored to the top of the airspeed bar
    public GameObject airspeedBar;  //The display object

    //Gets the current altitude from the listener
    private void dataUpdater(DataStruct newData)
    {
        airspeed = (float) newData.speedData;
    }
    
    // Use this for initialization
    void Start()
    {
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataUpdater);
    }


    //Changes the altitude display to match altitude
    public void SetAirspeedBar(float myAirspeed)
    {
        //myAltitude is value 0, 1
        float calc_airspeed = myAirspeed / maxAirspeed;
        if(calc_airspeed == 0)
        {
            airspeedBar.transform.localScale = new Vector3(1f, 0.001f, 1f);         //Changes the bar
            AirspeedTextMoving.transform.localScale = new Vector3(1f, 1000f, 1f);   //Moves the text
        }
        else
        {
            airspeedBar.transform.localScale = new Vector3(1f, calc_airspeed, 1f);              //Changes the bar
            AirspeedTextMoving.transform.localScale = new Vector3(1f, (1 / calc_airspeed), 1f); //Moves the text
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAirspeedBar(airspeed);
        AirspeedTextMoving.text = airspeed.ToString() + " kph"; //Changes the text to match altitude
    }
}
