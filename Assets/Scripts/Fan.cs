using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// File: Fan.cs \n
/// Date Last Modified: 3/15/2017 \n
/// Description: Fan code to manage the fan graphic. The program
/// locates the required game objects from Unity ("FanFill" and
/// "FanText") in order to change them. The FanFill changes
/// the graphics, and the FanText will display the fan value.
/// </summary>

public class Fan : MonoBehaviour
{
    /// <value> Used to store the the data value for the fan. </value>
    public float fanPercentage;
    /// <value> Representation of the RGBA color space. </value>
    public Color color;
    /// <value> Used to locate the fan image that will be animated based on the percentage. </value>
    public Image fan;
    /// <value> To be used for the textbox in unity. </value>
    public Text text;
    /// <value> Used to delay the loop of fan animation. </value>
    public static int count = 0;

    /// <summary>
    /// Gets the fan value from the data listener.
    /// <param name="newData"> New value coming from data generator. </param>
    /// </summary>
    public void dataListener(DataStruct newData)
    {
        fanPercentage = (int)newData.fanData;
    }

    /// <summary>
    /// Locates and grabs the image and text components from the Unity project for the fan bar and textbox.
    /// </summary>
    public void Start()
    {
        //get value from data receiver
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);

        //find the fan image component in Unity and set the value to zero.
        fan = GameObject.Find("FanFill").GetComponent<Image>();
        fan.fillAmount = 0f;

        //find the textbox component in Unity.
        text = GameObject.Find("FanText").GetComponent<Text>();
    }

    /// <summary>
    /// Changes the color of the fan graphic and text depending on the value of the fan percentage. \n
    /// Red: when the fan is between 0 and 10 percent. \n
    /// Yellow: when the fan is between 10 and 25 percent. \n
    /// Green: when the fan is 25 to 100 percent.
    /// </summary>
    public void changeColor()
    {
        //set color of text and image to red if fan percentage is between 0 and 10
        if (fanPercentage >= 0 && fanPercentage <= 1500)
        {
            fan.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            text.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        }
        //set color of text and image to yellow if fan percentage is between 10 and 25
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

    /// <summary>
    /// Simple loop animation to reduce the percentage value over time.
    /// This function is not used when getting data from generator.
    /// </summary>
    public void reduceFan()
    {
        count++;
        if (count == 20)
        {
            fanPercentage = fanPercentage - 500; //decrement the width of image
            count = 0;
            //reset fan to 100 when it gets to 0 (value doesnt go negative)
            if (fanPercentage < 0)
            {
                fanPercentage = 10000;
            }
        }
    }

    /// <summary>
    /// Update is called to update the graphic and textbox in the heads up display in Unity.
    /// </summary>
    public void Update()
    {
        fan.fillAmount = (float)fanPercentage / 10000; //get fan value from listener, convert to a percentage (x/100)
        //reduceFan(); //calls reduce fan function to show simple animation
        text.text = fanPercentage + " "; //show the fan value as text in the display
        changeColor(); //calls the change color function.
    }
}