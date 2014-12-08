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

            addOrders(1);
        }

        private void addOrders(int supplierIndex)
        {
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

            // 1. Click on the basket block at the bottom of the screen  
            // 1. The basket page is displayed with catalogues in grid view
            basketActions.ClickBasketBlock(browserInstance);

            // 2. Verify that order from a specific supplier functions as expected  
            // 2.1 Select a specific supplier by clicking  on the checkbox 
            // 2.1 The checkbox on the supplier is clicked    
            //LogWriter.Instance.Log("TESTCASE:_01_BasketGridView -> '2. Verify that order from a specific supplier functions as expected'. No checkbox on screen", LogWriter.eLogType.Error);
            //LogWriter.Instance.Log("TESTCASE:_01_BasketGridView -> Clicking on <Order Now> button instead", LogWriter.eLogType.Error);         
            // 2.1 The confirm order pop-up is displayed  
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
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            addOrders(1);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            addOrders(2);
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
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
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
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
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
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
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
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
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
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
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
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
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
        /// 3.                                                                                                                                                                                                                         3.1 The total is correct 
        /// </summary>
        [Test, Description("_08_BasketAddingAndRemovingProductQuantity"), Category("Basket"), Repeat(1)]
        public void _08_BasketAddingAndRemovingProductQuantity()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }
    }
}
