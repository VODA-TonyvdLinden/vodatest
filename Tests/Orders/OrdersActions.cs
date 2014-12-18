using System;
using System.Threading;

using TestProj.Classes;

using TestProj.Tests.Common;

namespace TestProj.Tests.Orders
{
    public class OrdersActions : Interfaces.IOrdersActions
    {
        // Test Case: 1. Select an order that has been placed  
        // Test Output: 1. A confirm order pop-up is displayed  
        public void SelectOrder(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {
            // placed an order
            Helpers.Instance.AddOrders(browserInstance, 1);

            basketActions.ClickBasketBlock(browserInstance);
            basketActions.ClickOrderNowButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"));

            LogWriter.Instance.Log(@"ISSUE 101: TESTCASE:_02_ViewConfirmedOrder -> Test step there is a step missing before we can get to this confirmation pop up.' -> 
                                    'step added for to make the test execute.' ->'2. Verify that the confirm order pop -up has the following' - Please update the test case.", LogWriter.eLogType.Error);

            basketActions.VerifyConfirmPopup(browserInstance);
            VerifyOrderNow(browserInstance);
        }

        // 2. Verify that the confirm order pop up has the following
        public void VerifyConfirmOrderPopup(Classes.Browser browserInstance)
        {
            // Test Case: 2.1 Make sure that name caption of the pop-up is confirm order 
            Helpers.Instance.Exists(browserInstance, "#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div.successMsg > strong");
            var confirmOrderHeader = Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div.successMsg > strong");
            browserInstance.Instance.Assert.True(() => confirmOrderHeader.Element.Text == "Confirm Order");

            // Test Case: 2.2 Make sure that the message on the body of the pop-up is proceed to view your order    
            // Test Output: 2.2 The message is displayed as proceed to view your order 
            Helpers.Instance.Exists(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > p:nth-child(1)");
            var confirmOrderMessage1 = Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > p:nth-child(1)");
            browserInstance.Instance.Assert.True(() => confirmOrderMessage1.Element.Text.Contains("proceed to view your order"));

            LogWriter.Instance.Log(@"TESTCASE:_02_ViewConfirmedOrder -> Test step in the order confirmation pop up there is button to view my order, and messager to prcess to view the order.' 
                                    '2. Verify that the confirm order pop -up has the following' - Please update the test case.", LogWriter.eLogType.Error);
        }

        // add un confirmed pop up
        public void PlaceUnConfirmedOrder(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {
            SelectOrder(browserInstance, basketActions);
            Thread.Sleep(3000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            Thread.Sleep(3000);
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
        }

        // add confirmed pop up.
        public void PlaceConfirmedOrder(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {
            SelectOrder(browserInstance, basketActions);
            Thread.Sleep(3000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(1)"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-body.text-center > div > button:nth-child(1)"));
            Thread.Sleep(3000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#orderComplete > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderComplete > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
        }

        // Test Case: 1. Click on the <orders> block  at the bottom of the screen
        // Test Output: 1. The orders history are displayed in a tabular format 
        public void VerifyOrdersBlockClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(3) > div"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-history-expanded-view?viewtype=grid"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table:nth-child(1)");
        }

        // Test Case: 2. Verify the following on the order history that is displayed
        public void VerifyOrderHistoryFields(Classes.Browser browserInstance)
        {
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

        // Test Case: 3. Click on the + sign in the select column
        // Test Output: 3. The + sign in the select column changes to - sign and a number of invoices under order number are displayed
        public void VerifyOrderHistoryExpandClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#accordion > td.width100 > div > div > img");
            var removeImage = Helpers.Instance.GetProxy(browserInstance, "#accordion > td.width100 > div > div > img");
            browserInstance.Instance.Click("#accordion > td.width100 > div > div > img");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/removeButton.8e76665e.png").On(removeImage), TimeSpan.FromMinutes(30));
        }

        // Test Case: 1. Click on the order number   
        // Test Output: 1. The Order detail displays items under that order that order
        public void VerifyOrderNumberClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(2)"), TimeSpan.FromMinutes(30));
            var orderNumber = Helpers.Instance.GetProxy(browserInstance, "#orderAccordion0 > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(2)");

            Helpers.Instance.ClickButton(browserInstance, orderNumber);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-detail?orderNumber=" + orderNumber.Element.Text), TimeSpan.FromMinutes(30));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table"), TimeSpan.FromMinutes(30));

            var orderDetails_OrderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");

            browserInstance.Instance.Assert.True(() => orderNumber.Element.Text == orderDetails_OrderNumber.Element.Text);
        }

        // Test Case: 2. Verify that the following in the order detail screen                                                                   
        // Test Case: 2.1 Verify that the list is split per supplier 
        public void VerifyOrdersSplitBySupplier(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log(@"TestCASE:ISSUE 105: _06_ViewOrderDetails -> Test step you cannot one order across multiple suppliers. '
                                    '2.1 Verify that the list is split per supplier' - Test case to be updated.", LogWriter.eLogType.Error);
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.subtitle > td");
        }

        // Test Case: 2.2 Verify the order number is displayed on top of the table                                                                                                                                                        2.1 The list is split per supplier   
        // Test Output: 2.2 The Order number is displayed on top of the table 
        public void VerifyOrderDetailsOrderNumber(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
            var orderDetails_OrderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");

            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-detail?orderNumber=" + orderDetails_OrderNumber.Element.Text);
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

        public void VerifyOrderDetailButtons(Classes.Browser browserInstance)
        {
            // Test Case: 2.4 Verify that the re-order button is displayed                    
            // Test Output: 2.4 The re-order button is displayed 
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)");
            var reOrderButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)");
            browserInstance.Instance.Assert.True(() => reOrderButton.Element.Text == "REORDER");

            // Test Case: 2.5 Verify that the unconfirmed order button is displayed 
            // Test Output: 2.5 The unconfirmed order button is displayed 
            LogWriter.Instance.Log(@"TestCASE:ISSUE 106: _06_ViewOrderDetails -> Test step we do not have the unconfirmed order button visible.'
                                    '2.5 Verify that the unconfirmed order button is displayed' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 2.6 Verify that the back to orders button is displayed  
            // Test Output: 2.6 The back to orders button is displayed  
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(3)");
            var backToOrdersButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(3)");
            browserInstance.Instance.Assert.True(() => backToOrdersButton.Element.Text == "BACK TO ORDERS");

            // Test Case: 2.7 Verify that the view invoice button is displayed 
            // Test Output: 2.7 The invoice button is displayed 
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4)");
            var viewInvoiceButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4)");
            browserInstance.Instance.Assert.True(() => viewInvoiceButton.Element.Text == "VIEW INVOICE");

        }

        // 1. Click on the order re-order button on the view order details screen, but please note that invoking this process the following events happen
        // 1. The result will be based on the outcome of the process
        public void VerifyReOrderButton(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
            var orderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");

            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(1)");
            var itemCode = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody.topBorderZero.ng-scope > tr.ng-scope > td:nth-child(1)");

            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(1)"));

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/basket-catalog-view?orderNumber=" + orderNumber.Element.Text.Trim()), TimeSpan.FromMinutes(30));

            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemSelector.ng-binding");
            var basketItemCount = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemSelector.ng-binding");
            browserInstance.Instance.Assert.True(() => basketItemCount.Element.Text.ToLower().Trim() == "1 items");
        }

        // Test Case: 1. Click on view invoices button on the view orders details screen
        // Test Output: 1. The invoices are displayed in a tabular format 
        public void VerifyViewInvoicesButtonClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");
            var orderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td");

            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4)");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button:nth-child(4"));

            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-invoices?orderNumber=" + orderNumber.Element.Text.Trim() + "&from=main"));
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table");
        }

        // Test Case: 2. Verify the caption of the order invoice  page is displayed as invoices 
        // Test Output: 2. The caption of the order invoice page is displayed as invoices 
        public void VerifyInvoiceHeader(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentHeader > span");
            var invoicesHeader = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentHeader > span");
            browserInstance.Instance.Assert.True(() => invoicesHeader.Element.Text.ToLower() == "invoices");
        }

        // Test Case: 3. Verify that the invoice for order number is displayed
        // Test Output: 3. The invoice order number is displayed
        public void VerifyInvoiceDetailOrderNumber(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody > tr:nth-child(1) > td");
            var invoicesOrderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr:nth-child(1) > td");
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-invoices?orderNumber=" + invoicesOrderNumber.Element.Text.Trim() + "&from=main");
        }

        public void VerifyInvoiceBackToOrdersButton(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.leftBlock > table > tbody > tr:nth-child(1) > td");
            var invoicesOrderNumber = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr:nth-child(1) > td");

            // 6. Verify that the back to orders button is displayed  
            // 6. The back to orders button is displayed   
            browserInstance.Instance.Assert.Exists("#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button");
            var backToOrdersButton = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.rightBlock > div.actionsWidget > div > div > div > div > button");
            browserInstance.Instance.Assert.True(() => backToOrdersButton.Element.Text == "BACK TO ORDERS");

            // 7. Click on the back to orders button   
            // 7. The Orders screen is displayed
            Helpers.Instance.ClickButton(browserInstance, backToOrdersButton);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/order-detail?orderNumber=" + invoicesOrderNumber.Element.Text.Trim() + "&from=main"), TimeSpan.FromMinutes(30));
        }

        // Click yes order button and the check out confirmation pop up will appear.
        private void VerifyOrderNow(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)"));
            Thread.Sleep(5000);
            Helpers.Instance.VerifyPopPup(browserInstance, "#checkoutConfirm");
        }

        /// 2.1 Make sure that the + sign is displayed, this means that the view is collapsed 
        private void VerifyOrderHistoryExpandImage(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#accordion > td.width100 > div > div > img");
            var plusImage = Helpers.Instance.GetProxy(browserInstance, "#accordion > td.width100 > div > div > img");
            browserInstance.Instance.Assert.Attribute("src", "http://aspnet.dev.afrigis.co.za/bopapp/images/addButton.c76fa761.png").On(plusImage);
        }

        private void VerifyOrderHistoryColumn(Classes.Browser browserInstance, string columnPath)
        {
            Helpers.Instance.Exists(browserInstance, columnPath);

            var invoiceNumberColumn = Helpers.Instance.GetProxy(browserInstance, columnPath);
            browserInstance.Instance.Assert.True(() => invoiceNumberColumn.Element.Text != "");
        }

        // 2.7  Make sure the total value is displayed and also the total value is correct
        private void VerifyOrderHistoryTotalColumn(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr > td.width150.bgred");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr > td.width120.bgred.ng-binding");

            var totalColumn = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody > tr > td.width120.bgred.ng-binding");
            browserInstance.Instance.Assert.True(() => totalColumn.Element.Text != "");

            var valueCells = browserInstance.Instance.FindMultiple("#alertsView > div.contentBody > div.leftBlock > div.orderAccordion > div.orderAccordionHeader > table > tbody > #accordion > td:nth-child(6)");
            decimal totalCellsValue = 0;

            valueCells.Elements.ForEach((elementTuple) =>
            {
                // sum the value cells
                FluentAutomation.ElementProxy valueCell = new FluentAutomation.ElementProxy(elementTuple.Item1, elementTuple.Item2);
                decimal cellValue;

                if (decimal.TryParse(valueCell.Element.Text, out cellValue))
                {
                    totalCellsValue = totalCellsValue + cellValue;
                }
            });

            string totalCellValueWithCurreny = "R " + totalCellsValue.ToString("##.00");

            browserInstance.Instance.Assert.True(() => totalColumn.Element.Text == totalCellValueWithCurreny);
        }

        private void VerifyOrderDetailColumn(Classes.Browser browserInstance, string columnPath)
        {
            browserInstance.Instance.Assert.Exists(columnPath);
            var column = Helpers.Instance.GetProxy(browserInstance, columnPath);
            browserInstance.Instance.Assert.True(() => column.Element.Text == "");
        }
    }
}
