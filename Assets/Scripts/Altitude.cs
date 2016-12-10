using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This class updates th alititude display with data rcieved from the Pi
public class Altitude : MonoBehaviour
{
    public float maxAltitude = 10f; //Max altitude of the display
    public float altitude = 10f;    //Current altitude, set to 10 to initialize
    public Text AltitudeTextMoving; //Moving text anchored to the top of the altitude bar
    public GameObject altitudeBar;  //The display object
    
    //Gets the current altitude from the listener
    private void myDataUpdater(DataStruct newData)
    {
        altitude = (float) newData.altitudeData;
    }

    // Use this for initialization
    void Start()
    {
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(myDataUpdater);
    }

    //Changes the altitude display to match altitude
    public void SetAltitudeBar(float myAltitude)
    {
        //myAltitude is value 0, 1
        float calc_altitude = myAltitude / maxAltitude;
        if (calc_altitude == 0)
        {
            altitudeBar.transform.localScale = new Vector3(1f, 0.001f, 1f);         //Changes the bar
            AltitudeTextMoving.transform.localScale = new Vector3(1f, 1000f, 1f);   //Moves the text
        }
        else
        {
            altitudeBar.transform.localScale = new Vector3(1f, calc_altitude, 1f);               //Changes the bar
            AltitudeTextMoving.transform.localScale = new Vector3(1f, (1 / calc_altitude), 1f);  //Moves the text
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAltitudeBar(altitude);   
        AltitudeTextMoving.text = altitude.ToString() + " m";   //Changes the text to match altitude
    }
}
