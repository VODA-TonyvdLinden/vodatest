using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.AccessingApplication
{
    [TestFixture, Description("AccessingApplicationMultiSpaza"), Category("AccessingApplication")]
    public class AccessingApplicationMultiSpaza
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();



        [TestFixtureSetUp]
        public void Initialise()
        {

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IAccessingApplicationActions, Tests.AccessingApplication.AccessingApplicationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            Helpers.Instance.Activate(browserInstance, true);

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        

        



        /// <summary>
        /// TEST: APPLICATION WITH MULTIPLE  SPAZA'S
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
        /// TEST STEPS:
        /// 1. Logon with a user that has multiple spazas on his profile    
        /// 2.Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name list  
        /// 3.Select any spaza on the list  
        /// 4. Verify the following changes basket switches to the outlet specific basket, orders needs to switch to the outlet specific orders (including invoices),catalogues, favourites and Messages  
        /// 5. Verify that these function is available on all screens, by selecting the user’s active spaza name
        /// 6. Verify that the default selected spaza at start-up will be the last selected spaza from the previous session. 
        /// TEST OUTPUT:
        /// 1.The application will present the user with an option to select a spaza from the list   
        /// 2.The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name list 
        /// 3. The selected spaza is displayed and  is only valid for the session   
        /// 4. The basket  switches to the outlet specific basket, orders needs to switch to the outlet specific orders (including invoices)
        /// ,catalogues, favourites and Messages remain the same"
        /// 5. The active spaza list is populated for users with multiple spazas
        /// 6.  The default selected spaza at start-up is the last selected spaza from the previous session. 
        /// </summary>
        [Test, Description("_04_ApplicationWithMultipleSpazas"), Category("Accessing app"), Repeat(1)]
        public void _04_ApplicationWithMultipleSpazas()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();

            // 1. Logon with a user that has multiple spaza's on his profile
            // 2.Verify that the preferred alias name is displayed on top right hand corner of the app with 
            //   the spaza owner's alias name and spaza name list
            accessingApplicationAction.VerifyPreferedAlias(browserInstance);
            accessingApplicationAction.VerifySpazaName(browserInstance);

            // 3.Select any spaza on the list  
            accessingApplicationAction.SwitchSpazaAndCheckBasket(browserInstance, accessingApplicationAction);

            LogWriter.Instance.Log("TESTCASE:_04_ApplicationWithMultipleSpazas -> Step 5 inferred by previous sections of the test. Update test case", LogWriter.eLogType.Error);
            // 5. Verify that these function is available on all screens, by selecting the user’s active spaza name
            // 5. The active spaza list is populated for users with multiple spaza's

            // 6. Verify that the default selected spaza at startup will be the last selected spaza from the previous session. 
            // 6.  The default selected spaza at startup is the last selected spaza from the previous session. 

            accessingApplicationAction.VerifySpazaNameForReturnToApp(browserInstance);
        }




    }
}
