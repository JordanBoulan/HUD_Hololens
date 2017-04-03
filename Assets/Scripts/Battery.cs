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

public class Battery : MonoBehaviour
{
    ///Set the battery percentage to 100 (max).
    public float batteryPercentage;
    ///Representation of the RGBA color space.
    public Color color;
    ///Used to locate the battery image that will be animated based on the percentage.
    public Image battery;
    ///To be used for the textbox in unity.
    public Text text;
    ///Used to delay the loop of battery animation.
    public static int count = 0;

    /// Gets the battery value from the data listener
    /// <param name="newData"></param>
    public void dataListener(DataStruct newData)
    {
        batteryPercentage = (int)newData.batteryData;
    }

    /// Locates and grabs the image and text components from the Unity project for the battery bar and textbox
    public void Start()
    {
        //get value from data receiver
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);

        //find the battery image component in Unity and set the value to zero.
        battery = GameObject.Find("batteryAnimation").GetComponent<Image>();
        battery.fillAmount = 0f;

        //find the textbox component in Unity.
        text = GameObject.Find("BatteryText").GetComponent<Text>();
    }

    /// Changes the color of the battery graphic and text depending on the value of the battery percentage \n
    /// Red: when the battery is between 0 and 10 percent \n
    /// Yellow: when the battery is between 10 and 25 percent \n
    /// Green: when the battery is 25 to 100 percent
    public void changeColor()
    {
        //set color of text and image to red if battery percentage is between 0 and 10
        if (batteryPercentage >= 0 && batteryPercentage <= 25)
        {
            battery.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            text.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            Warnings.setWarning("Battery: below 25%");
        }
        //set color of text and image to yellow if battery percentage is between 10 and 25
        else if (batteryPercentage > 25 && batteryPercentage <= 50)
        {
            battery.GetComponent<Image>().color = new Color32(251, 255, 0, 255);
            text.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
            Warnings.setWarning("Battery: below 50%");

        }
        //set color of text and image to green
        else
        {
            battery.GetComponent<Image>().color = new Color32(33, 255, 47, 155);
            text.GetComponent<Text>().color = new Color32(33, 255, 47, 255);
            Warnings.clearWarning();
        }
    }

    /// Simple loop animation to reduce the percentage value over time
    /// This function is not used when getting data from generator
    public void reduceBattery()
    {
        count++;
        if (count == 20)
        {
            batteryPercentage = batteryPercentage - 5; //decrement the width of image
            count = 0;
            //reset battery to 100 when it gets to 0 (value doesnt go negative)
            if (batteryPercentage == 0)
            {
                batteryPercentage = 100;
            }
        }
    }

    /// Update is called to update the graphic and textbox in the heads up display in Unity.
    public void Update()
    {	
        battery.fillAmount = (float)batteryPercentage / 100; //get battery value from listener, convert to a percentage (x/100)
        //reduceBattery(); //calls reduce battery function to show simple animation
        text.text = batteryPercentage + "%"; //show the battery value as text in the display
        changeColor(); //calls the change color function.
    }
}