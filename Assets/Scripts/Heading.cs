using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Author: Jordan Boulanger
/// Date: 4/11/17
/// Unity C# Script for the heading graphic. Whenever the user looks up or down (pitch rotation) or tilt 
/// their head left or right (roll rotation), the heading graphic in the middle of the HUD will change. 
/// </summary>

public class Heading : MonoBehaviour
{
    /// <value> Will be assigned to the camera's game object in Unity </value>
    public GameObject compassCamera;
    /// <value> Will be assigned to the heading graphic object in Unity </value>
    public GameObject headingBlock;
    /// <value> Z value on the x,y,z plane </value>
    private float currentZ = 0.0f;
    /// <value> X value on the x,y,z plane </value>
    private float currentX = 0.0f;
    /// <value> Will be assigned to the raw image object of the heading graphic </value>
    RawImage headingTexture;

    /// <summary>
    /// Gets the camera and heading (middle of UI) object from Unity and assigns them to a variable.
    /// </summary>
    void Start()
    {
        //get camera
        compassCamera = GameObject.FindGameObjectWithTag("CompassCamera");
        //get heading graphic
        headingBlock = GameObject.Find("Heading");
        //get raw image object of the heading graphic
        headingTexture =  headingBlock.GetComponent<RawImage>();
        //uvrect.Position
        
        
    }

    /// <summary>
    /// This function uses euler angles to determine the rotation on the x and z axis for the heading. 
    /// Whenever the user rotates their head a certain way, the heading graphics will change. The x 
    /// rotation will change depending on the user's pitch (looking up and down), and the z rotation 
    /// will change depending on the user's roll (tilting the head left of right).
    /// </summary>
    void Update()
    {
        //set z component
        currentZ = compassCamera.transform.localRotation.eulerAngles.z;
        //set x component
        currentX = compassCamera.transform.localRotation.eulerAngles.x;
        //using euler angles to transform the graphic
        headingBlock.transform.localRotation = Quaternion.Euler(headingBlock.transform.localRotation.eulerAngles.x, headingBlock.transform.localRotation.eulerAngles.y, -currentZ);

        float newY = 0.414f;
        if (currentX >= 180)
        {
            currentX = Mathf.Abs(currentX - 360);
            newY = 0.414f + (currentX / 10.0f * 0.027f);
  
        }

        else if (currentX < 180)
        {
            newY = 0.414f - (currentX / 10.0f * 0.027f);
        }

        else if (currentX == 0)
        {
            newY = 0.414f;
        }
        Rect newUvRect = new Rect(1, newY, 1, 0.18f);
        headingTexture.uvRect = newUvRect;

    }
}