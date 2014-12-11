using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
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
    [TestFixture, Description("Basket"), Category("Basket")]
    public class Basket
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
        /// TEST: BASKET  GRID VIEW
        /// Test Case ID: 23_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1.Click on the basket block at the bottom of the screen  
        /// 2. Verify that order from a specific supplier functions as expected                                                                                
        /// 2.1 Select a specific supplier by clicking  on the checkbox 
        /// 2.2 Click on the order now
        /// 3.Verify that delete orders from a specific supplier functions as expected                                                              
        /// 3.1 Select a specific supplier by clicking  on the checkbox   
        /// 3.2 Click on the delete icon
        /// 4.Verify that order all from basket  functions as expected                                                                                             
        /// 4.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers 
        /// 5.Verify that clear all from all basket functions as expected                                                      
        /// 5.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers    
        /// 5.2 Click on the clear <all> button  
        /// 6. Verify that the list view button is displayed
        /// TEST OUTPUT:
        /// 1. The basket page is displayed with catalogues in grid view
        /// 2.                                                                                                                                                                                                                  
        /// 2.1 The checkbox on the supplier is clicked    
        /// 2.2 The confirm order pop-up is displayed  
        /// 3.1 The checkbox on the supplier is clicked    
        /// 3.2 The order is deleted from that supplier
        /// 4.1 The checkboxes for different suppliers are selected    
        /// 5.                                                                                                                                                                                                                 
        /// 5.1 The checkboxes for different suppliers are selected      
        /// 5.2 This clear all selected catalogue basket   
        /// 6. The list view button is displayed   
        /// </summary>
        [Test, Description("_01_BasketGridView"), Category("Basket"), Repeat(1)]
        public void _01_BasketGridView()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);
            Thread.Sleep(3000);
            Helpers.Instance.AddOrders(browserInstance, 1);

            // 1. Click on the basket block at the bottom of the screen  
            // 1. The basket page is displayed with catalogues in grid view
            basketActions.ClickBasketBlock(browserInstance);

            // 2. Verify that order from a specific supplier functions as expected  
            // 2.1 Select a specific supplier by clicking  on the checkbox 
            // 2.1 The checkbox on the supplier is clicked    
            //LogWriter.Instance.Log("TESTCASE:_01_BasketGridView -> '2. Verify that order from a specific supplier functions as expected'. No checkbox on screen", LogWriter.eLogType.Error);
            //LogWriter.Instance.Log("TESTCASE:_01_BasketGridView -> Clicking on <Order Now> button instead", LogWriter.eLogType.Error);         
            // 2.1 The confirm order pop-up is displayed  
            basketActions.ClickOrderNowButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"));
            basketActions.CheckConfirmPopup(browserInstance);

            basketActions.ClickConfirmOrderPopupClose(browserInstance);

            // 3.Verify that delete orders from a specific supplier functions as expected                
            // 3.1 Select a specific supplier by clicking  on the checkbox   
            // 3.1 The checkbox on the supplier is clicked    
            //LogWriter.Instance.Log("TESTCASE:_01_BasketGridView -> '3.Verify that delete orders from a specific supplier functions as expected' -> test process incorrect. This process will open the order now screen.", LogWriter.eLogType.Error);

            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "3 Items");
            // 3.2 Click on the delete icon
            basketActions.ClickOrderDeleteButton(browserInstance);


            // 4.Verify that order all from basket  functions as expected                                
            // 4.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers 
            // 4.1 The checkboxes for different suppliers are selected  

            // 5.Verify that clear all from all basket functions as expected                             
            // 5.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers  
            // 5.1 The checkboxes for different suppliers are selected  

            // 5.2 Click on the clear <all> button  
            Helpers.Instance.AddOrders(browserInstance, 1);
            Helpers.Instance.AddOrders(browserInstance, 2);
            basketActions.ClickBasketBlock(browserInstance);

            // 5.2 This clear all selected catalogue basket   
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "6 Items");
            basketActions.ClickClearAllButton(browserInstance);
            // 6. Verify that the list view button is displayed
            // 6. The list view button is displayed  
            basketActions.CheckListViewButtonExists(browserInstance);

        }

        /// <summary>
        /// TEST: BASKET CONFIRM ORDER
        /// Test Case ID: 24_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1 Select a specific supplier by clicking  on the checkbox  
        /// 2. Click on the order now  
        /// 3. Verify the following the following for the confirm order caption screen                              
        /// 3.1 Make  sure that the message box form caption is written order now   
        /// 3.2 Make sure that the is a message asking the user about confirming the order with a yes or no 
        /// 3.3 Make sure that the total number of items and total price of items to be ordered are displayed   
        /// TEST OUTPUT:
        /// 1 The checkbox on the supplier is clicked         
        /// 2. The confirm order pop-up is displayed  
        /// 3.                                                                                                                                                                                                                 
        /// 3.1 The message box form caption is displayed on top of the pop-up as order now 
        /// 3.2 The message asking the user about confirm the order is displayed with a yes and no buttons  
        /// 3.3  The total number of items and total price of items are displayed on the pop-up 
        /// </summary>
        [Test, Description("_02_BasketConfirmOrder"), Category("Basket"), Repeat(1)]
        public void _02_BasketConfirmOrder()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);

            Helpers.Instance.AddOrders(browserInstance, 1);
            var noItems = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");
            var itemsPrice = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketValue.ng-binding");
            basketActions.ClickBasketBlock(browserInstance);
            basketActions.ClickOrderNowButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"));
            basketActions.CheckConfirmPopup(browserInstance);

            // 3. Verify the following the following for the confirm order caption screen                              
            // 3.1 Make  sure that the message box form caption is written order now 
            // 3.1 The message box form caption is displayed on top of the pop-up as order now 
            //Done in previous step

            // 3.2 Make sure that the is a message asking the user about confirming the order with a yes or no 
            basketActions.VerifyConfirmPopup(browserInstance);

            basketActions.VerifyConfirmPopupValues(browserInstance, noItems);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            Thread.Sleep(3000);
        }

        /// <summary>
        /// TEST: BASKET  LIST VIEW
        /// Test Case ID: 25_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1. Click on the <list view> button on the basket tab
        /// 2. Verify that on the list view the is a grid view button that will allow to switch back   
        /// 3. Verify that order from a specific supplier functions as expected                                                                                
        /// 3.1 Select a specific supplier by clicking  on any record from the table   
        /// 3.2 Click on the order now   
        /// 4.Verify that delete orders from a specific supplier functions as expected                                                              
        /// 4.1  Select a specific supplier by clicking  on any record from the table
        /// 4.2 Click on the delete icon   
        /// 5.Verify that order all from basket  functions as expected                                                                                             
        /// 5.1 Select more than one  supplier by clicking  on multiple rows on the table
        /// 5.2 Click on the order all button  
        /// 6.Verify that clear all from all basket functions as expected                                                               
        /// 6.1 Select more than one  supplier by clicking  on multiple rows on the table     
        /// 6.2 Click on the clear <all> button                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        /// TEST OUTPUT:
        /// 1. Basket items are displayed in a tabular format as a list    
        /// 2. The grid view button is displayed  
        /// 3.                                                                                                                                                                                                                  
        /// 3.1 The record on the list view table is selected   
        /// 3.2 The confirm order pop-up is displayed   
        /// 4.                                                                                                                                                                                                                   
        /// 4.1 The record on the list view table is selected   
        /// 4.2 The order is deleted from that supplier  
        /// 5.                                                                                                                                                                                                                 
        /// 5.1 Multiple records on the list view table are selected     
        /// 5.2 The confirm order pop-up is displayed  
        /// 6.                                                                                                                                                                                                                 
        /// 6.1 Multiple records on the list view table are selected  
        /// 6.2 This clear all selected catalogue basket      
        /// </summary>
        [Test, Description("_03_BasketListView"), Category("Basket"), Repeat(1)]
        public void _03_BasketListView()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);

            // 1. Click on the <list view> button on the basket tab
            /// 1. Basket items are displayed in a tabular format as a list 
            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);
            //GRID
            //Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button"));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1)"));
            //Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul");

            basketActions.VerifyListView(browserInstance);

            basketActions.VerifyListViewActions(browserInstance);

            // 3.2 Click on the order now   
            LogWriter.Instance.Log("ISSUE 41: TESTCASE: _03_BasketListView -> '3.2 Click on the order now' -> Test case step missed -> 'Navigate back to List view'", LogWriter.eLogType.Error);
            basketActions.ClickBasketBlock(browserInstance);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1)"));

            //Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr:nth-child(1) > td:nth-child(4) > div.orderNow > button"));
            /// 3.2 The confirm order pop-up is displayed   
            /// TestProj.Tests.Basket.Basket._03_BasketListView:
            basketActions.ClickOrderNowButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(4) > div.orderNow > button"));
            basketActions.CheckConfirmPopup(browserInstance);
            basketActions.ClickConfirmOrderPopupClose(browserInstance);

            // 4.Verify that delete orders from a specific supplier functions as expected               
            // 4.1  Select a specific supplier by clicking  on any record from the table
            /// 4.1 The record on the list view table is selected   
            LogWriter.Instance.Log("ISSUE 42: TESTCASE: _03_BasketListView -> '4.Verify that delete orders from a specific supplier functions as expected' -> Same as 3?", LogWriter.eLogType.Error);

            basketActions.DeleteOrderFromList(browserInstance);

            // 5.Verify that order all from basket  functions as expected                               
            // 5.1 Select more than one  supplier by clicking  on multiple rows on the table
            /// 5.1 Multiple records on the list view table are selected   
            LogWriter.Instance.Log("ISSUE 43: TESTCASE: _03_BasketListView -> '5.1 Select more than one  supplier by clicking  on multiple rows on the table' -> Test case incorrect. Cannot click on more than one item at a time.", LogWriter.eLogType.Error);
            basketActions.CheckOrderAllFunction(browserInstance, basketActions);
            /// 5.2 The confirm order pop-up is displayed  
            basketActions.CheckConfirmPopup(browserInstance);
            basketActions.ClickConfirmOrderPopupClose(browserInstance);

            // 6.Verify that clear all from all basket functions as expected                            
            // 6.1 Select more than one  supplier by clicking  on multiple rows on the table     
            LogWriter.Instance.Log("ISSUE 44: TESTCASE: _03_BasketListView -> '6.1 Select more than one  supplier by clicking  on multiple rows on the table' -> Test case incorrect. Cannot click on more than one item at a time.", LogWriter.eLogType.Error);
            basketActions.CheckClearAllFunction(browserInstance);
            
        }

        /// <summary>
        /// TEST: BASKET DETAIL GRID VIEW 
        /// Test Case ID: 26_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1.Click on the <grid> view button       
        /// 2.Click on the selected subcategory you wish to view products for                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        /// 3  Verify the following on the product view                                                                                        
        /// 3.1 Verify that the product icon is displayed 
        /// 3.2 Verify that the product price is displayed      
        /// 3.3 Verify that the <clear button > button is available        
        /// 3.4 Verify that the product description is displayed    
        /// 3.5 Verify that the list view button is displayed when user is on grid view mode   
        /// 4. Verify that the order all button is displayed 
        /// 5. Verify that the clear all button is displayed  
        /// TEST OUTPUT:
        /// 1. The application switches to grid view mode     
        /// 2. The selected subcategory products are displayed                                                                                                               
        /// 3                                                                                                                                                                                                             
        /// 3.1 The product icon is displayed     
        /// 3.2 The product price is displayed 
        /// 3.3 The clear  button is displayed   
        /// 3.4 The product description is displayed  
        /// 3.5 The list view button is displayed   
        /// 4. The order all button is displayed    
        /// 5. The Clear all button is displayed      
        /// </summary>
        [Test, Description("_04_BasketDetailGridView"), Category("Basket"), Repeat(1)]
        public void _04_BasketDetailGridView()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);

            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);
            //Go to detail view
            LogWriter.Instance.Log("ISSUE 45: TESTCASE: _04_BasketDetailGridView -> Step missed -> Click on a supplier/wholesaler", LogWriter.eLogType.Error);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemView"));
            Thread.Sleep(500);
            // 1.Click on the <grid> view button       
            /// 1. The application switches to grid view mode    
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button"));
            basketActions.CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div");

            LogWriter.Instance.Log("ISSUE 46: TESTCASE: _04_BasketDetailGridView -> Test case unclear - '2.Click on the selected subcategory you wish to view products for'", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("ISSUE 47: TESTCASE: _04_BasketDetailGridView -> Test case unclear - 'There are no sub categories here, only products previously added to the basket'", LogWriter.eLogType.Error);
            // 2.Click on the selected subcategory you wish to view products for               
            /// 2. The selected subcategory products are displayed     
            //Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));
            basketActions.CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div");
            // basketActions.CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div");

            // 3  Verify the following on the product view                                     
            basketActions.VerifyProductView(browserInstance);

            basketActions.VerifyButtons(browserInstance);
        }

        /// <summary>
        /// TEST: BASKET DETAIL LIST VIEW 
        /// Test Case ID: 27_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1. Click on the selected subcategory you wish to view products for   
        /// 2  Verify the following on the product view                                                                                        
        /// 2.1 Verify that the product icon is displayed   
        /// 2.2 Verify that the product price is displayed 
        /// 2.3 Verify that the <clear button > button is available   
        /// 2.4 Verify that the product description is displayed 
        /// 3. Verify that the grid view button is displayed when user is on grid view mode 
        ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              4. Verify that the order all button is displayed         
        /// 5. Verify that the clear all button is displayed    
        /// TEST OUTPUT:
        /// 1. The selected subcategory products are displayed        
        /// 2                                                                                                                                                                                                             
        /// 2.1 The product icon is displayed   
        /// 2.2 The product price is displayed  
        /// 2.3 The clear  button is displayed 
        /// 2.4 The product description is displayed   
        /// 3. The grid  view button is displayed     
        /// 4. The order all button is displayed  
        /// 5. The Clear all button is displayed    
        /// </summary>
        [Test, Description("_05_BasketDetailListView"), Category("Basket"), Repeat(1)]
        public void _05_BasketDetailListView()
        {
            // browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);

            LogWriter.Instance.Log("ISSUE 49: TESTCASE:_05_BasketDetailListView -> Test step missed '1.Click on the <list> view button '", LogWriter.eLogType.Error);
            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);

            //Go to detail view
            LogWriter.Instance.Log("ISSUE 50: TESTCASE: _05_BasketDetailListView -> Step missed -> Click on a supplier/wholesaler", LogWriter.eLogType.Error);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));
            Thread.Sleep(500);
            // 1. Click on the selected subcategory you wish to view products for   
            /// 1. The selected subcategory products are displayed        
            LogWriter.Instance.Log("ISSUE 51: TESTCASE: _05_BasketDetailListView -> Test case unclear - '2.Click on the selected subcategory you wish to view products for'", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("ISSUE 52: TESTCASE: _05_BasketDetailListView -> Test case unclear - 'There are no sub categories here, only products previously added to the basket'", LogWriter.eLogType.Error);
            LogWriter.Instance.Log("ISSUE 53: TESTCASE: _05_BasketDetailListView -> Test case missed - 'Click on the <list> view button'", LogWriter.eLogType.Error);

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button"));
            basketActions.CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div");
            basketActions.CheckElementExists(browserInstance, "#alertsView > table");

            // 2  Verify the following on the product view                                     
            // 2.1 Verify that the product icon is displayed   
            /// 2.1 The product icon is displayed   
            LogWriter.Instance.Log("ISSUE 54: TESTCASE: _05_BasketDetailListView -> Test case incorrect - '2.1 The product icon is displayed' -> No prosuct icon in list view", LogWriter.eLogType.Error);

            basketActions.VerifyDetailsValues(browserInstance, basketActions);

            // 3. Verify that the grid view button is displayed when user is on grid view mode 
            /// 3. The grid  view button is displayed     
            /// 4. The order all button is displayed  
            // 5. Verify that the clear all button is displayed    
            /// 5. The Clear all button is displayed    
            basketActions.VerifyButtons(browserInstance);

            basketActions.ClickPopupClose(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(7) > div > button");
        }



        /// <summary>
        /// TEST: BASKET VIEW ITEM DETAIL
        /// Test Case ID: 28_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1. Click on the product   
        /// 2. Verify that the product view screen                                                                                               
        /// 2.1 Verify that the product description is displayed   
        /// 2.2 Verify that product unit price is displayed  
        /// 2.3 Verify that the edit buttons are available for adding and removing products quantity   
        /// 2.4 Verify that the quantity field is displayed and not editable  
        /// 2.5 Verify that the total price field is displayed and not editable 
        /// 2.6  Verify that the favourite icon represented by a star with a plus sign  is displayed    
        /// 2.7 Verify that the save button is displayed    
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed 
        /// 2.                                                                                                                                                                                                                 
        /// 2.1 The product description is displayed
        /// 2.2 The product unit price is displayed   
        /// 2.3  The edit buttons are displayed  
        /// 2.4 The quantity field is displayed and is not editable  
        /// 2.5  The total price field is displayed and not editable
        /// 2.6  A star with a plus sign is displayed  
        /// 2.7 The save button is displayed
        /// </summary>
        [Test, Description("_06_BasketViewItemDetail"), Category("Basket"), Repeat(1)]
        public void _06_BasketViewItemDetail()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);

            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);
            //Click on supplier/wholesaler
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));

            // 1. Click on the product   
            /// 1. The product view screen is displayed 
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div > div.img"));

            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"), 30);

            // 2. Verify that the product view screen                                                   
            basketActions.VerifyPopupValues(browserInstance);

            basketActions.ClickPopupClose(browserInstance, "#product_modal > div > div > div.modal-header > div > button");

        }

        /// <summary>
        /// TEST: BASKET ADD ITEM TO FAVOURITES
        /// Test Case ID: 28_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1. Click on the product  
        /// 2. On the product view screen click on the favourites icon which is represented by a star and save
        /// 3. Verify step 2 by selecting the favourites tab, to see if the recently added product is displayed 
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed      
        /// 2. The product is saved to favourites  
        /// 3.The recently added product is displayed in the favourites 
        /// </summary>
        [Test, Description("_07_BasketAddItemToFavourites"), Category("Basket"), Repeat(1)]
        public void _07_BasketAddItemToFavourites()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearFavourites(browserInstance);
            Helpers.Instance.ClearBasket(browserInstance);
            Thread.Sleep(3000);
            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);
            //Click on supplier/wholesaler
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));

            string prodDescription = basketActions.ClickProduct(browserInstance);

            basketActions.TestFavButtonOnPopup(browserInstance);
            basketActions.CheckFavAdded(browserInstance, prodDescription);
        }

        /// <summary>
        /// TEST: BASKET  ADDING AND REMOVING PRODUCT QUANTITY
        /// Test Case ID: 28_FRS_Ref_5.3.1
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1. Click on the product
        /// 2. On the product view screen  click on the - sign for removing and + adding quantity and save   
        /// 3. verify the formula used for adding and removing product quantity                                                 
        /// 3.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed    
        /// 2.  The quantity addition button are working as expected 
        /// 3.                                                                                                                                                                                                                         
        /// 3.1 The total is correct 
        /// </summary>
        [Test, Description("_08_BasketAddingAndRemovingProductQuantity"), Category("Basket"), Repeat(1)]
        public void _08_BasketAddingAndRemovingProductQuantity()
        {
            //browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Helpers.Instance.ClearBasket(browserInstance);
            Thread.Sleep(3000);
            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);
            //Click on supplier/wholesaler
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));

            // 1. Click on the product
            /// 1. The product view screen is displayed    
            basketActions.ClickProduct(browserInstance);

            FluentAutomation.ElementProxy qtyBox = Helpers.Instance.GetProxy(browserInstance, "#itemQuantity");
            int qty;
            basketActions.TestAddRemoveButtons(browserInstance, out qtyBox, out qty);


            basketActions.VerifyFormula(browserInstance, qtyBox, qty);
        }


    }
}
