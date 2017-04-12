using UnityEngine;
using System.Collections;
using System;
using System.IO;

/// <summary>
/// File: UDPEvent.cs \n
/// Date Last Modified: 1/26/2017 \n
/// Description: An event to notifiy the other Unity scripts 
/// that data has been recieved
/// </summary>


public class UdpEvent : MonoBehaviour
{
    /// <value> event type </value>
    public delegate void DataRecievedEvent(DataStruct newData);
    /// <value> event instance </value>
    public static event DataRecievedEvent dataRecieved;

    /// <summary>
    /// Get data from the data listener.
    /// </summary>
    /// <param name="myNewData"></param>
    public static void onDataRecieved(DataStruct myNewData)  // function to fire event
    {
        if (dataRecieved != null)
        {
            dataRecieved(myNewData);
        }
    }

	
	/*
	in other unity script register a local function to be called when app recieves data:
	
	void start(){
		udpEvent.dataRecieved += new DataRecievedEvent(nameofLocalFunction);
	}
	void nameofLocalFunction(DataStruct newData){
		newData.altitude = myLocalalitude
	}
	*/
}
