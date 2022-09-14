using System;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA2;
using FlaUI.UIA3;
using FlaUI.UIA3.Converters;
using NUnit.Framework;

namespace Test_UI;

 public class Tests
{
    private Application app;
    private Window window;
    private ConditionFactory cf = new(new UIA3PropertyLibrary());
    
    [SetUp]
    public void Setup()
    {
        if (app is null)
        {
            app = Application.Launch(@"C:\RUAG\LiveSim\Setup\LiveSimApp\Release\LiveSimApp.exe");// PUT PATH HERE
            window = app.GetMainWindow(new UIA3Automation());
        }
    }

    [Test]
    public void Test1()
    {
        var buttonAvailable = window.FindFirstDescendant(cf.ByName("Save All Configs")).AsButton();
        buttonAvailable.Click();
        var newWindow = app.GetMainWindow(new UIA3Automation());
        var modalWindow = newWindow.ModalWindows[0];

        var yesButton = newWindow.FindFirstDescendant(cf.ByName("Yes"));
        
        Assert.NotNull(modalWindow);
        Assert.AreEqual("Confirmation ...",modalWindow.Title);
        Assert.NotNull(yesButton);
        Assert.AreEqual(modalWindow, yesButton.Parent);
    }
    
    [TearDown]
    public void CloseTests()
    {
        app.Close();
    }
}