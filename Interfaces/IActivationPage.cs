using System;
namespace TestProj.Interfaces
{
    public interface IActivationPage
    {
        void ActivationOneTimePinFieldValidation(Classes.Browser browserInstance);
        void CorrectUserDetails(Classes.Browser browserInstance);
        void FieldValidation(Classes.Browser browserInstance);
        void IncorrectUserDetails(Classes.Browser browserInstance);
        void VerifyActivationLandingPage(Classes.Browser browserInstance);
        void VerifyActivationOneTimePinLandingPage(Classes.Browser browserInstance);
    }
}
