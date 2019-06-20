using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Automation;
using System.Windows.Forms;
using Winium.Cruciatus.Core;
using System;

namespace WiniumTest
{
    [TestClass]
    public class DeleteCommisionFromOverviewTest
    {
        [TestMethod]
        public void TestDelete()
        {
            var Dlv005 = new Winium.Cruciatus.Application("C:/Users/alexandru.fleser/Desktop/Onboarding/Alexandru Radu Fleser/Application/DLV_005.solution/Dlv005_UI/bin/Debug/Dlv005_UI.exe");
            Dlv005.Start();

            var winFinder1 = By.Name("Manage external commission requirements").AndType(ControlType.Window);
            var win1 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder1);
            win1.FindElementByUid("DeleteButton").Click();

            SendKeys.SendWait("{ENTER}"); // How to press enter?

            var result = win1.FindElementByUid("OverviewNumbetTextBox");
            var resultText = result.Text();

            Assert.AreEqual("3", resultText);

            Dlv005.Close();

        }
    }
}