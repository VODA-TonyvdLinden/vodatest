using System;
namespace TestProj.Interfaces
{
    public interface IOrdersActions
    {
        void SelectOrder(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void VerifyConfirmOrderPopup(Classes.Browser browserInstance);
        void PlaceUnConfirmedOrder(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void PlaceConfirmedOrder(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void VerifyOrderHistoryFields(Classes.Browser browserInstance);
        void VerifyOrdersBlockClick(Classes.Browser browserInstance);
        void VerifyOrderHistoryExpandClick(Classes.Browser browserInstance);
        void VerifyOrderNumberClick(Classes.Browser browserInstance);
        void VerifyOrdersSplitBySupplier(Classes.Browser browserInstance);
        void VerifyOrderDetailsOrderNumber(Classes.Browser browserInstance);
        void VerifyOrderDetailColumns(Classes.Browser browserInstance);
        void VerifyOrderDetailButtons(Classes.Browser browserInstance);
        void VerifyReOrderButton(Classes.Browser browserInstance);
        void VerifyViewInvoicesButtonClick(Classes.Browser browserInstance);
        void VerifyInvoiceHeader(Classes.Browser browserInstance);
        void VerifyInvoiceDetailOrderNumber(Classes.Browser browserInstance);
        void VerifyInvoiceBackToOrdersButton(Classes.Browser browserInstance);
    }
}
