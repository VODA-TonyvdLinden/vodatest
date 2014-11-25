using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    [TestFixture, Description("Activation"), Category("Contains all Activation test classes")]
    public class Activation : Interfaces.IActivation
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        [TestFixtureSetUp]
        public void Initialise()
        {
            //Classes.ScreenshotRequirements req = new Classes.ScreenshotRequirements();
            //req.RequirementList.Add(new Classes.ScreenshotRequirement() { EventName = "Event 1", EntryRequired = true, ExitRequired = true });
            //req.RequirementList.Add(new Classes.ScreenshotRequirement() { EventName = "Event 2", EntryRequired = true, ExitRequired = true });
            //req.RequirementList.Add(new Classes.ScreenshotRequirement() { EventName = "Event 3", EntryRequired = true, ExitRequired = true });

            //Classes.XMLSeriallizer.Serialize(req, Properties.Settings.Default.ScreenshotRequirementsPath);
            //this = container.Resolve<Interfaces.IActivation>();

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }
        private static void getActivationControls(Classes.Browser browserInstance, out FluentAutomation.ElementProxy msisdn, out FluentAutomation.ElementProxy username, out FluentAutomation.ElementProxy activationNumber, out FluentAutomation.ElementProxy userAlias, out FluentAutomation.ElementProxy challengeAnswer, out FluentAutomation.ElementProxy nextButton, out FluentAutomation.ElementProxy errorMessage)
        {
            msisdn = browserInstance.Instance.Find("#msisdn");
            username = browserInstance.Instance.Find("#username");
            activationNumber = browserInstance.Instance.Find("#activationNumber");
            userAlias = browserInstance.Instance.Find("#userAlias");
            //FIELD CANNOT BE REQUIRED -> IT IS A DROP DOWN
            //var challengeQuestion = browserInstance.Instance.Find("challengeQuestion");
            challengeAnswer = browserInstance.Instance.Find("#challengeAnswer");
            nextButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");
            errorMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");

        }
        [Test, Description("ActivationOneTimePinFieldValidation"), Repeat(1)]
        public void ActivationOneTimePinFieldValidation()
        {
        }

        [Test, Description("ActivationFormCorrectUserDetails"), Repeat(1)]
        public void ActivationFormCorrectUserDetails()
        {
        }

        #region ActivationFormFieldValidation
        /// <summary>
        /// ActivationFormFieldValidation
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
        [Test, Description("ActivationFormFieldValidation"), Repeat(1)]
        public void ActivationFormFieldValidation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));
            FluentAutomation.ElementProxy msisdn;
            FluentAutomation.ElementProxy username;
            FluentAutomation.ElementProxy activationNumber;
            FluentAutomation.ElementProxy userAlias;
            FluentAutomation.ElementProxy challengeAnswer;
            FluentAutomation.ElementProxy nextButton;
            FluentAutomation.ElementProxy errorMessage;
            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeAnswer, out nextButton, out errorMessage);

            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            activationAction.TestMandatoryFields(browserInstance, msisdn, username, activationNumber, userAlias, challengeAnswer, errorMessage);
            activationAction.TestMSISDNInoutValidation(browserInstance, msisdn);
            activationAction.TestUsernameInputValidation(browserInstance, username);
            activationAction.TestActivationKeyInputValidation(browserInstance, activationNumber);
            activationAction.TestAliasInputValidation(browserInstance, userAlias);
        }


        #endregion

        #region ActivationFormIncorrectUserDetails
        /// <summary>
        /// ActivationFormIncorrectUserDetails
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
        [Test, Description("ActivationFormIncorrectUserDetails"), Repeat(1)]
        public void ActivationFormIncorrectUserDetails()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));
            FluentAutomation.ElementProxy msisdn;
            FluentAutomation.ElementProxy username;
            FluentAutomation.ElementProxy activationNumber;
            FluentAutomation.ElementProxy userAlias;
            FluentAutomation.ElementProxy challengeAnswer;
            FluentAutomation.ElementProxy nextButton;
            FluentAutomation.ElementProxy errorMessage;
            getActivationControls(browserInstance, out msisdn, out username, out activationNumber, out userAlias, out challengeAnswer, out nextButton, out errorMessage);

            //container.AddNewExtension<Interception>();

            //container.RegisterType<Interfaces.IActivationActions, Tests.Activation.ActivationActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            Interfaces.IActivationActions activationAction = container.Resolve<Interfaces.IActivationActions>();

            activationAction.TestInvalidActivationKey(browserInstance, msisdn, username, activationNumber, userAlias, nextButton, errorMessage);
            activationAction.TestInvalidUsername(browserInstance, username, activationNumber, nextButton, errorMessage);
            activationAction.TestMSISDNLengthLimitGreater(browserInstance, msisdn, username, activationNumber, userAlias, nextButton, errorMessage);
            activationAction.TestMSISDNLengthLimitSmaller(browserInstance, msisdn, username, activationNumber, userAlias, errorMessage);                                 
        }


        #endregion

        #region VerifyActivationLandingPage
        /// <summary>
        /// VerifyActivationLandingPage
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
        [Test, Description("VerifyActivationLandingPage"), Repeat(1)]
        public void VerifyActivationLandingPage()
        {
            //http://aspnet.dev.afrigis.co.za/bopapp/#/activation
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));

            //1. Verify that the vodacom logo and the red banner are displayed on the activation screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > img");
            var logo = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > img");
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/logo-rotated.e90367bc.png").On(logo);
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
            var redBanner = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
            browserInstance.Instance.Assert.Class("vodaBackgroundRed").On(redBanner);

            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div > div");
            var onlineOfflineIndicator = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div > div");
            browserInstance.Instance.Assert.Class("statusDisplay").On(onlineOfflineIndicator);
            browserInstance.Instance.Assert.Class("online").On(onlineOfflineIndicator);

            // 3. Verify that contact us and help me hyperlinks are displayed
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUs");
            var contactUs = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUs");
            //browserInstance.Instance.Assert.Attribute("text","Contact us").On(contactUs);
            browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMe");
            var helpMe = browserInstance.Instance.Find("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMe");
            //browserInstance.Instance.Assert.Attribute("text", "Help me").On(helpMe);

            // 4. See spelling, Grammar and alignment 
            //CONNOT DO THAT
            // 5. Verify that the next button is displayed and enabled
            browserInstance.Instance.Assert.Exists("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");
            var nextButton = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> What css class is used to disable the next button?", Classes.LogWriter.eLogType.Error);
            // 6. Verify that the colour of the next button is purple
            browserInstance.Instance.Assert.Class("purpleButton").On(nextButton);
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the next button, just the css class", Classes.LogWriter.eLogType.Error);
            // 7. Verify that text label on the next button is white
            Classes.LogWriter.Instance.Log("TESTCASE:VerifyActivationLandingPage -> Cannot evaluate the color of the text on the next button, just the css class", Classes.LogWriter.eLogType.Error);
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
        #endregion

        [Test, Description("VerifyActivationOneTimePinLandingPage"), Repeat(1)]
        public void VerifyActivationOneTimePinLandingPage()
        {
        }

        [Test, Description("CorrectOneTimePinAndApplicationOffline"), Repeat(1)]
        public void CorrectOneTimePinAndApplicationOffline()
        {
        }

        [Test, Description("IncorrectOneTimePin"), Repeat(1)]
        public void IncorrectOneTimePin()
        {
        }

        [Test, Description("ResendOneTimePin"), Repeat(1)]
        public void ResendOneTimePin()
        {
        }

        [Test, Description("CorrectOneTimePin"), Repeat(1)]
        public void CorrectOneTimePin()
        {
        }

        [Test, Description("SetupCatalogueLandingPage"), Repeat(1)]
        public void SetupCatalogueLandingPage()
        {
        }

        [Test, Description("SetupCatalogueValidations"), Repeat(1)]
        public void SetupCatalogueValidations()
        {
        }

        [Test, Description("SetupCatalogueOnDeviceGEOLocationService"), Repeat(1)]
        public void SetupCatalogueOnDeviceGEOLocationService()
        {
        }

        [Test, Description("SetupCatalogueSearchFieldReturningNoResults"), Repeat(1)]
        public void SetupCatalogueSearchFieldReturningNoResults()
        {
        }

        [Test, Description("SetupCatalogueSearchFieldAutoComplete"), Repeat(1)]
        public void SetupCatalogueSearchFieldAutoComplete()
        {
        }

        [Test, Description("SetupCatalogueLandingPageInterruptions"), Repeat(1)]
        public void SetupCatalogueLandingPageInterruptions()
        {
        }

        [Test, Description("SetupCatalogueSearchFieldReturningOneOrMultipleResults"), Repeat(1)]
        public void SetupCatalogueSearchFieldReturningOneOrMultipleResults()
        {
        }
    }
}
