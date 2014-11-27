using System;
namespace TestProj.Interfaces
{
    public interface IAccessingApplicationActions
    {
        void VerifyLogoAndBanner(Classes.Browser browserInstance);
        void VerifyOnlineIndicator(Classes.Browser browserInstance);
        void VerifyPageLinks(Classes.Browser browserInstance);
        void VerifyPreferedAlias(Classes.Browser browserInstance);
        void VerifySpazaName(Classes.Browser browserInstance);
        void VerifySpecialsExists(Classes.Browser browserInstance);
        void VerifyMarbilExists(Classes.Browser browserInstance);
    }
}
