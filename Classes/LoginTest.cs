using System;
using NUnit.Framework;

namespace TestProj.Classes
{
    public class LoginTest : Interfaces.ITestSecurity
    {
        public void TestLogin(Classes.Browser browserInstance)
        {
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);

            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
            //automater.TakeScreenshot("c:\\LogonScreen.jpg");

            browserInstance.Instance.Assert.Exists("#txtPassword");
            browserInstance.Instance.Assert.Exists("#txtUserID");

            browserInstance.Instance.Enter(Properties.Settings.Default.Username).In("#txtUserID");
            //automater.Enter(Properties.Settings.Default.Username).In("input[name='txtUserID']");
            browserInstance.Instance.Enter(Properties.Settings.Default.Password).In("input[name='txtPassword']");
            browserInstance.Instance.Click("input[name='btnSubmit'");

            browserInstance.Instance.Assert.Url(Properties.Settings.Default.URL);
            //automater.Assert.Exists("li.n29d-nav-create-profile > a");
            browserInstance.Instance.Assert.Text("Tony Van der Linden (tonyv@bbd.co.za)").In("#ctl00_lblPerson");
        }

        public void TestFailedLogin(Classes.Browser browserInstance)
        {
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
            //automater.TakeScreenshot("c:\\LogonScreen.jpg");

            browserInstance.Instance.Assert.Exists("#txtPassword");
            browserInstance.Instance.Assert.Exists("#txtUserID");

            browserInstance.Instance.Enter(Properties.Settings.Default.Username).In("#txtUserID");
            //automater.Enter(Properties.Settings.Default.Username).In("input[name='txtUserID']");
            browserInstance.Instance.Enter("FailedPasswor").In("input[name='txtPassword']");
            browserInstance.Instance.Click("input[name='btnSubmit'");
            //automater.Assert.Exists("li.n29d-nav-create-profile > a");
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
            //automater.Assert.Text("Invalid credentials - Please try again").In("#lblWarn");
        }

        //int logoffImageCounter = 0;
        public void TestLogoff(Classes.Browser browserInstance)
        {
            //automater.TakeScreenshot(string.Format("c:\\LogoffScreen{0}.jpg",logoffImageCounter++));

            var logoffButton = browserInstance.Instance.Find("#ctl00_RightContent_hplLogOff");

            //automater.Upload(logoffButton, "c:\\logoffbutton.txt");

            if (logoffButton != null)
                browserInstance.Instance.Click("#ctl00_RightContent_hplLogOff");
            //automater.Assert.Exists("li.n29d-nav-create-profile > a");
            //automater.Assert.Url(Properties.Settings.Default.LogonURL);
        }
    }
}
