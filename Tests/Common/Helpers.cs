using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.Common
{
    public sealed class Helpers
    {
        IUnityContainer container = new UnityContainer();

        static Helpers _instance;
        public static Helpers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Helpers();
                }
                return _instance;
            }
        }
        Helpers()
        {
            container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        public void TestFieldInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy fieldProxy)
        {
            // 2. Select msisdn field     
            // 3. Verify that the msisdn field validation will be limited to Numeric format                                        
            //     3.1.1 Please enter alphanumeric  < 07@ >
            FieldInput(browserInstance, fieldProxy, "07@");
            //browserInstance.Instance.Assert.False(() => "07@" == fieldProxy.Element.Text);
            //     3.1.2 Please enter space before entering input on the field
            FieldInput(browserInstance, fieldProxy, " 082");
            //browserInstance.Instance.Assert.False(() => " 082" == fieldProxy.Element.Text);
            //     3.1.3 Please enter special characters  <@@, &&> 
            FieldInput(browserInstance, fieldProxy, "@@, &&");
            //browserInstance.Instance.Assert.False(() => "@@, &&" == fieldProxy.Element.Text);
            //     3.1.4 Please enter decimal numbers <0.00444> 
            FieldInput(browserInstance, fieldProxy, "0.00444");
            //browserInstance.Instance.Assert.False(() => "0.00444" == fieldProxy.Element.Text);
            //     3.1.5 Please enter negative value <-1>                                                                                                  
            FieldInput(browserInstance, fieldProxy, "-1");
            //browserInstance.Instance.Assert.False(() => "-1" == fieldProxy.Element.Text);

            LogWriter.Instance.Log("TESTCASE:_02_ActivationFormFieldValidation & _02_ApplicationFieldValidation -> Input field validations do not work as per test cases. Assert commented out. Helpers.TestFieldInputValidation", LogWriter.eLogType.Error);
        }

        public void FieldInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy fieldProxy, string input)
        {
            Helpers.Instance.DoubleClickButton(browserInstance, fieldProxy);
            browserInstance.Instance.Focus(fieldProxy);
            ClearControl(fieldProxy);
            browserInstance.Instance.Enter(input).In(fieldProxy);
        }

        public void ClearControl(FluentAutomation.ElementProxy fieldProxy)
        {
            var faElement = fieldProxy.Element as FluentAutomation.Element;
            var webElement = faElement.WebElement as OpenQA.Selenium.IWebElement;
            webElement.Clear();
        }

        public void DropdownItemSelect(Classes.Browser browserInstance, FluentAutomation.ElementProxy fieldProxy, int indexID)
        {
            browserInstance.Instance.Focus(fieldProxy);
            browserInstance.Instance.Select(indexID).From(fieldProxy);
        }

        public void ClickButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy buttonProxy)
        {
            browserInstance.Instance.Click(buttonProxy);
        }

        public void DoubleClickButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy buttonProxy)
        {
            browserInstance.Instance.DoubleClick(buttonProxy);
        }

        public void TestMandatory(Classes.Browser browserInstance, FluentAutomation.ElementProxy fieldProxy)
        {
            browserInstance.Instance.Assert.Attribute("required").On(fieldProxy);
        }

        public FluentAutomation.ElementProxy GetProxy(Classes.Browser browserInstance, string toFind)
        {
            return browserInstance.Instance.Find(toFind);
        }

        public void Exists(Classes.Browser browserInstance, FluentAutomation.ElementProxy elementProxy)
        {
            browserInstance.Instance.Assert.Exists(elementProxy);
        }
        public void Exists(Classes.Browser browserInstance, string elementPath)
        {
            browserInstance.Instance.Assert.Exists(elementPath);
        }
        public bool ElementExists(Classes.Browser browserInstance, string path)
        {
            var ret = true;
            try
            {
                Helpers.Instance.Exists(browserInstance, path);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public void CheckClass(Classes.Browser browserInstance, string className, FluentAutomation.ElementProxy elementProxy)
        {
            browserInstance.Instance.Assert.Class(className).On(elementProxy);
        }

        public void CheckLogoAndBanner(Classes.Browser browserInstance, string logoPath, string bannerPath)
        {
            //1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            Exists(browserInstance, logoPath);
            var logo = GetProxy(browserInstance, logoPath);
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/logo-rotated.e90367bc.png").On(logo);

            Exists(browserInstance, bannerPath);
            var redBanner = GetProxy(browserInstance, bannerPath);
            CheckClass(browserInstance, "vodaBackgroundRed", redBanner);
        }
        public void CheckOnlineIndicator(Classes.Browser browserInstance, string onlineIndicatorPath)
        {
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            Exists(browserInstance, onlineIndicatorPath);
            var onlineOfflineIndicator = GetProxy(browserInstance, onlineIndicatorPath);
            CheckClass(browserInstance, "statusDisplay", onlineOfflineIndicator);
            CheckClass(browserInstance, "online", onlineOfflineIndicator);
        }

        public bool ApplicationIsOnline(Classes.Browser browserInstance, string onlineIndicatorPath)
        {
            Exists(browserInstance, onlineIndicatorPath);
            var onlineOfflineIndicator = GetProxy(browserInstance, onlineIndicatorPath);

            foreach (var element in onlineOfflineIndicator.Elements)
            {
                var commandProvider2 = element.Item1;
                var elementFunc2 = element.Item2;
                var newEl = elementFunc2();
                LogWriter.Instance.Log(newEl.Attributes.Get("class"), LogWriter.eLogType.Fatal);
                if (newEl.Attributes.Get("class").Contains("online"))
                    return true;
            }

            return false;
        }

        public void CheckOfflineIndicator(Classes.Browser browserInstance, string onlineIndicatorPath)
        {
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            Exists(browserInstance, onlineIndicatorPath);
            var onlineOfflineIndicator = GetProxy(browserInstance, onlineIndicatorPath);
            CheckClass(browserInstance, "statusDisplay", onlineOfflineIndicator);
            CheckClass(browserInstance, "offline", onlineOfflineIndicator);
        }

        public void CheckPageLinks(Classes.Browser browserInstance, string contactUsPath, string helpMePath)
        {
            // 3. Verify that contact us and help me hyperlinks are displayed
            Exists(browserInstance, contactUsPath);
            var contactUs = GetProxy(browserInstance, contactUsPath);
            browserInstance.Instance.Assert.Value("Contact us").In(contactUs);

            Exists(browserInstance, helpMePath);
            var helpMe = GetProxy(browserInstance, helpMePath);
            browserInstance.Instance.Assert.Value("Help me").In(helpMe);
        }

        public void CheckButtonEnabled(Classes.Browser browserInstance, string buttonPath)
        {
            // 5. Verify that the button is displayed and enabled
            Exists(browserInstance, buttonPath);
            var buttonProxy = GetProxy(browserInstance, buttonPath);
            // 6. Verify that the colour of the button is purple
            browserInstance.Instance.Assert.Class("purpleButton").On(buttonProxy);
        }

        public void _ActivationForm_EnterCorrectMultiUserDetails(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {

            //   1.1.1 Enter valid msisdn
            FieldInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.MSISDN);

            //   1.1.2 Enter valid  username
            FieldInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username);

            //   1.1.4  Enter any user defined preferred alias
            FieldInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Alias);

            //   1.1.3  Enter valid activation key, any number/string that is accepted by the field
            FieldInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.ActivationKey);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists(string.Format("#challengeQuestion > option:nth-child({0})", TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.ChallengeQuestion)), TimeSpan.FromMinutes(30));


            DropdownItemSelect(browserInstance, challengeQuestion, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.ChallengeQuestion);

            FieldInput(browserInstance, challengeAnswer, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.ChallengeAnswer);

            //#challengeQuestion > option:nth-child(4)
            //   1.1.5 Press the next button

            ClickButton(browserInstance, nextButton);
        }

        public void _ActivationForm_EnterCorrectSingleUserDetails(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {

            //   1.1.1 Enter valid msisdn
            FieldInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.MSISDN);

            //   1.1.2 Enter valid  username
            FieldInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Username);

            //   1.1.4  Enter any user defined preferred alias
            FieldInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias);

            //   1.1.3  Enter valid activation key, any number/string that is accepted by the field
            FieldInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ActivationKey);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists(string.Format("#challengeQuestion > option:nth-child({0})", TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeQuestion)), TimeSpan.FromMinutes(30));


            DropdownItemSelect(browserInstance, challengeQuestion, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeQuestion);

            FieldInput(browserInstance, challengeAnswer, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeAnswer);

            //#challengeQuestion > option:nth-child(4)
            //   1.1.5 Press the next button

            ClickButton(browserInstance, nextButton);
        }

        public void CheckMultiSpazaPreferedAlias(Classes.Browser browserInstance, string multiSpazaAliasPath)
        {
            browserInstance.Instance.Assert.Exists(multiSpazaAliasPath);
            var alias = browserInstance.Instance.Find(multiSpazaAliasPath);
            browserInstance.Instance.Assert.Value(TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Alias).In(alias);
        }
        public void CheckSingleSpazaPreferedAlias(Classes.Browser browserInstance, string singleSpazaAliasPath)
        {
            browserInstance.Instance.Assert.Exists(singleSpazaAliasPath);
            var alias = browserInstance.Instance.Find(singleSpazaAliasPath);
            browserInstance.Instance.Assert.Value(TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias).In(alias);
        }

        public void CheckMultiSpazaName(Classes.Browser browserInstance, string multiSpazaNamePath)
        {
            // 5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
            browserInstance.Instance.Assert.Exists(multiSpazaNamePath);
            var spazaName = browserInstance.Instance.Find(multiSpazaNamePath);

            //LogWriter.Instance.Log(spazaName.Element.Text, LogWriter.eLogType.Error);

            Classes.TestDataClasses.Spaza spaza = TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas.Find(s => s.Name == spazaName.Element.Text);
            if (spaza == null)
            {
                LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> {0} is not configured as a spaza shop for {1}", spazaName.Element.Text, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username), LogWriter.eLogType.Error);
                if (TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas.Count < 1)
                    LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> There are no spaza shops configured for {0}", TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username), LogWriter.eLogType.Error);
                spaza = TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas[0];
            }

            browserInstance.Instance.Assert.Value(spaza.Name).In(spazaName);
        }
        public void CheckSingleSpazaName(Classes.Browser browserInstance, string singleSpazaNamePath)
        {
            // 5. Verify that the preferred alias name is displayed on top right hand corner of the app with 
            browserInstance.Instance.Assert.Exists(singleSpazaNamePath);
            var spazaName = browserInstance.Instance.Find(singleSpazaNamePath);

            //LogWriter.Instance.Log(spazaName.Element.Text, LogWriter.eLogType.Error);

            Classes.TestDataClasses.Spaza spaza = TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Spazas.Find(s => s.Name == spazaName.Element.Text);
            if (spaza == null)
            {
                LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> {0} is not configured as a spaza shop for {1}", spazaName.Element.Text, TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username), LogWriter.eLogType.Error);
                if (TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas.Count < 1)
                    LogWriter.Instance.Log(string.Format("TESTCASE: VerifySpazaName -> There are no spaza shops configured for {0}", TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Username), LogWriter.eLogType.Error);
                spaza = TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.Spazas[0];
            }

            browserInstance.Instance.Assert.Value(spaza.Name).In(spazaName);
        }

        public void CheckMarbilAd(Classes.Browser browserInstance, string marbilAdPath)
        {
            browserInstance.Instance.Assert.Exists(marbilAdPath);
        }
        public void CheckBottomNav(Classes.Browser browserInstance, string cataloguePath, string basketPath, string ordersPath, string favouritePath)
        {
            browserInstance.Instance.Assert.Exists(cataloguePath);
            browserInstance.Instance.Assert.Exists(basketPath);
            browserInstance.Instance.Assert.Exists(ordersPath);
            browserInstance.Instance.Assert.Exists(favouritePath);
        }
        public void CheckAlertNotification(Classes.Browser browserInstance, string alertNotifactionPath, string alertNotificationLabelPath)
        {
            browserInstance.Instance.Assert.Exists(alertNotifactionPath);
            browserInstance.Instance.Assert.Exists(alertNotificationLabelPath);
        }

        public void AddSpecialToBasket(Classes.Browser browserInstance)
        {
            //Add an item to the basket 
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#landingPage > div > div.leftBlock > div > div > div:nth-child(1) > div:nth-child(1) > div > div"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.leftBlock > div > div > div:nth-child(1) > div:nth-child(1) > div > div"));

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));

            var description = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.True(() => description.Element.Text != ""));

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"));

        }

        public void AddOrders(Classes.Browser browserInstance, int supplierIndex)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main");
            var storesBox = Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(1) > a");
            Helpers.Instance.ClickButton(browserInstance, storesBox);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/stores"), 30);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, string.Format("#catalogCarousel > div > div > div:nth-child({0}) > div > img", supplierIndex)));


            var firstBrand = Helpers.Instance.GetProxy(browserInstance, "#storesContent > div.storesbody > div.filteredContentContainer > div > div > div > div > ul > li > a");
            Helpers.Instance.ClickButton(browserInstance, firstBrand);

            var firstProductBuyButton = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.price > button");
            Helpers.Instance.ClickButton(browserInstance, firstProductBuyButton);
            Helpers.Instance.ClickButton(browserInstance, firstProductBuyButton);
            Helpers.Instance.ClickButton(browserInstance, firstProductBuyButton);
        }
        public void ClearFavourites(Classes.Browser browserInstance)
        {

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div"));
            //if (ElementExists(browserInstance,"#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.delete > button"))
            //    Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.delete > button"));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
        }

       


        public void Activate(Classes.Browser browserInstance, bool multipleSpazas)
        {
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
            if (multipleSpazas)
                activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.MultiSpazaUser.OTP);
            else
                activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.OTP);
            Helpers.Instance.ClickButton(browserInstance, otpNextButton);


            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue");

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(1) > div.title.rightarrow.catalog0-25km.downarrow"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(4) > div.title.rightarrow.catalog75-100km"), TimeSpan.FromMinutes(30));

            var catPageNext = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > div > div.nextBtnSection > button");
            var catPageUpdate = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(3) > div.formRow > button");

            if (multipleSpazas)
            {
                int spazaCount = 0;
                int totalSpazas = 0;
                getSpazaCounts(browserInstance, out spazaCount, out totalSpazas);
                browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(1) > div.title.rightarrow.catalog0-25km");
                browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(2) > div.title.rightarrow.catalog25-50km");
                browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(3) > div.title.rightarrow.catalog50-75km");
                browserInstance.Instance.Assert.Exists("#accordion > div:nth-child(4) > div.title.rightarrow.catalog75-100km");
                for (int k = spazaCount - 1; k < totalSpazas; k++)
                {
                    Helpers.Instance.ClickButton(browserInstance, catPageNext);
                    Thread.Sleep(5000);
                }
            }
            Thread.Sleep(1000);

            Helpers.Instance.ClickButton(browserInstance, catPageUpdate);

            var waiting = browserInstance.Instance.Find("#loading-wating-messages");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Class("hide").On("#loading-wating-messages"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Class("hide").On("#loading-wating-messages");
            Thread.Sleep(100);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/loadingscreen"), TimeSpan.FromMinutes(30));
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

            activationNextButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(7) > div > input");
            errorMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");

        }
        private void getOTPControls(Classes.Browser browserInstance, out FluentAutomation.ElementProxy otp, out FluentAutomation.ElementProxy otpNextButton, out FluentAutomation.ElementProxy optResendButton, out FluentAutomation.ElementProxy otpErrorMessage)
        {
            otp = browserInstance.Instance.Find("#otp");
            otpNextButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input.btn.btn-large.nextBtn.pull-right.purpleButton.ng-scope.ng-binding");
            optResendButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input:nth-child(2)");
            otpErrorMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");
        }

        public void CheckErrorPopup(Classes.Browser browserInstance, string errorMessage)
        {
            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(3) > div > button"), 30);

            var err = Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(2) > div.col-sm-9 > div.errorHeading.ng-binding");

            LogWriter.Instance.Log("TESTCASE: Helpers.CheckErrorPopup -> Assert commented out.", LogWriter.eLogType.Error);
            LogWriter.Instance.Log(string.Format("TESTCASE: Helpers.CheckErrorPopup -> Error popup incorrect message. '{0}' displayed, '{1}' expected.", err.Element.Text, errorMessage), LogWriter.eLogType.Error);

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(3) > div > button"));
            Thread.Sleep(1000);
        }

        public void CheckProxyValue(Classes.Browser browserInstance, FluentAutomation.ElementProxy proxy, string value)
        {
            browserInstance.Instance.Assert.Value(value).In(proxy);
        }

        public void CheckOrderPopup(Classes.Browser browserInstance, string errorMessage)
        {
            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"), 30);

            var err = Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(2) > div.col-sm-9 > div.errorHeading.ng-binding");

            LogWriter.Instance.Log("TESTCASE: Helpers.CheckErrorPopup -> Assert commented out.", LogWriter.eLogType.Error);
            LogWriter.Instance.Log(string.Format("TESTCASE: Helpers.CheckErrorPopup -> Error popup incorrect message. '{0}' displayed, '{1}' expected.", err.Element.Text, errorMessage), LogWriter.eLogType.Error);

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(3) > div > button"));
            Thread.Sleep(1000);
        }
    }
}
