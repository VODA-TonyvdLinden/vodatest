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
        void VerifyUrgentActions(Classes.Browser browserInstance);
        void VerifyTextHighlightedRed(Classes.Browser browserInstance, string labelPath);
        void VerifyConfirmNowButtonClick(Classes.Browser browserInstance);
        void AddUnconfirmedOrder(Classes.Browser browserInstance);
        void VerifyAsyncNow(Classes.Browser browserInstance);
        void VerifyManageButtonClick(Classes.Browser browserInstance);
        void VeriftyManageCatalogueSearch(Classes.Browser browserInstance);
        void VeriftyManageExpandableArrows(Classes.Browser browserInstance);
        void VeriftyManageExpandableWholesalerSelect(Classes.Browser browserInstance);
        void VerifyDiagnoseButtonClick(Classes.Browser browserInstance);
        void VerifyDiagnoseCheckingNotificationLabels(Classes.Browser browserInstance);
        void VerifyDiagnoseCheckingNotificationTestButton(Classes.Browser browserInstance);
        void VerifyDiagnoseConnectionSpeedTestButton(Classes.Browser browserInstance);
        void VerifyDiagnoseCheckingNotificationTestButtonClick(Classes.Browser browserInstance);
        void VerifyDiagnoseConnectionSpeedTestButtonClick(Classes.Browser browserInstance);
        void VerifyDiagnoseResultPlaceholder(Classes.Browser browserInstance);
    }
}
