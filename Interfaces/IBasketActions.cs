using System;
namespace TestProj.Interfaces
{
    public interface IBasketActions
    {
        void AddOrders(Classes.Browser browserInstance, int supplierIndex);
        void CheckListViewButtonExists(Classes.Browser browserInstance);
        void ClickClearAllButton(Classes.Browser browserInstance);
        void ClickOrderDeleteButton(Classes.Browser browserInstance);
        void ClickConfirmOrderPopupClose(Classes.Browser browserInstance);
        void CheckConfirmPopup(Classes.Browser browserInstance);
        void ClickBasketBlock(Classes.Browser browserInstance);
        void VerifyConfirmPopupValues(Classes.Browser browserInstance, FluentAutomation.ElementProxy noItems);
        void VerifyConfirmPopup(Classes.Browser browserInstance);
        void ClickOrderNowButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy button);
        void CheckClearAllFunction(Classes.Browser browserInstance);
        void CheckOrderAllFunction(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void DeleteOrderFromList(Classes.Browser browserInstance);
        void VerifyListViewActions(Classes.Browser browserInstance);
        void VerifyListView(Classes.Browser browserInstance);
        void VerifyButtons(Classes.Browser browserInstance);
        void VerifyProductView(Classes.Browser browserInstance);
        void CheckElementExists(Classes.Browser browserInstance, string element);

    }
}
