using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// File: Battery.cs \n
/// Date Last Modified: 2/26/2017 \n
/// Description: Battery code to manage the battery graphic. The program
/// locates the required game objects from Unity ("batteryAnimation" and
/// "BatteryText") in order to change them. The batteryAnimation changes
/// the graphics, and the BatteryText will display the battery value.
/// </summary>

public class Battery : MonoBehaviour
{
    /// <value> Used to store the battery percentage. </value>
    public float batteryPercentage;
    /// <value> Representation of the RGBA color space. </value>
    public Color color;
    /// <value> Used to locate the battery image that will be animated based on the percentage. </value>
    public Image battery;
    /// <value> To be used for the textbox in unity. </value>
    public Text text;
    /// <value> Used to delay the loop of battery animation. </value>
    public static int count = 0;

    /// <summary>
    /// Gets the battery value from the data listener.
    /// <param name="newData"> New value coming from data generator. </param>
    /// </summary>
    public void dataListener(DataStruct newData)
    {
        batteryPercentage = (int)newData.batteryData;
    }

    /// <summary> 
    /// Locates and grabs the image and text components from the Unity project for the battery bar and textbox.
    /// </summary>
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
    /// <summary>
    /// Changes the color of the battery graphic and text depending on the value of the battery percentage. \n
    /// If the battery is at a certain value, it will display a warning text towards the bottom of the UI. \n
    /// Red: When the battery is between 0 and 10 percent. \n
    /// Yellow: When the battery is between 10 and 25 percent. \n
    /// Green: When the battery is 25 to 100 percent.
    /// </summary>
    public void changeColor()
    {
        //set color of text and image to red if battery percentage is between 0 and 25
        //displays a warning in the UI
        if (batteryPercentage >= 0 && batteryPercentage <= 25)
        {
            battery.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            text.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            Warnings.setWarning("WARNING: Battery below 25%");
        }
        //set color of text and image to yellow if battery percentage is between 25 and 50
        //displays a warning in the UI
        else if (batteryPercentage > 25 && batteryPercentage <= 50)
        {
            battery.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
            text.GetComponent<Text>().color = new Color32(255, 255, 0, 255);
            Warnings.setWarning("WARNING: Battery below 50%");

        }
        //set color of text and image to green
        //also clears warning message
        else
        {
            battery.GetComponent<Image>().color = new Color32(32, 255, 46, 155);
            text.GetComponent<Text>().color = new Color32(32, 255, 46, 255);
            Warnings.clearWarning();
        }
    }
    /// <summary>
    /// Simple loop animation to reduce the percentage value over time.
    /// This function is not used when getting data from generator.
    /// </summary>
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

    /// <summary>
    /// Update is called to update the graphic and textbox in the heads up display in Unity.
    /// </summary>
    public void Update()
    {	
        battery.fillAmount = (float)batteryPercentage / 100; //get battery value from listener, convert to a percentage (x/100)
        //reduceBattery(); //calls reduce battery function to show simple animation
        text.text = batteryPercentage + "%"; //show the battery value as text in the display
        changeColor(); //calls the change color function.
    }
}