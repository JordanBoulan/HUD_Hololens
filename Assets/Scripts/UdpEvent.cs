using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class UdpEvent : MonoBehaviour
{

    public delegate void DataRecievedEvent(DataStruct newData);

    public static event DataRecievedEvent dataRecieved;

    public static void onDataRecieved(DataStruct myNewData)
    {
        if (dataRecieved != null)
        {

            dataRecieved(myNewData);
        }
    }




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
