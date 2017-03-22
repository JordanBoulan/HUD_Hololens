using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// File: Battery.cs \n
/// Creation date: \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Battery code to manage the battery graphic. The program
/// locates the required game objects from Unity ("batteryAnimation" and
/// "BatteryText") in order to change them. The batteryAnimation changes
/// the graphics, and the BatteryText will display the battery value.
/// </summary>

public class Fan : MonoBehaviour
{
    ///Set the fan percentage to 100 (max).
    public float fanPercentage = 10000f;
    ///Representation of the RGBA color space.
    public Color color;
    ///Used to locate the battery image that will be animated based on the percentage.
    public Image fan;
    ///To be used for the textbox in unity.
    public Text text;
    ///Used to delay the loop of battery animation.
    public static int count = 0;

    /// Gets the battery value from the data listener
    /// <param name="newData"></param>
    //public void dataListener(DataStruct newData)
    //{
    //    fanPercentage = (int)newData.fandata;
    //}

    /// Locates and grabs the image and text components from the Unity project for the battery bar and textbox
    public void Start()
    {
        //get value from data receiver
        //UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);

        //find the battery image component in Unity and set the value to zero.
        fan = GameObject.Find("FanFill").GetComponent<Image>();
        fan.fillAmount = 0f;

        //find the textbox component in Unity.
        text = GameObject.Find("FanText").GetComponent<Text>();
    }

    /// Changes the color of the battery graphic and text depending on the value of the battery percentage \n
    /// Red: when the battery is between 0 and 10 percent \n
    /// Yellow: when the battery is between 10 and 25 percent \n
    /// Green: when the battery is 25 to 100 percent
    public void changeColor()
    {
        //set color of text and image to red if battery percentage is between 0 and 10
        if (fanPercentage >= 0 && fanPercentage <= 1500)
        {
            fan.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            text.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        }
        //set color of text and image to yellow if battery percentage is between 10 and 25
        else if (fanPercentage > 1500 && fanPercentage <= 3000)
        {
            fan.GetComponent<Image>().color = new Color32(251, 255, 0, 255);
            text.GetComponent<Text>().color = new Color32(251, 255, 0, 255);
        }
        //set color of text and image to green
        else
        {
            fan.GetComponent<Image>().color = new Color32(33, 255, 47, 155);
            text.GetComponent<Text>().color = new Color32(33, 255, 47, 255);
        }
    }

    /// Simple loop animation to reduce the percentage value over time
    /// This function is not used when getting data from generator
    public void reduceFan()
    {
        count++;
        if (count == 20)
        {
            fanPercentage = fanPercentage - 200; //decrement the width of image
            count = 0;
            //reset battery to 100 when it gets to 0 (value doesnt go negative)
            if (fanPercentage < 0)
            {
                fanPercentage = 10000;
            }
        }
    }

    /// Update is called to update the graphic and textbox in the heads up display in Unity.
    public void Update()
    {
        fan.fillAmount = (float)fanPercentage / 10000; //get battery value from listener, convert to a percentage (x/100)
        reduceFan(); //calls reduce battery function to show simple animation
        text.text = fanPercentage + " "; //show the battery value as text in the display
        changeColor(); //calls the change color function.
    }
}