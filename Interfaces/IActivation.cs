using System;
namespace TestProj.Interfaces
{
    //Interface of Tab Activation
    //Each method corresponds to a heading
    public interface IActivation
    {
        void ActivationOneTimePinFieldValidation(Classes.Browser browserInstance);
        void ActivationFormCorrectUserDetails(Classes.Browser browserInstance);
        void ActivationFormFieldValidation(Classes.Browser browserInstance);
        void ActivationFormIncorrectUserDetails(Classes.Browser browserInstance);
        void VerifyActivationLandingPage(Classes.Browser browserInstance);
        void VerifyActivationOneTimePinLandingPage(Classes.Browser browserInstance);
        void CorrectOneTimePinAndApplicationOffline(Classes.Browser browserInstance);
        void IncorrectOneTimePin(Classes.Browser browserInstance);
        void ResendOneTimePin(Classes.Browser browserInstance);
        void CorrectOneTimePin(Classes.Browser browserInstance);
        void SetupCatalogueLandingPage(Classes.Browser browserInstance);
        void SetupCatalogueValidations(Classes.Browser browserInstance);
        void SetupCatalogueOnDeviceGEOLocationService(Classes.Browser browserInstance);
        void SetupCatalogueSearchFieldReturningNoResults(Classes.Browser browserInstance);
        void SetupCatalogueSearchFieldAutoComplete(Classes.Browser browserInstance);
        void SetupCatalogueLandingPageInterruptions(Classes.Browser browserInstance);
        void SetupCatalogueSearchFieldReturningOneOrMultipleResults(Classes.Browser browserInstance);
    }
}
