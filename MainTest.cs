using System;
using NUnit.Framework;
using System.Threading;

namespace TestProj
{
    [TestFixture, Description("Main test"), Category("functional test")]
    public class MainTest
    {
        Classes.Browser automater;

        [TestFixtureSetUp]
        public void Initialise()
        {
            automater = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
		int i = 0;
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            automater.Instance.Dispose();
        }

        [Test,Description("Function 1"),Repeat(2)]
        public void TestMethod1()
        {
            automater.Navigate(new Uri(Properties.Settings.Default.URL));
            Classes.LoginTest login = new Classes.LoginTest();
            login.TestFailedLogin(automater.Instance);
            login.TestLogin(automater.Instance);

            login.TestLogoff(automater.Instance);
        }
    }
}
