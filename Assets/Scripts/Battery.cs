using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Battery : MonoBehaviour {

    public static int xVal; //x vallue of image size
    public Color color; //set color to the battery
    private Image battery;
    private Text text;

    
    private void dataListener(DataStruct newData)
    {
        xVal = (int) newData.batteryData;
    }

    // Use this for initialization
    void Start () 
    {
        battery = GameObject.Find("batteryAnimation").GetComponent<Image>();
        battery.fillAmount = 0f;
        text = GameObject.Find("BatteryText").GetComponent<Text>();
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(dataListener);
    }

    // Update is called once per frame
    void Update()
    {


        battery.fillAmount = xVal / 100;

        text.text = xVal + "%";


        if (xVal >= 0 && xVal <= 10)
        {
            battery.GetComponent<RawImage>().color = new Color32(255, 0, 0, 255); //red
        }
        else if (xVal > 10 && xVal <= 25)
        {
            battery.GetComponent<RawImage>().color = new Color32(251, 255, 0, 255);//yellow
        }
        else if (xVal > 25 && xVal <= 100)
        {
            battery.GetComponent<RawImage>().color = new Color32(33, 255, 47, 255);//green
        }


    }  
    
    
}
