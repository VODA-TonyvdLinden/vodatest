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
        void VerifyConfirm(Classes.Browser browserInstance);
        void VerifyOrdersBlockClick(Classes.Browser browserInstance);
        void VerifyConfirmedOrder(Classes.Browser browserInstance);
        void VerifyOrderHistoryFields(Classes.Browser browserInstance);
    }
}
