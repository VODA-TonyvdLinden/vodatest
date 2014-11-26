using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.AccessingApplication
{
    [TestFixture, Description("AccessingApplication"), Category("AccessingApplication")]
    public class AccessingApplication 
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
        /// 5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
        /// the spaza owner's alias name and spaza name
        /// 6. Verify that the user is served with specials on special block
        /// 7. Verify that the marbil add is displayed
        /// 8. Verify that the sub application are displayed and also greyed out
        /// 9. Verify that the catalogue , basket, orders and favourites blocks are displayed
        /// 10. Verify that the Alert Notification and label are displayed
        /// 11. Verify that the basket total value field is displayed
        /// 12. Verify that the basket label is displayed
        /// 13. Verify that the basket total amount of items field is displayed
        /// 14. Verify that the search field is displayed on the top right hand corner of the screen
        /// 15. Verify the text in the search field, it states that i am looking for
        /// 16. Verify that the search text field is editable
        /// TEST OUTPUT:
        /// 1. The vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyerlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to 
        ///   rotate from Portrait to landscape)
        /// 5. The preferred alias name is displayed on the top right hand corner of the app, with the name 
        ///   spaza owner's alias name and the spaza name
        /// 6. The specails are displayed on the special block
        /// 7. The marbil add is displayed
        /// 8. The Sub Applications are displayed with a grey colour to show they are not active
        /// 9. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page
        /// 10. The Alert Notification and label are displayed
        /// 11. The basket total value field is displayed
        /// 12. The basket label is displayed
        /// 13. The basket total amount fied is displayed
        /// 14. The Search field is displayed
        /// 15. The text that is displayed within the field i am looking for
        /// 16. The search text field is editable
        /// </summary>
        [Test, Description("ActivationLandingPage"), Repeat(1)]
        public void ActivationLandingPage()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1.Select the alerts search field
        /// 2. Verify that the alerts search field validations
        ///   2.1.1 Please enter alphanumeric  < 07@ >
        ///   2.1.2 Please enter space before entering input on the field
        ///   2.1.3 Please enter special characters  <@@, &&> 
        ///   2.1.4 Please enter decimal numbers <0.00444> 
        ///   2.1.5 Please enter negative value <-1>
        /// 2.Select the basket search field
        /// 3. Verify that the basket search field validations
        ///   3.1.1 Please enter alphanumeric  < 07@ >
        ///   3.1.2 Please enter space before entering input on the field
        ///   3.1.3 Please enter special characters  <@@, &&> 
        ///   3.1.4 Please enter decimal numbers <0.00444> 
        ///   3.1.5 Please enter negative value <-1>
        /// TEST OUTPUT:
        /// 1. Focus on the alerts search field
        /// 2.Invalid data should not be allowed to be entered in alerts field
        ///   2.1.1 aphanumerics are not allowed
        ///   2.1.2 a space before any input  is not allowed
        ///   2.1.3 special characters are not allowed
        ///   2.1.4 decimal numbers or float should are not allowed
        ///   2.1.5 Negative numbers should are not allowed
        /// 2. Focus on the basket search field
        /// 3.Invalid data should not be allowed to be entered in alerts field
        ///   3.1.1 aphanumerics are not allowed
        ///   3.1.2 a space before any input  is not allowed
        ///   3.1.3 special characters are not allowed
        ///   3.1.4 decimal numbers or float should are not allowed
        ///   3.1.5 Negative numbers should are not allowed
        /// </summary>
        [Test, Description("ApplicationFieldValidation"), Repeat(1)]
        public void ApplicationFieldValidation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Verify specials, contents if accessible
        ///   1.1 Select any specail within the catalogue to see if is selectable by a single click
        ///   1.2 double click on the item to see if clickable
        /// 2. Verify the Marbil add if is clickable
        ///   2.1 Click on the Marbil ad that is displayed on the screen.
        /// NB. Please note that the marbil add is only accesible when application is online 
        /// 3. Verify that the sub applications are in active for this version
        ///   3.1 Click on the sub application place holder to see if they are accessable
        /// TEST OUTPUT:
        /// 1.
        ///   1.1 The selected item is marked
        ///   1.2 The item is clickable and is displayed on the user interface two show that the user has selected it.
        /// 2.
        ///   2.1 The item selected from the marbil add is displayed
        /// NB. Please note that the marbil add is only accesible when application is online
        /// 3.
        ///   3.1 The sub applications place holders are not accessable
        /// </summary>
        [Test, Description("ApplicationLandingContentsFunctionality"), Repeat(1)]
        public void ApplicationLandingContentsFunctionality()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Logon with a user that has multiple spaza's on his profile
        /// 2.Verify that the preferred alias name is displayed on top right hand corner of the app with 
        ///   the spaza owner's alias name and spaza name list
        /// 3.Select any spaza on the list
        /// 4. Verify the following changes basket switches to the outlet specific basket,orders needs to 
        ///   switch to the outlet specific orders (including invoices),catalogues, favourites and Messages
        /// 5. Verify that these function is available on all screens, by selecting the user’s active spaza name
        /// 6. Verify that the default selected spaza at startup will be the last selected spaza from the previous session. 
        /// TEST OUTPUT:
        /// 1.The application will present the user with an option to select a spaza from the list
        /// 2.The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's 
        ///   alias name and the spaza name list
        /// 3. The selected spaza is displayed and  is only valid for the session
        /// 4. The basket  switches to the outlet specific basket,orders needs to switch to the outlet specific 
        ///   orders (including invoices),catalogues, favourites and Messages remain the same
        /// 5. The active spaza list is populated for users with multiple spaza's
        /// 6.  The default selected spaza at startup is the last selected spaza from the previous session. 
        /// </summary>
        [Test, Description("ApplicationWithMultipleSpazas"), Repeat(1)]
        public void ApplicationWithMultipleSpazas()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Logon with a user that has single spaza's on his profile
        /// 2.Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza 
        ///   owner's alias name and spaza name
        /// 3.Select any spaza on name
        /// 4. verify that the multiple spaza list function is de-activated if the user only has one spaza
        /// TEST OUTPUT:
        /// 1.The application will present the user with an option to select a spaza from the list
        /// 2.The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's 
        ///   alias name and the spaza name
        /// 3. No list should be presented                                                                                                                                                                    4.The multiple spaza function is deactivated on every screen.                                                                                                                                                                   
        /// </summary>
        [Test, Description("AccessApplicationWithSingleSpaza"), Repeat(1)]
        public void AccessApplicationWithSingleSpaza()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }
    }
}
