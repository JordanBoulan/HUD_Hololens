using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// File: Altitude.cs \n
/// Creation date:  \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Altitude code to manage the animation and text of the altitude graphic. The program 
/// finds the required game objects ("AltitudeBarFill" and "AltitudeText") in Unity in order to change them.
/// The AltitudeBarFill changes the graphics, and the AltitudeText will display the value of the airspeed.
/// </summary>

public class Altitude : MonoBehaviour
{

    ///Set the maximum value of the altitude.
    public static float maxAltitude = 10f;
    ///The airspeed property sets the value of the current altitude. This value will change depending on the user's height.
    public static float altitude;
    ///The altitude text locates the textbox in unity.
    private Text altitudeText;
    ///The altitude bar image locates the image in unity to be animated.
    private Image altitudeBar;

    /// Gets the altitude value from the data listener
    /// <param name="newData"></param>
    public void myDataUpdater(DataStruct newData)
    {
        altitude = (float)newData.altitudeData;
    }

    /// Locates and grabs the image and text components from the Unity project for the altitude bar and textbox
    void Start()
    {
        //gets values from data generator
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(myDataUpdater);

        //find the altitude bar component in unity, and set it to zero.
        altitudeBar = GameObject.Find("AltitudeBarFill").GetComponent<Image>();
        altitudeBar.fillAmount = 0f;

        //find the altitude textbox in unity to display the numerical value of the user's height
        altitudeText = GameObject.Find("AltitudeText").GetComponent<Text>();
        altitudeText.text = altitude.ToString();

        //loop that will reduce the altitude value over time. comment out this line to use the data reciever above.
        //InvokeRepeating("ReduceAltitude", 0f, 0.5f);
        SetAltitudeBar(altitude);
        altitudeText.text = altitude.ToString();
    }

    /// A simple loop to show a simple animation of the altitude graphic
    /// The function reduces the value airspeed over time. If the value is 0, it is reset to the max airspeed, 30.
    /// The value of the airspeed is also sent to the text box to display the current value.  
    /// 
    /// This function is not used when getting data from the listener.
    void ReduceAltitude()
    {
        altitudeBar.fillAmount = altitude / 100;
        altitude -= 1f;
        if (altitude < 0f)
        {
            altitude = 10f;
        }
        
    }

    /// Sets the airspeed value to the airspeed bar in the heads up display.
    /// Changes the altitude display to match altitude value
    /// <param name="myAltitude"></param>
    public void SetAltitudeBar(float myAltitude)
    {
        //myAltitude is value 0, 1
        altitudeBar.fillAmount = myAltitude / maxAltitude;
    }


}