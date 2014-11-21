using System;
namespace TestProj.Interfaces
{
    //Interface of Tab Activation
    //Each method corresponds to a heading
    public interface IActivation
    {
        void ActivationOneTimePinFieldValidation(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void ActivationFormCorrectUserDetails(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void ActivationFormFieldValidation(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void ActivationFormIncorrectUserDetails(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void VerifyActivationLandingPage(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void VerifyActivationOneTimePinLandingPage(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void CorrectOneTimePinAndApplicationOffline(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void IncorrectOneTimePin(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void ResendOneTimePin(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void CorrectOneTimePin(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueLandingPage(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueValidations(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueOnDeviceGEOLocationService(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueSearchFieldReturningNoResults(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueSearchFieldAutoComplete(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueLandingPageInterruptions(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
        void SetupCatalogueSearchFieldReturningOneOrMultipleResults(Classes.Browser browserInstance, Classes.ScreenshotRequirement screenshotRequirement);
    }
}
