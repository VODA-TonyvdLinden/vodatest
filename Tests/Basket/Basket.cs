using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Basket
{
    [TestFixture, Description("Catalogues"), Category("Catalogues")]
    public class Basket
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

            container.RegisterType<Interfaces.IBasketActions, Tests.Basket.BasketActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
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
        /// 1.Click on the basket block at the bottom of the screen
        /// 2. Verify that order from a specific supplier functions as expected
        ///   2.1 Select a specific supplier by clicking  on the checbox
        ///   2.2 Click on the order now
        /// 3.Verify that delete orders from a specific supplier functions asexpected
        ///   3.1 Select a specific supplier by clicking  on the checbox
        ///   3.2 Click on the delete icon
        /// 4.Verify that order all from basket  functions as expected
        ///   4.1 Select morethan one  supplier by clicking  on the checboxes of different suppliers
        ///   4.2 Click on the order all button
        /// 5.Verify that clear all from all basket functions as expected
        ///   5.1 Select morethan one  supplier by clicking  on the checboxes of different suppliers
        ///   5.2 Click on the clear <all> button
        /// 6. Verify that the list view button is displayed
        /// TEST OUTPUT:
        /// 1. The basket page is displayed with catalogues in grid view
        /// 2.
        ///   2.1 The checkbox on the supplier is clicked
        ///   2.2 The confirm order pop-up is displayed
        /// 3.
        ///   3.1 The checkbox on the supplier is clicked
        ///   3.2 The order is deleted from that supplier
        /// 4.
        ///   4.1 The checkboxes for different suppliers are selected
        ///   4.2 The confirm order pop-up is displayed
        /// 5.
        ///   5.1 The checkboxes for different suppliers are selected
        ///   5.2 This clear all selected catalogue basket
        /// 6. The list view button is displayed
        /// </summary>
        [Test, Description("BasketGridView"), Repeat(1)]
        public void BasketGridView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1 Select a specific supplier by clicking  on the checbox
        /// 2. Click on the order now
        /// 3. Verify the followingthe folowing for the confirm order caption screen
        ///   3.1 Make  sure that the message box form caption is written order now
        ///   3.2 Make sure that the is a message asking the user about confirming the order with a yes or no
        ///   3.3 Make sure that the total number of items and total price of items to be ordered are displayed
        /// TEST OUTPUT:
        /// 1 The checkbox on the supplier is clicked
        /// 2. The confirm order pop-up is displayed
        /// 3.
        ///   3.1 The message box form caption is displayed on top of the pop-up as order now
        ///   3.2 The message asking the user about confirm the order is displayed with a yes and no buttons
        ///   3.3  The total number of items and total price of items are displayed on the pop-up
        /// </summary>
        [Test, Description("BasketConfirmOrder"), Repeat(1)]
        public void BasketConfirmOrder()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the <list view> button on the basket tab
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
        /// 1. Basket items are displayed in a tabular format as a list
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
        [Test, Description("BasketListView"), Repeat(1)]
        public void BasketListView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the <grid> view button
        /// 2. Click on the selected subcategory you wish to view products for
        /// 3. Verify the following on the product view
        ///   3.1 Verify that the product icon is displayed
        ///   3.2 Verify that the product price is displayed
        ///   3.3 Verify that the <clear button > button is available
        ///   3.4 Verify that the product description is displayed
        ///   3.5 Verify that the list view button is displayed when user is on grid view mode
        /// 4. Verify that the order all button is displayed
        /// 5. Verify that the clear all button is displayed
        /// TEST OUTPUT:
        /// 1. The application switches to grid view mode
        /// 2. The selected subcatergory products are displayed
        /// 3.
        ///   3.1 The product icon is displayed
        ///   3.2 The product price is displayed
        ///   3.3 The clear  button is displayed
        ///   3.4 The product description is displayed
        ///   3.5 The list view button is displayed
        /// 4. The order all button is displayed
        /// 5. The Clear all button is displayed
        /// </summary>
        [Test, Description("BasketDetailGridView"), Repeat(1)]
        public void BasketDetailGridView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the selected subcategory you wish to view products for
        /// 2. Verify the following on the product view
        ///   2.1 Verify that the product icon is displayed
        ///   2.2 Verify that the product price is displayed
        ///   2.3 Verify that the <clear button > button is available
        ///   2.4 Verify that the product description is displayed
        /// 3. Verify that the grid view button is displayed when user is on grid view mode
        /// 4. Verify that the order all button is displayed
        /// 5. Verify that the clear all button is displayed
        /// TEST OUTPUT:
        /// 1. The selected subcatergory products are displayed
        /// 2.
        ///   2.1 The product icon is displayed
        ///   2.2 The product price is displayed
        ///   2.3 The clear  button is displayed
        ///   2.4 The product description is displayed
        /// 3. The grid  view button is displayed
        /// 4. The order all button is displayed
        /// 5. The Clear all button is displayed
        /// </summary>
        [Test, Description("BasketDetailListView"), Repeat(1)]
        public void BasketDetailListView()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
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
        [Test, Description("BasketViewItemDetail"), Repeat(1)]
        public void BasketViewItemDetail()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
        /// TEST STEPS:
        /// 1. Click on the product
        /// 2. On the product view screen click on the favourites icon which is represented by a star and save
        /// 3. Verify step 2 by selecting the favourites tab, to see if the recently added product is displayed
        /// TEST OUTPUT:
        /// 1. The product view screen is displayed
        /// 2. The product is saved to favourites
        /// 3.The recently adde product is displayed in the favourites m
        /// </summary>
        [Test, Description("BasketAddItemToFavourites"), Repeat(1)]
        public void BasketAddItemToFavourites()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }

        /// <summary>
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
        [Test, Description("BasketAddingAndRemovingProductQuantity"), Repeat(1)]
        public void BasketAddingAndRemovingProductQuantity()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IBasketActions basketActions = container.Resolve<Interfaces.IBasketActions>();
            //TODO
        }
    }
}
