using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.ContactUs
{
    [TestFixture, Description("ContactUs"), Category("ContactUs")]
    public class ContactUs
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

            container.RegisterType<Interfaces.IContactUs, Tests.ContactUs.ContactUsActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the contact us hyperlink
        /// 2. Verify the following on the contact us page                                                                                                   
        ///   2.1 Verify that customer service contact number                                                                                                                                                                                      
        ///   2.2 Verify that the perfect start-up label and icon are available
        ///   2.3 Verify that  the sub menus labels are displayed under perfect start-up menu
        ///   2.4 Verify that the self service label and icon are displayedon on the screen
        ///   2.5 Verify that the sub menus labels are displayed on thescreen
        ///   2.6 Verify that the frequently asked icon and label are displayed on the screen
        /// TEST OUTPUT:
        /// 1. The Contact us page is displayed
        /// 2.
        ///   2.1 The customer service number is displayed
        ///   2.2  The perfect start-up label and icon are displayed                                                                         
        ///   2.3 The sub menus are displayed under perfect start-up menu
        ///   2.4 The self service icon and label are displayed on the screen
        ///   2.5 The sub menus are displayed on the screen
        ///   2.6 The frequently asked label and icon are displayed
        /// </summary>
        [Test, Description("ContactUs"), Repeat(1)]
        public void ContactUsTest()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IContactUs contactUsActions = container.Resolve<Interfaces.IContactUs>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the help me hyperlink  depending on the landing page that you are on
        /// 2.  Verify that the relevant landing page  correct help wizard
        ///   2.1 When user is on the activation page and clicks on, the help me should be about activation
        ///   2.2 When user is on the application landing page and clicks on help me
        ///   2.3 When user is on the alerts landing page and clicks on help me
        ///   2.4  Repeat the above step for other landing pages that are not metioned above
        /// 3. Verify that the help page is an overlay
        ///   3.1 Verify that the help content delivered to a useris an overlay page on top of original page
        ///   3.2  Verify that  the exit button on that overlay page is available
        ///   3.3 Click on the <exit> button
        /// 4. Verify that reference id of each screen is indicated on the help screen
        ///   4.1 From any landing page click on help me hyperlink
        ///   4.2 Verify that the displayed help page has a reference id indicated on the helpscreen
        /// TEST OUTPUT:
        /// 1. The help screen of relavant screen is displayed
        /// 2.
        ///   2.1 The activation help wizard  is displayed
        ///   2.2 The help wizard needs to be about the application landing page
        ///   2.3 The help wizard needs to be about the alerts landing page
        ///   2.4 The relevant help wizard should be displayed for relevant page
        /// 3.
        ///   3.1 The help content page is an overlay page on top of the original page
        ///   3.2 The Exit button is displayed on the overlay page
        ///   3.3 User is returned to the original page
        /// 4.
        ///   4.1 The relevant help wizard is displayed
        ///   4.2 The reference number is displayed
        /// </summary>
        [Test, Description("Help"), Repeat(1)]
        public void Help()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IContactUs contactUsActions = container.Resolve<Interfaces.IContactUs>();
            //TODO
        }
    }
}
