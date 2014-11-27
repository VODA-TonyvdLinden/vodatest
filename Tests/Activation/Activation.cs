using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProj.Classes;

namespace TestProj.Tests.Activation
{
    [TestFixture, Description("Activation"), Category("Activation")]
    public class Activation
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
        FluentAutomation.ElementProxy mcatSearch;
        FluentAutomation.ElementProxy mcatSearchButton;
        FluentAutomation.ElementProxy mcatBackButton;
        FluentAutomation.ElementProxy mcatNextButton;
        FluentAutomation.ElementProxy mcatUpdateButton;
        FluentAutomation.ElementProxy mcatErrorMessage;

        [TestFixtureSetUp]
        public void Initialise()
        {
            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeAnswer, out activationNextButton, out activationErrorMessage);
            getOTPControls(browserInstance, out otp, out otpNextButton, out optResendButton, out otpErrorMessage);
            getManageCatalogueControls(browserInstance, out mcatSearch, out mcatSearchButton, out mcatBackButton, out mcatNextButton, out mcatUpdateButton, out mcatErrorMessage);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
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

        private void getManageCatalogueControls(Classes.Browser browserInstance, out FluentAutomation.ElementProxy mcatSearch, out FluentAutomation.ElementProxy mcatSearchButton, out FluentAutomation.ElementProxy mcatBackButton, out FluentAutomation.ElementProxy mcatNextButton, out FluentAutomation.ElementProxy mcatUpdateButton, out FluentAutomation.ElementProxy mcatErrorMessage)
        {
            mcatSearch = browserInstance.Instance.Find("#msisdn");
            mcatSearchButton = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div.formRow.catalogsearch > button");
            mcatBackButton = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > div > div.backBtnSection > button");
            mcatNextButton = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > div > div.nextBtnSection > button");
            mcatUpdateButton = browserInstance.Instance.Find("#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(3) > div.formRow > button");
            mcatErrorMessage = browserInstance.Instance.Find(""); ; // No error message outputting at the moment - still to be identified where on the page this is displayed
        }

        /// <summary>
        /// TEST: VERIFY ACTIVATION LANDING PAGE
        /// Test Case ID: 1_FRS_Ref_6.1.1
        /// Category: Activation Page
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activativation Page
        /// TEST STEPS:
        /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, Grammar and alignment 
        /// 5. Verify that the next button is displayed and enabled
        /// 6. Verify that the colour of the next button is purple
        /// 7. Verify that text label on the next button is white
        /// 8. Verfy that activation form contains msisdn,username,activation code and preferred alias fields      
        /// 9. Verify that the Application buttons are displayed at the bottom of the screen
        /// TEST OUTPUT:
        /// 1. The vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyerlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to rotate from Portrait to landscape)
        /// 5. The next button is displayed and enabled
        /// 6. The button colour is purple
        /// 7. The text label on the next button is white
        /// 8.The msisdn, username, activation code and preffered alias fields are
        /// 9. The Application button are displayed at the bottom screen
        /// </summary>
        /// <param name="browserInstance"></param>
        /// <param name="screenshotRequirement"></param>
        [Test, Description("_01_VerifyActivationLandingPage"), Category("Activation Page"), Repeat(1)]
        public void _01_VerifyActivationLandingPage()
        {
            //http://aspnet.dev.afrigis.co.za/bopapp/#/activation
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));

            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
            activationAction.VerifyLogoAndBanner(browserInstance);
            activationAction.VerifyOnlineIndicator(browserInstance);
            activationAction.VerifyPageLinks(browserInstance);
            // 4. See spelling, Grammar and alignment 
            //CONNOT DO THAT
            activationAction.VerifyActivationPageNextButton(browserInstance);
            activationAction.VerifyFieldExist(browserInstance);
        }

        /// <summary>
        /// TEST: ACTIVATION FORM  FIELD VALIDATIONS
        /// Test Case ID: 1_FRS_Ref_6.1.1
        /// Category: Activation form
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activativation Page
        /// TEST STEPS::
        /// 1. Verify that all text fields are mandatory on the form
        /// 1.1.1 Please don’t enter anything on all the fields and click next
        /// 2. Select msisdn field                                                                                                                                     
        /// 3. Verify that the msisdn field validation will be limited to Numeric format                                        
        ///   3.1.1 Please enter alphanumeric  < 07@ >
        ///   3.1.2 Please enter space before entering input on the field
        ///   3.1.3 Please enter special characters  <@@, &&> 
        ///   3.1.4 Please enter decimal numbers <0.00444> 
        ///   3.1.5 Please enter negative value <-1>                                                                                                  
        /// 4. Select username field                                                   
        /// 5. Validate the username field
        ///   5.1.1 Please enter space before entering input on username field                                                                                              
        /// 6. select activation key field
        /// 7. Validate the activation key field
        ///   7.1.1 Please enter space before entering input on the field                                                                    
        /// 8. Validate Preferred Alias field
        ///   8.1.1 Please enter space before entering input on the field 
        /// TEST OUTPUT:
        /// 1. Error Message is displayed: “E1-1-7 – Please complete all fields”. all fields must either be highlited in red or displayed on the screen
        /// 2. Focus on the msisdn field
        /// 3.Invalid data should not be allowed to be entered in msisdn field
        ///   3.1.1 aphanumerics are not allowed
        ///   3.1.2 a space before any input  is not allowed
        ///   3.1.3 special characters are not allowed
        ///   3.1.4 decimal numbers or float should are not allowed
        ///   3.1.5 Negative numbers should are not allowed
        /// 4. Focus is on username field
        /// 5. Invalid data should not be allowed to be entered in username field
        ///   5.1.1 The space should not be allowed
        /// 6.Focus is on activation key field
        /// 7.Invalid data shoud not be allowed to be entered in the activation key field
        ///   7.1.1 A space before typing any input is not allowed
        /// 8.Invalid data shoud not be allowed to be entered in the activation key field
        ///   8.1.1  A space before typing any input is not allowed
        /// </summary>
        /// <param name="browserInstance"></param>
        /// <param name="screenshotRequirement"></param>
        [Test, Description("_02_ActivationFormFieldValidation"), Category("Activation Page"), Repeat(1)]
        public void _02_ActivationFormFieldValidation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));

            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeAnswer, out activationNextButton, out activationErrorMessage);

            activationAction.TestMandatoryFields(browserInstance, msisdn, username, activationNumber, userAlias, challengeAnswer, activationErrorMessage);
            activationAction.TestMSISDNInputValidation(browserInstance, msisdn);
            activationAction.TestUsernameInputValidation(browserInstance, username);
            activationAction.TestActivationKeyInputValidation(browserInstance, activationNumber);
            activationAction.TestAliasInputValidation(browserInstance, userAlias);
        }

        /// <summary>
        /// TEST: ACTIVATION FORM INCORRECT USER DETAILS
        /// Test Case ID: 1_FRS_Ref_6.1.1
        /// Category: Activation form
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activativation Page
        /// Test steps:
        /// 1. Invalid Activation Key – does not match one saved in BOP Manager                                             
        ///   1.1.1 Enter valid msisdn                                                                                                                              
        ///   1.1.2 Enter valid  username                                                                                                                        
        ///   1.1.3  Enter Invalid activation key, any number/string that is accepted by the field                                   
        ///   1.1.4  Enter any user defined preferred alias                                                                                             
        ///   1.1.5 click on the next button                                                                                                                   
        /// 2. Invalid username  – username does not match one saved in BOP Manager                                             
        ///   2.1.1 Enter valid msisdn                                                                                                                           
        ///   2.1.2 Enter invalid  username                                                                                                                        
        ///   2.1.3  Enter valid activation key, any number/string that is accepted by the field                                   
        ///   2.1.4  Enter any user defined preferred alias                                                                                             
        ///   2.1.5 click on the next button                               
        /// 3. Verify that is limited to 10 digit numbers
        ///   3.1.1 Enter msisdn number > 10 , meaning 9 digit on msisdn field                                                                                                                          
        ///   3.1.2 Enter valid  username                                                                                                                       
        ///   3.1.3  Enter Invalid activation key, any number/string that is accepted by the field                                   
        ///   3.1.4  Enter any user defined preferred alias                                                                                             
        ///   3.1.5 click on the next button                                                                                                                   
        ///   3.2.1 Enter msisdn number less than 10 , meaning 11 digit on msisdn field                                                          
        ///     3.2.1.1 Enter valid  username                                                                                                                        
        ///     3.2.1.2  Enter valid activation key, any number/string that is accepted by the field                                   
        ///     3.2.1.3 Enter any user defined preferred alias                                                                                             
        ///     3.2.1.4 click on the next button      
        /// TEST OUTPUT:
        /// 1.This is an intergartion test covers that covers  the communication between app,mas and bop
        ///   1.1.1The  msisdn is displayed in the msisdn field
        ///   1.1.2 The username is displayed in the username field
        ///   1.1.3 The activation key is displayed in the activation field key
        ///   1.1.4 The preferred alias is displayed
        ///   1.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
        /// 2. This is an intergartion test that covers the communication between app, mas and bop
        ///   2.1.1The msisdn is displayed in the msisdn field
        ///   2.1.2 The username is displayed in the username field
        ///   2.1.3  The activation key is displayed in the activation field key
        ///   2.1.4  The preferred alias is displayed
        ///   2.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
        /// 3.This is to verify that the correct number of digits
        ///   3.1.1The msisdn is displayed in the msisdn field
        ///   3.1.2 The username is displayed in the username field
        ///   3.1.3  The activation key is displayed in the activation field key
        ///   3.1.4  The preferred alias is displayed
        ///   3.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
        ///   3.2.1 The msisdn is displayed in the msisdn field
        ///     3.2.1.1 The username is displayed
        ///     3.2.1.2  The valid activation is displayed
        ///     3.2.1.3  The preferred alias is displayed
        ///     3.2.1.4  An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
        /// </summary>
        /// <param name="browserInstance"></param>
        /// <param name="screenshotRequirement"></param>
        [Test, Description("_03_ActivationFormIncorrectUserDetails"), Category("Activation form"), Repeat(1)]
        public void _03_ActivationFormIncorrectUserDetails()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));

            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            activationAction.TestInvalidActivationKey(browserInstance, msisdn, username, activationNumber, userAlias, activationNextButton, activationErrorMessage);
            activationAction.TestInvalidUsername(browserInstance, msisdn, username, activationNumber, userAlias, activationNextButton, activationErrorMessage);
            activationAction.TestMSISDNLengthLimitGreater(browserInstance, msisdn, username, activationNumber, userAlias, activationNextButton, activationErrorMessage);
            activationAction.TestMSISDNLengthLimitSmaller(browserInstance, msisdn, username, activationNumber, userAlias, activationErrorMessage);
        }

        /// <summary>
        /// TEST: ACTIVATION FORM CORRECT USER DETAILS
        /// Test Case ID: 1_FRS_Ref_6.1.1
        /// Category: Activation form
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activativation Page
        /// TEST STEPS:
        /// 1.Valid User Details - Verify that Username and Activation Key Matches with BOP Manager
        ///   1.1.1 Enter valid msisdn
        ///   1.1.2 Enter valid  username
        ///   1.1.3  Enter valid activation key, any number/string that is accepted by the field
        ///   1.1.4  Enter any user defined preferred alias
        ///   1.1.5 Press the next button
        /// TEST OUTPUT:
        /// 1. This is a positive testfor verify the Bop details match the ones on BOP
        ///   1.1.1 The msisdn is displayed
        ///   1.1.2 The username is displayed
        ///   1.1.3  The Valid Key is displayed
        ///   1.1.4  The preferred alias is displayed
        ///   1.1.5   The OTP screen is displayed, with a message " The One time pin message is displayed as " A One Time 
        /// Pin has been sent to 0*****1234. Please enter the One time Pin here to continue                                                                  
        /// </summary>
        [Test, Description("_04_ActivationFormCorrectUserDetails"), Category("Activation form"), Repeat(1)]
        public void _04_ActivationFormCorrectUserDetails()
        {


            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            //   1.1.1 Enter valid msisdn
            activationAction.MSISDNInput(browserInstance, msisdn, TestData.Instance.DefaultData.ActivationData.MSISDN);

            //   1.1.2 Enter valid  username
            activationAction.UsernameInput(browserInstance, username, TestData.Instance.DefaultData.ActivationData.Username);

            //   1.1.3  Enter valid activation key, any number/string that is accepted by the field
            activationAction.ActivationKeyInput(browserInstance, activationNumber, TestData.Instance.DefaultData.ActivationData.ActivationKey);

            //   1.1.4  Enter any user defined preferred alias
            activationAction.AliasInput(browserInstance, userAlias, TestData.Instance.DefaultData.ActivationData.Alias);
            //   1.1.5 Press the next button
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormCorrectUserDetails -> CHALLENGE ANSWER is required, but the test does not specify that it needs to be filled out. UPDATE TEST", Classes.LogWriter.eLogType.Error);
            browserInstance.Instance.Enter("NOT REQUIRED").In(challengeAnswer);

            activationAction.ClickNext(browserInstance, activationNextButton);

            activationAction.ValidateOTPStart(browserInstance, msisdn.Element.Text);
        }

        /// <summary>
        /// TEST: ACTIVATION FORM CORRECT USER DETAILS
        /// Test Case ID: 1_FRS_Ref_6.1.1
        /// Category: Activation_form
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activation Page
        /// TEST STEPS:
        /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, Grammar and alignment 
        /// 5. Verify that the next button is displayed and enabled
        /// 6. Verify that the colour of the next button is purple, with white text
        /// 7. Verify that the resend button is displayed and enabled
        /// 8. Verify that the colour of the resend button is purple, with white text
        /// 9. Verify that one time pin field label is displayed
        /// 10. Verify that the one time pin field is displayed
        /// 11. Verify that the Application buttons are displayed at the bottom of the screen
        /// 12. Verify that the  One time pin message is displayed on page load,with 5 digits of the cellphone number hidden
        /// TEST OUTPUT:
        /// 1. The vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyerlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to rotate from Portrait to landscape)
        /// 5. The next button is displayed and enabled
        /// 6. The button colour is purple, with white text
        /// 7. The resend button is displayed and enabled
        /// 8. The button colour is purple, with white text
        /// 9. The one time pin filed label is displayed on the screen
        /// 10. The one time pin field is displayed
        /// 11. The Application button are displayed at the bottom screen
        /// 12. The One time pin is sent on the msisdn and  a message is displayed as 
        /// " A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue"
        /// </summary>
        [Test, Description("_05_VerifyActivationOneTimePinLandingPage"), Category("Activation_form"), Repeat(1)]
        public void _05_VerifyActivationOneTimePinLandingPage()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            // 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            activationAction.VerifyLogoAndBanner(browserInstance);
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            activationAction.VerifyOnlineIndicator(browserInstance);
            // 3. Verify that contact us and help me hyperlinks are displayed
            activationAction.VerifyPageLinks(browserInstance);
            // 4. See spelling, Grammar and alignment 
            Classes.LogWriter.Instance.Log("Cannot check spelling and grammer automatically", Classes.LogWriter.eLogType.Error);
            // 5. Verify that the next button is displayed and enabled
            // 6. Verify that the colour of the next button is purple, with white text
            activationAction.VerifyOTPNextButton(browserInstance);
            // 7. Verify that the resend button is displayed and enabled
            // 8. Verify that the colour of the resend button is purple, with white text
            activationAction.VerifyResendButton(browserInstance);
            activationAction.VerifyOntTimeLable(browserInstance);
            activationAction.VerifyOTPErrorMessage(browserInstance, Classes.TestData.Instance.DefaultData.ActivationData.MSISDN);
        }

        /// <summary>
        /// TEST: VERIFY ACTIVATION ONE TIME PIN LANDING PAGE
        /// Test Case ID: 2_FRS_Ref_5.1.1
        /// Category: Verify_user(OTP)
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activation Page
        /// TEST STEPS:
        /// 1. Verify that all text fields are mandatory on the form
        ///   1.1.1 Please don’t enter anything on the fields and click next
        /// 2. Select OTP field
        /// 3.  Verify that the msisdn field validation will be limited to Numeric format
        ///   3.1.1 Please enter space before entering input on the field
        ///   3.1.2 Please enter decimal numbers <0.00444>
        ///   3.1.3 Please enter negative value <-1>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        /// TEST OUTPUT:
        ///   1.1.1 Error Message is displayed: “E1-1-7 – Please complete all fields”. and the field is highlited in red
        /// 2.Focus is on the OTP field
        /// 3.Invalid data should not be allowed to be entered in the OTP field
        ///   3.1.1 a space before any input  is not allowed
        ///   3.1.2  decimal or float numbers are not allowed
        ///   3.1.3  A negative number is not allowed
        /// </summary>
        [Test, Description("_06_ActivationOneTimePinFieldValidation"), Category("Verify_user(OTP)"), Repeat(1)]
        public void _06_ActivationOneTimePinFieldValidation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
        }

        /// <summary>
        /// TEST: CORRECT ONE TIME PIN AND APPLICATION OFFLINE
        /// Test Case ID: 2_FRS_Ref_5.1.1
        /// Category: Verify_user(OTP)
        /// Feature: Activation
        /// Pre-Condition: Application Offline
        /// Environment: 
        /// TEST STEPS:
        /// 1. Please enter <OTP number>  that has been sent to your msisdn
        /// 2. Press the <next>  button
        /// 3. Verify that the when the application is online again it returns to the activation page
        /// TEST OUTPUT:
        /// 1. The one time pin entered is displayed  the one time pin filed
        /// 2. An error message is displayed[ error: “O1-2-6 – You are not online. Please check your connectivity and try again”
        /// 3. When the application is online again, it must return to the activation page
        /// </summary>
        [Test, Description("_07_CorrectOneTimePinAndApplicationOffline"), Category("Verify_user(OTP)"), Repeat(1)]
        public void _07_CorrectOneTimePinAndApplicationOffline()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            Classes.LogWriter.Instance.Log("TESTCASE:CorrectOneTimePinAndApplicationOffline -> Incomplete test case -> Must check if application is offline", Classes.LogWriter.eLogType.Error);
            //Check if application is offline
            var onlineOfflineIndicator = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div > div");
            browserInstance.Instance.Assert.Class("offline").On(onlineOfflineIndicator);

            // 1. Please enter <OTP number>  that has been sent to your msisdn
            activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.OTP);

            // 2. Press the <next>  button
            activationAction.ClickNext(browserInstance, otpNextButton);

            // 2. An error message is displayed[ error: “O1-2-6 – You are not online. Please check your connectivity and try again”
            Classes.LogWriter.Instance.Log("This error is not displayed due to browser always online --> O1-2-6 – You are not online. Please check your connectivity and try again", Classes.LogWriter.eLogType.Error);
            Classes.LogWriter.Instance.Log("TESTCASE:CorrectOneTimePinAndApplicationOffline -> Cannot simulate the off-line status as the browser will allways be online", Classes.LogWriter.eLogType.Error);

        }

        /// <summary>
        /// TEST: INCORRECT ONE TIME PIN
        /// Test Case ID: 2_FRS_Ref_5.1.1
        /// Category: Verify_user(OTP)
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activation Page
        /// TEST STEPS:
        /// 1. Enter Invalid OTP
        ///   1.1 Please enter <Invalid OTP>
        ///   1.2 Press the <next> button
        /// 2. Expired OTP
        ///   2.1 Plese enter <Expired OTP>
        ///   2.2 Press the <next> button
        /// TEST OUTPUT:
        /// 1.
        ///   1.1 The one time pin entered is displayed  the one time pin filed
        ///   1.2 An error message is displayed[ error: “O1-2-8 – Passwords do not match. Please try again”
        /// 2.
        ///   2.1 The one time pin entered is displayed  the one time pin filed
        ///   2.2 An error message is displayed[ error: “O1-2-8 – Passwords do not match. Please try again”
        /// </summary>
        [Test, Description("_08_IncorrectOneTimePin"), Category("Verify_user(OTP)"), Repeat(1)]
        public void _08_IncorrectOneTimePin()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            /// 1. Enter Invalid OTP
            ///   1.1 Please enter <Invalid OTP>
            /// 1. Please enter <OTP number>  that has been sent to your msisdn
            activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.InvalidOTP);

            ///   1.2 An error message is displayed[ error: “O1-2-8 – Passwords do not match. Please try again”
            browserInstance.Instance.Assert.True(() => otpErrorMessage.Element.Text == "O1-2-8 – Passwords do not match. Please try again");
            Classes.LogWriter.Instance.Log("TESTCASE: _08_IncorrectOneTimePin -> Require an Invalid OTP number for testing", Classes.LogWriter.eLogType.Error);
            Classes.LogWriter.Instance.Log("TESTCASE: _08_IncorrectOneTimePin -> Cannot test an invalid OTP number. Waiting for the new release of the browser app", Classes.LogWriter.eLogType.Error);

            //    1.2. Press the <next>  button
            activationAction.ClickNext(browserInstance, otpNextButton);

            /// 2. Expired OTP
            ///   2.1 Plese enter <Expired OTP>
            activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.ExpiredOPT);

            ///   2.2 An error message is displayed[ error: “O1-2-8 – Passwords do not match. Please try again”
            browserInstance.Instance.Assert.True(() => otpErrorMessage.Element.Text == "O1-2-8 – Passwords do not match. Please try again");
            Classes.LogWriter.Instance.Log("TESTCASE: _08_IncorrectOneTimePin -> Require an Expired OTP number for testing", Classes.LogWriter.eLogType.Error);
            Classes.LogWriter.Instance.Log("TESTCASE: _08_IncorrectOneTimePin -> Cannot test an Expired OTP number. Waiting for the new release of the browser app", Classes.LogWriter.eLogType.Error);

            ///   2.2 Press the <next> button
            activationAction.ClickNext(browserInstance, otpNextButton);
        }

        /// <summary>
        /// TEST: RESEND ONE TIME PIN
        /// Test Case ID: 2_FRS_Ref_5.1.1
        /// Category: Verify_user(OTP)
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activation Page 
        /// TEST STEPS:
        /// 1. OTP not received
        ///   1.1 Press the <Resent OTP> button
        /// 2. OTP deleted or lost
        ///   2.1 Press the <Resent OTP> button
        /// TEST OUTPUT:
        ///   1.1 The One time pin is sent on the msisdn and  a message is displayed as 
        ///   " A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue"
        /// 2.
        ///   2.1  The One time pin is sent on the msisdn and  a message is displayed as 
        ///   " A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue"                                                                                                                                                                                                         
        /// </summary>
        [Test, Description("_09_ResendOneTimePin"), Category("Verify_user(OTP)"), Repeat(1)]
        public void _09_ResendOneTimePin()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            /// 1. OTP not received
            ///   1.1 Press the <Resent OTP> button
            activationAction.ClickNext(browserInstance, optResendButton);

            ///   1.1 The One time pin is sent on the msisdn and  a message is displayed as 
            ///   " A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue"
            activationAction.VerifyOTPErrorMessage(browserInstance, Classes.TestData.Instance.DefaultData.ActivationData.MSISDN);
            Classes.LogWriter.Instance.Log("TESTCASE: _09_ResendOneTimePin -> OTP NOT RECEIVED -> One Time Pin message cannot be tested as it is not displayed after the Resend Button is clicked", Classes.LogWriter.eLogType.Error);
            

            /// 2. OTP deleted or lost
            ///   2.1 Press the <Resent OTP> button
            activationAction.ClickNext(browserInstance, optResendButton);

            ///   2.1  The One time pin is sent on the msisdn and  a message is displayed as 
            ///   " A One Time Pin has been sent to 0*****1234. Please enter the One time Pin here to continue" 
            activationAction.VerifyOTPErrorMessage(browserInstance, Classes.TestData.Instance.DefaultData.ActivationData.MSISDN);
            Classes.LogWriter.Instance.Log("TESTCASE: _09_ResendOneTimePin -> OTP NOT DELETE/LOST -> One Time Pin message cannot be tested as it is not displayed after the Resend Button is clicked", Classes.LogWriter.eLogType.Error);
        }

        /// <summary>
        /// TEST: CORRECT ONE TIME PIN
        /// Test Case ID: 2_FRS_Ref_5.1.1
        /// Category: Verify_user(OTP)
        /// Feature: Activation
        /// Pre-Condition: NONE
        /// Environment: Activtion Page
        /// TEST STEPS:
        /// 1. Please enter the  <OTP number>  that has been sent to your msisdn
        /// 2. Press the <next>  button
        /// TEST OUTPUT:
        /// 1. The one time pin entered is displayed  the one time pin filed
        /// 2. The application setup catalogue screen is displayed
        /// </summary>
        [Test, Description("_10_CorrectOneTimePin"), Category("Verify_user(OTP)"), Repeat(1)]
        public void _10_CorrectOneTimePin()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            /// 1. Please enter the  <OTP number>  that has been sent to your msisdn
            /// 1. The one time pin entered is displayed  the one time pin filed
            activationAction.EnterAndVerifyOTPValue(browserInstance, otp, Classes.TestData.Instance.DefaultData.ActivationData.OTP);

            /// 2. Press the <next>  button
            activationAction.ClickNext(browserInstance, otpNextButton);
            
            /// 2. The application setup catalogue screen is displayed
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue");

            Classes.LogWriter.Instance.Log("TESTCASE: _10_CorrectOneTimePin -> Test case passed", Classes.LogWriter.eLogType.Info);
        }

        /// <summary>
        /// TEST: SETUP CATALOGUE LANDING PAGE
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, Grammar and alignment 
        /// 5. Verify that the update button is displayed and enabled
        /// 6. Verify that the colour of the update button is purple
        /// 7. Verify that text label on the update  button is white
        /// 8. Verify that Manage Catalogue Label is displayed
        /// 9. Verify that the select wholesaler a label is is displayed
        /// 10. Search functionality field is displayed with a search icon/button
        /// TEST OUTPUT:
        /// 1. The vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyerlinks are displayed
        /// 4. spelling, grammar and alignment correct (screen should resize on all devices also able to rotate from Portrait to landscape)
        /// 5. The update button is displayed and enabled
        /// 6. The button colour is purple
        /// 7. The text label on the update button is white
        /// 8. The manage catalogue label is displayed
        /// 9. The please select a wholesaler from list below, or use the search functionality label is displayed
        /// 10. The search field is displayed with the icon
        /// </summary>
        [Test, Description("_11_SetupCatalogueLandingPage"), Category("Setup_Catalogue"), Repeat(1)]
        public void _11_SetupCatalogueLandingPage()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            activationAction.VerifyLogoAndBanner(browserInstance);
            /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            activationAction.VerifyOnlineIndicator(browserInstance);
            /// 3. Verify that contact us and help me hyperlinks are displayed
            activationAction.VerifyPageLinks(browserInstance);
            /// 4. See spelling, Grammar and alignment 
            //CONNOT DO THAT
            Classes.LogWriter.Instance.Log("TESTCASE: _11_SetupCatalogueLandingPage -> Cannot check spelling and grammer", Classes.LogWriter.eLogType.Info);

            /// 5. The update button is displayed and enabled
            /// 6. The button colour is purple
            /// 7. The text label on the update button is white
            /// 8. The manage catalogue label is displayed
            /// 9. The please select a wholesaler from list below, or use the search functionality label is displayed
            /// 10. The search field is displayed with the icon
            activationAction.VerifyCatalogueLandingPage(browserInstance);
        }

        /// <summary>
        /// TEST: SETUP CATALOGUE VALIDATIONS
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. Search Field Validation
        ///     1.1 Without entering anything click on search button.
        ///     1.2 Click in the search field and press Enter key.
        ///     1.3 Enter any  one character and click on search button/press Enter key .
        ///     1.4 Enter only special characters and click on Search button.
        ///     1.5 Enter  only numbers and click on search button
        ///     1.6 Enter alphanumeric characters and click on search button
        ///     1.7 Enter  alphanumeric characters and special characters and click on search button.
        ///     1.8 Enter string more than the max char limit of the field.
        ///     1.9 Enter string with spaces(before string , after string  and in between) and verify the results.                                                                                                                                                                                                                                                                                                                                         
        /// TEST OUTPUT:
        ///     1.1 An error message is displayed: “E1-1-7 – Please complete all fields”. all fields must either be highlited in red or displayed on the screen
        ///     1.2 An error message is displayed"
        ///     1.3 An error message is displayed"
        ///     1.4 An error message is displayed"
        ///     1.5 An error message is displayed"
        ///     1.6 An error message is displayed"
        ///     1.7 An error message is displayed"
        ///     1.8 An error message is displayed"
        ///     1.9 An error message is displayed"
        /// </summary>
        [Test, Description("_12_SetupCatalogueValidations"), Category("Setup_Catalogue"), Repeat(1)]
        public void _12_SetupCatalogueValidations()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            /// 1. Search Field Validation
            activationAction.TestCatalogueSeachValidation(browserInstance, mcatSearch, mcatSearchButton, mcatErrorMessage);


        }

        /// <summary>
        /// TEST: SETUP CATALOGUE ON DEVICE GEO LACATION SERVICE
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. On device geo loaction setup
        /// 1.1  Please make sure that the gps /device geo location service is on, before testing search 
        /// functionality because the application uses that to determine the location of the user
        /// TEST OUTPUT:
        /// 1.
        /// 1.1 The geo location service is on and the user device can be located.This location is sent to 
        /// MAS to determine the list of wholesalers that the user has close proximity to.
        /// </summary>
        [Test, Description("_13_SetupCatalogueOnDeviceGEOLocationService"), Category("Setup_Catalogue"), Repeat(1)]
        public void _13_SetupCatalogueOnDeviceGEOLocationService()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
        }

        /// <summary>
        /// TEST: SETUP CATALOGUE SEARCH FIELD  RETURNING NO RESULTS
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. Search returning no result
        /// 1.1 Enter the wholesaler value in search field which is not an allowable wolesaler  and verify the user interface.
        /// TEST OUTPUT:
        /// 1.1 An Error message should be displayed in the search field " results not found"
        /// </summary>
        [Test, Description("_14_SetupCatalogueSearchFieldReturningNoResults"), Category("Setup_Catalogue"), Repeat(1)]
        public void _14_SetupCatalogueSearchFieldReturningNoResults()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
        }

        /// <summary>
        /// TEST: SETUP CATALOGUE SEARCH FIELD  AUTO COMPLETE 
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. Search AutoComplete
        /// 1.1 Enter first / middle/ last  word of any wholesaler e.g Makro  and verify the search results
        /// 1.2 Select the one from the pre-populated list
        /// TEST OUTPUT:
        /// 1.
        /// 1.1 All the names starting with the first name will be pre-populated with a list
        /// 1.2 The record you have selected is displayed with records that are related to that search, with 
        /// their respective distance ranges
        /// </summary>
        [Test, Description("_15_SetupCatalogueSearchFieldAutoComplete"), Category("Setup_Catalogue"), Repeat(1)]
        public void _15_SetupCatalogueSearchFieldAutoComplete()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
        }

        /// <summary>
        /// TEST: SETUP CATALOGUE LANDING PAGE INTERRUPTIONS
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. Activation process interruption
        ///   1.1 Please enter the wholesaler value e.g <makro> and set it offline
        ///   1.2 Verify that the when the application is online again it returns to the Verify User(OTP) Page
        /// 2. No Server response
        ///   1.1 If the appliaction is online and the allowable is selected and no action or record is returned
        ///   1.2 Verify that the when the server is back-up
        ///   1.2 Verify that the when the server is back-up
        /// TEST OUTPUT:
        /// 1.
        ///   1.1 An error message is displayed[ error: “O1-2-6 – You are not online. Please check your connectivity and try again”
        ///   1.2  When the application is online again, it must return to the activation page
        /// 2.
        ///   1.1 An error message is displayed E1-3-1 – No response from server, please try again”.
        ///   1.2 When the server is back up again, it must return to the activation pag 
        /// </summary>
        [Test, Description("_16_SetupCatalogueLandingPageInterruptions"), Category("Setup_Catalogue"), Repeat(1)]
        public void _16_SetupCatalogueLandingPageInterruptions()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
        }

        /// <summary>
        /// TEST: SETUP CATALOGUE SEARCH FIELD  RETURNING ONE OR MULTIPLE RESULTS
        /// Test Case ID: 3_FRS_Ref_5.1.1
        /// Category: Setup_Catalogue
        /// Feature: Activation
        /// Pre-Condition: Verify_user(OTP)
        /// Environment: 
        /// TEST STEPS:
        /// 1. Search returning one or multiple results
        ///   1.1 Enter the allowable wholesaler <Makro>  in search field which  give any results and verify the user interface
        /// 2. Verify groupings arrows are expandable
        ///   2.1 Select 0 - 25km arrow  and expand it
        ///   2.2 Select 25 - 50km arrow and expand it
        ///   2.3 Select  50 - 75km arrow and expand it
        ///   2.4 Select  75 - 100km arrow and expand it
        /// 3.  Select one store from each range
        ///   3.1 Select 0 - 25km  and select one wholesaler under that range by checbox
        ///   3.2 Select 25 - 50km  and select one wholesaler under that range by checbox
        ///   3.3 Select 50 - 75km  and select one wholesaler under that range by checbox
        ///   3.4  Select 75 - 100km  and select one wholesaler under that range by checbox
        ///   3.5 Press the <update> button
        /// TEST OUTPUT:
        /// 1.
        ///   1.1 The wholesaler records which are found are displayed as catalogue – outlet name  on the screen, 
        ///   with a location list sorted by group and grouped into groups on increments 25km
        /// 2.
        ///   2.1 The 0 - 25km is expanded and also displaying stores within that distance proximity
        ///   2.2 The 25 - 50km is expanded and also displaying stores within that distance proximity
        ///   2.3 The 50 - 75km is expanded and also displaying stores within that distance proximity
        ///   2.4 The 75 - 100km is expanded and also displaying stores within that distance proximity
        /// 3.
        ///   3.1 The selected wholesaler is displayed with a checkbox next to it
        ///   3.2  Select 25 - 50km  and select one wholesaler under that range by checbox
        ///   3.3  Select 50 - 75km  and select one wholesaler under that range by checbox
        ///   3.4  Select 50 - 75km  and select one wholesaler under that range by checbox
        ///   3.5   The Application Landing Page is Displayed
        /// </summary>
        [Test, Description("_17_SetupCatalogueSearchFieldReturningOneOrMultipleResults"), Category("Setup_Catalogue"), Repeat(1)]
        public void _17_SetupCatalogueSearchFieldReturningOneOrMultipleResults()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-verifyuser"));
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();
        }
    }
}
