using System;
namespace TestProj.Interfaces
{
    //Interface of Tab Activation
    //Each method corresponds to a heading
    public interface IActivation
    {
        void ActivationOneTimePinFieldValidation();
        void ActivationFormCorrectUserDetails();
        void ActivationFormFieldValidation();
        void ActivationFormIncorrectUserDetails();
        void VerifyActivationLandingPage();
        void VerifyActivationOneTimePinLandingPage();
        void CorrectOneTimePinAndApplicationOffline();
        void IncorrectOneTimePin();
        void ResendOneTimePin();
        void CorrectOneTimePin();
        void SetupCatalogueLandingPage();
        void SetupCatalogueValidations();
        void SetupCatalogueOnDeviceGEOLocationService();
        void SetupCatalogueSearchFieldReturningNoResults();
        void SetupCatalogueSearchFieldAutoComplete();
        void SetupCatalogueLandingPageInterruptions();
        void SetupCatalogueSearchFieldReturningOneOrMultipleResults();
    }
}
