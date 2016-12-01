using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class rotate : MonoBehaviour
{
    public float zVal=1;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(Vector3.forward, Time.deltaTime, Space.Self);
        transform.Rotate(0,0,2);

        //if (zVal > 45)
        //{
        //    transform.Rotate(0, 0, -1);
       // }
        

        //if (zVal > 45)//if zRotate > 45 degrees, rotate back the other way.
        //{
        //    transform.Rotate(Vector3.back, Time.deltaTime, Space.Self);
        //}




    }

}
