using System.Threading;
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
    private ConditionFactory cf = new(new UIA3PropertyLibrary());
    
    [SetUp]
    public void Setup()
    {
        if (app is null)
        {
            // app = Application.Attach(2084);  PUT ProcesID
            app = Application.Launch("");// PUT PATH HERE
            window = app.GetMainWindow(new UIA3Automation());
        }
    }

    [Test]
    public void TestSaveAllConfigs()
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
    
    [Test]
    public void TestRunGateways()
    {
        Thread.Sleep(6000);
        var tabItems = window.FindFirstDescendant(cf.ByName("Window"));
        tabItems.Click();

        var newWindow = app.GetMainWindow(new UIA3Automation());
        var gatewayButton = newWindow.FindFirstDescendant(cf.ByName("Gateway Manager")).AsButton();
        gatewayButton.Click();

        newWindow = app.GetMainWindow(new UIA3Automation());
        var gatewayTabItem = newWindow
            .FindFirstDescendant(cf.ByName("Gateway Manager")).AsTabItem();
        gatewayTabItem.Click();
        
        
        newWindow = app.GetMainWindow(new UIA3Automation());
        var allButton = newWindow.FindFirstDescendant(cf.ByName("All"));
        
        Assert.IsTrue(allButton.IsAvailable);
        Assert.IsTrue(allButton.IsEnabled);
    }
    
    [TearDown]
    public void CloseTests()
    {
        //app.Close();
    }
}