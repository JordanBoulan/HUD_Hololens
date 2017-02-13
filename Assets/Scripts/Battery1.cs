/* 
 * @author: Kevin Do
 * @discription: create image graphic, and change the size of the width to show battery decreasing
 *  
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battery1 : MonoBehaviour {

    public static int xVal=100;//value of the width of the image
    public Color color;//color of the image
    public static int count = 0;
    private UnityEngine.UI.RawImage battery;
    

    // Use this for initialization
    void Start () {
        battery = GetComponent<RawImage>();
    }

	// Update is called once per frame
	void Update ()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(xVal, 28);//create image size 100x100
        count++;
        if (count == 20)
        {
            xVal = xVal - 5; //decrement the width of image
            count = 0;
            //reset battery to 100 when it gets to 0 (value doesnt go negative)
            if (xVal == 0)
            {
                xVal = 100;
            }
        }
        
        if (xVal >= 0 && xVal <= 10)
        {
            GetComponent<RawImage>().color = new Color32(255, 0, 0, 255); //red
        }
        else if (xVal > 10 && xVal <= 25)
        {
            GetComponent<RawImage>().color = new Color32(251, 255, 0, 255);//yellow
        }
        else if (xVal>25 && xVal<=100)
        {
            GetComponent<RawImage>().color = new Color32(33, 255, 47, 255);//green
        }
    }
    
}
