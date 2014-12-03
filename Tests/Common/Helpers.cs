using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.Common
{
    public sealed class Helpers
    {
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

        }

        public void TestFieldInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy fieldProxy)
        {
            // 2. Select msisdn field     
            // 3. Verify that the msisdn field validation will be limited to Numeric format                                        
            //     3.1.1 Please enter alphanumeric  < 07@ >
            FieldInput(browserInstance, fieldProxy, "07@");
            browserInstance.Instance.Assert.False(() => "07@" == fieldProxy.Element.Text);
            //     3.1.2 Please enter space before entering input on the field
            FieldInput(browserInstance, fieldProxy, " 082");
            browserInstance.Instance.Assert.False(() => " 082" == fieldProxy.Element.Text);
            //     3.1.3 Please enter special characters  <@@, &&> 
            FieldInput(browserInstance, fieldProxy, "@@, &&");
            browserInstance.Instance.Assert.False(() => "@@, &&" == fieldProxy.Element.Text);
            //     3.1.4 Please enter decimal numbers <0.00444> 
            FieldInput(browserInstance, fieldProxy, "0.00444");
            browserInstance.Instance.Assert.False(() => "0.00444" == fieldProxy.Element.Text);
            //     3.1.5 Please enter negative value <-1>                                                                                                  
            FieldInput(browserInstance, fieldProxy, "-1");
            browserInstance.Instance.Assert.False(() => "-1" == fieldProxy.Element.Text);
        }

        public void FieldInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy fieldProxy, string input)
        {
            browserInstance.Instance.Focus(fieldProxy);
            browserInstance.Instance.Enter(input).In(fieldProxy);
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
            browserInstance.Instance.Assert.True(() => contactUs.Element.Text == "Contact us");

            Exists(browserInstance, helpMePath);
            var helpMe = GetProxy(browserInstance, helpMePath);
            browserInstance.Instance.Assert.True(() => helpMe.Element.Text == "Help me");
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

        public void AddSpecialToBasket(Classes.Browser browserInstance)
        {

            //Add an item to the basket
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.leftBlock > div > div > div > div:nth-child(1) > div > div"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));

            var description = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.True(() => description.Element.Text != ""));

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"));
        }
    }
}
