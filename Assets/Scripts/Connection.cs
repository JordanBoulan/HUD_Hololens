using UnityEngine;
using System.Collections;
using System;
using System.IO;

#if !UNITY_EDITOR
using Windows.Networking.Sockets;
    using Windows.Networking;
    using Newtonsoft.Json.Serialization;
#endif

/// <summary>
/// File: Connection.cs \n
/// Author: Jordan Boulanger \n
/// Creation date: \n
/// Date Last Modified: 1/26/2017 \n
/// Description: UDP Connection initilization and listener. Fires a C# event (udpEvent) 
/// when it recieves data.
/// </summary>
/// 

public class Connection : MonoBehaviour
{
    #if !UNITY_EDITOR
            DatagramSocket socket;
            string listenPort = "5005";



    #endif
        // use this for initialization
    #if !UNITY_EDITOR
        async void Start()
        {
    #endif
    #if UNITY_EDITOR
        void Start()
        {
    #endif

    

    #if !UNITY_EDITOR
                Debug.Log("Waiting for a connection...");

                socket = new DatagramSocket();
                socket.MessageReceived += Socket_MessageReceived;
       

                try
                {
                await socket.BindEndpointAsync(null, listenPort);
                
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                    Debug.Log(SocketError.GetStatus(e.HResult).ToString());
                    return;
                }

                Debug.Log("exit start");
    #endif
        }

    #if !UNITY_EDITOR
            private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
                Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
            {

                try
                {
                    //Read the message that was received from the UDP echo client.
                    Stream streamIn = args.GetDataStream().AsStreamForRead();
                    StreamReader reader = new StreamReader(streamIn);

                    string jsonData = await reader.ReadLineAsync();
                    DataStruct newData = Newtonsoft.Json.JsonConvert.DeserializeObject<DataStruct>(jsonData);
                    UdpEvent.onDataRecieved(newData);
                }

                catch (Exception e)
                {
                    Exception mye = e;
            
                }
            }
    #endif
}