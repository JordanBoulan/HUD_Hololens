/* 
 * @author: Kevin Do
 * @discription: Get battery text component, and changes the number value
 *  
 */
 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatteryManager : MonoBehaviour
{
    public static int battery; // The battery.
    public static int count;
    Text text;                 // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();
        // Set value of the battery.
        battery = 100;
    }


    void Update()
    {
        // Set the displayed text battery value then % symbol.
        text.text = battery + "%";
        count++;
        if (count == 20)
        {
            battery = battery - 5;
            //reset number to 100 when battery gets to 0 (value doesnt go negative)
            if (battery == 0)
            {
                battery = 100;
            }
            count = 0;
        }

    }
}