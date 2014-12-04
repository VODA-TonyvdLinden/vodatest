using System;
namespace TestProj.Interfaces
{
    public interface IAccessingApplicationActions
    {
        void VerifyLogoAndBanner(Classes.Browser browserInstance);
        void VerifyOnlineIndicator(Classes.Browser browserInstance);
        void VerifyPageLinks(Classes.Browser browserInstance);
        void VerifyPreferedAlias(Classes.Browser browserInstance, string aliasName);
        void VerifySpazaName(Classes.Browser browserInstance);
        void VerifySpecialsExists(Classes.Browser browserInstance);
        void VerifyMarbilExists(Classes.Browser browserInstance);
        void VerifySubApplicationsExists(Classes.Browser browserInstance);
        void VerifyBottomNavExists(Classes.Browser browserInstance);
        void VerifyAlertNotificationExists(Classes.Browser browserInstance);
        void VerifyBasketTotalFieldExists(Classes.Browser browserInstance);
        void VerifyBasketLabelExists(Classes.Browser browserInstance);
        void VerifyBasketTotalAmountExists(Classes.Browser browserInstance);
        void VerifySearchFieldExists(Classes.Browser browserInstance);
        void VerifySearchFieldTextExists(Classes.Browser browserInstance);
        void VerifySearchFieldTextEditableExists(Classes.Browser browserInstance);
        void VerifyAlertSearchBox(Classes.Browser browserInstance);
        void VerifyBaskSearchBox(Classes.Browser browserInstance);
        void VerifySubAppAccessibility(Classes.Browser browserInstance);
        void VerifyMarbilAccessibility(Classes.Browser browserInstance);
        void VerifySpecialAccessibility(Classes.Browser browserInstance);
        void SelectSpaza(Classes.Browser browserInstance, string spazaName);
        void AddSpecialToBasket(Classes.Browser browserInstance);
        void VerifySpazaNameForReturnToApp(Classes.Browser browserInstance);
        void SwitchSpazaAndCheckBasket(Classes.Browser browserInstance, Interfaces.IAccessingApplicationActions accessingApplicationAction);


    }
}
