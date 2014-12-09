using System;
namespace TestProj.Interfaces
{
    public interface IOrdersActions
    {
        void VerifyBasketBlockClick(Classes.Browser browserInstance);
        void VerifyOrderSelect(Classes.Browser browserInstance);
        void VerifyOrderNow(Classes.Browser browserInstance);
        void VerifyConfirmOrderPopupContent(Classes.Browser browserInstance);
        void VerifyCancelOrder(Classes.Browser browserInstance);
        void VerifyUnConfirmedOrder(Classes.Browser browserInstance);
        void VerifyConfirm(Classes.Browser browserInstance);
        void VerifyOrdersBlockClick(Classes.Browser browserInstance);
        void VerifyConfirmedOrder(Classes.Browser browserInstance);
        void VerifyOrderHistoryFields(Classes.Browser browserInstance);
        void VerifyOrderHistoryExpandImage(Classes.Browser browserInstance);
        void VerifyOrderHistoryExpandClick(Classes.Browser browserInstance);
        void VerifyOrderNumberClick(Classes.Browser browserInstance);
        void VerifyOrdersSplitBySupplier(Classes.Browser browserInstance);
        void VerifyOrderDetailsOrderNumber(Classes.Browser browserInstance);
        void VerifyOrderDetailColumns(Classes.Browser browserInstance);
        void VerifyOrderDetailColumn(Classes.Browser browserInstance, string columnPath);
        void VerifyOrderDetailButtons(Classes.Browser browserInstance);
        void VerifyReOrderButton(Classes.Browser browserInstance);
        FluentAutomation.ElementProxy VerifyViewInvoicesButtonClick(Classes.Browser browserInstance);
        void VerifyInvoiceHeader(Classes.Browser browserInstance);
        void VerifyInvoiceDetailOrderNumber(Classes.Browser browserInstance, string orderNumber);
        void VerifyInvoiceBackToOrdersButton(Classes.Browser browserInstance, string orderNumber);
    }
}
