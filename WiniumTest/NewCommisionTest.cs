using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Automation;
using Winium.Cruciatus.Core;

namespace WiniumTest
{
    [TestClass]
    public class NewCommisionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var Dlv005 = new Winium.Cruciatus.Application("C:/Users/alexandru.fleser/Desktop/Onboarding/Alexandru Radu Fleser/Application/DLV_005.solution/Dlv005_UI/bin/Debug/Dlv005_UI.exe");
            Dlv005.Start();

            var winFinder1 = By.Name("Manage external commission requirements").AndType(ControlType.Window);
            var win1 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder1);
            win1.FindElementByUid("NewButton").Click();

            //var comboBox = win1.FindElementByUid("TestingTypeComboBox").ToComboBox();

            //comboBox.Expand();
            //comboBox.ScrollTo("Driving dynamics").Click();

            // var gridclick = win1.FindElementByUid("dataGridView2").ToDataGrid();

            // gridclick.SelectCell(0,0);

            win1.FindElementByUid("TestingContentTextBox").SetText("Garone");
            win1.FindElementByUid("SortTestsComboBox").SetText("Immendingen");

            win1.FindElementByUid("RoutesTypeComboBox").SetText("Nürburgring");
            win1.FindElementByUid("TestingTypeComboBox").SetText("Full load DL");
            win1.FindElementByUid("SeriesButton").Click();
            var winFinder2 = By.Name("SelectionTable").AndType(ControlType.Window);
            var win2 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder2);
            win2.FindElementByUid("AdoptButton").Click();
            win1.FindElementByUid("CustomerOEButton").Click();
            var win3 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder2);
            win3.FindElementByUid("AdoptButton").Click();
            win1.FindElementByUid("CustomerButton").Click();
            var win5 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder2);
            win5.FindElementByUid("AdoptButton").Click();
            win1.FindElementByUid("ChiefButton").Click();
            var win4 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder2);
            win4.FindElementByUid("AdoptButton").Click();
            win1.FindElementByUid("EngineeringButton").Click();
            var win6 = Winium.Cruciatus.CruciatusFactory.Root.FindElement(winFinder2);
            win6.FindElementByUid("AdoptButton").Click();
            win1.FindElementByUid("DrivingAuthorizationComboBox").SetText("T3");
            win1.FindElementByUid("HVQualificationComboBox").SetText("Hochvolt 3");
            win1.FindElementByUid("SpecialQualificationComboBox").SetText("-");
            win1.FindElementByUid("includeSaturdayworkCheckBox").Click();
            win1.FindElementByUid("includeSundayworkCheckBox").Click();
            win1.FindElementByUid("includeSaturdayworkCheckBox").Click();
            win1.FindElementByUid("dataGridView2").SetFocus();
            win1.FindElementByUid("dataGridView2").SetText(" Dadadadz");

            Console.ReadKey();
        }
    }
}