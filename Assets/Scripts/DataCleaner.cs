using UnityEngine;
using System.Collections;

public class DataCleaner : MonoBehaviour {

    public static int packetCounter = 0;
    private static int badPackets {get; set;}
    

    private static int MIN_AIRSPEED = 0;
    private static int MAX_AIRSPEED = 30;

    private static int MIN_ALTITUDE = 0;
    private static int MAX_ALTITUDE = 10;

    private static int MIN_FAN = 1000;
    private static int MAX_FAN = 10000;

    private static int MIN_BATTERY = 0;
    private static int MAX_BATTERY = 100;

    private static int MIN_HEADING = -180;
    private static int MAX_HEADING = 180;

    private static int MIN_GFORCE_ANGLE = 0;
    private static int MAX_GFORCE_ANGLE = 360;

    private static int MIN_GFORCE_RADIUS = 0;
    private static int MAX_GFORCE_RADIUS = 1;

    public static bool checkDataValues(DataStruct newData)
    {
        packetCounter += 1;
        if (packetCounter != newData.packetNumber)
            Warnings.setWarning("Packet(s) Lost: " + Connection.lostPackets);
        if ((newData.speedData <= MIN_AIRSPEED) || (newData.speedData >= MAX_AIRSPEED))
        {
            badPackets += 1;
            return false;
        }
        if ((newData.altitudeData <= MIN_ALTITUDE) || (newData.altitudeData >= MAX_ALTITUDE))
        {
            badPackets += 1;
            return false;
        }
        if ((newData.fanData <= MIN_FAN) || (newData.fanData >= MAX_FAN))
        {
            badPackets += 1;
            return false;
        }
        //if ((newData.headingData <= MIN_HEADING) || (newData.headingData >= MAX_HEADING))
       // {
        //    badPackets += 1;
        //    return false;
        //}
        if ((newData.gforceData[0] <= MIN_GFORCE_ANGLE) || (newData.gforceData[0] >= MAX_GFORCE_ANGLE))
        {
            badPackets += 1;
            return false;
        }
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
    void Start()
    {
        badPackets = 0;
    }
}
