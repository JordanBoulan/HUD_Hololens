//gForce.cs

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
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

//****
	
public class gForce : MonoBehaviour
{
    //does this have to be outside the class above? @****
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

    /// <value> The theta angle for gforce. </value>  
    public float theta;
    /// <value> The gForce value. </value>  
    public float gForceVal;
    /// <value> Use for the brake text in Unity. </value>
    public Text brakeText;
    /// <value> Use for the right text in Unity. </value>
    public Text rightText;
    /// <value> Use for the left text in Unity. </value>
    public Text leftText;
    /// <value> Use for the acceleration text in Unity. </value>
    public Text accelText;
    /// <value> Use to delay the loop of text animation. </value>
    public static int count = 0;
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
    /// <param name="gForceVal"></param>
    /// <param name="theta"></param>
	public void calculateGforce(float gForceVal, float theta)
	{
        float gForceX = gForceVal * (float)Math.Cos(theta);
        float gForceY = gForceVal * (float)Math.Sin(theta);
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
        gforceImage = GameObject.Find("gforceDot").GetComponent<RawImage>();
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
            xPos = gForceVal * (float)Math.Cos(theta);// * 6.05f;
            yPos = gForceVal * (float)Math.Sin(theta);// * 6.05f;
        }
        

        //gforceImage.transform.localPosition = new Vector3(xPos, yPos, 0);
		calculateGforce(gForceVal, theta);
        /*
        // set dot in gForce to a random location with a radius of 4.
		gforceImage.transform.localPosition = UnityEngine.Random.insideUnitCircle * 4;
        
        //decrease the value of the text for brake, right, left, and acceleration.
        count++;
        if (count == 20)
        {
            gForce1 = gForce1 - 5; //decrement the width of image
            count = 0;
            //reset battery to 100 when it gets to 0 (value doesnt go negative)
            if (gForce1 == 0)
            {
                gForce1 = 100;
            }
        }
        */

        //set values to textbox in Unity
        brakeText.text = gForce1.brake + "";
        rightText.text = gForce1.right + "";
        leftText.text  = gForce1.left  + "";
        accelText.text = gForce1.accel + "";
    }

}