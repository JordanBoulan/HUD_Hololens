//UDPEvent.cs

using UnityEngine;
using System.Collections;
using System;
using System.IO;

/*
Author: Jordan Boulanger
An event to notifiy the other Unity scripts that data has been recieved
*/

public class UdpEvent : MonoBehaviour
{

    public delegate void DataRecievedEvent(DataStruct newData); // event type

    public static event DataRecievedEvent dataRecieved;  //event instance
 
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



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
