using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Basket
{
    public class BasketActions : Interfaces.IBasketActions
    {
        public void CheckListViewButtonExists(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button");
        }

        public void ClickClearAllButton(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "0 Items");
        }

        public void ClickOrderDeleteButton(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.delete > button"));
            // 3.2 The order is deleted from that supplier
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "0 Items");
        }

        public void ClickConfirmOrderPopupClose(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log("TESTCASE:_01_BasketGridView -> Test case step missed -> CLICK CLOSE BUTTON", LogWriter.eLogType.Error);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            Thread.Sleep(1000);
        }

        public void CheckConfirmPopup(Classes.Browser browserInstance)
        {
            Thread.Sleep(5000);
            // 2.2 Click on the order now
            // 2.2 The confirm order pop-up is displayed 
            var orderNowButton = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button");
            Helpers.Instance.ClickButton(browserInstance, orderNowButton);

            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(2)"), 30);
            Helpers.Instance.Exists(browserInstance, "#orderNow > div");
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-header.vodaBackgroundGrey > div.successMsg > strong"), "Order now");
        }

        public void ClickBasketBlock(Classes.Browser browserInstance)
        {
            var basketBlock = Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(2) > div");
            Helpers.Instance.ClickButton(browserInstance, basketBlock);
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/basket-catalog-view?viewtype=grid");
        }
    }
}
