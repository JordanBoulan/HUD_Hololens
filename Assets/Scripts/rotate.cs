using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// File: Rotate.cs \n
/// Date Last Modified: 2/18/2017 \n
/// Description: Simple rotate script to rotate an object clockwise.
/// Currently set to the images components in the fanSpinning game
/// object for the rotations you see in the HUD
/// </summary>

public class Rotate : MonoBehaviour
{
    /// <value> Set the zVal for rotation. (-) value would make the image rotate clockwise, 
    /// while a (+) value would make it rotate counter-clockwise. The greater the value,
    /// the faster it would rotate. 
    /// </value>
    private float zVal = -2;

    /// <summary>
    /// Function to rotate the object. 
    /// </summary>
    public void Update() {
        //rotates object clockwise
        transform.Rotate(0, 0, zVal);
    }
}
