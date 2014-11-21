using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace TestProj
{
    [TestFixture, Description("Test runner"), Category("Contains all test classes")]
    public class TestRunner
    {
        Classes.Browser browserInstance;
        IUnityContainer container;

        [TestFixtureSetUp]
        public void Initialise()
        {
            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.ITestUnit, MainTest>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.I_1_FRS_Ref_6_1_1, Tests.Activation._1_FRS_Ref_6_1_1>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.I_2_FRS_Ref_5_1_1, Tests.Activation._2_FRS_Ref_5_1_1>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.I_3_FRS_Ref_5_1_1, Tests.Activation._3_FRS_Ref_5_1_1>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        [Test, Description("Start"), Repeat(1)]
        public void Go()
        {
            Interfaces.ITestUnit test1 = container.Resolve<Interfaces.ITestUnit>();

            test1.TestMethod(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = true, ExitRequired = true });
        }

        [Test, Description("ActivationTests"), Repeat(1)]
        public void ActivationTests()
        {
            Interfaces.I_1_FRS_Ref_6_1_1 _1_FRS_Ref_6_1_1 = container.Resolve<Interfaces.I_1_FRS_Ref_6_1_1>();
            Interfaces.I_2_FRS_Ref_5_1_1 _2_FRS_Ref_5_1_1 = container.Resolve<Interfaces.I_2_FRS_Ref_5_1_1>();
            Interfaces.I_3_FRS_Ref_5_1_1 _3_FRS_Ref_5_1_1 = container.Resolve<Interfaces.I_3_FRS_Ref_5_1_1>();

            _1_FRS_Ref_6_1_1._1_ActivationPage(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });
            _1_FRS_Ref_6_1_1._2_ActivationForm(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });

            _2_FRS_Ref_5_1_1._1_Verify_User_OTP(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });
            _3_FRS_Ref_5_1_1._1_Setup_Catalogue(browserInstance, new Classes.ScreenshotRequirement() { EntryRequired = false, ExitRequired = false });
        }
    }
}
