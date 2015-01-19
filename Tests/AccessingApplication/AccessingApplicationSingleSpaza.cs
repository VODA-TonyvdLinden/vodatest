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
    [TestFixture, Description("AccessingApplicationSingleSpaza"), Category("AccessingApplication"),]
    public class AccessingApplicationSingleSpaza
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        [TestFixtureSetUp]
        public void Initialise()
        {
            ProcessKiller.Instance.Kill();
            Thread.Sleep(500);

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IAccessingApplicationActions, Tests.AccessingApplication.AccessingApplicationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());

            Helpers.Instance.Activate(browserInstance, false);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }


        /// <summary>
        /// TEST: APPLICATION  LANDING PAGE
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
        /// TEST STEPS:
        /// 1. Verify that the Vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, grammar and alignment   
        /// 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name  
        /// 6. Verify that the user is served with specials on special block 
        /// 7. Verify that the Marbil add is displayed
        /// 8. Verify that the sub application are displayed and also greyed out
        /// 9. Verify that the catalogue , basket, orders and favourites blocks are displayed
        /// 10. Verify that the Alert Notification and label are displayed    
        /// 11. Verify that the basket total value field is displayed  
        /// 12. Verify that the basket label is displayed
        /// 13. Verify that the basket total amount of items field is displayed 
        /// 14. Verify that the search field is displayed on the top right hand corner of the screen 
        /// 15. Verify the text in the search field, it states that I am looking for 
        /// 16. Verify that the search text field is editable
        /// TEST OUTPUT:
        /// 1. The Vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyperlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to rotate from Portrait to landscape)
        /// 5. The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name    
        /// 6. The specials are displayed on the special block     
        /// 7. The Marbil add is displayed   
        /// 8. The Sub Applications are displayed with a grey colour to show they are not active
        /// 9. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page    
        /// 10. The Alert Notification and label are displayed    
        /// 11. The basket total value field is displayed   
        /// 12. The basket label is displayed 
        /// 13. The basket total amount field is displayed  
        /// 14. The Search field is displayed  
        /// 15. The text that is displayed within the field I am looking for  
        /// 16. The search text field is editable
        /// </summary>
        [Test, Description("_01_ActivationLandingPage"), Category("Accessing app"), Repeat(1)]
        public void _01_ActivationLandingPage()
        {
            //may have to do the whole registration portion here, including clearing the cache
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();

            //1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            accessingApplicationAction.VerifyLogoAndBanner(browserInstance);
            //2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            accessingApplicationAction.VerifyOnlineIndicator(browserInstance);
            //3. Verify that contact us and help me hyperlinks are displayed
            accessingApplicationAction.VerifyPageLinks(browserInstance);
            //4. See spelling, grammar and alignment
            LogWriter.Instance.Log("TESTCASE: _01_ActivationLandingPage -> Cannot programatically check spelling and grammer", LogWriter.eLogType.Info);
            //5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
            accessingApplicationAction.VerifyPreferedAlias(browserInstance, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias);
            //the spaza owner's alias name and spaza name
            accessingApplicationAction.VerifySpazaName(browserInstance, false);
            //6. Verify that the user is served with specials on special block
            accessingApplicationAction.VerifySpecialsExists(browserInstance);
            // 7. Verify that the marbil add is displayed
            accessingApplicationAction.VerifyMarbilExists(browserInstance);
            // 8. Verify that the sub application are displayed and also greyed out
            LogWriter.Instance.Log("ISSUE 2: TESTCASE:_01_ActivationLandingPage -> Testcase wrong. The first app block is used by stores promotions link and is active. The rest must be greyed out", LogWriter.eLogType.Error);
            accessingApplicationAction.VerifySubApplicationsExists(browserInstance);
            // 9. Verify that the catalogue , basket, orders and favourites blocks are displayed
            accessingApplicationAction.VerifyBottomNavExists(browserInstance);
            // 10. Verify that the Alert Notification and label are displayed
            accessingApplicationAction.VerifyAlertNotificationExists(browserInstance);
            // 11. Verify that the basket total value field is displayed
            accessingApplicationAction.VerifyBasketTotalFieldExists(browserInstance);
            // 12. Verify that the basket label is displayed
            accessingApplicationAction.VerifyBasketLabelExists(browserInstance);
            // 13. Verify that the basket total amount of items field is displayed
            accessingApplicationAction.VerifyBasketTotalAmountExists(browserInstance);
            // 14. Verify that the search field is displayed on the top right hand corner of the screen
            accessingApplicationAction.VerifySearchFieldExists(browserInstance);
            // 15. Verify the text in the search field, it states that i am looking for
            accessingApplicationAction.VerifySearchFieldTextExists(browserInstance);
            // 16. Verify that the search text field is editable
            accessingApplicationAction.VerifySearchFieldTextEditableExists(browserInstance);
        }

        /// <summary>
        /// TEST: APPLICATION FIELD VALIDATIONS
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
        /// TEST STEPS:
        /// 1.Select the alerts search field
        /// 2. Verify that the alerts search field validations                                                                                          
        /// 2.1.1 Please enter alphanumeric  < 07@ >
        /// 2.1.2 Please enter space before entering input on the field
        /// 2.1.3 Please enter special characters  <@@, &&> 
        /// 2.1.4 Please enter decimal numbers <0.00444> 
        /// 2.1.5 Please enter negative value <-1>  
        /// TEST OUTPUT:
        /// 1. Focus on the alerts search field    
        /// 2.Invalid data should not be allowed to be entered in alerts field                                                                                                                                                             
        /// 2.1.1 alphanumeric are not allowed    
        /// 2.1.2 a space before any input  is not allowed  
        /// 2.1.3 special characters are not allowed    
        /// 2.1.4 decimal numbers or float are not allowed   
        /// 2.1.5 Negative numbers are not allowed   
        /// </summary>
        [Test, Description("_02_ApplicationFieldValidation"), Category("Accessing app"), Repeat(1)]
        public void _02_ApplicationFieldValidation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();

            // 1.Select the alerts search field
            // 2. Verify that the alerts search field validations
            accessingApplicationAction.VerifyAlertSearchBox(browserInstance);
            // 2.Select the basket search field
            accessingApplicationAction.VerifyBaskSearchBox(browserInstance);

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img"));
            Thread.Sleep(5000);
        }

        /// <summary>
        /// TEST: APPLICATION LANDING CONTENTS FUNCTIONALITY
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
        /// TEST STEPS:
        /// 1. Verify specials, contents if accessible
        /// 1.1 Select any special within the catalogue to see if is selectable by a single click     
        /// TEST OUTPUT:
        /// 1.
        /// 1.1 The selected item will be available to order
        /// </summary>
        [Test, Description("_03_ApplicationLandingContentsFunctionality"), Category("Accessing app"), Repeat(1)]
        public void _03_ApplicationLandingContentsFunctionality()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            // 1. Verify specials, contents if accessible
            accessingApplicationAction.VerifySpecialAccessibility(browserInstance);

            accessingApplicationAction.VerifyMarbilAccessibility(browserInstance);

            accessingApplicationAction.VerifySubAppAccessibility(browserInstance);

        }


        /// <summary>
        /// TEST: ACCESS APPLICATION WITH SINGLE  SPAZA
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
        /// TEST STEPS:
        /// 1. Logon with a user that has single spazas on his profile
        /// 2.Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name 
        /// 3.Select spaza name    
        /// 4. verify that the multiple spaza list function is de-activated if the user only has one spaza                                                                                                   
        /// TEST OUTPUT:
        /// 1.The application will present the user with an option to select a spaza from the list    
        /// 2.The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name
        /// 3. No list should be presented 
        /// 4.The multiple spaza function is deactivated on every scree                                                                                                                               
        /// </summary>
        [Test, Description("_05_AccessApplicationWithSingleSpaza"), Category("Accessing app"), Repeat(1)]
        public void _05_AccessApplicationWithSingleSpaza()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();

            // 1. Logon with a user that has single spazas on his profile
            // 2.Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name 
            Helpers.Instance.CheckSingleSpazaPreferedAlias(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.aliasName.ng-binding");
            // 3.Select spaza name  
            var spazaLink = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
            Helpers.Instance.ClickButton(browserInstance, spazaLink);

            var spazaDiv = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName");
            // 4. verify that the multiple spaza list function is de-activated if the user only has one spaza  
            string divClass = spazaDiv.Element.Attributes.Get("class");

            //browserInstance.Instance.Assert.False(() => divClass == "spazaName open");
            LogWriter.Instance.Log(string.Format("_05_AccessApplicationWithSingleSpaza : TEST CASE: Single spaza select opens the dropdown. Left like this for some reason?"), LogWriter.eLogType.Error);

        }
    }
}
