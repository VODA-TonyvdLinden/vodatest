using System;
namespace TestProj.Interfaces
{
    public interface IActivationActions
    {
        void TestActivationKeyInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber);
        void TestAliasInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy userAlias);
        void TestInvalidActivationKey(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage);
        void TestInvalidUsername(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage);
        void TestMandatoryFields(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy errorMessage);
        void TestMSISDNInoutValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn);
        void TestMSISDNLengthLimitGreater(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage);
        void TestMSISDNLengthLimitSmaller(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy errorMessage);
        void TestUsernameInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy username);
    }
}
