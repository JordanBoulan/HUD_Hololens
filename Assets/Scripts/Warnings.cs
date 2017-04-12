using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// File: UDPEvent.cs \n
/// Date Last Modified: 4/1/2017 \n
/// Description: A script to display warnings at the bottom of the screen
/// for the user. Warnings for each component will be called in the 
/// component's script.
/// </summary>

public class Warnings : MonoBehaviour
{
    /// <value> Use to get the warning text from Unity. </value>
    private Text warning;
    /// <value> Warning text. </value>
    private static string warningText;

    /// <summary>
    /// Locates and grabs the text component from the Unity project for the warnings.
    /// </summary>
    void Start()
    {
        //get text component from Unity
        warning = GetComponent<Text>();
        //set text to display nothing.
        warningText = "";
    }

    /// <summary>
    /// Update warning text in Unity to what is needed to be displayed based on the graphics.
    /// </summary>
    void Update()
    {
        warning.text = warningText;
    }

    /// <summary>
    /// Set the warning text to display a message.
    /// </summary>
    /// <param name="warningString"></param>
    public static void setWarning(string warningString)
    {
        warningText = warningString;

    }

    /// <summary>
    /// Set the warning text to display nothing on the screen.
    /// </summary>
    public static void clearWarning()
    {
        warningText = "";
    }
}
