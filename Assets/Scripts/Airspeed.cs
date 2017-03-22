using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// File: Airspeed.cs \n
/// Creation date: \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Airspeed code to manage the animation and text of the airspeed graphic. The program 
/// finds the required game objects ("AirspeedBarFill" and "AirspeedText") in Unity in order to change them.
/// The AirspeedBarFill changes the graphics, and the AirspeedText will display the value of the airspeed.
/// </summary>

public class Airspeed : MonoBehaviour
{

    ///Set the maximum value of the airspeed.
    public float maxAirspeed = 30f;
    ///The airspeed property sets the value of the current airspeed. This value will change depending on the user's speed.
    public float airspeed = 30f;
    ///The airspeed text locates the textbox in unity.
    private Text airspeedText;
    ///The airspeed bar image locates the image in unity to be animated.
    private Image airspeedBar;

    /// Gets the airspeed from the data listener
    /// <param name="newData"></param>
    private void dataUpdater(DataStruct newData)
    {
        airspeed = (float)newData.speedData;
    }

    /// Locates and grabs the image and text components from the Unity project for the airspeed bar and textbox
    void Start()
    {
        //get values from data generator
        //UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataUpdater);

        //find the airspeed bar component in unity, and set it to zero.
        airspeedBar = GameObject.Find("AirspeedBarFill").GetComponent<Image>();
        airspeedBar.fillAmount = 0f;

        //find the airspeed textbox in unity to display the numerical value of the user's speed
        airspeedText = GameObject.Find("AirspeedText").GetComponent<Text>();
        airspeedText.text = airspeed.ToString();

        //loop that will reduce the airspeed value over time. comment out this line to use the data reciever above.
        InvokeRepeating("ReduceAirspeed", 0f, 0.2f);
    }

    /// A simple loop to show a simple animation of the airspeed graphic
    /// The function reduces the value airspeed over time. If the value is 0, it is reset to the max airspeed, 30.
    /// The value of the airspeed is also sent to the text box to display the current value.  
    /// 
    /// This function is not used when getting data from the listener.
    private void ReduceAirspeed()
    {
        airspeedBar.fillAmount = airspeed / 100;
        airspeed -= 1f;
        if (airspeed < 0f)
        {
            airspeed = 30f;
        }
        SetAirspeedBar(airspeed);
        airspeedText.text = airspeed.ToString();
    }

    /// Sets the airspeed value to the airspeed bar in the heads up display.
    /// Changes the airspeed display to match user's airspeed
    /// <param name="myAirspeed"></param>
    private void SetAirspeedBar(float myAirspeed)
    {
        //myAltitude is value 0, 1
        airspeedBar.fillAmount = myAirspeed / maxAirspeed;
    }


}