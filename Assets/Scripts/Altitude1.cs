using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Altitude1 : MonoBehaviour
{
    public static float maxAltitude = 10f;
    public static float altitude = 10f;
    public Text AltitudeTextMoving;
    private Image altitudeBar;


    // Use this for initialization
    void Start()
    {
        altitudeBar = GameObject.Find("AltitudeBarFill").GetComponent<Image>();
        altitudeBar.fillAmount = 0f;
        
        AltitudeTextMoving = GameObject.Find("AltitudeText").GetComponent<Text>();
        AltitudeTextMoving.text = altitude.ToString();
        InvokeRepeating("ReduceAltitude", 0f, 0.5f);
    }

    void ReduceAltitude()
    {
        altitudeBar.fillAmount = altitude / 100;
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
        altitudeBar.fillAmount = myAltitude / maxAltitude;
        
    }
 
    // Update is called once per frame
    void Update()
    {
       
    }
}
