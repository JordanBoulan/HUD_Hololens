using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Heading : MonoBehaviour
{

    public GameObject compassCamera;
    public GameObject headingBlock;
    private float currentZ = 0.0f;
    private float currentX = 0.0f;
    RawImage headingTexture;

    // Use this for initialization
    void Start()
    {
        compassCamera = GameObject.FindGameObjectWithTag("CompassCamera");
        headingBlock = GameObject.Find("Heading");
        headingTexture =  headingBlock.GetComponent<RawImage>();
        //uvrect.Position
        
        
    }


    // Update is called once per frame
    void Update()
    {
        currentZ = compassCamera.transform.localRotation.eulerAngles.z;
        currentX = compassCamera.transform.localRotation.eulerAngles.x;
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