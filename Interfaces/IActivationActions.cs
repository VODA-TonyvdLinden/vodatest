using System;
namespace TestProj.Interfaces
{
    public interface IActivationActions
    {
        //void MSISDNInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, string input);
        //void UsernameInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy username, string input);
        //void AliasInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy alias, string input);
        //void ActivationKeyInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber, string input);
        void ClickNext(Classes.Browser browserInstance, FluentAutomation.ElementProxy nextButton);
        void TestActivationKeyInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy activationNumber);
        void TestAliasInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy userAlias);
        void TestInvalidActivationKey(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage);
        void TestInvalidUsername(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage);
        void TestValidUserDetails(Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy nextButton);
        void TestMandatoryFields(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy challengeQuestion, FluentAutomation.ElementProxy challengeAnswer, FluentAutomation.ElementProxy errorMessage);
        void TestMSISDNInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn);
        void TestMSISDNLengthLimitGreater(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy nextButton, FluentAutomation.ElementProxy errorMessage);
        void TestMSISDNLengthLimitSmaller(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy msisdn, FluentAutomation.ElementProxy username, FluentAutomation.ElementProxy activationNumber, FluentAutomation.ElementProxy userAlias, FluentAutomation.ElementProxy errorMessage);
        void TestUsernameInputValidation(TestProj.Classes.Browser browserInstance, FluentAutomation.ElementProxy username);
        void VerifyLogoAndBanner(Classes.Browser browserInstance);
        void VerifyOnlineIndicator(Classes.Browser browserInstance);
        void VerifyPageLinks(Classes.Browser browserInstance);
        void VerifyOTPNextButton(Classes.Browser browserInstance);
        void VerifyActivationPageNextButton(Classes.Browser browserInstance);
        //void VerifyNextButton(Classes.Browser browserInstance, string cssPath);
        void VerifyResendButton(Classes.Browser browserInstance);
        void VerifyFieldExist(Classes.Browser browserInstance);
        void ValidateOTPStart(Classes.Browser browserInstance, string msisdnNo);
        void VerifyOTPErrorMessage(Classes.Browser browserInstance, string msisdnNo);
        void VerifyOntTimeLable(Classes.Browser browserInstance);
        void EnterAndVerifyOTPValue(Classes.Browser browserInstance, FluentAutomation.ElementProxy otp, string otpVal);
        void VerifyCatalogueLandingPage(Classes.Browser browserInstance);
        //void SearchInput(Classes.Browser browserInstance, FluentAutomation.ElementProxy search, string input);
        void ClickSearchButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy mcatSearchButton);
        void TestCatalogueSeachValidation(Classes.Browser browserInstance, FluentAutomation.ElementProxy searchValue, FluentAutomation.ElementProxy mcatSearchButton, FluentAutomation.ElementProxy mcatErrorMessage);
    }

}
