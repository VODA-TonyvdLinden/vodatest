using System;
namespace TestProj.Interfaces
{
    public interface IBasketActions
    {
        void CheckListViewButtonExists(Classes.Browser browserInstance);
        void ClickClearAllButton(Classes.Browser browserInstance);
        void ClickOrderDeleteButton(Classes.Browser browserInstance);
        void ClickConfirmOrderPopupClose(Classes.Browser browserInstance);
        void CheckConfirmPopup(Classes.Browser browserInstance);
        void ClickBasketBlock(Classes.Browser browserInstance);
        void VerifyConfirmPopupValues(Classes.Browser browserInstance, FluentAutomation.ElementProxy noItems, FluentAutomation.ElementProxy itemPrice);
        void VerifyConfirmPopup(Classes.Browser browserInstance);
        void ClickOrderNowButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy button);
        void CheckClearAllFunction(Classes.Browser browserInstance);
        void CheckOrderAllFunction(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void DeleteOrderFromList(Classes.Browser browserInstance);
        void VerifyListViewActions(Classes.Browser browserInstance);
        void VerifyListView(Classes.Browser browserInstance);
        void VerifyButtons(Classes.Browser browserInstance);
        void VerifyProductView(Classes.Browser browserInstance);
        void VerifyDetailsValues(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void CheckElementExists(Classes.Browser browserInstance, string element);
        void VerifyPopupValues(Classes.Browser browserInstance);
        void CheckFavAdded(Classes.Browser browserInstance, string prodDescription);
        void TestFavButtonOnPopup(Classes.Browser browserInstance);
        string ClickProduct(Classes.Browser browserInstance);
        void VerifyFormula(Classes.Browser browserInstance, FluentAutomation.ElementProxy qtyBox, int qty);
        void TestAddRemoveButtons(Classes.Browser browserInstance, out FluentAutomation.ElementProxy qtyBox, out int qty);
        void ClickPopupClose(Classes.Browser browserInstance, string path);

    }
}
