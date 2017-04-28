using UnityEngine;
using NSubstitute;
using UnityEditor;
using NUnit.Framework;
using UnityEngine.UI;

public class HololensUnitTests {

    [Test]
    public void batteryTest()
    {
        var myBattery = Substitute.For<Battery>();
       
        myBattery.Start();
        myBattery.batteryPercentage = 10;
        myBattery.changeColor();
        Color tst = new Color32(255, 0, 0, 255);
        Assert.AreEqual(tst, myBattery.battery.color);  
    }



    
    
   // Example Test

    /*
    [Test]
	public void EditorTest()
	{
		//Arrange
		var gameObject = new GameObject();

		//Act
		//Try to rename the GameObject
		var newGameObjectName = "My game object";
		gameObject.name = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, gameObject.name);
	}
   */
}
