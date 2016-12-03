using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Altitude : MonoBehaviour
{
    public float maxAltitude = 10f;
    public float altitude = 10f;
    public Text AltitudeTextMoving;
    public GameObject altitudeBar;

    private void myDataUpdater(DataStruct newData)
    {
        altitude = (float) newData.altitudeData;
    }

    // Use this for initialization
    void Start()
    {
        UdpEvent.dataRecieved += new UdpEvent.DataRecievedEvent(myDataUpdater);
    }

   

    public void SetAltitudeBar(float myAltitude)
    {
        //myAltitude is value 0, 1
        float calc_altitude = myAltitude / maxAltitude;
        if (calc_altitude == 0)
        {
            altitudeBar.transform.localScale = new Vector3(1f, 0.001f, 1f);
            AltitudeTextMoving.transform.localScale = new Vector3(1f, 1000f, 1f);
        }
        else
        {
            altitudeBar.transform.localScale = new Vector3(1f, calc_altitude, 1f);
            AltitudeTextMoving.transform.localScale = new Vector3(1f, (1 / calc_altitude), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAltitudeBar(altitude);
        AltitudeTextMoving.text = altitude.ToString() + " m";
    }
}
