using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Threading;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Favourites
{
    [TestFixture, Description("Favourites"), Category("Favourites")]
    public class Favourites
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

            container.RegisterType<Interfaces.IFavouritesActions, Tests.Favourites.FavouritesActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());

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
        /// TEST: FAVOURITES IN GRID VIEW
        /// Test Case ID: 29_FRS_Ref_5.3.2
        /// Category: Favourites
        /// Feature: Favourites
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1.Click on the favourites block on the bottom of the screen 
        /// 2.Verify that delete orders from a specific supplier functions as expected                                                              
        /// 2.1 Select a specific supplier by clicking  on the checkbox    
        /// 2.2 Click on the delete icon    
        /// 3.Verify that clear all from all basket functions as expected                                                      
        /// 3.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers 
        /// 3.2 Click on the clear <all> button  
        /// 4. Verify that the list view button is displayed
        /// TEST OUTPUT:
        /// 1. The favourites page is displayed with catalogues in grid view    
        /// 2.                                                                                                                                                                                                                   
        /// 2.1 The checkbox on the supplier is clicked   
        /// 2.2 The order is deleted from that supplier 
        /// 3.                                                                                                                                                                                                                                                                                                                                                                                                                   
        /// 3.1 The checkboxes for different suppliers are selected
        /// 3.2 This clear all selected catalogue basket
        /// 4. The list view button is displayed 
        /// </summary>
        [Test, Description("_01_FavouritesInGridView"), Category("Favourites"), Repeat(1)]
        public void _01_FavouritesInGridView()
        {
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();

            catalogueActions.AddFavouriteProduct(browserInstance);

            // Test Case: 1. Click on the favourites block on the bottom of the screen 
            // Test Output: 1. The favourites page is displayed with catalogues in grid view
            favActions.VerifyFavouriteIconClick(browserInstance);

            // Test Case: 2. Verify that delete orders from a specific supplier functions as expected                                                              
            // Test Case: 2.1 Select a specific supplier by clicking  on the checkbox    
            // Test Output: 2.1 The checkbox on the supplier is clicked 
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 114: _01_FavouritesInGridView -> Test step we do not have the checkbox to select a specific supplier.'
                                    '2.1 Select a specific supplier by clicking  on the checkbox ' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 2.2 Click on the delete icon    
            // Test Output: 2.2 The order is deleted from that supplier
            favActions.VerifyDeleteIcon(browserInstance);

            // Test Case: 3. Verify that clear all from all basket functions as expected  
            catalogueActions.AddFavouriteProduct(browserInstance);
            favActions.VerifyFavouriteIconClick(browserInstance);

            // Test Case: 3.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers 
            // Test Output: 3.1 The checkboxes for different suppliers are selected
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 114: _01_FavouritesInGridView -> Test step we do not have the checkbox to select a specific supplier. '. 
                                    '3.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers ' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 3.2 Click on the clear <all> button  
            // Test Output: 3.2 This clear all selected catalogue basket
            favActions.VerifyClearAllButtonClick(browserInstance);

            // Test Case: 4. Verify that the list view button is displayed
            // Test Output: 4. The list view button is displayed
            favActions.VerifyListViewButton(browserInstance);
        }

        /// <summary>
        /// TEST: FAVOURITES IN LIST VIEW
        /// Test Case ID: 30_FRS_Ref_5.3.2
        /// Category: Favourites
        /// Feature: Favourites
        /// Pre-Condition: None
        /// Environment: Favourites landing page
        /// TEST STEPS:
        /// 1 Click on the list view button on the screen   
        /// 2. Click on the subcategory product  
        /// 3.Verify that delete orders from a specific supplier functions as expected                                                              
        /// 3.1 Select a specific supplier by clicking  on the checkbox 
        /// 4.Verify that clear all from all basket functions as expected                                                              
        /// 4.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers    
        /// 4.2 Click on the clear <all> button    
        /// 5. Verify that the grid view button is displayed  
        /// TEST OUTPUT:
        /// 1 The display mode switches to list view where items are displayed in a tabular format   
        /// 2. product are displayed  
        /// 3                                                                                                                                                                                                            
        /// 3.1The checkbox on the supplier is clicked   
        /// 4.                                                                                                                                                                                                                                                                                                                                                                                                                   
        /// 4.1 The checkboxes for different suppliers are selected  
        /// 4.2 This clear all selected catalogue basket   
        /// 5. The list view button is displayed      
        /// </summary>
        [Test, Description("_02_FavouritesInListView"), Category("Favourites"), Repeat(1)]
        public void _02_FavouritesInListView()
        {
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();

            catalogueActions.AddFavouriteProduct(browserInstance);
            favActions.VerifyFavouriteIconClick(browserInstance);

            // Test Case: 1 Click on the list view button on the screen
            // Test Output: 1 The display mode switches to list view where items are displayed in a tabular format 
            favActions.VerifyListViewClick(browserInstance);

            // Test Case: 2. Click on the subcategory product
            // Test Output: 2. product are displayed 
            favActions.VerifyListViewProductClick(browserInstance);

            // Test Case: 3.Verify that delete orders from a specific supplier functions as expected
            // Test Case:  3.1 Select a specific supplier by clicking  on the checkbox 
            // Test Output: 3.1 The checkbox on the supplier is clicked
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 114: _02_FavouritesInListView -> Test step we do not have the checkbox to select a specific supplier. '. 
                                    '3.1 Select a specific supplier by clicking  on the checkbox ' - Test case to be updated.", LogWriter.eLogType.Error);
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 115: _02_FavouritesInListView -> Test step is not complete.' 
                                    '3.1 Select a specific supplier by clicking  on the checkbox ' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 4.Verify that clear all from all basket functions as expected  
            // Test Case: 4.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers 
            // Test Output: 4.1 The checkboxes for different suppliers are selected  
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 114: _02_FavouritesInListView -> Test step we do not have the checkbox to select a specific supplier. '. 
                                    '4.1 Select more than one  supplier by clicking  on the checkboxes of different suppliers  ' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 4.2 Click on the clear <all> button  
            // Test Output: 4.2 This clear all selected catalogue basket 
            favActions.VerifyClearAllButtonClick(browserInstance);

            // Test Case: 5. Verify that the grid view button is displayed
            // Test Output: 5. The list view button is displayed
            favActions.VerifyGridViewButton(browserInstance);
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 116:_02_FavouritesInListView -> Test step the expected output is not correct.' 
                                    '5. Verify that the grid view button is displayed' - Test case to be updated.", LogWriter.eLogType.Error);
        }

        /// <summary>
        /// TEST: FAVOURITES DETAIL GRID VIEW
        /// Test Case ID: 31_FRS_Ref_5.3.2
        /// Category: Favourites
        /// Feature: Favourites
        /// Pre-Condition: None
        /// Environment: Favourites landing page
        /// TEST STEPS:
        /// 1. Click on the <grid view> button      
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
        /// 1. Favourites  items are displayed  are displayed in a grid view         
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
        [Test, Description("_03_FavouritesDetailGridView"), Category("Favourites"), Repeat(1)]
        public void _03_FavouritesDetailGridView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();

            catalogueActions.AddFavouriteProduct(browserInstance);
            favActions.VerifyFavouriteIconClick(browserInstance);

            // Test Case: 1. Click on the <grid view> button 
            // Test Output: 1. Favourites  items are displayed  are displayed in a grid view 
            favActions.VerifyGridViewButtonClick(browserInstance);

            // Test Case: 2. Verify that on the list view the is a grid view button that will allow to switch back
            // Test Output: 2. The grid view button is displayed
            favActions.VerifyGridViewButtonOnListView(browserInstance);

            // Test Case: 3. Verify that order from a specific supplier functions as expected  
            // Test Case: 3.1 Select a specific supplier by clicking  on any record from the table  
            favActions.VerifyListViewProductClick(browserInstance);

            // Test Case: 3.2 Click on the order now 
            // Test Output: 3.2 The confirm order pop-up is displayed
            favActions.VerifyConfirmOrderPopup(browserInstance);

            // Test Case: 4.Verify that delete orders from a specific supplier functions as expected 
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.modal-header > div > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.modal-header > div > button"));
            favActions.VerifyGridViewProductOrderDelete(browserInstance);

            // Test Case: 5.Verify that order all from basket  functions as expected                                                                                             
            // Test Case: 5.1 Select more than one  supplier by clicking  on multiple rows on the table  
            // Test Case: 5.2 Click on the order all button     
            // Test Output: 5.2 The confirm order pop-up is displayed  
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 118: _03_FavouritesDetailGridView -> Test step we do not have the order all button in the favourite tab and we do not have the ability to select more than supplier.' 
                                    '5.Verify that order all from basket  functions as expected ' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case 6.Verify that clear all from all basket functions as expected                                                               
            // Test Case 6.1 Select more than one  supplier by clicking  on multiple rows on the table    
            // Test Case 6.2 Click on the clear <all> button  
            // Test Output: 6.2 This clear all selected catalogue basket
            catalogueActions.AddFavouriteProduct(browserInstance);
            favActions.VerifyFavouriteIconClick(browserInstance);
            favActions.VerifyGridViewProductClick(browserInstance);
            favActions.VerifyGridViewClearAllButtonClick(browserInstance, catalogueActions);
        }

        /// <summary>
        /// TEST: FAVOURITES DETAIL LIST VIEW 
        /// Test Case ID: 32_FRS_Ref_5.3.2
        /// Category: Favourites
        /// Feature: Favourites
        /// Pre-Condition: None
        /// Environment: Favourites landing page
        /// TEST STEPS:
        /// 1. Click on the <list view> button         
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
        /// 1. Favourites items are displayed as tabular format     
        /// 2. The grid view button is displayed  
        /// 3.                                                                                                                                                                                                                  3.1 The record on the list view table is selected  
        /// 3.2 The confirm order pop-up is displayed  
        /// 4.                                                                                                                                                                                                                   4.1 The record on the list view table is selected 
        /// 4.2 The order is deleted from that supplier  
        /// 5.                                                                                                                                                                                                                 5.1 Multiple records on the list view table are selected
        /// 5.2 The confirm order pop-up is displayed   
        /// 6.                                                                                                                                                                                                                 6.1 Multiple records on the list view table are selected   
        /// 6.2 This clear all selected catalogue basket 
        /// </summary>
        [Test, Description("_04_FavouritesDetailListView"), Category("Favourites"), Repeat(1)]
        public void _04_FavouritesDetailListView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();

            catalogueActions.AddFavouriteProduct(browserInstance);
            favActions.VerifyFavouriteIconClick(browserInstance);

            // Test Case: 1. Click on the <list view> button 
            // Test Output: 1. Favourites items are displayed as tabular format
            favActions.VerifyListViewClick(browserInstance);

            // Test Case: 2. Verify that on the list view the is a grid view button that will allow to switch back  
            // Test Output: 2. The grid view button is displayed
            favActions.VerifyGridViewButton(browserInstance);

            // Test Case: 3. Verify that order from a specific supplier functions as expected                                                                                
            // Test Case: 3.1 Select a specific supplier by clicking  on any record from the table 
            // Test Case: 3.2 Click on the order now   
            // Test Output: 3.2 The confirm order pop-up is displayed
            favActions.VerifyListViewProductClick(browserInstance);
            favActions.VerifyConfirmOrderPopup(browserInstance);

            // Test Case: 4.Verify that delete orders from a specific supplier functions as expected                                                              
            // Test Case: 4.1  Select a specific supplier by clicking  on any record from the table       
            // Test Case: 4.2 Click on the delete icon  
            // Test Output: 4.2 The order is deleted from that supplier
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.modal-header > div > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.modal-header > div > button"));
            favActions.VerifyFavouriteIconClick(browserInstance);
            favActions.VerifyListViewClick(browserInstance);
            favActions.VerifyListViewProductOrderDelete(browserInstance);

            // Test Case: 5.Verify that order all from basket  functions as expected                                                                                             
            // Test Case: 5.1 Select more than one  supplier by clicking  on multiple rows on the table
            // Test Case: 5.2 Click on the order all button   
            // Test Output: 5.2 The confirm order pop-up is displayed  
            LogWriter.Instance.Log(@"ISSUE 118: TESTCASE:_04_FavouritesDetailListView -> Test step we do not have the order all button in the favourite tab and we do not have the ability to select more than supplier. '. 
                                    '5.Verify that order all from basket  functions as expected ' - Test case to be updated.", LogWriter.eLogType.Error);

            // Test Case: 6. Verify that clear all from all basket functions as expected 
            catalogueActions.AddFavouriteProduct(browserInstance);
            favActions.VerifyFavouriteIconClick(browserInstance);
            favActions.VerifyListViewClearAllButtonClick(browserInstance, catalogueActions);
        }

        /// <summary>
        /// TEST: FAVOURITES VIEW ITEM DETAIL
        /// Test Case ID: 33_FRS_Ref_5.3.2
        /// Category: Favourites
        /// Feature: Favourites
        /// Pre-Condition: None
        /// Environment: Favourites landing page
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
        [Test, Description("_05_FavouritesViewItemDetail"), Category("Favourites"), Repeat(1)]
        public void _05_FavouritesViewItemDetail()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();

            // Test Case: 1. Click on the product
            // Test Output: 1. The product view screen is displayed
            favActions.ClickFavouriteProduct(browserInstance, catalogueActions);

            // Test Case: 2. Verify that the product view screen  
            favActions.VerifyProductViewScreen(browserInstance);
        }

        /// <summary>
        /// TEST: FAVOURITES ADD ITEM TO FAVOURITES
        /// Test Case ID: 33_FRS_Ref_5.3.2
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
        [Test, Description("_06_FavouritesAddItemToFavourites"), Category("Basket"), Repeat(1)]
        public void _06_FavouritesAddItemToFavourites()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();

            // Test Case: 1. Click on the product
            // Test Output: 1. The product view screen is displayed  
            var productDescription = favActions.ClickBasketProduct(browserInstance, basketActions);

            // Test Case: 2. On the product view screen click on the favourites icon which is represented by a star and save
            // Test Output: 2. The product is saved to favourites
            favActions.AddBasketProductToFavourites(browserInstance);

            // Test Case: 3. Verify step 2 by selecting the favourites tab, to see if the recently added product is displayed 
            // Test Output: 3.The recently added product is displayed in the favourites 
            favActions.VerifyAddFavouriteProduct(browserInstance, productDescription);
        }

        /// <summary>
        /// TEST: FAVOURITES  ADDING AND REMOVING PRODUCT QUANTITY
        /// Test Case ID: 33_FRS_Ref_5.3.2
        /// Category: Basket
        /// Feature: Basket
        /// Pre-Condition: None
        /// Environment: Basket landing page
        /// TEST STEPS:
        /// 1. Click on the product    
        /// 2. On the product view screen  click on the - sign for removing and + adding quantity and save  
        /// 3.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed                   
        /// 2.  The quantity addition button are working as expected   
        /// 3.                                                                                                                                                                                                                         
        /// 3.1 The total is correct    
        /// </summary>
        [Test, Description("_07_FavouritesAddingAndRemovingProductQuantity"), Category("Basket"), Repeat(1)]
        public void _07_FavouritesAddingAndRemovingProductQuantity()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            Interfaces.ICataloguesActions catalogActions = container.Resolve<Interfaces.ICataloguesActions>();

            // Test Case: 1. Click on the product
            // Test Output: 1. The product view screen is displayed  
            var productDescription = favActions.ClickBasketProduct(browserInstance, basketActions);

            // 2. On the product view screen  click on the - sign for removing and + adding quantity and save 
            // 2.  The quantity addition button are working as expected 

            catalogActions.VerifyProductQuantityClick(browserInstance);

            // 3.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct

            catalogActions.VerifyProductTotal(browserInstance);
        }
    }
}
