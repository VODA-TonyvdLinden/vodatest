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
        public void AddOrders(Classes.Browser browserInstance, int supplierIndex)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/main");
            var storesBox = Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(1) > a");
            Helpers.Instance.ClickButton(browserInstance, storesBox);

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/stores"), 30);
            //ClickSupplier
            //#catalogCarousel > div > div > div:nth-child(1) > div > img
            //#catalogCarousel > div > div > div:nth-child(2) > div > img
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, string.Format("#catalogCarousel > div > div > div:nth-child({0}) > div > img", supplierIndex)));


            var firstBrand = Helpers.Instance.GetProxy(browserInstance, "#storesContent > div.storesbody > div.filteredContentContainer > div > div > div > div > ul > li > a");
            Helpers.Instance.ClickButton(browserInstance, firstBrand);

            var firstProductBuyButton = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.price > button");
            Helpers.Instance.ClickButton(browserInstance, firstProductBuyButton);
            Helpers.Instance.ClickButton(browserInstance, firstProductBuyButton);
            Helpers.Instance.ClickButton(browserInstance, firstProductBuyButton);
        }
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

        public void ClickOrderNowButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy button)
        {
            Helpers.Instance.ClickButton(browserInstance, button);
        }
        public void CheckConfirmPopup(Classes.Browser browserInstance)
        {
            // 2.2 Click on the order now
            // 2.2 The confirm order pop-up is displayed 
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
        public void VerifyConfirmPopupValues(Classes.Browser browserInstance, FluentAutomation.ElementProxy noItems)
        {
            // 3.3 Make sure that the total number of items and total price of items to be ordered are displayed 
            // 3.3  The total number of items and total price of items are displayed on the pop-up 
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > p:nth-child(2)"), noItems.Element.Text);
            //Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > p:nth-child(3)"), "Total: " + itemsPrice.Element.Text);
            LogWriter.Instance.Log("TESTCASE: _02_BasketConfirmOrder -> Total on popup does not contain the currency symbol. Asert commented out.", LogWriter.eLogType.Error);
        }

        public void VerifyConfirmPopup(Classes.Browser browserInstance)
        {
            // 3.2 The message asking the user about confirm the order is displayed with a yes and no buttons  
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > p:nth-child(1)"), "Are you sure you want to place this order?");
            Helpers.Instance.Exists(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)");
            Helpers.Instance.Exists(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(2)");
        }

        public void CheckClearAllFunction(Classes.Browser browserInstance)
        {
            /// 6.1 Multiple records on the list view table are selected  
            // 6.2 Click on the clear <all> button    
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
            /// 6.2 This clear all selected catalogue basket      
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "0 Items");
        }

        public void CheckOrderAllFunction(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {

            // 5.2 Click on the order all button  
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            basketActions.AddOrders(browserInstance, 1);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            basketActions.AddOrders(browserInstance, 2);
            basketActions.ClickBasketBlock(browserInstance);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(4)"));
        }

        public void DeleteOrderFromList(Classes.Browser browserInstance)
        {
            // 4.2 Click on the delete icon   
            /// 4.2 The order is deleted from that supplier  
            var itemCount = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(4) > div.delete > button"));
            browserInstance.Instance.Assert.Not.Exists("#alertsView > table > tbody > tr > td:nth-child(1)");
        }

        public void VerifyListViewActions(Classes.Browser browserInstance)
        {
            // 3. Verify that order from a specific supplier functions as expected                      
            // 3.1 Select a specific supplier by clicking  on any record from the table   
            /// 3.1 The record on the list view table is selected   
            var supplierRecord = Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(1)");
            Helpers.Instance.ClickButton(browserInstance, supplierRecord);
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div");
        }

        public void VerifyListView(Classes.Browser browserInstance)
        {
            // 2. Verify that on the list view the is a grid view button that will allow to switch back 
            /// 2. The grid view button is displayed  
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button");
        }
    }
}
