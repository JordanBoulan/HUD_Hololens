using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Author: Kevin Do
/// File: Temperature.cs \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Temperature code to manage the temperature graphic. The 
/// program locates the required game objects from Unity ("TemperatureFill" 
/// and "TemperatureText") in order to change them. The TemperatureFill will
/// manage the graphics while TemperatureText will display the value to the text box
/// </summary>

public class Temperature : MonoBehaviour
{
    /// <value> Stores the data value for temperature. </value>
    public static float TemperatureDegree; //x value of image size
    /// <value> Used to locate the temperature image in Unity to be animated. </value>
    public Image temperatureImage;
    /// <value> Used to locate the textbox in Unity. </value>
    public Text temperatureText;
    /// <value> Used to delay the loop of the animation. </value>
    public static int count = 0;

    /// <summary>
    /// Gets the temperature value from the data listener
    /// <param name="newData"> New value coming from data generator. </param>
    /// </summary>
    public void dataListener(DataStruct newData)
    {
        TemperatureDegree = (int)newData.tempData;
    }

    /// <summary> 
    /// Locates and grabs the image and text components from the Unity project for the temperature graphic and textbox 
    /// </summary>
    public void Start()
    {
        //get value from data generator
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);

        //find the temperature image component in Unity and set the value to zero.
        temperatureImage = GameObject.Find("TemperatureFill").GetComponent<Image>();
        temperatureImage.fillAmount = 0f;

        //find the textbox component in Unity.
        temperatureText = GameObject.Find("TemperatureText").GetComponent<Text>();
    }

    /// <summary>
    /// Update is called to update the temperature graphic and textbox in the heads up display in Unity.
    /// </summary>
    public void Update()
    {
        temperatureImage.fillAmount = (float)TemperatureDegree / 100; //get battery value from listener, convert to a percentage (x/100)
        temperatureText.text = TemperatureDegree.ToString(); //show the temperature value as text in the display
    }
}
