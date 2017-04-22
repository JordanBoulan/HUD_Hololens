using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Grant Ikehara, Kevin Do
/// File: Altitude.cs \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Altitude code to manage the animation and text of the altitude graphic. The program 
/// finds the required game objects ("AltitudeBarFill" and "AltitudeText") in Unity in order to change them.
/// The AltitudeBarFill changes the graphics, and the AltitudeText will display the value of the airspeed.
/// </summary>

public class Altitude : MonoBehaviour
{

    /// <value> Set the maximum value of the altitude. </value>
    public float maxAltitude = 10f;
    /// <value> The altitude property sets the value of the current altitude. This value will change depending on the user's height. </value>
    public float altitude = 0f;
    /// <value> The altitude text locates the textbox in unity. </value>
    private Text altitudeText;
    /// <value> The altitude bar image locates the image in unity to be animated. </value>
    private Image altitudeBar;

    /// <summary>
    /// Gets the altitude value from the data listener.
    /// <param name ="newData"> New value coming from data generator. </param> 
    /// </summary>
    public void myDataUpdater(DataStruct newData)
    {
        altitude = (float)newData.altitudeData;
    }

    /// <summary>
    /// Locates and grabs the image and text components from the Unity project for the altitude bar and textbox.
    /// </summary>
    public void Start()
    {
        //gets values from data generator
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(myDataUpdater);

        //find the altitude bar component in unity, and set it to zero.
        altitudeBar = GameObject.Find("AltitudeBarFill").GetComponent<Image>();
        altitudeBar.fillAmount = 0f;
        
        //find the altitude textbox in unity to display the numerical value of the user's height
        altitudeText = GameObject.Find("AltitudeText").GetComponent<Text>();
        altitudeText.text = "0.0";
    }

    /// <summary>
    /// Sets the airspeed value to the airspeed bar in the heads up display.
    /// Changes the altitude display to match altitude value.
    /// </summary>

    public void Update()
    {
        altitudeBar.fillAmount = altitude / maxAltitude;
        altitudeText.text = altitude.ToString();

    }

}