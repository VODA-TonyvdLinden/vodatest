using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.Activation
{
    public class ActivationActions : TestProj.Interfaces.IActivationActions
    {
        public void MSISDNInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, string input)
        {
            browserInstance.Instance.Focus(msisdn);
            browserInstance.Instance.Enter(input).In(msisdn);
        }
        public void UsernameInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy username, string input)
        {
            browserInstance.Instance.Focus(username);
            browserInstance.Instance.Enter(input).In(username);
        }
        public void AliasInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy alias, string input)
        {
            browserInstance.Instance.Focus(alias);
            browserInstance.Instance.Enter(input).In(alias);
        }
        public void ActivationKeyInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber, string input)
        {
            browserInstance.Instance.Focus(activationNumber);
            browserInstance.Instance.Enter(input).In(activationNumber);
        }
        public void ClickNext(Classes.Browser browserInstance, FluentAutomation.ElementProxy nextButton)
        {
            browserInstance.Instance.Click(nextButton);
        }

        public void TestAliasInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy userAlias)
        {
            // 8. Validate Preferred Alias field
            // 8.1.1 Please enter space before entering input on the field   
            AliasInput(browserInstance, userAlias, " TEST");
            browserInstance.Instance.Assert.False(() => " TEST" == userAlias.Element.Text);
        }

        public void TestActivationKeyInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber)
        {
            // 6. select activation key field
            // 7. Validate the activation key field
            // 7.1.1 Please enter space before entering input on the field 
            ActivationKeyInput(browserInstance, activationNumber, " TEST");
            browserInstance.Instance.Assert.False(() => " TEST" == activationNumber.Element.Text);
        }

        public void TestUsernameInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy username)
        {
            // 4. Select username field                                                   
            // 5. Validate the username field
            // 5.1.1 Please enter space before entering input on username field    
            UsernameInput(browserInstance, username, " TEST");
            browserInstance.Instance.Assert.False(() => " TEST" == username.Element.Text);

        }
        public void TestMSISDNInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn)
        {
            // 2. Select msisdn field     
            // 3. Verify that the msisdn field validation will be limited to Numeric format                                        
            //     3.1.1 Please enter alphanumeric  < 07@ >
            MSISDNInput(browserInstance, msisdn, "07@");
            browserInstance.Instance.Assert.False(() => "07@" == msisdn.Element.Text);
            //     3.1.2 Please enter space before entering input on the field
            MSISDNInput(browserInstance, msisdn, " 082");
            browserInstance.Instance.Assert.False(() => " 082" == msisdn.Element.Text);
            //     3.1.3 Please enter special characters  <@@, &&> 
            MSISDNInput(browserInstance, msisdn, "@@, &&");
            browserInstance.Instance.Assert.False(() => "@@, &&" == msisdn.Element.Text);
            //     3.1.4 Please enter decimal numbers <0.00444> 
            MSISDNInput(browserInstance, msisdn, "0.00444");
            browserInstance.Instance.Assert.False(() => "0.00444" == msisdn.Element.Text);
            //     3.1.5 Please enter negative value <-1>                                                                                                  
            MSISDNInput(browserInstance, msisdn, "-1");
            browserInstance.Instance.Assert.False(() => "-1" == msisdn.Element.Text);
        }

        public void TestMandatoryFields(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy errorMessage)
        {
            //browserInstance.Instance.Assert.Exists(msisdn);
            //LogWriter.Instance.Log(msisdn.Element.Text, LogWriter.eLogType.Fatal);
            // 1. Verify that all text fields are mandatory on the form
            browserInstance.Instance.Assert.Attribute("required").On(msisdn);
            browserInstance.Instance.Assert.Attribute("required").On(username);
            browserInstance.Instance.Assert.Attribute("required").On(activationNumber);
            browserInstance.Instance.Assert.Attribute("required").On(userAlias);
            browserInstance.Instance.Assert.Attribute("required").On(challengeAnswer);

            // 1.1.1 Please don’t enter anything on all the fields and click next
            //Test must be Error Message is displayed: “E1-1-7 – Please complete all fields”. 
            //  all fields must either be highlited in red or displayed on the screen            
            browserInstance.Instance.Click("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");

            browserInstance.Instance.Assert.Exists("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");
            browserInstance.Instance.Assert.True(() => "E1-1-7 – Please complete all fields" == errorMessage.Element.Text);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormFieldValidation -> all fields must either be highlited in red or displayed on the screen. Which one?", Classes.LogWriter.eLogType.Error);
        }

        public void TestMSISDNLengthLimitSmaller(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy errorMessage)
        {
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> TEST CASE DESCRIPTION INVALID: 3.2.1 Enter msisdn number less than 10 , meaning 11 digit on msisdn field", Classes.LogWriter.eLogType.Error);
            // 3.2.1 Enter msisdn number less than 10 , meaning 11 digit on msisdn field            
            MSISDNInput(browserInstance, msisdn, "08212345678");
            // 3.2.1.1 Enter valid  username    
            UsernameInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.Username);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", Classes.LogWriter.eLogType.Error);
            // 3.2.1.2  Enter valid activation key, any number/string that is accepted by the field 
            ActivationKeyInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.ActivationKey);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid ACTIVATION No required", Classes.LogWriter.eLogType.Error);
            // 3.2.1.3 Enter any user defined preferred alias     
            AliasInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.Alias);
            // 3.2.1.4 click on the next button   
            // TEST:  3.2.1.4  An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(msisdn);
            browserInstance.Instance.Assert.Class("ng-invalid").On(msisdn);
        }

        public void TestMSISDNLengthLimitGreater(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage)
        {
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> TEST CASE DESCRIPTION INVALID: 3.1.1 Enter msisdn number > 10 , meaning 9 digit on msisdn field", Classes.LogWriter.eLogType.Error);
            // 3. Verify that is limited to 10 digit numbers
            // 3.1.1 Enter msisdn number > 10 , meaning 9 digit on msisdn field                     
            MSISDNInput(browserInstance, msisdn, "082123456");
            // 3.1.2 Enter valid  username     
            UsernameInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.Username);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", Classes.LogWriter.eLogType.Error);
            // 3.1.3  Enter Invalid activation key, any number/string that is accepted by the field 
            ActivationKeyInput(browserInstance, activationNumber, "4444444419");
            // 3.1.4  Enter any user defined preferred alias                                        
            AliasInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.Alias);
            // 3.1.5 click on the next button                                                       
            ClickNext(browserInstance, nextButton);
            // TEST: 3.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(msisdn);
            browserInstance.Instance.Assert.Class("ng-invalid").On(msisdn);
        }

        public void TestInvalidUsername(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage)
        {
            // 2. Invalid username  – username does not match one saved in BOP Manager              
            // 2.1.1 Enter valid msisdn                                                             
            MSISDNInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.MSISDN);
            // 2.1.2 Enter invalid  username          
            UsernameInput(browserInstance, username, "InvalidTest User");
            // 2.1.3  Enter valid activation key, any number/string that is accepted by the field   
            ActivationKeyInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.ActivationKey);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid ACTIVATION No required", Classes.LogWriter.eLogType.Error);
            // 2.1.4  Enter any user defined preferred alias                                        
            AliasInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.Alias);
            // 2.1.5 click on the next button                               
            ClickNext(browserInstance, nextButton);
            // TEST: 2.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(username);
            browserInstance.Instance.Assert.Class("ng-invalid").On(username);
        }



        public void TestInvalidActivationKey(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage)
        {
            // 1. Invalid Activation Key – does not match one saved in BOP Manager                  
            // 1.1.1 Enter valid msisdn                                   
            MSISDNInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.MSISDN);
            Classes.LogWriter.Instance.Log("TESTCASEActivationFormIncorrectUserDetails -> Valid MSISDN no required", Classes.LogWriter.eLogType.Error);
            // 1.1.2 Enter valid  username                                                          
            UsernameInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.Username);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", Classes.LogWriter.eLogType.Error);
            // 1.1.3  Enter Invalid activation key, any number/string that is accepted by the field 
            ActivationKeyInput(browserInstance, activationNumber, "44444109");
            // 1.1.4  Enter any user defined preferred alias                                        
            AliasInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.Alias);
            // 1.1.5 click on the next button   
            ClickNext(browserInstance, nextButton);
            // TEST: 1.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(activationNumber);
            browserInstance.Instance.Assert.Class("ng-invalid").On(activationNumber);
        }
        public void VerifyLogoAndBanner(Classes.Browser browserInstance)
        {
            //1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > img");
            var logo = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > img");
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/logo-rotated.e90367bc.png").On(logo);
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
            var redBanner = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
            browserInstance.Instance.Assert.Class("vodaBackgroundRed").On(redBanner);
        }
        public void VerifyOnlineIndicator(Classes.Browser browserInstance)
        {
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div > div");
            var onlineOfflineIndicator = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div > div");
            browserInstance.Instance.Assert.Class("statusDisplay").On(onlineOfflineIndicator);
            browserInstance.Instance.Assert.Class("online").On(onlineOfflineIndicator);
        }
        public void VerifyPageLinks(Classes.Browser browserInstance)
        {
            // 3. Verify that contact us and help me hyperlinks are displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUs");
            var contactUs = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUs");
            browserInstance.Instance.Assert.True(() => contactUs.Element.Text == "Contact us");
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMe");
            var helpMe = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMe");
            browserInstance.Instance.Assert.True(() => helpMe.Element.Text == "Help me");
        }


        public void VerifyOTPNextButton(Classes.Browser browserInstance)
        {
            VerifyNextButton(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input.btn.btn-large.nextBtn.pull-right.purpleButton.ng-scope.ng-binding");
        }
        public void VerifyActivationPageNextButton(Classes.Browser browserInstance)
        {
            VerifyNextButton(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");
        }

        public void VerifyNextButton(Classes.Browser browserInstance, string cssPath)
        {
            // 5. Verify that the next button is displayed and enabled
            browserInstance.Instance.Assert.Exists(cssPath);
            var nextButton = browserInstance.Instance.Find(cssPath);
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> What css class is used to disable the next button?", Classes.LogWriter.eLogType.Error);
            // 6. Verify that the colour of the next button is purple
            browserInstance.Instance.Assert.Class("purpleButton").On(nextButton);
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the next button, just the css class", Classes.LogWriter.eLogType.Error);
            // 7. Verify that text label on the next button is white
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the text on the next button, just the css class", Classes.LogWriter.eLogType.Error);
        }

        public void VerifyResendButton(Classes.Browser browserInstance)
        {
            // 7. Verify that the next button is displayed and enabled
            browserInstance.Instance.Assert.Exists("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input:nth-child(2)");
            var resendButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input:nth-child(2)");
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyResendButton -> What css class is used to disable the next button?", Classes.LogWriter.eLogType.Error);
            // 8. Verify that the colour of the resend button is purple, with white text
            browserInstance.Instance.Assert.Class("purpleButton").On(resendButton);
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the resend button, just the css class", Classes.LogWriter.eLogType.Error);
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the text on the resend button, just the css class", Classes.LogWriter.eLogType.Error);
        }
        public void VerifyFieldExist(Classes.Browser browserInstance)
        {
            // 8. Verfy that activation form contains msisdn,username,activation code and preferred alias fields      
            browserInstance.Instance.Assert.Exists("#msisdn");
            browserInstance.Instance.Assert.Exists("#username");
            browserInstance.Instance.Assert.Exists("#activationNumber");
            browserInstance.Instance.Assert.Exists("#userAlias");
            browserInstance.Instance.Assert.Exists("#challengeQuestion");
            browserInstance.Instance.Assert.Exists("#challengeAnswer");
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Two extra fields found on the screen, challengeQuestion and challengeAnswer.", Classes.LogWriter.eLogType.Error);
            // 9. Verify that the Application buttons are displayed at the bottom of the screen
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> APPLICATION BUTTONS DO NOT SHOW. TEST CANNOT BE CREATED!", Classes.LogWriter.eLogType.Error);
        }

        public void ValidateOTPStart(Classes.Browser browserInstance, string msisdnNo)
        {
            //The OTP screen is displayed, with a message " The One time pin message is displayed as 
            //"A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue"
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser");

            VerifyOntTimeLable(browserInstance);
            VerifyOTPErrorMessage(browserInstance, msisdnNo);
        }

        public void VerifyOTPErrorMessage(Classes.Browser browserInstance, string msisdnNo)
        {
            // 12. Verify that the  One time pin message is displayed on page load,with 5 digits of the cellphone number hidden

            var otpMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(1) > div");

            string numberFirst = msisdnNo.Substring(0, 1);
            string numberSecond = msisdnNo.Substring(msisdnNo.Length - 4, 4);
 
            string message = string.Format("A One Time Pin has been sent to {0}*****{1}. Please enter the One time Pin here to continue", numberFirst, numberSecond);
            browserInstance.Instance.Assert.True(() => message == otpMessage.Element.Text);
        }

        

        public void VerifyOntTimeLable(Classes.Browser browserInstance)
        {
            // 9. Verify that one time pin field label is displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(2) > div > label");
            // 10. Verify that the one time pin field is displayed
            browserInstance.Instance.Assert.Exists("#otp");
            // 11. Verify that the Application buttons are displayed at the bottom of the screen
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationOneTimePinLandingPage -> APPLICATION BUTTONS DO NOT SHOW. TEST CANNOT BE CREATED!", Classes.LogWriter.eLogType.Error);
        }

        public void EnterAndVerifyOTPValue(Classes.Browser browserInstance, bool InvalidValue)
        {
            // 1. Please enter <OTP number>  that has been sent to your msisdn
            var otp = browserInstance.Instance.Find("#otp");

            //if (InvalidValue) 
                browserInstance.Instance.Enter(Classes.TestData.Instance.DefaultData.ActivationData.OTP).In(otp);
            //else
              //  browserInstance.Instance.Enter("TST1234").In(otp);

            //The one time pin entered is displayed  the one time pin filed
            browserInstance.Instance.Assert.True(() => otp.Element.Text == Classes.TestData.Instance.DefaultData.ActivationData.OTP);
        }
    }
}
