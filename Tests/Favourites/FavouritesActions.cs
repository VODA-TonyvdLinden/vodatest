using System;
using System.Threading;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Favourites
{
    public class FavouritesActions : Interfaces.IFavouritesActions
    {
        // Test Case: 1. Click on the favourites block on the bottom of the screen 
        // Test Output: 1. The favourites page is displayed with catalogues in grid view
        public void VerifyFavouriteIconClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/favourites-catalog-view"), TimeSpan.FromMinutes(30));
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope > div > ul > li:nth-child(1) > div.brandinfo > div.itemView");
        }

        // Test Case: 2 Click on the delete icon    
        // Test Output: 2 The order is deleted from that supplier
        public void VerifyDeleteIcon(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.delete > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.delete > button"));
            browserInstance.Instance.Assert.Not.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView");
        }

        // Test Case: 3 Click on the clear <all> button  
        // Test Output: 3 This clear all selected catalogue basket
        public void VerifyClearAllButtonClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
            browserInstance.Instance.Assert.Not.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView");
        }

        // Test Case: 4. Verify that the list view button is displayed
        // Test Output: 4. The list view button is displayed
        public void VerifyListViewButton(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button");
            var reOrderButton = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button");
            browserInstance.Instance.Assert.True(() => reOrderButton.Element.Text.Contains("LIST VIEW"));
        }

        // Test Case: 1 Click on the list view button on the screen
        // Test Output: 1 The display mode switches to list view where items are displayed in a tabular format 
        public void VerifyListViewClick(Classes.Browser browserInstance)
        {
            VerifyListViewButton(browserInstance);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button"));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/favourites-catalog-view?viewtype=list");
            browserInstance.Instance.Assert.Exists("#alertsView > table");
        }

        // Test Case: 2. Click on the subcategory product
        // Test Output: 2. product are displayed 
        public void VerifyListViewProductClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#alertsView > table > tbody > tr > td:nth-child(1)");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(1)"));
            Thread.Sleep(3000);
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product > div");
        }

        // Test Case: 5. Verify that the grid view button is displayed
        // Test Output: 5. The list view button is displayed
        public void VerifyGridViewButton(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button");
            var reOrderButton = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button");
            browserInstance.Instance.Assert.True(() => reOrderButton.Element.Text.Contains("GRID VIEW"));
        }

        // Test Case: 1. Click on the <grid view> button 
        // Test Output: 1. Favourites  items are displayed  are displayed in a grid view 
        public void VerifyGridViewButtonClick(Classes.Browser browserInstance)
        {
            VerifyGridViewButton(browserInstance);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button"));
            browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/favourites-catalog-view?viewtype=grid");
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope");
        }

        // Test Case: 2. Verify that on the list view the is a grid view button that will allow to switch back
        // Test Output: 2. The grid view button is displayed
        public void VerifyGridViewButtonOnListView(Classes.Browser browserInstance)
        {
            VerifyListViewClick(browserInstance);
            VerifyGridViewButton(browserInstance);
        }

        // Test Case: 3.2 Click on the order now 
        // Test Output: 3.2 The confirm order pop-up is displayed
        public void VerifyConfirmOrderPopup(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 117: _03_FavouritesDetailGridView -> Test step we do not have the order now button.' 
                                    '3.2 Click on the order now ' - Test case to be updated.", LogWriter.eLogType.Error);

            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product > div");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product > div"));
            Helpers.Instance.VerifyPopPup(browserInstance, "#product_modal");
        }

        public void VerifyGridViewProductClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product > div");
        }

        public void VerifyGridViewProductOrderDelete(Classes.Browser browserInstance)
        {
            // Test Case: 4.1  Select a specific supplier by clicking  on any record from the table 
            VerifyFavouriteIconClick(browserInstance);
            VerifyGridViewProductClick(browserInstance);

            // Test Case: 4.2 Click on the delete icon  
            // Test Output: 4.2 The order is deleted from that supplier 
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div.delete > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div.delete > button"));
            browserInstance.Instance.Assert.Not.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div.product > div.img");
        }

        // Test Case 6.Verify that clear all from all basket functions as expected                                                               
        // Test Case 6.1 Select more than one  supplier by clicking  on multiple rows on the table    
        // Test Case 6.2 Click on the clear <all> button  
        // Test Output: 6.2 This clear all selected catalogue basket
        public void VerifyGridViewClearAllButtonClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.AddFavouriteItem(browserInstance);
            VerifyFavouriteIconClick(browserInstance);
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
            browserInstance.Instance.Assert.Not.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope > div > ul");
        }

        public void VerifyListViewProductOrderDelete(Classes.Browser browserInstance)
        {
            // Test Case: 4.1  Select a specific supplier by clicking  on any record from the table 
            VerifyFavouriteIconClick(browserInstance);
            VerifyListViewClick(browserInstance);
            VerifyListViewProductClick(browserInstance);

            // Test Case: 4.2 Click on the delete icon  
            // Test Output: 4.2 The order is deleted from that supplier 
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.delete > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.delete > button"));
            browserInstance.Instance.Assert.Not.Exists("#alertsView > table > tbody > tr > td:nth-child(1)");
        }

        // Test Case: 6. Verify that clear all from all basket functions as expected                                                               
        // Test Case: 6.1 Select more than one  supplier by clicking  on multiple rows on the table     
        // Test Case: 6.2 Click on the clear <all> button 
        // Test Output: 6.2 This clear all selected catalogue basket
        public void VerifyListViewClearAllButtonClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.AddFavouriteItem(browserInstance);
            VerifyFavouriteIconClick(browserInstance);
            VerifyListViewClick(browserInstance);
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));

            browserInstance.Instance.Assert.Not.Exists("#alertsView > table > tbody > tr");
        }

        // Test Case: 1. Click on the product
        // Test Output: 1. The product view screen is displayed
        public void ClickFavouriteProduct(Classes.Browser browserInstance)
        {
            Helpers.Instance.AddFavouriteItem(browserInstance);
            VerifyFavouriteIconClick(browserInstance);
            VerifyGridViewProductClick(browserInstance);
            VerifyConfirmOrderPopup(browserInstance);
        }

        public void VerifyProductViewScreen(Classes.Browser browserInstance)
        {
            // Test Case: 2.1 Verify that the product description is displayed   
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");

            // Test Case: 2.2 Verify that product unit price is displayed
            // Test Output: 2.2 The product unit price is displayed
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.unitPrice.ng-binding");

            // Test Case: 2.3 Verify that the edit buttons are available for adding and removing products quantity  
            // Test Output: 2.3  The edit buttons are displayed  
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button");
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button");

            // Test Case: 2.4 Verify that the quantity field is displayed and not editable 
            // Test Output: 2.4 The quantity field is displayed and is not editable
            browserInstance.Instance.Assert.Exists("#itemQuantity");
            var quantityInput = Helpers.Instance.GetProxy(browserInstance, "#itemQuantity");
            Helpers.Instance.FieldInput(browserInstance, quantityInput, "3");
            // browserInstance.Instance.Assert.Value("3").Not.In(quantityInput);

            // Test Case: 2.5 Verify that the total price field is displayed and not editable     
            // Test Output: 2.5  The total price field is displayed and not editable
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");

            // Test Case: 2.6  Verify that the favourite icon represented by a star with a plus sign  is displayed 
            // Test Output: 2.6  A star with a plus sign is displayed  
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");
            //Helpers.Instance.CheckClass(browserInstance, "addToFavButton", Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button"));
            //browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product > div > div.decsriptionOverlay.ng-binding");

            // Test Case: 2.7 Verify that the save button is displayed             
            // Test Output: 2.7 The save button is displayed
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button");
        }

        // Test Case: 3. Verify step 2 by selecting the favourites tab, to see if the recently added product is displayed 
        // Test Output: 3.The recently added product is displayed in the favourites 
        public void VerifyAddFavouriteProduct(Classes.Browser browserInstance, FluentAutomation.ElementProxy productDescription)
        {
            VerifyFavouriteIconClick(browserInstance);
            VerifyGridViewProductClick(browserInstance);

            var favouriteProductDescription = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product > div > div.decsriptionOverlay.ng-binding");
            browserInstance.Instance.Assert.True(() => productDescription.Element.Text == favouriteProductDescription.Element.Text);
        }

        // Test Case: 2. On the product view screen click on the favourites icon which is represented by a star and save
        // Test Output: 2. The product is saved to favourites
        public void AddBasketProductToFavourites(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");
            var favouriteButton = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");
            if (!favouriteButton.Element.Attributes.Get("class").Contains("removeToFavButton"))
            {
                Helpers.Instance.ClickButton(browserInstance, favouriteButton);
            }
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"));
            Thread.Sleep(5000);
        }

        // Test Case: 1. Click on the product
        // Test Output: 1. The product view screen is displayed  
        public FluentAutomation.ElementProxy ClickBasketProduct(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {
            Helpers.Instance.AddOrders(browserInstance, 1);
            basketActions.ClickBasketBlock(browserInstance);

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemView"));
            var productDescription = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div > div.img > div.decsriptionOverlay.ng-binding");

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div > div.img"));
            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"), 30);
            return productDescription;
        }
    }
}
