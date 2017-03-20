using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Warnings : MonoBehaviour
{
    private Text warning;
    private static string warningText;

    // Use this for initialization
    void Start()
    {
        warning = GetComponent<Text>();
        warningText = "";

    }

    // Update is called once per frame
    void Update()
    {
        warning.text = warningText;
    }

    public static void setWarning(string warningString)
    {
        warningText = warningString;

    }

    public static void clearWarning()
    {
        warningText = "";
    }
}
