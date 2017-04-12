//gForce.cs

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// File: gForce.cs \n
/// Author: Kevin Do \n
/// Creation date: \n
/// Date Last Modified: 1/26/2017 \n
/// Description: 
/// </summary>

struct gForceValues
{
	public float brake;
	public float accel;
	public float left;		
	public float right;
		
};
	
public class gForce : MonoBehaviour
{

    public float theta;
    public float gForceVal;
    ///Set value for each component to 100
    //public float gForce1 = 100f;
    ///Use for the brake textbox in Unity
    public Text brakeText;
    ///Use for the right textbox in Unity
    public Text rightText;
    ///Use for the left textbox in Unity
    public Text leftText;
    ///Use for the acceleration textbox in Unity
    public Text accelText;
    ///Used to delay the loop of text animation.
    public static int count = 0;
    //Use to get image from Unity
    public RawImage gforceImage;

    //radius of circle ~6.38
	
	gForceValues gForce1;
    private float[] gForceData;

    public void dataListener(DataStruct newData)
    {
		gForceData = newData.gforceData;
        theta = gForceData[0];
		gForceVal = gForceData[1];
    }
	
	public void calculateGforce(float gForceVal, float theta)
	{
        float gForceX = gForceVal * (float)Math.Cos(theta);
        float gForceY = gForceVal * (float)Math.Sin(theta);
		if (theta < 90 && theta >= 0)
		{
            gForce1.brake = gForceY;
            gForce1.accel = 0;
            gForce1.left = 0;
            gForce1.accel = gForceX;
		}
		if (theta >= 90 && theta < 180)
		{
            gForce1.brake = gForceY;
            gForce1.accel = 0;
            gForce1.left = gForceX;
            gForce1.right = 0;
		}
		if (theta >= 180 && theta < 270)
		{
            gForce1.brake = 0;
            gForce1.accel = gForceY;
            gForce1.left = gForceX;
            gForce1.right = 0;
		}
		if (theta >= 270 && theta <= 360)
		{
            gForce1.brake = 0;
            gForce1.accel = gForceY;
            gForce1.left = 0;
            gForce1.right = gForceX;
		}
		
	}


    // Use this for initialization
    void Start()
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

        //image
        gforceImage = GameObject.Find("gforceImage").GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = 0;
        float yPos = 0;
        
        if (gForceVal != 0) { 
            xPos = gForceVal * (float)Math.Cos(theta);
            yPos = gForceVal * (float)Math.Sin(theta);
            }
        

        gforceImage.transform.localPosition = new Vector3(xPos, yPos, 0);
		calculateGforce(gForceVal, theta);
        /*
		gforceImage.transform.localPosition = UnityEngine.Random.insideUnitCircle * 4;
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
        }*/

        //set values to textbox in Unity
        
        brakeText.text = gForce1.brake + "";
        rightText.text = gForce1.right + "";
        leftText.text  = gForce1.left  + "";
        accelText.text = gForce1.accel + "";




    }
}