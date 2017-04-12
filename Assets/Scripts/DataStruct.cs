using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// File: DataStruct.cs \n
/// Date Last Modified: 1/26/2017 \n
/// Description: Gets the values from data generator for each of the different features in the 
/// Heads Up Display in Unity. Gets the battery, altitude, speed, temperature, fan, and gForce data.
/// </summary>

public class DataStruct : MonoBehaviour
{
    public int packetNumber { get; set; }
    /// <summary> get values for battery. </summary>
    public double batteryData { get; set; }
    /// <summary> get values for altitude. </summary>
    public double altitudeData { get; set; }
    /// <summary> get values for airspeed. </summary>
    public double speedData { get; set; }
    /// <summary> get values for temperature. </summary>
    public double tempData { get; set; }
    /// <summary> get values for heading. </summary>
    public string headingData { get; set; }
    /// <summary> get values for fan. </summary>
    public double fanData { get; set; }
    /// <summary> get values for gforce. </summary>
    public float[] gforceData { get; set; }    
}


