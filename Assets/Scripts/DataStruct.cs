using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// File: DataStruct.cs \n
/// Author: Jordan Boulanger \n
/// Creation date: \n
/// Date Last Modified: 1/26/2017 \n
/// Description: Gets the values from data generator for each of the different features in the 
/// Heads Up Display in Unity. Gets the battery, altitude, speed, temperature, heading, and fan data
/// </summary>

public class DataStruct : MonoBehaviour
{
    public int packetNumber { get; set; }
    ///get values for battery
    public double batteryData { get; set; }
    ///get values for altitude
    public double altitudeData { get; set; }
    ///get values for speed
    public double speedData { get; set; }
    ///get values for temperature
    public double tempData { get; set; }
    ///get values for heading
    public string headingData { get; set; }
    ///get values for fan
    public double fandata { get; set; }
    ///get values for gforce
    public double gforce { get; set; }
    ///get values for gforceAngle
    public double gforceAngle { get; set; }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

