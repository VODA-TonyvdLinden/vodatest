using System;
namespace TestProj.Interfaces
{
    public interface IContactUs
    {
        void VerifyContactUsLablelAndIcon(Classes.Browser browserInstance, string icon, string label);
        void VerifyContactUsSubMenus(Classes.Browser browserInstance, string menu1, string menu2, string menu3, string menu4);
        void GoToContactUs(Classes.Browser browserInstance);
        void VerifyCustomerServiceContactNo(Classes.Browser browserInstance);
        void VerifyPerfectStartupSection(Classes.Browser browserInstance);
        void VerifySelfServiceSection(Classes.Browser browserInstance);
        void VerifyFAQSection(Classes.Browser browserInstance);
    }
}
