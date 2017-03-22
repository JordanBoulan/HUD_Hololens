//Temperature.cs 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// File: Temperature.cs \n
/// Author: Kevin Do\n
/// Creation date: \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Temperature code to manage the temperature graphic. The 
/// program locates the required game objects from Unity ("TemperatureFill" 
/// and "TemperatureText") in order to change them. The TemperatureFill will
/// manage the graphics while TemperatureText will display the value to the text box

public class Temperature : MonoBehaviour
{
    ///Set the temperature value to 0.
    public static float TemperaturePercentage = 0f; //x value of image size
    ///Used to locate the temperature image in Unity to be animated.
    public Image temperature;
    ///Used to locate the textbox in Unity.
    public Text tempText;
    ///Used to delay the loop of the animation.
    public static int count = 0;
  
    /// Gets the temperature value from the data listener
    /// <param name="newData"></param>
    public void dataListener(DataStruct newData)
    {
		TemperaturePercentage = (int)newData.batteryData;
    }

    // Use this for initialization
    public void Start()
    {
        //get value from data generator
        //UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);

        //find the temperature image component in Unity and set the value to zero.
        temperature = GameObject.Find("TemperatureFill").GetComponent<Image>();
        temperature.fillAmount = 0f;
        
        //find the textbox component in Unity.
        tempText = GameObject.Find("TemperatureText").GetComponent<Text>();
    }

    ///Simple loop to increase the temperature over time.
    public void increaseTemp()
    {
        count++;
        if (count == 10)
        {
            TemperaturePercentage = TemperaturePercentage + 3; //increase temperature
            count = 0;
            //reset battery to 100 when it gets to 0 (value doesnt go negative)
            if (TemperaturePercentage > 100)
            {
                TemperaturePercentage = 0;
            }
        }
    }

    /// Update is called to update the graphic and textbox in the heads up display in Unity.
    public void Update()
    {
        temperature.fillAmount = (float)TemperaturePercentage / 100; //get battery value from listener, convert to a percentage (x/100)
        increaseTemp(); //calls function to animate
        tempText.text = TemperaturePercentage.ToString(); //show the temperature value as text in the display

    }


}
