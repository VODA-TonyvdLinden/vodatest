﻿using System;
using NUnit.Framework;

namespace TestProj.Classes
{
    public class LoginTest
    {
        public void TestLogin(FluentAutomation.Interfaces.IActionSyntaxProvider automater)
        {
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);

            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
            //automater.TakeScreenshot("c:\\LogonScreen.jpg");

            automater.Assert.Exists("#txtPassword");
            automater.Assert.Exists("#txtUserID");

            automater.Enter(Properties.Settings.Default.Username).In("#txtUserID");
            //automater.Enter(Properties.Settings.Default.Username).In("input[name='txtUserID']");
            automater.Enter(Properties.Settings.Default.Password).In("input[name='txtPassword']");
            automater.Click("input[name='btnSubmit'");

            automater.Assert.Url(Properties.Settings.Default.URL);
            //automater.Assert.Exists("li.n29d-nav-create-profile > a");
            automater.Assert.Text("Tony Van der Linden (tonyv@bbd.co.za)").In("#ctl00_lblPerson");
        }

        public void TestFailedLogin(FluentAutomation.Interfaces.IActionSyntaxProvider automater)
        {
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
            //automater.TakeScreenshot("c:\\LogonScreen.jpg");

            automater.Assert.Exists("#txtPassword");
            automater.Assert.Exists("#txtUserID");

            automater.Enter(Properties.Settings.Default.Username).In("#txtUserID");
            //automater.Enter(Properties.Settings.Default.Username).In("input[name='txtUserID']");
            automater.Enter("FailedPasswor").In("input[name='txtPassword']");
            automater.Click("input[name='btnSubmit'");
            //automater.Assert.Exists("li.n29d-nav-create-profile > a");
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
            //automater.Assert.Text("Invalid credentials - Please try again").In("#lblWarn");
        }

        int logoffImageCounter = 0;
        public void TestLogoff(FluentAutomation.Interfaces.IActionSyntaxProvider automater)
        {
            //automater.TakeScreenshot(string.Format("c:\\LogoffScreen{0}.jpg",logoffImageCounter++));

            var logoffButton = automater.Find("#ctl00_RightContent_hplLogOff");

            //automater.Upload(logoffButton, "c:\\logoffbutton.txt");

            if (logoffButton != null)
                automater.Click("#ctl00_RightContent_hplLogOff");
            //automater.Assert.Exists("li.n29d-nav-create-profile > a");
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
        }
    }
}
