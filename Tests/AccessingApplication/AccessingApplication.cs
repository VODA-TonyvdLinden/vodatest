﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.AccessingApplication
{
    [TestFixture, Description("AccessingApplication"), Category("AccessingApplication")]
    public class AccessingApplication
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        FluentAutomation.ElementProxy msisdn;
        FluentAutomation.ElementProxy username;
        FluentAutomation.ElementProxy activationNumber;
        FluentAutomation.ElementProxy userAlias;
        FluentAutomation.ElementProxy challengeAnswer;
        FluentAutomation.ElementProxy activationNextButton;
        FluentAutomation.ElementProxy activationErrorMessage;
        FluentAutomation.ElementProxy otpNextButton;
        FluentAutomation.ElementProxy otp;
        FluentAutomation.ElementProxy optResendButton;
        FluentAutomation.ElementProxy otpErrorMessage;

        [TestFixtureSetUp]
        public void Initialise()
        {
            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IAccessingApplicationActions, Tests.AccessingApplication.AccessingApplicationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());


            activate(browserInstance);

            

            

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        private void activate(Classes.Browser browserInstance)
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeAnswer, out activationNextButton, out activationErrorMessage);


            activationAction.MSISDNInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.MSISDN);
            activationAction.UsernameInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.Username);
            activationAction.ActivationKeyInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.ActivationKey);
            activationAction.AliasInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.Alias);
            browserInstance.Instance.Enter("NOT REQUIRED").In(challengeAnswer);
            activationAction.ClickNext(browserInstance, activationNextButton);


            Thread.Sleep(2000);
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser");

            getOTPControls(browserInstance, out otp, out otpNextButton, out optResendButton, out otpErrorMessage);
            activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.OTP);
            activationAction.ClickNext(browserInstance, otpNextButton);

            Thread.Sleep(2000);
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue");

            var catPageNext = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > div > div.nextBtnSection > button");
            var catPageUpdate = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(3) > div.formRow > button");
            activationAction.ClickNext(browserInstance, catPageNext);
            activationAction.ClickNext(browserInstance, catPageUpdate);

            var waiting = browserInstance.Instance.Find("#loading-wating-messages");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Class("hide").On("#loading-wating-messages"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Class("hide").On("#loading-wating-messages");
            Thread.Sleep(100);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main");

            Thread.Sleep(3000);

            //browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img"), TimeSpan.FromMinutes(30));

        }

        private void getActivationControls(Classes.Browser browserInstance, out FluentAutomation.ElementProxy msisdn, out FluentAutomation.ElementProxy username, out FluentAutomation.ElementProxy activationNumber, out FluentAutomation.ElementProxy userAlias, out FluentAutomation.ElementProxy challengeAnswer, out FluentAutomation.ElementProxy activationNextButton, out FluentAutomation.ElementProxy errorMessage)
        {
            msisdn = browserInstance.Instance.Find("#msisdn");
            username = browserInstance.Instance.Find("#username");
            activationNumber = browserInstance.Instance.Find("#activationNumber");
            userAlias = browserInstance.Instance.Find("#userAlias");
            //FIELD CANNOT BE REQUIRED -> IT IS A DROP DOWN
            //var challengeQuestion = browserInstance.Instance.Find("challengeQuestion");
            challengeAnswer = browserInstance.Instance.Find("#challengeAnswer");

            activationNextButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");
            errorMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");

        }
        private void getOTPControls(Classes.Browser browserInstance, out FluentAutomation.ElementProxy otp, out FluentAutomation.ElementProxy otpNextButton, out FluentAutomation.ElementProxy optResendButton, out FluentAutomation.ElementProxy otpErrorMessage)
        {
            otp = browserInstance.Instance.Find("#otp");
            otpNextButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input.btn.btn-large.nextBtn.pull-right.purpleButton.ng-scope.ng-binding");
            optResendButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input:nth-child(2)");
            otpErrorMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");
        }

        /// <summary>
        /// TEST: APPLICATION  LANDING PAGE
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
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
            accessingApplicationAction.VerifyPreferedAlias(browserInstance);
            //the spaza owner's alias name and spaza name
            accessingApplicationAction.VerifySpazaName(browserInstance);
            //6. Verify that the user is served with specials on special block
            accessingApplicationAction.VerifySpecialsExists(browserInstance);
            // 7. Verify that the marbil add is displayed
            accessingApplicationAction.VerifyMarbilExists(browserInstance);
            // 8. Verify that the sub application are displayed and also greyed out
            LogWriter.Instance.Log("TESTCASE:_01_ActivationLandingPage -> Testcase wrong. The first app block is used by stores promotions link and is active. The rest must be greyed out", LogWriter.eLogType.Error);
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
        [Test, Description("_02_ApplicationFieldValidation"), Category("Accessing app"), Repeat(1)]
        public void _02_ApplicationFieldValidation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
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
        [Test, Description("_03_ApplicationLandingContentsFunctionality"), Category("Accessing app"), Repeat(1)]
        public void _03_ApplicationLandingContentsFunctionality()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }

        /// <summary>
        /// TEST: APPLICATION WITH MULTIPLE  SPAZA'S
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
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
        [Test, Description("_04_ApplicationWithMultipleSpazas"), Category("Accessing app"), Repeat(1)]
        public void _04_ApplicationWithMultipleSpazas()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ACCESS APPLICATION WITH SINGLE  SPAZA
        /// Test Case ID: 4_FRS_Ref_5.1.7
        /// Category: Accessing app
        /// Feature: Accessing app
        /// Pre-Condition: None
        /// Environment: Application Landiing page
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
        [Test, Description("_05_AccessApplicationWithSingleSpaza"), Category("Accessing app"), Repeat(1)]
        public void _05_AccessApplicationWithSingleSpaza()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAccessingApplicationActions accessingApplicationAction = container.Resolve<Interfaces.IAccessingApplicationActions>();
            //TODO
        }
    }
}
