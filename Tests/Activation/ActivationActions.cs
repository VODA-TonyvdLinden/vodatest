using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Activation
{
    public class ActivationActions : TestProj.Interfaces.IActivationActions
    {
        public void TestAliasInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy userAlias)
        {
            // 8. Validate Preferred Alias field
            browserInstance.Instance.Focus(userAlias);
            // 8.1.1 Please enter space before entering input on the field   
            browserInstance.Instance.Enter(" TEST").In(userAlias);
            browserInstance.Instance.Assert.False(() => " TEST" == userAlias.Element.Text);
        }

        public void TestActivationKeyInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber)
        {
            // 6. select activation key field
            browserInstance.Instance.Focus(activationNumber);
            // 7. Validate the activation key field
            // 7.1.1 Please enter space before entering input on the field 
            browserInstance.Instance.Enter(" TEST").In(activationNumber);
            browserInstance.Instance.Assert.False(() => " TEST" == activationNumber.Element.Text);                                                        
        }

        public void TestUsernameInputValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy username)
        {
            // 4. Select username field                                                   
            browserInstance.Instance.Focus(username);
            // 5. Validate the username field
            // 5.1.1 Please enter space before entering input on username field                                                                                              
            browserInstance.Instance.Enter(" TEST").In(username);
            browserInstance.Instance.Assert.False(() => " TEST" == username.Element.Text);            

        }

        public void TestMSISDNInoutValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn)
        {
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
            browserInstance.Instance.Assert.False(() => "-1" == msisdn.Element.Text);
        }

        public void TestMandatoryFields(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy errorMessage)
        {
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
            browserInstance.Instance.Enter("08212345678").In(msisdn);
            // 3.2.1.1 Enter valid  username    
            browserInstance.Instance.Enter("Test User").In(username);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", Classes.LogWriter.eLogType.Error);
            // 3.2.1.2  Enter valid activation key, any number/string that is accepted by the field 
            browserInstance.Instance.Enter("0000000000").In(activationNumber);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid ACTIVATION No required", Classes.LogWriter.eLogType.Error);
            // 3.2.1.3 Enter any user defined preferred alias     
            browserInstance.Instance.Enter("Test User").In(userAlias);
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
            browserInstance.Instance.Enter("082123456").In(msisdn);
            // 3.1.2 Enter valid  username     
            browserInstance.Instance.Enter("Test User").In(username);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", Classes.LogWriter.eLogType.Error);
            // 3.1.3  Enter Invalid activation key, any number/string that is accepted by the field 
            browserInstance.Instance.Enter("0123456789").In(activationNumber);
            // 3.1.4  Enter any user defined preferred alias                                        
            browserInstance.Instance.Enter("Test User").In(userAlias);
            // 3.1.5 click on the next button                                                       
            browserInstance.Instance.Click(nextButton);
            // TEST: 3.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(msisdn);
            browserInstance.Instance.Assert.Class("ng-invalid").On(msisdn);
        }

        public void TestInvalidUsername(Classes.Browser browserInstance, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage)
        {
            // 2. Invalid username  – username does not match one saved in BOP Manager              
            // 2.1.1 Enter valid msisdn                                                             
            // 2.1.2 Enter invalid  username                                                        
            browserInstance.Instance.Enter("InvalidTest User").In(username);
            // 2.1.3  Enter valid activation key, any number/string that is accepted by the field   
            browserInstance.Instance.Enter("0000000000").In(activationNumber);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid ACTIVATION No required", Classes.LogWriter.eLogType.Error);
            // 2.1.4  Enter any user defined preferred alias                                        
            // 2.1.5 click on the next button                               
            browserInstance.Instance.Click(nextButton);
            // TEST: 2.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(username);
            browserInstance.Instance.Assert.Class("ng-invalid").On(username);
        }

        public void TestInvalidActivationKey(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage)
        {
            // 1. Invalid Activation Key – does not match one saved in BOP Manager                  
            // 1.1.1 Enter valid msisdn                                                             
            browserInstance.Instance.Enter("0821234567").In(msisdn);
            Classes.LogWriter.Instance.Log("TESTCASEActivationFormIncorrectUserDetails -> Valid MSISDN no required", Classes.LogWriter.eLogType.Error);
            // 1.1.2 Enter valid  username                                                          
            browserInstance.Instance.Enter("Test User").In(username);
            Classes.LogWriter.Instance.Log("TESTCASE:ActivationFormIncorrectUserDetails -> Valid UNSERNAME required", Classes.LogWriter.eLogType.Error);
            // 1.1.3  Enter Invalid activation key, any number/string that is accepted by the field 
            browserInstance.Instance.Enter("0123456789").In(activationNumber);
            // 1.1.4  Enter any user defined preferred alias                                        
            browserInstance.Instance.Enter("Test User").In(userAlias);
            // 1.1.5 click on the next button   
            browserInstance.Instance.Click(nextButton);
            // TEST: 1.1.5 An Error Message is displayed  "E1-1 -3 invalid input". Field must be highlited in red
            browserInstance.Instance.Assert.True(() => "E1-1 -3 invalid input" == errorMessage.Element.Text);
            browserInstance.Instance.Assert.Class("has-error").On(activationNumber);
            browserInstance.Instance.Assert.Class("ng-invalid").On(activationNumber);
        }
    }
}
