﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Orders
{
    [TestFixture, Description("Orders"), Category("Orders")]
    public class Orders
    {
        Classes.Browser browserInstance;
        IUnityContainer container = new UnityContainer();

        [TestFixtureSetUp]
        public void Initialise()
        {
            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IOrdersActions, Tests.Orders.OrdersActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the basket block at the bottom of the screen
        /// 2. Select an order that is pending confirmation
        /// 3. Verify that the confirmation pop-up has the following items
        ///   3.1 Confirm that the name caption of the pop-up is confirm order
        ///   3.2  Make sure that the message content on that pop-up states that your order has been generated do you 
        ///     want to continue and place order, also the cancel and yes order is displayed
        /// 4. Click on the Cancel button and verify thatthe order is still intact
        /// 5.Select again the order that is pending confirmation
        /// 6. Click on order now
        /// TEST OUTPUT:
        /// 1. The basket page is displayed with catalogues in grid view
        /// 2.  The confirmation pop-up is displayed
        /// 3.
        ///   3.1 The pop-up name caption is confirm order
        ///   3.2  The message content in the pop-up is  displayed as your order has been generated do you want to 
        ///     continue and place order and also the yes and cancel order button is displayed
        /// 4.The Order is still the same, nothing has changed
        /// 5. The confirmation pop-up is displayed
        /// 6. The basket is cleared , the order was successfully placed
        /// </summary>
        [Test, Description("PendingOrdersConfirmation"), Repeat(1)]
        public void PendingOrdersConfirmation()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Select an order that has been placed
        /// 2. Verify that the confirm order pop -up has the following
        ///   2.1 Make sure that name caption of the pop-up is confirm order
        ///   2.2 Make sure that the message on the body of the pop-up is proceed to view your order
        ///   2.3 make sure that the view my order button is displayed
        /// 3. Click on the view order button
        /// TEST OUTPUT:
        /// 1. A confirm order pop-up is displayed
        /// 2.
        ///   2.1 The pop-up name caption is confirm order
        ///   2.2 The message is displayed as proceed to view your order
        ///   2.3 The view my order button is displayed
        /// 3. The orders that have been confirmed are displayed
        /// </summary>
        [Test, Description("ViewConfirmedOrder"), Repeat(1)]
        public void ViewConfirmedOrder()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the <orders> block  at the bottom of the screen
        /// 2. Verify the following on the order history that is displayed
        ///   2.1 Make sure that the + sign is displayed, this means that the view is collapsed
        ///   2.2 Make aure that the order number is displayed in the order column
        ///   2.3 Make sure that the supplier column has a number of suppliers displayed
        ///   2.4 Make sure that the date the order was placed is displayed
        ///   2.5 Make sure that the invoice column is displayed
        ///   2.6  Make sure that the value column is displayed with a value
        ///   2.7  Make sure the total value is displayed and also the total value is correct
        /// TEST OUTPUT:
        /// 1. The orders history are displayed in a tabular format
        /// 2.
        ///   2.1 The select column has a + sign to show that it is in a collapsed view
        ///   2.2 The order number is displayed in the order number column
        ///   2.3 The supplier number is displayed in the column
        ///   2.4 The Date the order was placed  is displayed
        ///   2.5 The invoice number is displayed in the invoice column
        ///   2.6 The value column is displayed with a value
        ///   2.7 The total value is displayed and also correct
        /// </summary>
        [Test, Description("OrderHistoryCollapsedView"), Repeat(1)]
        public void OrderHistoryCollapsedView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the <orders> block  at the bottom of the screen
        /// 2. Verify the following on the order history that is displayed
        ///   2.1 Make sure that the + sign is displayed, this means that the view is collapsed
        ///   2.2 Make aure that the order number is displayed in the order column
        ///   2.3 Make sure that the supplier column has a number of suppliers displayed
        ///   2.4 Make sure that the date the order was placed is displayed
        ///   2.5 Make sure that the invoice column is displayed
        ///   2.6  Make sure that the value column is displayed with a value
        ///   2.7  Make sure the total value is displayed and also the total value is correct
        /// 3. Click on the + sign in the select column
        /// TEST OUTPUT:
        /// 1. The orders history are displayed in a tabular format
        /// 2.
        ///   2.1 The select column has a + sign to show that it is in a collapsed view
        ///   2.2 The order number is displayed in the order number column
        ///   2.3 The supplier number is displayed in the column
        ///   2.4 The Date the order was placed  is displayed
        ///   2.5 The invoice number is displayed in the invoice column
        ///   2.6 The value column is displayed with a value
        ///   2.7 The total value is displayed and also correct
        /// 3. The + sign in the select column changes to - sign and a number of invoices under order number are diplayed
        /// </summary>
        [Test, Description("OrderHistoryExpanded"), Repeat(1)]
        public void OrderHistoryExpanded()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the <orders> block  at the bottom of the screen
        /// 2. Verify the following on the order history that is displayed
        ///   2.1 Make sure that the + sign is displayed, this means that the view is collapsed
        ///   2.2 Make aure that the order number is displayed in the order column
        ///   2.3 Make sure that the supplier column has a number of suppliers displayed
        ///   2.4 Make sure that the date the order was placed is displayed
        ///   2.5 Make sure that the invoice column is displayed
        ///   2.6  Make sure that the value column is displayed with a value
        ///   2.7  Make sure the total value is displayed and also the total value is correct
        /// 3. Click on the + sign in the select column
        /// 4. Click on the invoice
        /// 5. Verify that the following are displayed
        ///   5.1 Verify that contact number of supplier, vat number, supplier name,supplier address, invoice copy, 
        ///     order number, order date and invoice date are prepolulated
        ///   5.2 Verify that the item code, name, brand, pack size, price, distributor, qty, and price value column 
        ///     are pre-polated with values
        /// 6. Verify that the <back to orders> button is displayed
        /// 7. Verify that the  <view order> button is displayed
        /// 8.  Verify that the <log discrepiancy> is displayed
        /// 9. Verify that the total amount of items is correct
        /// TEST OUTPUT:
        /// 1. The orders history are displayed in a tabular format
        /// 2.
        ///   2.1 The select column has a + sign to show that it is in a collapsed view
        ///   2.2 The order number is displayed in the order number column
        ///   2.3 The supplier number is displayed in the column
        ///   2.4 The Date the order was placed  is displayed
        ///   2.5 The invoice number is displayed in the invoice column
        ///   2.6 The value column is displayed with a value
        ///   2.7 The total value is displayed and also correct
        /// 3. The + sign in the select column changes to - sign and a number of invoices under order number are diplayed
        /// 4. The invoice is displayed in detail
        /// 5.
        ///   5.1 The contact number of supplier, vat number, supplier name,supplier address, invoice copy, order number, 
        ///     order date and invoice date are prepolulated  and displayed
        ///   5.2 The  following items are displayed item code, name, brand, pack size, price, distributor, qty, and price 
        ///     value column are pre-polated with values
        /// 6. The Back to orders button is displayed
        /// 7. The view order button is displayed
        /// 8. The log discrepiancy button is displayed
        /// 9. Total number of items is correct
        /// </summary>
        [Test, Description("ViewInvoiceDetail"), Repeat(1)]
        public void ViewInvoiceDetail()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the order number
        /// 2. Verify that the following in the order detail screen
        ///   2.1 Verify that the list is split per supplier
        ///   2.2 Verify the order number is displayed on top of the table
        ///   2.3 Verify that item sku code, name, brand, pack size, price ,invoice no, qty and total colums are displayed 
        ///     and pre-populated with values
        ///   2.4 Verify that the re-order button is displayed
        ///   2.5 Verify that the unconfirmed order button is displayed
        ///   2.6 Verify that the back to orders button is displayed
        ///   2.7 Verify that the view invoice button is displayed
        /// TEST OUTPUT:
        /// 1. The Order detail displays items under that order that order
        /// 2.
        ///   2.1 The list is splited per supplier
        ///   2.2 The Order number is displayed on top of the table
        ///   2.3 The following items are displayed sku code, name, brand, pack size, price ,invoice no, qty and total 
        ///     colums are displayed and pre-populated with values
        ///   2.4 The re-order button is displayed
        ///   2.5 The unconfirmed order button is displayed
        ///   2.6 The back to orders button is displayed
        ///   2.7 The invoice button is displayed
        /// </summary>
        [Test, Description("ViewOrderDetails"), Repeat(1)]
        public void ViewOrderDetails()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the order re-order button on the view order details screen, but please note that invonking this process 
        ///   the following events happen
        /// 1. Invoking REORDER will follow the following steps:
        /// a. Add all SKU’s in the order to the correct catalogue baskets
        /// b. Add the quantity of the order in the basket
        /// c. If a SKU is not available in the catalogue, ignore the SKU
        /// d. If a SKU already exists in the basket, append the quantity
        /// e. Order basket
        /// TEST OUTPUT:
        /// 1. The result will be based on the outcome of the process
        /// </summary>
        [Test, Description("ReOrder"), Repeat(1)]
        public void ReOrder()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the unconfirmed orders button
        /// 2. Follow the order process
        /// 3. confirm all unconfirmed orders
        /// TEST OUTPUT:
        /// 1. The unconfirmed order is displayed if they are unconfirmed orders
        /// 2. The order is confirmed
        /// 3. The Unconfirmed order button should be inactive if there are no more orders to confirm
        /// </summary>
        [Test, Description("UnconfirmedOrders"), Repeat(1)]
        public void UnconfirmedOrders()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on view invoices button on the view orders details screen
        /// 2. Verify the caption of the order invoice  page is displayed as invoices
        /// 3. Verify that the invoice for order number is displayed
        /// 4. Verify that the invoice number, supplier, invoice date and value are pre-populated
        /// 5. Verify that  the total number of value price is correct
        /// 6. Verify that the back to orders button is displayed
        /// 7. Click on the back to orders button
        /// TEST OUTPUT:
        /// 1. The invoices are displayed in a tabular format
        /// 2. The caption of the order invoice page is displayed as invoices
        /// 3. The invoice order number is displayed
        /// 4.The  invoice number, supplier, invoice date , value are pre-populated  and displayed
        /// 5. The total number value price is displayed and correct
        /// 6. The back to orders button is displayed
        /// 7. The Orders screen is displayed
        /// </summary>
        [Test, Description("ViewInvoices"), Repeat(1)]
        public void ViewInvoices()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Go to view invoice detail scren
        /// 2. Click on the <log descrepancy> button
        /// 3. Verify that the order descrepancy screen is displayed with the following
        ///   3.1 Verify that the order number is displayed
        ///   3.2  Verify that the item code, name, brand, pack size, price, qty, value and discrepancy colums are displayed 
        ///     and pre-populated with values and also has a discrepancy field which is a drop down
        ///   3.3 Verify that the submit button is displayed on the screen
        /// 4. Select the discrepancy field
        /// 5.Click on the drop down arrow on the discrepancy field
        /// 6. User select a discrepancy reason that this applicable
        /// 7. Click on the submit button
        /// TEST OUTPUT:
        /// 1.The view invoice detail screen is displayed
        /// 2. The order discrepancy screen is displayed
        /// 3.
        ///   3.1 The order number is displayed
        ///   3.2  The  item code, name, brand, pack size, price, qty, value , discrepancy colums are displayed and pre-populated 
        ///     with values and also discrepancy field  which is a drop down
        ///   3.3 The submit button is displayed on the screen
        /// 4. The discrepancy field is populated
        /// 5. The list of order discrepancies are displayed on the drop down
        /// 6. The discrepancy reason is displayed
        /// 7. The discrepancy is successfully logged
        /// </summary>
        [Test, Description("LoggingDiscrepancies"), Repeat(1)]
        public void LoggingDiscrepancies()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            //TODO
        }
    }
}
