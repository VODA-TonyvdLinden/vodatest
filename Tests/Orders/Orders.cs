using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

using NUnit.Framework;

using System;
using System.Threading;

using TestProj.Classes;

using TestProj.Tests.Common;

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
            ProcessKiller.Instance.Kill();
            Thread.Sleep(500);

            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IOrdersActions, Tests.Orders.OrdersActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
            container.RegisterType<Interfaces.IBasketActions, Tests.Basket.BasketActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());

            Helpers.Instance.Activate(browserInstance, false);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST: PENDING ORDER CONFIRMATION
        /// Test Case ID: 34_FRS_Ref_5.3.3
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Basket  landing page
        /// TEST STEPS:
        /// 1. Click on the basket block at the bottom of the screen          
        /// 2. Click on the 'Order Now' button
        /// 3. Verify un confirmed order error popup                                                  
        /// 3.1 Verify pop up heading 'Pending Offer'
        /// 3.2 Verify message containing 'You have an unconfirmed order' is displayed 
        /// 3.3 Verify back button is displayed 
        /// 5.Select again the order that is pending confirmation 
        /// 6. Click on order now  
        /// TEST OUTPUT:
        /// 1. The basket page is displayed with catalogues in grid view  
        /// 2. The pending order error pop-up is displayed 
        /// 3.                                                                                                                                                                                                                 
        /// 3.1 Pop up heading is diaplayed with 'Pending Order'  
        /// 3.2 Message is displayed
        /// 3.3 Back Button is displayed  
        /// </summary>
        [Test, Description("_01_PendingOrdersConfirmation"), Category("Orders"), Repeat(1)]
        public void _01_PendingOrdersConfirmation()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();

            // Place Unconfirmed Order
            Helpers.Instance.PlaceUnConfirmedOrder(browserInstance);

            // Test Case 1:  Click on the basket block at the bottom of the screen
            // Test Output 1: The basket page is displayed with catalogues in grid view
            Helpers.Instance.GoToBasket(browserInstance);
            Thread.Sleep(3000);

            // Test Case: 2 Click on the 'Order Now' button
            // Test Output: 2 Verify un confirmed order error popup
            // Tets Output: 2.1 Popup with heading 'Pending Offer'
            // Tets Output: 2.2 Popup with message containing 'You have an unconfirmed order'
            // Tets Output: 2.2 Popup with back button is displayed
            Helpers.Instance.WaitFor(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button");
            basketActions.ClickOrderNowButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"));
            basketActions.VerifyUnConfirmOrderPopup(browserInstance);
            
            //Clean up
            Helpers.Instance.ConfirmUnConfirmedOrder(browserInstance);
        }

        /// <summary>
        /// TEST: VIEW CONFIRMED  ORDER
        /// Test Case ID: 35_FRS_Ref_5.3.3
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Basket  landing page
        /// TEST STEPS:
        /// 1. Select an order that has been placed   
        /// 2. Verify that the confirm order pop -up has the following                                                         
        /// 2.1 Make sure that name caption of the pop-up is confirm order  
        /// 2.2 Make sure that the message on the body of the pop-up is proceed to view your order    
        /// 2.3 make sure that the view my order button is displayed   
        /// 3. Click on the view order button  
        /// TEST OUTPUT:
        /// 1. A confirm order pop-up is displayed    
        /// 2.                                                                                                                                                                                                                 
        /// 2.1 The pop-up name caption is confirm order  
        /// 2.2 The message is displayed as proceed to view your order  
        /// 2.3 The view my order button is displayed  
        /// 3. The orders that have been confirmed are displayed
        /// </summary>
        [Test, Description("_02_ViewConfirmedOrder"), Category("Orders"), Repeat(1)]
        public void _02_ViewConfirmedOrder()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));

            // Test Case: 1. Select an order that has been placed   
            // Test Output: 1. A confirm order pop-up is displayed  
            Thread.Sleep(3000);
            ordersActions.SelectOrder(browserInstance);

            // Test Case: 2. Verify that the confirm order pop -up has the following 

            // Test Case: 2.3 make sure that the view my order button is displayed   
            // Test Output: 2.3 The view my order button is displayed  
            Thread.Sleep(3000);
            ordersActions.VerifyConfirmOrderPopup(browserInstance);

            // 3. Click on the view order button 
            LogWriter.Instance.Log(@"ISSUE 103: TESTCASE:_02_ViewConfirmedOrder -> Test step in the order confirmation pop up there is no button to view my order, this test cannot be tested.' 
                                    '3. Click on the view order button' - Please update the test case.", LogWriter.eLogType.Error);

            Thread.Sleep(3000);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#checkoutConfirm > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            //Clean up
            Thread.Sleep(3000);
            Helpers.Instance.ConfirmUnConfirmedOrder(browserInstance);
        }

        /// <summary>
        /// TEST: ORDER HISTORY COLLAPSED VIEW
        /// Test Case ID: 36_FRS_Ref_5.4
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1. Click on the <orders> block  at the bottom of the screen     
        /// 2. Verify the following on the order history that is displayed                                                       
        /// 2.1 Make sure that the + sign is displayed, this means that the view is collapsed  
        /// 2.2 Make sure that the order number is displayed in the order column     
        /// 2.3 Make sure that the supplier column has a number of suppliers displayed 
        /// 2.4 Make sure that the date the order was placed is displayed 
        /// 2.5 Make sure that the invoice column is displayed 
        /// 2.6  Make sure that the value column is displayed with a value 
        /// 2.7  Make sure the total value is displayed and also the total value is correct
        /// TEST OUTPUT:
        /// 1. The orders history are displayed in a tabular format   
        /// 2.    
        /// 2.1 The select column has a + sign to show that it is in a collapsed view  
        /// 2.2 The order number is displayed in the order number column
        /// 2.3 The supplier number is displayed in the column 
        /// 2.4 The Date the order was placed  is displayed
        /// 2.5 The invoice number is displayed in the invoice column
        /// 2.6 The value column is displayed with a value  
        /// 2.7 The total value is displayed and also correct                                                                                                                                                                                                                                                                                                               
        /// </summary>
        [Test, Description("_03_OrderHistoryCollapsedView"), Category("Orders"), Repeat(1)]
        public void _03_OrderHistoryCollapsedView()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Thread.Sleep(3000);

            Helpers.Instance.PlaceConfirmedOrder(browserInstance);

            // Test Case: 1. Click on the <orders> block  at the bottom of the screen
            // Test Output: 1. The orders history are displayed in a tabular format
            Thread.Sleep(3000);
            ordersActions.VerifyOrdersBlockClick(browserInstance);

            // Test Case: 2. Verify the following on the order history that is displayed
            Thread.Sleep(3000);
            ordersActions.VerifyOrderHistoryFields(browserInstance);
        }

        /// <summary>
        /// TEST: ORDER HISTORY EXPANDED
        /// Test Case ID: 37_FRS_Ref_5.4
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1. Click on the <orders> block  at the bottom of the screen    
        /// 2. Verify the following on the order history that is displayed                                                             
        /// 2.1 Make sure that the + sign is displayed, this means that the view is collapsed    
        /// 2.2 Make sure that the order number is displayed in the order column   
        /// 2.3 Make sure that the supplier column has a number of suppliers displayed  
        /// 2.4 Make sure that the date the order was placed is displayed 
        /// 2.5 Make sure that the invoice column is displayed 
        /// 2.6  Make sure that the value column is displayed with a value  
        /// 2.7  Make sure the total value is displayed and also the total value is correct  
        /// 3. Click on the + sign in the select column                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        /// TEST OUTPUT:
        /// 1. The orders history are displayed in a tabular format      
        /// 2. 
        /// 2.1 The select column has a + sign to show that it is in a collapsed view
        /// 2.2 The order number is displayed in the order number column 
        /// 2.3 The supplier number is displayed in the column 
        /// 2.4 The Date the order was placed  is displayed  
        /// 2.5 The invoice number is displayed in the invoice column    
        /// 2.6 The value column is displayed with a value  
        /// 2.7 The total value is displayed and also correct
        /// 3. The + sign in the select column changes to - sign and a number of invoices under order number are displayed 
        /// </summary>
        [Test, Description("_04_OrderHistoryExpanded"), Category("Orders"), Repeat(1)]
        public void _04_OrderHistoryExpanded()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));

            Thread.Sleep(3000);
            Helpers.Instance.PlaceConfirmedOrder(browserInstance);

            // Test Case: 1. Click on the <orders> block  at the bottom of the screen
            // Test Output: 1. The orders history are displayed in a tabular format
            Thread.Sleep(3000);
            ordersActions.VerifyOrdersBlockClick(browserInstance);

            // Test Case: 2. Verify the following on the order history that is displayed
            Thread.Sleep(3000);
            ordersActions.VerifyOrderHistoryFields(browserInstance);

            // Test Case: 3. Click on the + sign in the select column
            // Test Output: 3. The + sign in the select column changes to - sign and a number of invoices under order number are displayed
            Thread.Sleep(3000);
            ordersActions.VerifyOrderHistoryExpandClick(browserInstance);
        }

        /// <summary>
        /// TEST: VIEW INVOICE DETAIL
        /// Test Case ID: 38_FRS_Ref_5.4.1
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1. Click on the <orders> block  at the bottom of the screen     
        /// 2. Verify the following on the order history that is displayed                                                             
        /// 2.1 Make sure that the + sign is displayed, this means that the view is collapsed   
        /// 2.2 Make sure that the order number is displayed in the order column 
        /// 2.3 Make sure that the supplier column has a number of suppliers displayed 
        /// 2.4 Make sure that the date the order was placed is displayed    
        /// 2.5 Make sure that the invoice column is displayed  
        /// 2.6  Make sure that the value column is displayed with a value   
        /// 2.7  Make sure the total value is displayed and also the total value is correct  
        /// 3. Click on the + sign in the select column    
        /// 4. Click on the invoice  
        /// 5. Verify that the following are displayed                                                                                         
        /// 5.1 Verify that contact number of supplier, vat number, supplier name, supplier address, invoice copy, order number, order date and invoice date are prepopulated        
        /// 5.2 Verify that the item code, name, brand, pack size, price, distributor, qty, and price value column are pre-populated with values       
        /// 6. Verify that the <back to orders> button is displayed     
        /// 7. Verify that the  <view order> button is displayed
        /// 8.  Verify that the <log discrepancy> is displayed 
        /// 9. Verify that the total amount of items is correct 
        /// TEST OUTPUT:
        /// 1. The orders history are displayed in a tabular format    
        /// 2.                                                                                                                                                                                                                 2.1 The select column has a + sign to show that it is in a collapsed view  
        /// 2.2 The order number is displayed in the order number column  
        /// 2.3 The supplier number is displayed in the column
        /// 2.4 The Date the order was placed  is displayed   
        /// 2.5 The invoice number is displayed in the invoice column  
        /// 2.6 The value column is displayed with a value  
        /// 2.7 The total value is displayed and also correct   
        /// 3. The + sign in the select column changes to - sign and a number of invoices under order number are displayed  
        /// 4. The invoice is displayed in detail 
        /// 5.                                                                                                                                                                                                                   5.1 The contact number of supplier, vat number, supplier name, supplier address, invoice copy, order number, order date and invoice date are prepopulated  and displayed    
        /// 5.2 The  following items are displayed item code, name, brand, pack size, price, distributor, qty, and price value column are pre-populated with values  
        /// 6.The Back to orders button is displayed    
        /// 7. The view order button is displayed   
        /// 8. The log discrepancy button is displayed    
        /// 9. Total number of items is correct   
        /// </summary>
        [Test, Description("_05_ViewInvoiceDetail"), Category("Orders"), Repeat(1)]
        public void _05_ViewInvoiceDetail()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Thread.Sleep(30000);
            Helpers.Instance.PlaceConfirmedOrder(browserInstance);

            // Test Case: 1. Click on the <orders> block  at the bottom of the screen  
            // Test Output: 1. The orders history are displayed in a tabular format
            Thread.Sleep(30000);
            ordersActions.VerifyOrdersBlockClick(browserInstance);

            // Test Case: 2. Verify the following on the order history that is displayed    
            Thread.Sleep(30000);
            ordersActions.VerifyOrderHistoryFields(browserInstance);

            // Test Case: 3. Click on the + sign in the select column
            // Test Output: 3. The + sign in the select column changes to - sign and a number of invoices under order number are displayed  
            Thread.Sleep(30000);
            ordersActions.VerifyOrderHistoryExpandClick(browserInstance);

            // Test Case: 4. Click on the invoice  
            // Test Output: 4. The invoice is displayed in detail
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 104: _05_ViewInvoiceDetail -> Test step we cannot automate this step and the following steps as we have no control as to when the invoices will appear on the system.'
                                    ' 4. Click on the invoice' - Test case need to be manually tested.", LogWriter.eLogType.Error);

            Thread.Sleep(3000);
        }

        /// <summary>
        /// TEST: VIEW ORDER DETAILS
        /// Test Case ID: 39_FRS_Ref_5.4.1
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1. Click on the order number   
        /// 2. Verify that the following in the order detail screen                                                                   
        /// 2.1 Verify that the list is split per supplier  
        /// 2.2 Verify the order number is displayed on top of the table 
        /// 2.3 Verify that item sku code, name, brand, pack size, price ,invoice no, qty and total columns are displayed and pre-populated with values       
        /// 2.4 Verify that the re-order button is displayed  
        /// 2.5 Verify that the unconfirmed order button is displayed 
        /// 2.6 Verify that the back to orders button is displayed  
        /// 2.7 Verify that the view invoice button is displayed     
        /// TEST OUTPUT:
        /// 1. The Order detail displays items under that order that order
        /// 2.                                                                                                                                                                                                                   2.1 The list is split per supplier   
        /// 2.2 The Order number is displayed on top of the table  
        /// 2.3 The following items are displayed sku code, name, brand, pack size, price ,invoice no, qty and total columns are displayed and pre-populated with values    
        /// 2.4 The re-order button is displayed  
        /// 2.5 The unconfirmed order button is displayed 
        /// 2.6 The back to orders button is displayed  
        /// 2.7 The invoice button is displayed                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        /// </summary>
        [Test, Description("_06_ViewOrderDetails"), Category("Orders"), Repeat(1)]
        public void _06_ViewOrderDetails()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));

            Thread.Sleep(3000);
            Helpers.Instance.PlaceConfirmedOrder(browserInstance, true);
            Thread.Sleep(3000);
            ordersActions.VerifyOrdersBlockClick(browserInstance);

            // Test Case: 1. Click on the order number   
            // Test Output: 1. The Order detail displays items under that order that order
            Thread.Sleep(3000);
            ordersActions.VerifyOrderNumberClick(browserInstance);

            // Test Case: 2. Verify that the following in the order detail screen                                                                   
            // Test Case: 2.1 Verify that the list is split per supplier 
            Thread.Sleep(3000);
            ordersActions.VerifyOrdersSplitBySupplier(browserInstance);

            // Test Case: 2.2 Verify the order number is displayed on top of the table                                                                                                                                                        2.1 The list is split per supplier   
            // Test Output: 2.2 The Order number is displayed on top of the table 
            Thread.Sleep(3000);
            ordersActions.VerifyOrderDetailsOrderNumber(browserInstance);

            // Test Case: 2.3 Verify that item sku code, name, brand, pack size, price ,invoice no, qty and total columns are displayed and pre-populated with values   
            // Test Output: 2.3 The following items are displayed sku code, name, brand, pack size, price ,invoice no, qty and total columns are displayed and pre-populated with values 
            Thread.Sleep(3000);
            ordersActions.VerifyOrderDetailColumns(browserInstance);

            // Test Case: 2.4 Verify that the re-order button is displayed                    
            // Test Output: 2.4 The re-order button is displayed 
            // Test Case: 2.5 Verify that the unconfirmed order button is displayed 
            // Test Output: 2.5 The unconfirmed order button is displayed 
            // Test Case: 2.6 Verify that the back to orders button is displayed  
            // Test Output: 2.6 The back to orders button is displayed  
            // Test Case: 2.7 Verify that the view invoice button is displayed 
            // Test Output: 2.7 The invoice button is displayed 
            Thread.Sleep(3000);
            ordersActions.VerifyOrderDetailButtons(browserInstance);
        }

        /// <summary>
        /// TEST: RE- ORDER
        /// Test Case ID: 39_FRS_Ref_5.4.1
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1. Click on the order re-order button on the view order details screen, but please note that invoking this process the following events happen    
        /// 1. Invoking REORDER will follow the following steps:
        /// a. Add all SKU’s in the order to the correct catalogue baskets"
        /// b. Add the quantity of the order in the basket
        /// c. If a SKU is not available in the catalogue, ignore the SKU
        /// d. If a SKU already exists in the basket, append the quantity
        /// e. Order basket"
        /// TEST OUTPUT:
        /// 1. The result will be based on the outcome of the process
        /// a. SKU will be added to the correct catalogue basket
        /// b. The quantity of the order is added in the basket
        /// c. If a SKU is not available in the catalogue, ignore the SKU
        /// d. The Basket appends quantity if SJU already exist
        /// e. Order basket is invoked
        /// </summary>
        [Test, Description("_07_ReOrder"), Category("Orders"), Repeat(1)]
        public void _07_eOrder()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Thread.Sleep(3000);
            Helpers.Instance.PlaceConfirmedOrder(browserInstance);
            Thread.Sleep(3000);
            ordersActions.VerifyOrdersBlockClick(browserInstance);
            Thread.Sleep(30000);
            ordersActions.VerifyOrderNumberClick(browserInstance);

            // 1. Click on the order re-order button on the view order details screen, but please note that invoking this process the following events happen
            // 1. The result will be based on the outcome of the process
            Thread.Sleep(3000);
            ordersActions.VerifyReOrderButton(browserInstance);
        }

        /// <summary>
        /// TEST: UNCONFIRMED ORDERS
        /// Test Case ID: 39_FRS_Ref_5.4.1
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1. Click on the unconfirmed orders button  
        /// 2. Follow the order process 
        /// 3. confirm all unconfirmed orders
        /// TEST OUTPUT:
        /// 1. The unconfirmed order is displayed if they are unconfirmed orders       
        /// 2. The order is confirmed   
        /// 3. The Unconfirmed order button should be inactive if there are no more orders to confirm
        /// </summary>
        [Test, Description("_08_UnconfirmedOrders"), Category("Orders"), Repeat(1)]
        public void _08_UnconfirmedOrders()
        {
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));

            // Place Unconfirmed Order
            Helpers.Instance.PlaceUnConfirmedOrder(browserInstance);
            ordersActions.VerifyOrdersBlockClick(browserInstance);
            ordersActions.VerifyOrderNumberClick(browserInstance);

            // Test Case: 1. Click on the unconfirmed orders button 
            // Test Output: 1. The unconfirmed order is displayed if they are unconfirmed orders
            ordersActions.VerifyUnconfirmOrderButton(browserInstance);

            Thread.Sleep(3000);
            Helpers.Instance.ConfirmUnConfirmedOrder(browserInstance);
        }

        /// <summary>
        /// TEST: VIEW INVOICES
        /// Test Case ID: 40_FRS_Ref_5.4.1
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
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
        [Test, Description("_09_ViewInvoices"), Category("Orders"), Repeat(1)]
        public void _09_ViewInvoices()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            Helpers.Instance.PlaceConfirmedOrder(browserInstance);
            ordersActions.VerifyOrdersBlockClick(browserInstance);
            ordersActions.VerifyOrderNumberClick(browserInstance);

            // Test Case: 1. Click on view invoices button on the view orders details screen
            // Test Output: 1. The invoices are displayed in a tabular format 
            ordersActions.VerifyViewInvoicesButtonClick(browserInstance);

            // Test Case: 2. Verify the caption of the order invoice  page is displayed as invoices 
            // Test Output: 2. The caption of the order invoice page is displayed as invoices 
            ordersActions.VerifyInvoiceHeader(browserInstance);

            // Test Case: 3. Verify that the invoice for order number is displayed
            // Test Output: 3. The invoice order number is displayed
            ordersActions.VerifyInvoiceDetailOrderNumber(browserInstance);

            // Test Case: 4. The  invoice number, supplier, invoice date , value are pre-populated  and displayed  
            // Test Output: 4. The  invoice number, supplier, invoice date , value are pre-populated  and displayed 
            // Test Case: 5. Verify that  the total number of value price is correct
            // Test Output: 5. The total number value price is displayed and correct
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 108: _09_ViewInvoices -> Test step cannot be tested since we cannot trigger instant invoices in our test cases.'
                                    ' 4.The  invoice number, supplier, invoice date , value are pre-populated  and displayed ' - Test case need to be manually tested.", LogWriter.eLogType.Error);

            // Test Case: 6. Test Case: Verify that the back to orders button is displayed  
            // Test Output: 6. The back to orders button is displayed
            // Test Case: 7. Click on the back to orders button   
            // Test Output: 7. The Orders screen is displayed   
            ordersActions.VerifyInvoiceBackToOrdersButton(browserInstance);
        }

        /// <summary>
        /// TEST: LOGGING DISCREPANCIES
        /// Test Case ID: 41_FRS_Ref_5.4.4
        /// Category: Orders
        /// Feature: Orders
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1.Go to view invoice detail screen   
        /// 2. Click on the <log discrepancy> button       
        /// 3. Verify that the order discrepancy screen is displayed with the following                            
        /// 3.1 Verify that the order number is displayed     
        /// 3.2  Verify that the item code, name, brand, pack size, price, qty, value and discrepancy columns are displayed and pre-populated with values and also has a discrepancy field which is a drop down  
        /// 3.3 Verify that the submit button is displayed on the screen   
        /// 4. Select the discrepancy field  
        /// 5.Click on the drop down arrow on the discrepancy field 
        /// 6. User select a discrepancy reason that this applicable       
        /// 7. Click on the submit button                                                                            
        /// TEST OUTPUT:
        /// 1.The view invoice detail screen is displayed      
        /// 2. The order discrepancy screen is displayed 
        /// 3.                                                                                                                                                                                                                   3.1 The order number is displayed   
        /// 3.2  The  item code, name, brand, pack size, price, qty, value , discrepancy columns are displayed and pre-populated with values and also discrepancy field  which is a drop down   
        /// 3.3 The submit button is displayed on the screen   
        /// 4. The discrepancy field is populated    
        /// 5. The list of order discrepancies are displayed on the drop down  
        /// 6. The discrepancy reason is displayed 
        /// 7. The discrepancy is successfully logged
        /// </summary>
        [Test, Description("_10_LoggingDiscrepancies"), Category("Orders"), Repeat(1)]
        public void _10_LoggingDiscrepancies()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();

            LogWriter.Instance.Log(@"TESTCASE:ISSUE 109: _10_LoggingDiscrepancies -> Test step cannot be tested since we cannot trigger instant invoices and we do not have the log discrepancy button in the view invoices detail screen.' 
                                    '2. Click on the <log discrepancy> button' - Test case need to be manually tested.", LogWriter.eLogType.Error);
        }

        /// <summary>
        /// TEST: SEARCHING ORDERS
        /// Test Case ID: 41_FRS_Ref_5.1.9
        /// Category: Orders
        /// Feature: Search
        /// Pre-Condition: None
        /// Environment: Orders landing page
        /// TEST STEPS:
        /// 1.Go to view invoice detail screen   
        /// 2. Click on the <search> button
        /// "3. Searching online orders
        /// 3.1 Select the date range to less than 30 days in the past"
        /// 3.2  Select the search button
        /// "4. Searching offline (historic) orders
        /// 4.1 Select the date range to more than 6 months old (please ensure BOPMan contains older orders)"
        /// 4.2 Select the search button
        /// TEST OUTPUT:
        /// 1.The view invoice detail screen is displayed      
        /// 2. The Search Order popup appears
        /// 3.                                                                                                                                                                                                                   3.1 The date range is set
        /// 3.2  The Orders grid is completed with matcing items
        /// 4.1 The date range is set
        /// 3.2  The Orders grid is completed with matcing items
        /// </summary>
        [Test, Description("_11_SearchingOrders"), Category("Orders"), Repeat(1)]
        public void _11_SearchingOrders()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IOrdersActions ordersActions = container.Resolve<Interfaces.IOrdersActions>();
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 110: _11_SearchingOrders -> Test step cannot be tested since we cannot trigger instant invoices and we do not have search functionality to search invoices.' 
                                    '2. Click on the <search> button' - Test case need to be manually tested.", LogWriter.eLogType.Error);

        }
    }
}
