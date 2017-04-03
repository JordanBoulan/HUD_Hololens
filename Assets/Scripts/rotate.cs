//https://forum.unity3d.com/threads/is-there-an-horizon-line.23064/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class rotate : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //transform.Rotate(Vector3.forward, Time.deltaTime, Space.Self);
        transform.Rotate(0,0,-2);
        //transform.rotation = Random.rotation;



    }

}
