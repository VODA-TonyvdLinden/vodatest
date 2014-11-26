using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Alerts
{
    [TestFixture, Description("Alerts"), Category("Alerts")]
    public class Alerts
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

            container.RegisterType<Interfaces.IAlertsActions, Tests.Alerts.AlertsActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
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
        /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, grammar and alignment
        /// 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias 
        ///   name and spaza name
        /// 6. Verify that the marbil add is displayed
        /// 7. Verify that the sub application are displayed and also greyed out
        /// 8. Verify that the catalogue , basket, orders and favourites blocks are displayed
        /// 9. Verify that the Alert Notification and label are displayed
        /// 10. Verify that the basket total value field is displayed
        /// 11. Verify that the basket label is displayed
        /// 12. Verify that the basket total amount of items field is displayed
        /// 13. Verify that the search field is displayed on the top right hand corner of the screen
        /// 14. Verify the text in the search field, it states that i am looking for
        /// 15. Verify that the search text field is editable
        /// 16. Verify that the notification section is displayed
        /// 17. Verify that actions section in the screen is displayed with an notification exclamation
        /// 18. Verify that  Order alerts label is displayed
        /// 19. Veriy that the Order Alerts label, has sub labels namely You have recieved new invoices, you have an 
        ///   unconfirmed order, your catalogue is out of sync
        /// 20. Verify that the system alerts label is displayed
        /// 21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection 
        /// issues and change active spaza label is displayed
        /// 22. Verify that the following buttons are displayed on the screen, view invoices button, confirm now button,  
        ///   sync now button, manage button,diagnose button and change now
        /// TEST OUTPUT:
        /// 
        /// </summary>
        [Test, Description("AlertsLandingPageVerfication"), Repeat(1)]
        public void AlertsLandingPageVerfication()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }



        /// <summary>
        /// TEST STEPS:
        /// 
        /// TEST OUTPUT:
        /// 
        /// </summary>
        [Test, Description("Method"), Repeat(1)]
        public void Method()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }
    }
}
