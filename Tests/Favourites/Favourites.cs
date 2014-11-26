using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            browserInstance = new Classes.Browser(Classes.Browser.eBrowser.Chrome);
            browserInstance.Config.ScreenshotPath(Properties.Settings.Default.ScreenshotPath);
            browserInstance.Instance.Wait(5);

            container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interfaces.IFavouritesActions, Tests.Favourites.FavouritesActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
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
        /// 2.Verify that delete orders from a specific supplier functions asexpected
        ///   2.1 Select a specific supplier by clicking  on the checbox
        ///   2.2 Click on the delete icon
        /// 3.Verify that clear all from all basket functions as expected
        ///   3.1 Select morethan one  supplier by clicking  on the checboxes of different suppliers
        ///   3.2 Click on the clear <all> button
        /// 4. Verify that the list view button is displayed
        /// TEST OUTPUT:
        /// 1. The favourites page is displayed with catalogues in grid view
        /// 2.
        ///   2.1 The checkbox on the supplier is clicked
        ///   2.2 The order is deleted from that supplier
        /// 3.
        ///   3.1 The checkboxes for different suppliers are selected
        ///   3.2 This clear all selected catalogue basket
        /// 4. The list view button is displayed
        /// </summary>
        [Test, Description("_01_FavouritesInGridView"), Category("Favourites"), Repeat(1)]
        public void _01_FavouritesInGridView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
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
        /// 3.Verify that delete orders from a specific supplier functions asexpected
        ///   3.1 Select a specific supplier by clicking  on the checbox
        ///   3.2 Click on the delete icon
        /// 4.Verify that clear all from all basket functions as expected
        ///   4.1 Select morethan one  supplier by clicking  on the checboxes of different suppliers
        ///   4.2 Click on the clear <all> button
        /// 5. Verify that the grid view button is displayed
        /// TEST OUTPUT:
        /// 1 The display mode switches to list view where items are displayed in a tabular format
        /// 2. product are displayed
        /// 3
        ///   3.1 The checkbox on the supplier is clicked
        ///   3.2 The order is deleted from that supplier
        /// 4.
        ///   4.1 The checkboxes for different suppliers are selected
        ///   4.2 This clear all selected catalogue basket
        /// 5. The list view button is displayed
        /// </summary>
        [Test, Description("_02_FavouritesInListView"), Category("Favourites"), Repeat(1)]
        public void _02_FavouritesInListView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
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
        /// 2. Verify that on the list view the is a grid view button that will alllow to switch back
        /// 3. Verify that order from a specific supplier functions as expected
        ///   3.1 Select a specific supplier by clicking  on any record from the table
        ///   3.2 Click on the order now
        /// 4.Verify that delete orders from a specific supplier functions asexpected
        ///   4.1  Select a specific supplier by clicking  on any record from the table
        ///   4.2 Click on the delete icon
        /// 5.Verify that order all from basket  functions as expected
        ///   5.1 Select morethan one  supplier by clicking  on mutiple rows on the table
        ///   5.2 Click on the order all button
        /// 6.Verify that clear all from all basket functions as expected
        ///   6.1 Select morethan one  supplier by clicking  on mutiple rows on the table
        ///   6.2 Click on the clear <all> button
        /// TEST OUTPUT:
        /// 1. Favourites  items are displayed  are displayed in a grid view
        /// 2. The grid view button is displayed
        /// 3.
        ///   3.1 The record on the list view table is selected
        ///   3.2 The confirm order pop-up is displayed
        /// 4.
        ///   4.1 The record on the list view table is selected
        ///   4.2 The order is deleted from that supplier
        /// 5.
        ///   5.1 Multiple records on the list view table are selected
        ///   5.2 The confirm order pop-up is displayed
        /// 6.
        ///   6.1 Multiple records on the list view table are selected
        ///   6.2 This clear all selected catalogue basket
        /// </summary>
        [Test, Description("_03_FavouritesDetailGridView"), Category("Favourites"), Repeat(1)]
        public void _03_FavouritesDetailGridView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
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
        /// 2. Verify that on the list view the is a grid view button that will alllow to switch back
        /// 3. Verify that order from a specific supplier functions as expected
        ///   3.1 Select a specific supplier by clicking  on any record from the table
        ///   3.2 Click on the order now
        /// 4.Verify that delete orders from a specific supplier functions asexpected
        ///   4.1  Select a specific supplier by clicking  on any record from the table
        ///   4.2 Click on the delete icon
        /// 5.Verify that order all from basket  functions as expected
        ///   5.1 Select morethan one  supplier by clicking  on mutiple rows on the table
        ///   5.2 Click on the order all button
        /// 6.Verify that clear all from all basket functions as expected
        ///   6.1 Select morethan one  supplier by clicking  on mutiple rows on the table
        ///   6.2 Click on the clear <all> button
        /// TEST OUTPUT:
        /// 1. Favourites items are displayed as tabular format
        /// 2. The grid view button is displayed
        /// 3.
        ///   3.1 The record on the list view table is selected
        ///   3.2 The confirm order pop-up is displayed
        /// 4.
        ///   4.1 The record on the list view table is selected
        ///   4.2 The order is deleted from that supplier
        /// 5.
        ///   5.1 Multiple records on the list view table are selected
        ///   5.2 The confirm order pop-up is displayed
        /// 6.
        ///   6.1 Multiple records on the list view table are selected
        ///   6.2 This clear all selected catalogue basket
        /// </summary>
        [Test, Description("_04_FavouritesDetailListView"), Category("Favourites"), Repeat(1)]
        public void _04_FavouritesDetailListView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
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
        ///   2.1 Verify that the product description is displayed
        ///   2.2 Verify that product unit price is displayed
        ///   2.3 Verify that the edit buttons are available for adding and removing products quantity
        ///   2.4 Verify that the quantity field is displayed and not editable
        ///   2.5 Verify that the total price field is displayed and not editable
        ///   2.6  Verify that the favourite icon represented by a star with a plus sign  is displayed
        ///   2.7 Verify that the save button is displayed
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed
        /// 2.
        ///   2.1 The product description is displayed
        ///   2.2 The product unit price is displayed
        ///   2.3  The edit buttons are displayed
        ///   2.4 The quantity field is displayed and is not editable
        ///   2.5  The total price field is displayed and not editable
        ///   2.6  A star with a plus sign is displayed
        ///   2.7 The save button is displayed
        /// </summary>
        [Test, Description("_05_FavouritesViewItemDetail"), Category("Favourites"), Repeat(1)]
        public void _05_FavouritesViewItemDetail()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
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
        /// 3.The recently adde product is displayed in the favourites m
        /// </summary>
        [Test, Description("_06_FavouritesAddItemToFavourites"), Category("Basket"), Repeat(1)]
        public void _06_FavouritesAddItemToFavourites()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
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
        /// 3. verify the formular used for adding and removing product quatity
        ///   3.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed
        /// 2.  The quantiy addition button are working as expected
        /// 3.
        ///   3.1 The total is correct
        /// </summary>
        [Test, Description("_07_FavouritesAddingAndRemovingProductQuantity"), Category("Basket"), Repeat(1)]
        public void _07_FavouritesAddingAndRemovingProductQuantity()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IFavouritesActions favActions = container.Resolve<Interfaces.IFavouritesActions>();
            //TODO
        }
    }
}
