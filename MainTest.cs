using System;
using NUnit.Framework;
using System.Threading;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace TestProj
{
    //[TestFixture, Description("Main test"), Category("functional test")]
    public class MainTest : Interfaces.ITestUnit
    {
        //[Test,Description("Function 1"),Repeat(2)]
        public void TestMethod(Classes.Browser automator)
        {

            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<Interception>();
            container.RegisterType<Interfaces.ITestSecurity, Classes.LoginTest>(
              new Interceptor<InterfaceInterceptor>(),
              new InterceptionBehavior<Classes.Timer>());


            automator.Navigate(new Uri(Properties.Settings.Default.LogonURL));
            //automater.Instance.Wait(5);
            //automater.Instance.TakeScreenshot("StartTestMethod1");

            Interfaces.ITestSecurity login = container.Resolve<Interfaces.ITestSecurity>();
            login.TestLogin(automator);
            login.TestLogoff(automator);

            //Classes.LoginTest login = new Classes.LoginTest();
            //login.TestFailedLogin(automater.Instance);
            //login.TestLogoff(automater.Instance);
            //login.TestLogin(automator);
            //automater.Instance.Wait(5);

            //login.TestLogoff(automator);
           // automater.Instance.Wait(5);

            //automater.Instance.TakeScreenshot("c:\\EndTestMethod1.jpg");

        }
    }
}
