using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Activation
{
    public class ActivationActions : TestProj.Interfaces.IActivationActions
    {

        public void TestAliasInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy userAlias)
        {
            // 8. Validate Preferred Alias field
            // 8.1.1 Please enter space before entering input on the field   
            Helpers.Instance.FieldInput(browserInstance, userAlias, " TEST");

            //browserInstance.Instance.Assert.Value(" TEST").Not.In(userAlias);

            LogWriter.Instance.Log("ISSUE 9: TESTCASE:_02_ActivationFormFieldValidation -> Alias field validation not working -> ' TEST'. Assert commented out.", LogWriter.eLogType.Error);
        }

        public void TestActivationKeyInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber)
        {
            // 6. select activation key field
            // 7. Validate the activation key field
            // 7.1.1 Please enter space before entering input on the field 
            Helpers.Instance.TestFieldInputValidation(browserInstance, activationNumber);
        }

        public void TestUsernameInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy username)
        {
            // 4. Select username field                                                   
            // 5. Validate the username field
            // 5.1.1 Please enter space before entering input on username field    
            Helpers.Instance.FieldInput(browserInstance, username, " TEST");

            //browserInstance.Instance.Assert.Value(" TEST").Not.In(username);

            LogWriter.Instance.Log("ISSUE 8: TESTCASE:_02_ActivationFormFieldValidation -> Username field validation not working -> ' TEST'. Assert commented out.", LogWriter.eLogType.Error);
        }
        public void TestMSISDNInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn)
        {
            // 2. Select msisdn field     
            // 3. Verify that the msisdn field validation will be limited to Numeric format      
            Helpers.Instance.TestFieldInputValidation(browserInstance, msisdn);
        }

        public void TestMandatoryFields(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer)
        {
            // 1. Verify that all text fields are mandatory on the form
            Helpers.Instance.TestMandatory(browserInstance, msisdn);
            Helpers.Instance.TestMandatory(browserInstance, username);
            Helpers.Instance.TestMandatory(browserInstance, activationNumber);
            Helpers.Instance.TestMandatory(browserInstance, userAlias);
            Helpers.Instance.TestMandatory(browserInstance, challengeQuestion);
            Helpers.Instance.TestMandatory(browserInstance, challengeAnswer);

            // 1.1.1 Please don’t enter anything on all the fields and click next
            //Test must be Error Message is displayed: “E1-1-7 – Please complete all fields”. 
            //  all fields must either be highlited in red or displayed on the screen            
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(7) > div > input"));

            CheckErrorPopup(browserInstance, "E1-1-7", "Please complete all fields.");

            Helpers.Instance.CheckClass(browserInstance, "has-error", msisdn);
            Helpers.Instance.CheckClass(browserInstance, "has-error", username);
            Helpers.Instance.CheckClass(browserInstance, "has-error", activationNumber);
            Helpers.Instance.CheckClass(browserInstance, "has-error", userAlias);
            Helpers.Instance.CheckClass(browserInstance, "has-error", challengeQuestion);
            Helpers.Instance.CheckClass(browserInstance, "has-error", challengeAnswer);
        }

        public void CheckErrorPopup(Classes.Browser browserInstance, string errorCode, string errorMessage)
        {
            Thread.Sleep(1000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#errorModal > div"));
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(2) > div.col-sm-9 > div.errorHeading.ng-binding"), errorCode);
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(2) > div.col-sm-9 > div.errorDetailsList > ul > li.ng-binding"), errorMessage);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(3) > div > button"));
            Thread.Sleep(1000);
        }

        public void TestMSISDNLengthLimitSmaller(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {
            LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> TEST CASE DESCRIPTION INVALID: 3.2.1 Enter msisdn number less than 10 , meaning 11 digit on msisdn field", LogWriter.eLogType.Error);
            // 3.2.1 Enter msisdn number less than 10 , meaning 11 digit on msisdn field         
            Helpers.Instance.FieldInput(browserInstance, msisdn, "0821");
            // 3.2.1.1 Enter valid  username    
            Helpers.Instance.FieldInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Username);
            LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", LogWriter.eLogType.Error);
            // 3.2.1.2  Enter valid activation key, any number/string that is accepted by the field 
            Helpers.Instance.FieldInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ActivationKey);
            LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid ACTIVATION No required", LogWriter.eLogType.Error);
            // 3.2.1.3 Enter any user defined preferred alias     
            Helpers.Instance.FieldInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias);

            Helpers.Instance.DropdownItemSelect(browserInstance, challengeQuestion, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeQuestion);

            Helpers.Instance.FieldInput(browserInstance, challengeAnswer, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeAnswer);
            // 3.2.1.4 click on the next button   
            Helpers.Instance.ClickButton(browserInstance, nextButton);
            // TEST:  3.2.1.4  An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            CheckErrorPopup(browserInstance, "E1-1-7", "Please complete all fields.");

            Helpers.Instance.CheckClass(browserInstance, "has-error", msisdn);
        }

        public void TestMSISDNLengthLimitGreater(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {
            LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> TEST CASE DESCRIPTION INVALID: 3.1.1 Enter msisdn number > 10 , meaning 9 digit on msisdn field", LogWriter.eLogType.Error);
            // 3. Verify that is limited to 10 digit numbers
            // 3.1.1 Enter msisdn number > 10 , meaning 9 digit on msisdn field                     
            Helpers.Instance.FieldInput(browserInstance, msisdn, "08212345623");
            // 3.1.2 Enter valid  username     
            Helpers.Instance.FieldInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Username);
            LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", LogWriter.eLogType.Error);
            // 3.1.3  Enter Invalid activation key, any number/string that is accepted by the field 
            Helpers.Instance.FieldInput(browserInstance, activationNumber, "4444444419");
            // 3.1.4  Enter any user defined preferred alias                                        
            Helpers.Instance.FieldInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias);

            Helpers.Instance.DropdownItemSelect(browserInstance, challengeQuestion, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeQuestion);

            Helpers.Instance.FieldInput(browserInstance, challengeAnswer, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeAnswer);
            // 3.1.5 click on the next button                                                       
            Helpers.Instance.ClickButton(browserInstance, nextButton);
            // TEST: 3.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red

            CheckErrorPopup(browserInstance, "E1-1-7", "Please complete all fields.");

            Helpers.Instance.CheckClass(browserInstance, "has-error", msisdn);
        }

        public void TestInvalidUsername(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {
            // 2. Invalid username  – username does not match one saved in BOP Manager              
            // 2.1.1 Enter valid msisdn                                                             
            Helpers.Instance.FieldInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.MSISDN);
            // 2.1.2 Enter invalid  username          
            Helpers.Instance.FieldInput(browserInstance, username, "InvalidTestUser");
            // 2.1.3  Enter valid activation key, any number/string that is accepted by the field   
            Helpers.Instance.FieldInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ActivationKey);
            // 2.1.4  Enter any user defined preferred alias                                        
            Helpers.Instance.FieldInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias);

            Helpers.Instance.DropdownItemSelect(browserInstance, challengeQuestion, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeQuestion);

            Helpers.Instance.FieldInput(browserInstance, challengeAnswer, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeAnswer);
            // 2.1.5 click on the next button                               
            Helpers.Instance.ClickButton(browserInstance, nextButton);
            // TEST: 2.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            //browserInstance.Instance.Assert.Value("E1-1 -3 invalid input").In(errorMessage);
            Thread.Sleep(2000);
            CheckErrorPopup(browserInstance, "E2-1-13", "Username or ActivationCode is invalid");
        }

        public void TestValidSingleUserDetails(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {
            Helpers.Instance._ActivationForm_EnterCorrectSingleUserDetails(browserInstance, msisdn, username, activationNumber, userAlias, challengeQuestion, challengeAnswer, nextButton);
        }
        public void TestValidMultiUserDetails(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {
            Helpers.Instance._ActivationForm_EnterCorrectMultiUserDetails(browserInstance, msisdn, username, activationNumber, userAlias, challengeQuestion, challengeAnswer, nextButton);
        }

        public void ClickNext(Classes.Browser browserInstance, FluentAutomation.ElementProxy nextButton)
        {
            Helpers.Instance.ClickButton(browserInstance, nextButton);
        }

        public void TestInvalidActivationKey(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton)
        {
            // 1. Invalid Activation Key – does not match one saved in BOP Manager                  
            // 1.1.1 Enter valid msisdn                                   
            Helpers.Instance.FieldInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.MSISDN);
            // 1.1.2 Enter valid  username                                                          
            Helpers.Instance.FieldInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Username);
            // 1.1.3  Enter Invalid activation key, any number/string that is accepted by the field 
            Helpers.Instance.FieldInput(browserInstance, activationNumber, "44444109");
            // 1.1.4  Enter any user defined preferred alias                                        
            Helpers.Instance.FieldInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.Alias);

            Helpers.Instance.DropdownItemSelect(browserInstance, challengeQuestion, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeQuestion);

            Helpers.Instance.FieldInput(browserInstance, challengeAnswer, TestData.Instance.DefaultData.ActivationData.SingleSpazaUser.ChallengeAnswer);
            // 1.1.5 click on the next button   
            Helpers.Instance.ClickButton(browserInstance, nextButton);
            // TEST: 1.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            //browserInstance.Instance.Assert.Value("E1-1 -3 invalid input").In(errorMessage);


        }
        public void VerifyLogoAndBanner(Classes.Browser browserInstance)
        {
            //1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            Helpers.Instance.CheckLogoAndBanner(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > img", "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
        }
        public void VerifyOnlineIndicator(Classes.Browser browserInstance)
        {
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            Helpers.Instance.CheckOnlineIndicator(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div > div");
        }
        public void VerifyPageLinks(Classes.Browser browserInstance)
        {
            // 3. Verify that contact us and help me hyperlinks are displayed
            Helpers.Instance.CheckPageLinks(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUs", "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMe");
        }

        public void VerifyOTPNextButton(Classes.Browser browserInstance)
        {
            // 5. Verify that the button is displayed and enabled
            Helpers.Instance.CheckButtonEnabled(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input.btn.btn-large.nextBtn.pull-right.purpleButton.ng-scope.ng-binding");

            LogWriter.Instance.Log("ISSUE 22: TESTCASE:VerifyActivationLandingPage -> What css class is used to disable the next button?", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("ISSUE 23: TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the next button, just the css class", LogWriter.eLogType.Error);
            // 7. Verify that text label on the next button is white
            LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the text on the next button, just the css class", LogWriter.eLogType.Info);

        }
        public void VerifyActivationPageNextButton(Classes.Browser browserInstance)
        {
            Helpers.Instance.CheckButtonEnabled(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(7) > div > input");

            LogWriter.Instance.Log("ISSUE 24: TESTCASE:VerifyActivationLandingPage -> What css class is used to disable the next button?", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("ISSUE 25: TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the next button, just the css class", LogWriter.eLogType.Error);
            // 7. Verify that text label on the next button is white
            LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the text on the next button, just the css class", LogWriter.eLogType.Error);

        }

        public void VerifyResendButton(Classes.Browser browserInstance)
        {
            // 7. Verify that the next button is displayed and enabled
            Helpers.Instance.CheckButtonEnabled(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input:nth-child(2)");

            LogWriter.Instance.Log("ISSUE 26: TESTCASE:VerifyResendButton -> What css class is used to disable the next button?", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("ISSUE 27: TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the resend button, just the css class", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the text on the resend button, just the css class", LogWriter.eLogType.Info);
        }
        public void VerifyFieldExist(Classes.Browser browserInstance)
        {
            // 8. Verfy that activation form contains msisdn,username,activation code and preferred alias fields      
            Helpers.Instance.Exists(browserInstance, "#msisdn");
            Helpers.Instance.Exists(browserInstance, "#username");
            Helpers.Instance.Exists(browserInstance, "#activationNumber");
            Helpers.Instance.Exists(browserInstance, "#userAlias");
            Helpers.Instance.Exists(browserInstance, "#challengeQuestion");
            Helpers.Instance.Exists(browserInstance, "#challengeAnswer");
        }

        public void ValidateOTPStart(Classes.Browser browserInstance, string msisdnNo)
        {
            //The OTP screen is displayed, with a message " The One time pin message is displayed as 
            //"A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue"
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser");

            VerifyOntTimeLable(browserInstance);
            VerifyOTPErrorMessage(browserInstance, msisdnNo);
        }

        public void VerifyOTPErrorMessage(Classes.Browser browserInstance, string msisdnNo, bool resent = false)
        {
            // 12. Verify that the  One time pin message is displayed on page load,with 5 digits of the cellphone number hidden
            var otpMessage = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(1) > div");

            string numberFirst = msisdnNo.Substring(0, 4);
            string numberSecond = msisdnNo.Substring(msisdnNo.Length - 2, 2);
             
            string message = resent?
                string.Format("A One Time Pin has been re-sent to {0}****{1}. Please enter the One Time Pin here to continue", numberFirst, numberSecond)
            :
                string.Format("A One Time Pin has been sent to {0}****{1}. Please enter the One time Pin here to continue", numberFirst, numberSecond);
            browserInstance.Instance.Assert.Value(message).In(otpMessage);
        }



        public void VerifyOntTimeLable(Classes.Browser browserInstance)
        {
            // 9. Verify that one time pin field label is displayed
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(2) > div > label");
            // 10. Verify that the one time pin field is displayed
            Helpers.Instance.Exists(browserInstance, "#otp");
            // 11. Verify that the Application buttons are displayed at the bottom of the screen
        }

        public void EnterAndVerifyOTPValue(Browser browserInstance, FluentAutomation.ElementProxy otp, string otpVal)
        {
            // 1. Please enter <OTP number>  that has been sent to your msisdn
            var varOtp = browserInstance.Instance.Find("#otp");

            Helpers.Instance.FieldInput(browserInstance, varOtp, otpVal);
            //browserInstance.Instance.Enter(otpVal).In(varOtp);
            //The one time pin entered is displayed  the one time pin filed
            browserInstance.Instance.Assert.Value(otpVal).In(otp);

        }

        public void VerifyCatalogueLandingPage(Classes.Browser browserInstance)
        {
            /// 5. Verify that the update button is displayed and enabled
            var updateButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(3) > div.formRow > button");
            browserInstance.Instance.Assert.True(() => updateButton.Element.Attributes.Get("disabled") != "disabled");

            LogWriter.Instance.Log("ISSUE 29: TESTCASE: _11_SetupCatalogueLandingPage -> There is no test case that tests the update button enable/disable for single spaza users", LogWriter.eLogType.Error);
            /// 6. Verify that the colour of the update button is purple
            /// 7. Verify that text label on the update  button is white
            LogWriter.Instance.Log("ISSUE 30: TESTCASE: _11_SetupCatalogueLandingPage -> Cannot verify the button background colour and label text on the button", LogWriter.eLogType.Info);
            /// 8. Verify that Manage Catalogue Label is displayed
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.subtitle > ul.alerts > li");
            /// 9. Verify that the select wholesaler a label is is displayed
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div:nth-child(1)");
            /// 10. Search functionality field is displayed with a search icon/button  
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div.formRow.catalogsearch > button");
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
        }

        public void ClickSearchButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy mcatSearchButton)
        {
            Helpers.Instance.ClickButton(browserInstance, mcatSearchButton);
        }

        public void CatSearchTest(Classes.Browser browserInstance, FluentAutomation.ElementProxy searchValue, FluentAutomation.ElementProxy mcatSearchButton, string val, bool expectResult)
        {
            Helpers.Instance.FieldInput(browserInstance, searchValue, val);
            ClickSearchButton(browserInstance, mcatSearchButton);
            if (expectResult)
            {
                Helpers.Instance.Exists(browserInstance, "#catalog75-100km > ul > li:nth-child(1)");
            }
            else
            {
                Thread.Sleep(3000);
                browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#outletSearchNotFound > div"));
                Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#outletSearchNotFound > div > div > div.modal-header.vodaBackgroundGrey > div.successMsg > strong"), "No Search Result !");
                Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#outletSearchNotFound > div > div > div.modal-body.text-center > p:nth-child(1)"), "Sorry, your search returned no results!");
                Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#outletSearchNotFound > div > div > div.modal-body.text-center > div > button"));
                Thread.Sleep(2000);
            }
        }

        public void ResetSearch(Classes.Browser browserInstance, FluentAutomation.ElementProxy searchValue, FluentAutomation.ElementProxy mcatSearchButton)
        {
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "", true);
        }

        public void TestCatalogueSeachValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy searchValue, FluentAutomation.ElementProxy mcatSearchButton, FluentAutomation.ElementProxy mcatErrorMessage)
        {

            /// 1.1 Without entering anything click on search button.
            /// 1.1 An error message is displayed: “E1-1-7 – Please complete all fields”. all fields must either be highlighted in red or displayed on the screen  
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "", true);
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields. all fields must either be highlited in red or displayed on the screen").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 14: TESTCASE: _12_SetupCatalogueValidations -> 1.1 (NO VALUE) -> Cannot test the error message -> E1-1-7 – Please complete all fields. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.2 Click in the search field and press Enter key.
            LogWriter.Instance.Log("TESTCASE: _12_SetupCatalogueValidations -> 1.2 Cannot simulate press enter key. Test ignored ", LogWriter.eLogType.Info);

            /// 1.3 Enter any  one character and click on search button/press Enter key .
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "x", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, "x");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 15: TESTCASE: _12_SetupCatalogueValidations -> 1.3 (ONE CHAR) -> No error message found on page. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.4 Enter only special characters and click on Search button
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "%$#@", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, "%$#@");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 16: TESTCASE: _12_SetupCatalogueValidations -> 1.4 (SPECIAL CHARS) -> No error message found on page. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.5 Enter  only numbers and click on search button
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "1234567", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, "1234567");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 17: TESTCASE: _12_SetupCatalogueValidations -> 1.5 (ONLY NUMBERS) -> No error message found on page. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.6 Enter alphanumeric characters and click on search button
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "1Te2s", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, "1Te2st");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 18: TESTCASE: _12_SetupCatalogueValidations -> 1.6 (APLHA CHARS) -> No error message found on page. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.7 Enter  alphanumeric characters and special characters and click on search button.
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "1Te2st!@#", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, "1Te2st!@#");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 19: TESTCASE: _12_SetupCatalogueValidations -> 1.7 (APLHA + SPECIAL CHARS) -> No error message found on page. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.8 Enter string more than the max char limit of the field.     
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 20: TESTCASE: _12_SetupCatalogueValidations -> 1.8 (MAX LIMIT) -> Cannot test max limit as there ae no max limit set on the element. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

            /// 1.9 Enter string with spaces(before string , after string  and in between) and verify the results      
            CatSearchTest(browserInstance, searchValue, mcatSearchButton, " TEST WITH SPACES BEFORE AND AFTER ", false);
            ResetSearch(browserInstance, searchValue, mcatSearchButton);
            //Helpers.Instance.FieldInput(browserInstance, searchValue, " TEST WITH SPACES BEFORE AND AFTER ");
            //ClickSearchButton(browserInstance, mcatSearchButton);
            //browserInstance.Instance.Assert.Value("E1-1-7 – Please complete all fields.").In(mcatErrorMessage);
            //LogWriter.Instance.Log("ISSUE 21: TESTCASE: _12_SetupCatalogueValidations -> 1.9 (SPACES BEFORE AND AFTER) -> No error message found on page. Assert commented out -> Nothing happens", LogWriter.eLogType.Error);

        }

        public void ValidateOTP(Classes.Browser browserInstance)
        {
            // 3.1.1 Please enter space before entering input on the field 
            // 3.1.1 a space before any input  is not allowed 
            // 3.1.2 Please enter decimal numbers <0.00444> 
            // 3.1.2  decimal or float numbers are not allowed
            // 3.1.3 Please enter negative value <-1>  
            // 3.1.3  A negative number is not allowed 

            //var otpNextButton = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(4) > div > div > input.btn.btn-large.nextBtn.pull-right.purpleButton.ng-scope.ng-binding");
            //var otpErrorMessage = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");
            var otp = Helpers.Instance.GetProxy(browserInstance, "#otp");

            Helpers.Instance.TestFieldInputValidation(browserInstance, otp);
        }
    }
}
