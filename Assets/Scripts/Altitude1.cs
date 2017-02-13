using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Altitude1 : MonoBehaviour
{
    public float maxAltitude = 10f;
    public float altitude = 10f;
    public Text AltitudeTextMoving;
    public GameObject altitudeBar;

    // Use this for initialization
    void Start()
    {
        AltitudeTextMoving.text = altitude.ToString();
        InvokeRepeating("ReduceAltitude", 0f, 0.5f);
    }

    void ReduceAltitude()
    {
        altitude -= 1f;
        if (altitude < 0f)
        {
            altitude = 10f;
        }
        SetAltitudeBar(altitude);
        AltitudeTextMoving.text = altitude.ToString();
    }

    public void SetAltitudeBar(float myAltitude)
    {
        //myAltitude is value 0, 1
        float calc_altitude = myAltitude / maxAltitude;
        if (calc_altitude == 0)
        {
            altitudeBar.transform.localScale = new Vector3(1f, 0.001f, 1f);
            //AltitudeTextMoving.transform.localScale = new Vector3(1f, 1000f, 1f);
        }
        else
        {
            altitudeBar.transform.localScale = new Vector3(1f, calc_altitude, 1f);
           // AltitudeTextMoving.transform.localScale = new Vector3(1f, (1 / calc_altitude), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
