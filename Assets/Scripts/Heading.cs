using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Heading : MonoBehaviour
{

    public GameObject compassCamera;
    public GameObject headingBlock;
    private float currentZ = 0.0f;

    // Use this for initialization
    void Start()
    {
        compassCamera = GameObject.FindGameObjectWithTag("CompassCamera");
        headingBlock = GameObject.Find("Heading");
    }


    // Update is called once per frame
    void Update()
    {
        currentZ = compassCamera.transform.localRotation.eulerAngles.z;
        headingBlock.transform.localRotation = Quaternion.Euler(headingBlock.transform.localRotation.eulerAngles.x, headingBlock.transform.localRotation.eulerAngles.y, -currentZ);

        

    }
}