
//http://www.tallior.com/introduction-unity-test-tools/

using NUnit.Framework;

[TestFixture]
public class TemperatureTest{

    [Test]
    public void increaseTemp()
    {
        var temp = new Temperature();

        Temperature.TemperaturePercentage = 40;

        temp.increaseTemp(15f);

        Assert.AreEqual(55f,Temperature.TemperaturePercentage);

    }


}
