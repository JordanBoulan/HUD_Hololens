using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Author: Grant Ikehara, Kevin Do
/// File: Airspeed.cs \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Airspeed code to manage the animation and text of the airspeed graphic. The program 
/// finds the required game objects ("AirspeedBarFill" and "AirspeedText") in Unity in order to change them.
/// The AirspeedBarFill changes the graphics, and the AirspeedText will display the value of the airspeed.
/// </summary>

public class Airspeed : MonoBehaviour
{

    /// <value> Set the maximum value of the airspeed. </value>
    public float maxAirspeed = 30f;
    /// <value> The airspeed property sets the value of the current airspeed. This value will change depending on the user's speed. </value>
    public float airspeed = 0f;
    /// <value> The airspeed text locates the textbox in unity. </value>
    private Text airspeedText;
    /// <value> The airspeed bar image locates the image in unity to be animated. </value>
    private Image airspeedBar;

    /// <summary>
    /// Gets the airspeed from the data listener
    /// <param name="newData"> New value coming from data generator. </param> 
    /// </summary>
    public void dataUpdater(DataStruct newData)
    {
        airspeed = (float)newData.speedData;
    }

    /// <summary>
    /// Locates and grabs the image and text components from the Unity project for the airspeed bar and textbox.
    /// </summary>
    public void Start()
    {
        //get values from data generator
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataUpdater);

        //find the airspeed bar component in unity, and set it to zero.
        airspeedBar = GameObject.Find("AirspeedBarFill").GetComponent<Image>();
        airspeedBar.fillAmount = 0f;

        //find the airspeed textbox in unity to display the numerical value of the user's speed
        airspeedText = GameObject.Find("AirspeedText").GetComponent<Text>();
        airspeedText.text = "0.0";
    }

    /// <summary>
    /// Sets the airspeed value to the airspeed bar in the heads up display.
    /// Changes the airspeed display to match user's airspeed.
    /// </summary>
    public void Update()
    {
        //myAltitude is value 0
        airspeedBar.fillAmount = airspeed / maxAirspeed;
        airspeedText.text = airspeed.ToString();
    }


}