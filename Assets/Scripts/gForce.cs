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

public class gForce : MonoBehaviour
{

    public float theta;
    public float gForceVal;
    ///Set value for each component to 100
    public float gForce1 = 100f;
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
    public Image gforceImage;

    //radius of circle ~6.38




    public void dataListener(DataStruct newData)
    {
        //gForceVal = (int)newData.gforce;
        //theta = (int)newData.gforceAngle;
    }


    // Use this for initialization
    void Start()
    {
        //get value from data generator
        //UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);

        //brake text
        brakeText = GameObject.Find("BrakeText").GetComponent<Text>();
        //right text
        rightText = GameObject.Find("RightText").GetComponent<Text>();
        //left text
        leftText = GameObject.Find("LeftText").GetComponent<Text>();
        //acceleration text
        accelText = GameObject.Find("AccelText").GetComponent<Text>();

        //image
        gforceImage = GameObject.Find("gforceImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = 1.710022f;
        float yPos = -0.3299866f;
        //x = gForceVal*cos(theta)
        //y = gForceVal*sin(theta)

        //gforceImage.transform.localPosition = new Vector3(xPos, yPos, 0);
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
        }

        //set values to textbox in Unity
        brakeText.text = gForce1+ "";
        rightText.text = gForce1 + "";
        leftText.text = gForce1 + "";
        accelText.text = gForce1 + "";




    }


}