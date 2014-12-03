using System;
namespace TestProj.Interfaces
{
    public interface IAlertsActions
    {
        void VerifyVodacomLogoAndBanner(Classes.Browser browserInstance);
        void VerifyOnlineOffilineIndicator(Classes.Browser browserInstance);
        void VerifyContactUsHelpMeLinks(Classes.Browser browserInstance);
        void VerifySpazaAliasAndName(Classes.Browser browserInstance);
        void VerifyMarbilAd(Classes.Browser browserInstance);
        void VerifyBottomNavigationBlocks(Classes.Browser browserInstance);
        void VerifyNotificationAndLabel(Classes.Browser browserInstance);
        void VerifyBasketTotal(Classes.Browser browserInstance);
        void VerifySearchField(Classes.Browser browserInstance);
        void VerifyNotificationSection(Classes.Browser browserInstance);
        void VerifyActionSection(Classes.Browser browserInstance);
        void VerifyOrderAlerts(Classes.Browser browserInstance);
        void VerifySystemAlerts(Classes.Browser browserInstance);
        void VerifySideButtons(Classes.Browser browserInstance);
    }
}
