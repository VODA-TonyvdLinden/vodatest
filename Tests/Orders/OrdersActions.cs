using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Tests.Common;

namespace TestProj.Tests.Orders
{
    public class OrdersActions : Interfaces.IOrdersActions
    {
        //TEST STEPS 1:  Click on the basket block at the bottom of the screen
        //TEST OUTPUT 1: The basket page is displayed with catalogues in grid view
        public void VerifyBasketBlockClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(2) > div"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/basket-catalog-view?viewtype=grid"), TimeSpan.FromMinutes(30));
        }

        public void VerifyOrdersBlockClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(3) > div"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-history-expanded-view?viewtype=grid"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table:nth-child(1)");
        }

        // TEST STEPS 2: Select an order that is pending confirmation
        // TEST OUTPUT 2:  The confirmation pop-up is displayed
        public void VerifyOrderSelect(Classes.Browser browserInstance)
        {
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)"), TimeSpan.FromMinutes(30));

            Thread.Sleep(3000);
        }

        public void VerifyOrderNow(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(1)"), TimeSpan.FromMinutes(30));
        }

        // TEST STEPS 1:  3. Verify that the confirmation pop-up has the following items
        // TEST STEPS 1:  3.1 Confirm that the name caption of the pop-up is confirm order
        // TEST STEPS 1:  3.2  Make sure that the message content on that pop-up states that your order has been generated do you want to continue and place order, also the cancel and yes order is displayed 
        // TEST STEPS 1:  3.2  The message content in the pop-up is  displayed as your order has been generated do you want to continue and place order and also the yes and cancel order button is displayed
        public void VerifyConfirmOrderPopupContent(Classes.Browser browserInstance)
        {
            var confirmOrderHeader = Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div.successMsg > strong");
            var confirmOrderMessage1 = Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > p:nth-child(1)");
            var confirmOrderMessage2 = Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > p:nth-child(2)");

            browserInstance.Instance.Assert.True(() => confirmOrderHeader.Element.Text == "Confirm Order");
            browserInstance.Instance.Assert.True(() => confirmOrderMessage1.Element.Text == "Your order has been generated.");
            browserInstance.Instance.Assert.True(() => confirmOrderMessage2.Element.Text == "Do you want to continue and place the order?");
            Helpers.Instance.Exists(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(1)");
            Helpers.Instance.Exists(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(2)");
        }

        // 4. Click on the Cancel button and verify that the order is still intact 
        // 4.The Order is still the same, nothing has changed
        public void VerifyCancelOrder(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(2)"));
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button");
        }

        // 6. Click on order now  
        // 6. The basket is cleared , the order was successfully placed 
        public void VerifyConfirm(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(1)"));
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button");
        }

        public void VerifyConfirmedOrder(Classes.Browser browserInstance)
        {

            Thread.Sleep(3000);
            Helpers.Instance.AddSpecialToBasket(browserInstance);
            Thread.Sleep(3000);
            VerifyBasketBlockClick(browserInstance);
            VerifyOrderSelect(browserInstance);
            VerifyOrderNow(browserInstance);
            Thread.Sleep(3000);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(1)"));
            Thread.Sleep(3000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#orderComplete > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderComplete > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            Thread.Sleep(3000);
        }

        public void VerifyOrderHistoryFields(Classes.Browser browserInstance)
        {

            // 2. Verify the following on the order history that is displayed                                                       
            // 2.1 Make sure that the + sign is displayed, this means that the view is collapsed 
            VerifyOrderHistoryExpandImage(browserInstance);

            // 2.2 Make sure that the order number is displayed in the order column  
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(2)");

            // 2.3 Make sure that the supplier column has a number of suppliers displayed
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(3)");

            // 2.4 Make sure that the date the order was placed is displayed 
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(4)");

            // 2.5 Make sure that the invoice column is displayed 
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(5)");

            // 2.6  Make sure that the value column is displayed with a value 
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(6)");

            // 2.7  Make sure the total value is displayed and also the total value is correct
            VerifyOrderHistoryTotalColumn(browserInstance);
        }

        /// 2.1 Make sure that the + sign is displayed, this means that the view is collapsed 
        public void VerifyOrderHistoryExpandImage(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#accordion > td.width100 > div > div > img");
            var plusImage = Helpers.Instance.GetProxy(browserInstance, "#accordion > td.width100 > div > div > img");
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/addButton.c76fa761.png").On(plusImage);
        }

        public void VerifyOrderHistoryColumn(Classes.Browser browserInstance, string columnPath)
        {
            Helpers.Instance.Exists(browserInstance, columnPath);

            var invoiceNumberColumn = Helpers.Instance.GetProxy(browserInstance, columnPath);
            browserInstance.Instance.Assert.True(() => invoiceNumberColumn.Element.Text != "");
        }

        // 2.7  Make sure the total value is displayed and also the total value is correct
        public void VerifyOrderHistoryTotalColumn(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr > td.width150.bgred");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr > td.width120.bgred.ng-binding");

            var totalColumn = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr > td.width120.bgred.ng-binding");
            browserInstance.Instance.Assert.True(() => totalColumn.Element.Text != "");
        }
    }
}
