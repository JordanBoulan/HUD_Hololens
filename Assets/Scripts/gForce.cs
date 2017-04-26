//gForce.cs

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Author: Allen Black, Kevin Do
/// File: gForce.cs \n
/// Date Last Modified: 3/29/2017 \n
/// Description: GForce code to manage the GForce graphic. the program
/// locates the following game objects from Unity: "BrakeText", "RightText", 
/// "LeftText", "AccelText", and "gforceDot". The gForceDot is calculated 
/// by taking the gForce value and theta value from the data generator, and 
/// finding the x and y values using sine and cosine. Those values are then
/// set to the brake, right, left, and acceleration data depending on its 
/// location on the x y plane.
/// </summary>

/// <summary>
/// Enclose the 4 different variables for the gForce values.
/// </summary>
struct gForceValues
{
    /// <value> Brake value </value>
    public float brake;
    /// <value> Acceleration value </value>
    public float accel;
    /// <value> Left value </value>
    public float left;
    /// <value> Right value </value>
    public float right;
};

public class gForce : MonoBehaviour
{
   

    /// <value> The theta angle for gforce. </value>  
    public float theta;
    /// <value> The gForce value. </value>  
    public float gForceVal;
    /// <value> Use for the brake text in Unity. </value>
    private Text brakeText;
    /// <value> Use for the right text in Unity. </value>
    private Text rightText;
    /// <value> Use for the left text in Unity. </value>
    private Text leftText;
    /// <value> Use for the acceleration text in Unity. </value>
    private Text accelText;
    /// <value> Used to get image from Unity. </value>
    private RawImage gforceImage;
    /// <value> Used to set values of teh gForce. </value>
    gForceValues gForce1;
    /// <value> Saves values for the gFroce data. </value>
    private float[] gForceData;

    /// <summary>
    /// Gets the gForce and theta value from the data listener.
    /// <param name="newData"> New value coming from data generator. </param>
    /// </summary>
    public void dataListener(DataStruct newData)
    {
		gForceData = newData.gforceData;
        theta = gForceData[0];
		gForceVal = gForceData[1];
    }

    /// <summary>
    /// This function calculates the x and y values for the gForce, then determines the brake, accelerataion, left, and right values.
    /// </summary>
    /// <param name="gForceVal"> The gForce value from data generator. </param>
    /// <param name="theta"> The theta value from data generator. </param>
    public void calculateGforce(float gForceVal, float theta)
	{
        float gForceX = Math.Abs(gForceVal * (float)Math.Cos(theta));
        float gForceY = Math.Abs(gForceVal * (float)Math.Sin(theta));
        //quadrant 1
        if (theta >= 0 && theta < 90)
		{
            gForce1.brake = gForceY;
            gForce1.accel = 0;
            gForce1.left = 0;
            gForce1.right = gForceX;
		}
        //quadrant 2
		if (theta >= 90 && theta < 180)
		{
            gForce1.brake = gForceY;
            gForce1.accel = 0;
            gForce1.left = gForceX;
            gForce1.right = 0;
		}
        //quadrant 3
		if (theta >= 180 && theta < 270)
		{
            gForce1.brake = 0;
            gForce1.accel = gForceY;
            gForce1.left = gForceX;
            gForce1.right = 0;
		}
        //quadrant 4
		if (theta >= 270 && theta <= 360)
		{
            gForce1.brake = 0;
            gForce1.accel = gForceY;
            gForce1.left = 0;
            gForce1.right = gForceX;
		}		
	}

    /// <summary>
    /// Locates and grabs the image and text components from the Unity project for the gForce dot and textboxes.
    /// </summary> 
    public void Start()
    {
        //get value from data generator
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);
        //brake text
        brakeText = GameObject.Find("BrakeText").GetComponent<Text>();
        //right text
        rightText = GameObject.Find("RightText").GetComponent<Text>();
        //left text
        leftText = GameObject.Find("LeftText").GetComponent<Text>();
        //acceleration text
        accelText = GameObject.Find("AccelText").GetComponent<Text>();
        //get dot image
        gforceImage = GameObject.Find("gforceImage").GetComponent<RawImage>();
    }

    /// <summary>
    /// Update is called to set the values for the circle graphic, and text boxes.
    /// The function also sets the x and y values when needed.
    /// </summary>
    public void Update()
    {
        //center of the gForce circle texture
        float xPos = 0;
        float yPos = 0;
        
        //if the gForce value is not 0, find the x and y value from the data generator.
        if (gForceVal != 0) {
            xPos = gForceVal * (float)Math.Cos(theta) * 95f;
            yPos = gForceVal * (float)Math.Sin(theta) * 95f;
        }
        
        gforceImage.transform.localPosition = new Vector3(xPos, yPos, 0);
		calculateGforce(gForceVal, theta);

        //set values to textbox in Unity
        brakeText.text = gForce1.brake + "";
        rightText.text = gForce1.right + "";
        leftText.text  = gForce1.left  + "";
        accelText.text = gForce1.accel + "";
    }

}