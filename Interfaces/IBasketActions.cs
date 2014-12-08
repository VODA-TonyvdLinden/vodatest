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

    }
}
