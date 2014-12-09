using System;
using System.Threading;

using TestProj.Classes;

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
            Thread.Sleep(5000);
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

        public void VerifyUnConfirmedOrder(Classes.Browser browserInstance)
        {
            Thread.Sleep(3000);
            Helpers.Instance.AddOrders(browserInstance, 1);
            Thread.Sleep(3000);
            VerifyBasketBlockClick(browserInstance);
            VerifyOrderSelect(browserInstance);
            VerifyOrderNow(browserInstance);
            Thread.Sleep(3000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            Thread.Sleep(3000);
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
        }

        public void VerifyConfirmedOrder(Classes.Browser browserInstance)
        {

            Thread.Sleep(3000);
            Helpers.Instance.AddOrders(browserInstance, 1);
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

            // Test Case: 2.1 Make sure that the + sign is displayed, this means that the view is collapsed  
            VerifyOrderHistoryExpandImage(browserInstance);

            // Test Case: 2.2 Make sure that the order number is displayed in the order column 
            // Test Output: 2.2 The order number is displayed in the order number column  
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(2)");

            // Test Case: 2.3 Make sure that the supplier column has a number of suppliers displayed 
            // Test Output: 2.3 The supplier number is displayed in the column
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(3)");

            // Test Case: 2.4 Make sure that the date the order was placed is displayed    
            // Test Output: 2.4 The Date the order was placed  is displayed   
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(4)");

            // Test Case: 2.5 Make sure that the invoice column is displayed   
            // Test Output: 2.5 The invoice number is displayed in the invoice column  
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(5)");

            // Test Case: 2.6  Make sure that the value column is displayed with a value   
            // Test Output: 2.6 The value column is displayed with a value  
            VerifyOrderHistoryColumn(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(6)");

            // Test Case: 2.7  Make sure the total value is displayed and also the total value is correct
            // Test Output: 2.7 The total value is displayed and also correct   
            VerifyOrderHistoryTotalColumn(browserInstance);
        }

        /// 2.1 Make sure that the + sign is displayed, this means that the view is collapsed 
        public void VerifyOrderHistoryExpandImage(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#accordion > td.width100 > div > div > img");
            var plusImage = Helpers.Instance.GetProxy(browserInstance, "#accordion > td.width100 > div > div > img");
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/addButton.c76fa761.png").On(plusImage);
        }

        public void VerifyOrderHistoryExpandClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#accordion > td.width100 > div > div > img");
            var removeImage = Helpers.Instance.GetProxy(browserInstance, "#accordion > td.width100 > div > div > img");
            browserInstance.Instance.Click("#accordion > td.width100 > div > div > img");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/removeButton.8e76665e.png").On(removeImage), TimeSpan.FromMinutes(30));
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

        // Test Case: 1. Click on the order number   
        // Test Output: 1. The Order detail displays items under that order that order
        public void VerifyOrderNumberClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#accordion > td:nth-child(2)"), TimeSpan.FromMinutes(30));
            var orderNumber = Helpers.Instance.GetProxy(browserInstance, "#accordion > td:nth-child(2)");

            Helpers.Instance.ClickButton(browserInstance, orderNumber);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-detail?orderNumber=" + orderNumber.Element.Text), TimeSpan.FromMinutes(30));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table"), TimeSpan.FromMinutes(30));
        }

        // Test Case: 2. Verify that the following in the order detail screen                                                                   
        // Test Case: 2.1 Verify that the list is split per supplier 
        public void VerifyOrdersSplitBySupplier(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log(@"TESTCASE:_06_ViewOrderDetails -> Test step you cannot place one order across mutliple suppliers. '. 
                                    '2.1 Verify that the list is split per supplier' - Test case to be updated.", LogWriter.eLogType.Error);
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.subtitle > td");
        }

        // Test Case: 2.2 Verify the order number is displayed on top of the table                                                                                                                                                        2.1 The list is split per supplier   
        // Test Output: 2.2 The Order number is displayed on top of the table 
        public void VerifyOrderDetailsOrderNumber(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
            var orderDetails_OrderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
        }

        // Test Case: 2.3 Verify that item sku code, name, brand, pack size, price ,invoice no, qty and total columns are displayed and pre-populated with values   
        // Test Output: 2.3 The following items are displayed sku code, name, brand, pack size, price ,invoice no, qty and total columns are displayed and pre-populated with values 
        public void VerifyOrderDetailColumns(Classes.Browser browserInstance)
        {
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(1)");
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(2)");
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.subtitle > td");
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(3)");
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(4)");
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(5)");
            VerifyOrderDetailColumn(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(6)");
        }

        public void VerifyOrderDetailColumn(Classes.Browser browserInstance, string columnPath)
        {
            browserInstance.Instance.Assert.Exists(columnPath);
            var column = Helpers.Instance.GetProxy(browserInstance, columnPath);
            browserInstance.Instance.Assert.True(() => column.Element.Text == "");
        }

        public void VerifyOrderDetailButtons(Classes.Browser browserInstance)
        {
            // Test Case: 2.4 Verify that the re-order button is displayed                    
            // Test Output: 2.4 The re-order button is displayed 
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)");

            // Test Case: 2.5 Verify that the unconfirmed order button is displayed 
            // Test Output: 2.5 The unconfirmed order button is displayed 
            LogWriter.Instance.Log(@"TESTCASE:_06_ViewOrderDetails -> Test step we do not have this button visible. '. 
                                    '2.5 Verify that the unconfirmed order button is displayed' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 2.6 Verify that the back to orders button is displayed  
            // Test Output: 2.6 The back to orders button is displayed  
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(3)");

            // Test Case: 2.7 Verify that the view invoice button is displayed 
            // Test Output: 2.7 The invoice button is displayed 
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4)");

        }

        // 1. Click on the order re-order button on the view order details screen, but please note that invoking this process the following events happen
        // 1. The result will be based on the outcome of the process
        public void VerifyReOrderButton(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)");
            var orderNumber = Helpers.Instance.GetProxy(browserInstance, "#accordion > td:nth-child(2)");
            var itemCode = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(1)");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/basket-catalog-view?orderNumber=" + orderNumber.Element.Text), TimeSpan.FromMinutes(30));

            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemSelector.ng-binding");
            var basketItemCount = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemSelector.ng-binding");
            browserInstance.Instance.Assert.True(() => basketItemCount.Element.Text.ToLower().Trim() == "1 items");
        }

        // 1. Click on view invoices button on the view orders details screen
        // 1. The invoices are displayed in a tabular format 
        public FluentAutomation.ElementProxy VerifyViewInvoicesButtonClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4)"), TimeSpan.FromMinutes(30));
            var orderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-invoices?orderNumber=" + orderNumber.Element.Text + "&from=main"));
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table");

            return orderNumber;
        }

        public void VerifyInvoiceHeader(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentHeader > span");
            var invoicesHeader = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentHeader > span");
            browserInstance.Instance.Assert.True(() => invoicesHeader.Element.Text.ToLower() == "invoices");
        }

        public void VerifyInvoiceDetailOrderNumber(Classes.Browser browserInstance, string orderNumber)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody > tr:nth-child(1) > td");
            var invoicesOrderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr:nth-child(1) > td");
            browserInstance.Instance.Assert.True(() => invoicesOrderNumber.Element.Text.ToLower().Contains(orderNumber));
        }

        public void VerifyInvoiceBackToOrdersButton(Classes.Browser browserInstance, string orderNumber)
        {// 6. Verify that the back to orders button is displayed  
            // 6. The back to orders button is displayed   
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button");

            // 7. Click on the back to orders button   
            // 7. The Orders screen is displayed
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-detail?orderNumber=" + orderNumber + "&from=main"), TimeSpan.FromMinutes(30));
        }

    }
}
