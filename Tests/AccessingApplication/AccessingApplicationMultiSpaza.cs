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

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IAccessingApplicationActions, Tests.AccessingApplication.AccessingApplicationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());


            activate(browserInstance, true);

        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        private void activate(Classes.Browser browserInstance, bool multipleSpazas)
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeQuestion, out challengeAnswer, out activationNextButton, out activationErrorMessage);

            if (multipleSpazas)
                activationAction.TestValidMultiUserDetails(browserInstance, msisdn, username, activationNumber, userAlias, challengeQuestion, challengeAnswer, activationNextButton);
            else
                activationAction.TestValidSingleUserDetails(browserInstance, msisdn, username, activationNumber, userAlias, challengeQuestion, challengeAnswer, activationNextButton);

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

            int spazaCount = 0;
            int totalSpazas = 0;
            getSpazaCounts(browserInstance, out spazaCount, out totalSpazas);

            for (int k = spazaCount; k < totalSpazas; k++)
            {
                Helpers.Instance.ClickButton(browserInstance, catPageNext);
                Thread.Sleep(100);
            }

            browserInstance.Instance.Assert.Exists("#catalog1");
            browserInstance.Instance.Assert.Exists("#catalog2");
            browserInstance.Instance.Assert.Exists("#catalog3");
            browserInstance.Instance.Assert.Exists("#catalog4");

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

        private static void getSpazaCounts(Classes.Browser browserInstance, out int spazaCount, out int totalSpazas)
        {
            var SpazaCountElement = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > div > div.txt.ng-binding");

            var working = SpazaCountElement.Element.Text.Split(':')[0];
            working = working.Replace("Editing catalog for outlets ", "").Replace(" of ", ":");
            string[] counts = working.Split(':');
            spazaCount = int.Parse(counts[0]);
            totalSpazas = int.Parse(counts[1]);
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
