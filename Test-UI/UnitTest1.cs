using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using NUnit.Framework;

namespace Test_UI;

 public class Tests
{
    private Application app;
    private Window window;
    
    [SetUp]
    public void Setup()
    {
        if (app is null)
        {
            app = Application.Launch(@"");// PUT PATH HERE
            window = app.GetMainWindow(new UIA3Automation());
        }
    }

    [Test]
    public void Test1()
    {
        ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
        var buttonAvailable = window.FindFirstDescendant(cf.ByName("Save All Configs")).AsButton().IsAvailable;
        Assert.AreEqual(true, buttonAvailable);
        app.Close();
    }

    [TearDown]
    public void CloseTests()
    {
        app.Close();
    }
}