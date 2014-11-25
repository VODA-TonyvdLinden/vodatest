using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    public class Activation : Interfaces.IActivation
    {
        public void ActivationOneTimePinFieldValidation(Classes.Browser browserInstance)
        {
        }

        public void ActivationFormCorrectUserDetails(Classes.Browser browserInstance)
        {
        }

        /// <summary>
        /// ActivationFormFieldValidation
        /// Test steps:
        /// 1. Verify that all text fields are mandatory on the form
        /// 1.1.1 Please don’t enter anything on all the fields and click next
        /// 2. Select msisdn field                                                                                                                                     
        /// 3. Verify that the msisdn field validation will be limited to Numeric format                                        
        ///     3.1.1 Please enter alphanumeric  < 07@ >
        ///     3.1.2 Please enter space before entering input on the field
        ///     3.1.3 Please enter special characters  <@@, &&> 
        ///     3.1.4 Please enter decimal numbers <0.00444> 
        ///     3.1.5 Please enter negative value <-1>                                                                                                  
        /// 4. Select username field                                                   
        /// 5. Validate the username field
        /// 5.1.1 Please enter space before entering input on username field                                                                                              
        /// 6. select activation key field
        /// 7. Validate the activation key field
        /// 7.1.1 Please enter space before entering input on the field                                                                    
        /// 8. Validate Preferred Alias field
        /// 8.1.1 Please enter space before entering input on the field                                                                                                                       
        /// </summary>
        /// <param name="browserInstance"></param>
        /// <param name="screenshotRequirement"></param>
        public void ActivationFormFieldValidation(Classes.Browser browserInstance)
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/activation"));

            // 1. Verify that all text fields are mandatory on the form
            var msisdn = browserInstance.Instance.Find("#msisdn");
            browserInstance.Instance.Assert.Attribute("required").On(msisdn);
            var username = browserInstance.Instance.Find("#username");
            browserInstance.Instance.Assert.Attribute("required").On(username);
            var activationNumber = browserInstance.Instance.Find("#activationNumber");
            browserInstance.Instance.Assert.Attribute("required").On(activationNumber);
            var userAlias = browserInstance.Instance.Find("#userAlias");
            browserInstance.Instance.Assert.Attribute("required").On(userAlias);
            //FIELD CANNOT BE REQUIRED -> IT IS A DROP DOWN
            //var challengeQuestion = browserInstance.Instance.Find("challengeQuestion");
            var challengeAnswer = browserInstance.Instance.Find("#challengeAnswer");
            browserInstance.Instance.Assert.Attribute("required").On(challengeAnswer);

            // 1.1.1 Please don’t enter anything on all the fields and click next
            //Test must be Error Message is displayed: “E1-1-7 – Please complete all fields”. 
            //  all fields must either be highlited in red or displayed on the screen            
            browserInstance.Instance.Click("body > div:nth-child(2) > div > div.activationContentMiddle > form > div:nth-child(8) > div > input");

            browserInstance.Instance.Assert.Exists("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");
            var errorMessage = browserInstance.Instance.Find("body > div:nth-child(2) > div > div.activationContentMiddle > form > div.ng-binding");
            //browserInstance.Instance.Assert.True(() => "E1-1-7 – Please complete all fields" == errorMessage.Element.Text);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormFieldValidation -> all fields must either be highlited in red or displayed on the screen. Which one?", Classes.LogWriter.eLogType.Error);


            // 2. Select msisdn field     
            browserInstance.Instance.Focus(msisdn);                                                                                                                    
            // 3. Verify that the msisdn field validation will be limited to Numeric format                                        
            //     3.1.1 Please enter alphanumeric  < 07@ >
            browserInstance.Instance.Enter("07@").In(msisdn);
//            browserInstance.Instance.Assert.False(() => "07@" == msisdn.Element.Text);
            //     3.1.2 Please enter space before entering input on the field
            browserInstance.Instance.Enter(" 082").In(msisdn);
//            browserInstance.Instance.Assert.False(() => " 082" == msisdn.Element.Text);
            //     3.1.3 Please enter special characters  <@@, &&> 
            browserInstance.Instance.Enter("@@, &&").In(msisdn);
//            browserInstance.Instance.Assert.False(() => "@@, &&" == msisdn.Element.Text);
            //     3.1.4 Please enter decimal numbers <0.00444> 
            browserInstance.Instance.Enter("0.00444").In(msisdn);
//            browserInstance.Instance.Assert.False(() => "0.00444" == msisdn.Element.Text);
            //     3.1.5 Please enter negative value <-1>                                                                                                  
            browserInstance.Instance.Enter("-1").In(msisdn);
//            browserInstance.Instance.Assert.False(() => "-1" == msisdn.Element.Text);
            // 4. Select username field                                                   
            browserInstance.Instance.Focus(username);
            // 5. Validate the username field
            // 5.1.1 Please enter space before entering input on username field                                                                                              
            browserInstance.Instance.Enter(" TEST").In(username);
//            browserInstance.Instance.Assert.False(() => " TEST" == username.Element.Text);            
            // 6. select activation key field
            browserInstance.Instance.Focus(activationNumber);
            // 7. Validate the activation key field
            // 7.1.1 Please enter space before entering input on the field 
            browserInstance.Instance.Enter(" TEST").In(activationNumber);
//            browserInstance.Instance.Assert.False(() => " TEST" == activationNumber.Element.Text);                                                        
            // 8. Validate Preferred Alias field
            browserInstance.Instance.Focus(userAlias);
            // 8.1.1 Please enter space before entering input on the field   
            browserInstance.Instance.Enter(" TEST").In(userAlias);
//            browserInstance.Instance.Assert.False(() => " TEST" == userAlias.Element.Text);                                                        
        }

        /// <summary>
        /// ActivationFormIncorrectUserDetails
        /// Test steps:
        /// 1. Invalid Activation Key – does not match one saved in BOP Manager                                             
        /// 1.1.1 Enter valid msisdn                                                                                                                              
        /// 1.1.2 Enter valid  username                                                                                                                        
        /// 1.1.3  Enter Invalid activation key, any number/string that is accepted by the field                                   
        /// 1.1.4  Enter any user defined preferred alias                                                                                             
        /// 1.1.5 click on the next button                                                                                                                   
        /// 2. Invalid username  – username does not match one saved in BOP Manager                                             
        /// 2.1.1 Enter valid msisdn                                                                                                                           
        /// 2.1.2 Enter invalid  username                                                                                                                        
        /// 2.1.3  Enter valid activation key, any number/string that is accepted by the field                                   
        /// 2.1.4  Enter any user defined preferred alias                                                                                             
        /// 2.1.5 click on the next button                               
        /// 3. Verify that is limited to 10 digit numbers
        /// 3.1.1 Enter msisdn number > 10 , meaning 9 digit on msisdn field                                                                                                                          
        /// 3.1.2 Enter valid  username                                                                                                                       
        /// 3.1.3  Enter Invalid activation key, any number/string that is accepted by the field                                   
        /// 3.1.4  Enter any user defined preferred alias                                                                                             
        /// 3.1.5 click on the next button                                                                                                                   
        /// 3.2.1 Enter msisdn number less than 10 , meaning 11 digit on msisdn field                                                          
        /// 3.2.1.1 Enter valid  username                                                                                                                        
        /// 3.2.1.2  Enter valid activation key, any number/string that is accepted by the field                                   
        /// 3.2.1.3 Enter any user defined preferred alias                                                                                             
        /// 3.2.1.4 click on the next button                                                                                                                                                                                                                                                                                                                   
        /// </summary>
        /// <param name="browserInstance"></param>
        /// <param name="screenshotRequirement"></param>
        public void ActivationFormIncorrectUserDetails(Classes.Browser browserInstance)
        {
        }

        /// <summary>
        /// VerifyActivationLandingPage
        /// Test steps:
        /// 1. Verify that the vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, Grammar and alignment 
        /// 5. Verify that the next button is displayed and enabled
        /// 6. Verify that the colour of the next button is purple
        /// 7. Verify that text label on the next button is white
        /// 8. Verfy that activation form contains msisdn,username,activation code and preferred alias fields      
        /// 9. Verify that the Application buttons are displayed at the bottom of the screen
        /// </summary>
        /// <param name="browserInstance"></param>
        /// <param name="screenshotRequirement"></param>
        public void VerifyActivationLandingPage(Classes.Browser browserInstance)
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

        public void VerifyActivationOneTimePinLandingPage(Classes.Browser browserInstance)
        {
        }

        public void CorrectOneTimePinAndApplicationOffline(Classes.Browser browserInstance)
        {
        }

        public void IncorrectOneTimePin(Classes.Browser browserInstance)
        {
        }

        public void ResendOneTimePin(Classes.Browser browserInstance)
        {
        }

        public void CorrectOneTimePin(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueLandingPage(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueValidations(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueOnDeviceGEOLocationService(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueSearchFieldReturningNoResults(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueSearchFieldAutoComplete(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueLandingPageInterruptions(Classes.Browser browserInstance)
        {
        }

        public void SetupCatalogueSearchFieldReturningOneOrMultipleResults(Classes.Browser browserInstance)
        {
        }
    }
}
