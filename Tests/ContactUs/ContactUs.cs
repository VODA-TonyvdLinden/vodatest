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

namespace TestProj.Tests.ContactUs
{
    [TestFixture, Description("ContactUs"), Category("ContactUs")]
    public class ContactUs
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        FluentAutomation.ElementProxy msisdn;
        FluentAutomation.ElementProxy username;
        FluentAutomation.ElementProxy activationNumber;
        FluentAutomation.ElementProxy userAlias;
        FluentAutomation.ElementProxy challengeQuestion;
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
            ProcessKiller.Instance.Kill();
            Thread.Sleep(500);

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IContactUs, Tests.ContactUs.ContactUsActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());

            activate(browserInstance);
        }

        private void activate(Classes.Browser browserInstance)
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeQuestion, out challengeAnswer, out activationNextButton, out activationErrorMessage);

            activationAction.TestValidMultiUserDetails(browserInstance, msisdn, username, activationNumber, userAlias, challengeQuestion, challengeAnswer, activationNextButton);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser");

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#otp"), TimeSpan.FromMinutes(30));

            getOTPControls(browserInstance, out otp, out otpNextButton, out optResendButton, out otpErrorMessage);
            activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.OTP);
            Helpers.Instance.ClickButton(browserInstance, otpNextButton);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue");

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(1) > div.title.rightarrow.catalog1.downarrow"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(4) > div.title.rightarrow.catalog4"), TimeSpan.FromMinutes(30));

            var catPageNext = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > div > div.nextBtnSection > button");
            var catPageUpdate = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(3) > div.formRow > button");
            Helpers.Instance.ClickButton(browserInstance, catPageNext);
            Thread.Sleep(100);
            Helpers.Instance.ClickButton(browserInstance, catPageNext);

            browserInstance.Instance.Assert.Exists("#messageBlock > ul > li:nth-child(1) > div > div:nth-child(3) > ul > li");
            browserInstance.Instance.Assert.Exists("#messageBlock > ul > li:nth-child(2) > div > div:nth-child(3) > ul > li");

            Helpers.Instance.ClickButton(browserInstance, catPageUpdate);

            var waiting = browserInstance.Instance.Find("#loading-wating-messages");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Class("hide").On("#loading-wating-messages"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Class("hide").On("#loading-wating-messages");
            Thread.Sleep(100);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main");
            Thread.Sleep(3000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img"), TimeSpan.FromMinutes(30));
        }
        private void getActivationControls(Classes.Browser browserInstance, out FluentAutomation.ElementProxy msisdn, out FluentAutomation.ElementProxy username, out FluentAutomation.ElementProxy activationNumber, out FluentAutomation.ElementProxy userAlias, out FluentAutomation.ElementProxy challengeQuestion, out FluentAutomation.ElementProxy challengeAnswer, out FluentAutomation.ElementProxy activationNextButton, out FluentAutomation.ElementProxy errorMessage)
        {
            msisdn = browserInstance.Instance.Find("#msisdn");
            username = browserInstance.Instance.Find("#username");
            activationNumber = browserInstance.Instance.Find("#activationNumber");
            userAlias = browserInstance.Instance.Find("#userAlias");
            challengeQuestion = browserInstance.Instance.Find("#challengeQuestion");
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

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST: CONTACT US 
        /// Test Case ID: 16_FRS_Ref_5.1.7
        /// Category: Contact Us
        /// Feature: Contact Us
        /// Pre-Condition: None
        /// Environment: Any Landing Page
        /// TEST STEPS:
        /// 1. Click on the contact us hyperlink     
        /// . Verify the following on the contact us page                                                                                                   
        /// 2.1 Verify that customer service contact number   "
        /// 2.2 Verify that the perfect start-up label and icon are available  
        /// 2.3 Verify that  the sub menus labels are displayed under perfect start-up menu
        /// 2.4 Verify that the self service label and icon are displayed on on the screen   
        /// 2.5 Verify that the sub menus labels are displayed on the screen     
        /// 2.6 Verify that the frequently asked icon and label are displayed on the screen                                                  
        /// TEST OUTPUT:
        /// 1. The Contact us page is displayed    
        /// 2.1 The customer service number is displayed    
        /// 2.2  The perfect start-up label and icon are displayed 
        /// 2.3 The sub menus are displayed under perfect start-up menu
        /// 2.4 The self service icon and label are displayed on the screen    
        /// 2.5 The sub menus are displayed on the screen    
        /// 2.6 The frequently asked label and icon are displayed
        /// </summary>
        [Test, Description("_01_ContactUs"), Category("Contact Us"), Repeat(1)]
        public void _01_ContactUsTest()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Interfaces.IContactUs contactUsActions = container.Resolve<Interfaces.IContactUs>();

            contactUsActions.GoToContactUs(browserInstance);

            contactUsActions.VerifyCustomerServiceContactNo(browserInstance);

            contactUsActions.VerifyPerfectStartupSection(browserInstance);

            contactUsActions.VerifySelfServiceSection(browserInstance);

            contactUsActions.VerifyFAQSection(browserInstance);

            LogWriter.Instance.Log("TESTCASE:_01_ContactUsTest -> Test case incomplete - There is no test specified for the back button. Update test case", LogWriter.eLogType.Error);
        }

        /// <summary>
        /// TEST: HELP
        /// Test Case ID: 17_FRS_Ref_5.1.7
        /// Category: Alerts
        /// Feature: Help Me
        /// Pre-Condition: None
        /// Environment: Any Landing Page
        /// TEST STEPS:
        /// 1. Click on the help me hyperlink  depending on the landing page that you are on  
        /// 2.  Verify that the relevant landing page  correct help wizard                                                     
        /// 2.1 When user is on the activation page and clicks on, the help me should be about activation   
        /// 2.2 When user is on the application landing page and clicks on help me     
        /// 2.3 When user is on the alerts landing page and clicks on help me   
        /// 2.4  Repeat the above step for other landing pages that are not mentioned above    
        /// 3. Verify that the help page is an overlay                                                                                        
        /// 3.1 Verify that the help content delivered to a user is an overlay page on top of original page   
        /// 3.2  Verify that  the exit button on that overlay page is available   
        /// 3.3 Click on the <exit> button   
        /// 4. Verify that reference id of each screen is indicated on the help screen                                  
        /// 4.1 From any landing page click on help me hyperlink  
        /// 4.2 Verify that the displayed help page has a reference id indicated on the help screen                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        /// TEST OUTPUT:
        /// 1. The help screen of relevant screen is displayed   
        /// 2.                                                                                                                                                                                                                     
        /// 2.1 The activation help wizard  is displayed    
        /// 2.2 The help wizard needs to be about the application landing page  
        /// 2.3 The help wizard needs to be about the alerts landing page   
        /// 2.4 The relevant help wizard should be displayed for relevant page    
        /// 3.1 The help content page is an overlay page on top of the original page   
        /// 3.2 The Exit button is displayed on the overlay page  
        /// 3.3 User is returned to the original page 
        /// 4.                                                                                                                                                                                                                  
        /// 4.1 The relevant help wizard is displayed   
        /// 4.2 The reference number is displayed                                                                                                                                                            
        /// </summary>
        [Test, Description("_02_Help"), Category("Alerts"), Repeat(1)]
        public void _02_Help()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IContactUs contactUsActions = container.Resolve<Interfaces.IContactUs>();

            LogWriter.Instance.Log("TESTCASE:_02_Help -> Help page has not been implemented.", LogWriter.eLogType.Error);
            //TODO
        }
    }
}
