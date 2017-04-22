using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Allen Black
/// File: DataCleaner.cs \n
/// Date Last Modified: 4/19/2017 \n
/// Description: This program checks for any bad packets that were sent from the data generator in \n
/// the Raspberry Pi. If a value outside its range is sent over, it will report it as a bad packet, \n
/// and ignore the value.
/// </summary>


public class DataCleaner : MonoBehaviour {

    /// <value> Use to count the number of packets coming from the data generator. </value>
    public static int packetCounter = 0;
    /// <value> Use to count the number of bad packets (data out of range) from data generator. </value>
    private static int badPackets {get; set;}

    /// <value> Minimum battery value allowed (value cant be less than 0). </value>
    private static int MIN_BATTERY = 0;
    /// <value> Maximum battery value allowed (value cannot be greater than 100). </value>
    private static int MAX_BATTERY = 100;

    /// <value> Minumum airspeed value allowed (value cant be less than 0). </value>
    private static int MIN_AIRSPEED = 0;
    /// <value> Maximum airspeed value allowed (value cant be greater than 30). </value>
    private static int MAX_AIRSPEED = 30;

    /// <value> Minimum altitude value allowed (value cant be less than 0). </value>
    private static int MIN_ALTITUDE = 0;
    /// <value> Maximum altitude value allowed (value cant be greater than 10). </value>
    private static int MAX_ALTITUDE = 10;

    /// <value> Minimum value set for Fan speed. </value>
    private static int MIN_FAN = 1000;
    /// <value> Maximum value for the Fan speed. </value>
    private static int MAX_FAN = 10000;

    /// <value> Minimum temperature value allowed (value cant be less than 0). </value>
    private static int MIN_TEMPERATURE = 0;
    /// <value> Maximum temperature value allowed (value cannot be greater than 120). </value>
    private static int MAX_TEMPERATURE = 120;

    /// <value> Minimum angle of the GForce. </value>
    private static int MIN_GFORCE_ANGLE = 0;
    /// <value> Maximum angle of the GForce. </value>
    private static int MAX_GFORCE_ANGLE = 360;

    /// <value> Minimum radius of the GForce. </value>
    private static int MIN_GFORCE_RADIUS = 0;
    /// <value> Maximum radius of the GForce. </value>
    private static int MAX_GFORCE_RADIUS = 1;

    /// <summary>
    /// For each data value coming in from the data generator, this function will check whether or not
    /// the value is within the range. If the value is outside the range, it will be counted as a bad 
    /// packet, and ignored.
    /// </summary>
    /// <param name="newData"> gets the data for each graphic from the data generator. </param>
    /// <returns> returns false if the values from the data generator are ouside the limits. </returns>
    public static bool checkDataValues(DataStruct newData)
    {
        packetCounter += 1;
        //gives error
        //if (packetCounter != newData.packetNumber)
        //    Warnings.setWarning("Packet(s) Lost: " + Connection.lostPackets);

        //check battery data
        if ((newData.batteryData <= MIN_BATTERY) || (newData.batteryData >= MAX_BATTERY))
        {
            badPackets += 1;
            return false;
        }
        //check airspeed data
        if ((newData.speedData <= MIN_AIRSPEED) || (newData.speedData >= MAX_AIRSPEED))
        {
            badPackets += 1;
            return false;
        }
        //check altitude data
        if ((newData.altitudeData <= MIN_ALTITUDE) || (newData.altitudeData >= MAX_ALTITUDE))
        {
            badPackets += 1;
            return false;
        }
        //check fan data
        if ((newData.fanData <= MIN_FAN) || (newData.fanData >= MAX_FAN))
        {
            badPackets += 1;
            return false;
        }
        //check temperature data
        if ((newData.tempData <= MIN_TEMPERATURE) || (newData.tempData >= MAX_TEMPERATURE))
        {
            badPackets += 1;
            return false;
        }
        //check gforce angle data
        if ((newData.gforceData[0] <= MIN_GFORCE_ANGLE) || (newData.gforceData[0] >= MAX_GFORCE_ANGLE))
        {
            badPackets += 1;
            return false;
        }
        //check gforce radius data
        if ((newData.gforceData[1] <= MIN_GFORCE_RADIUS) || (newData.gforceData[1] >= MAX_GFORCE_RADIUS))
        {
            badPackets += 1;
            return false;
        }
        else
        {
            Warnings.clearWarning();
            return true;
        }
    }

    /// <summary>
    /// Sets the number of bad packets to 0. The value will increase if a value outside of its range is sent.
    /// </summary>
    void Start()
    {
        badPackets = 0;
    }
}
