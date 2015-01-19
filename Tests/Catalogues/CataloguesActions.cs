using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Catalogues
{
    public class CataloguesActions : Interfaces.ICataloguesActions
    {
        // Test Case: 1. Verify that  the advert is displayed on a full page and also for 5 seconds
        // Test Output: 1. The Interstitial advert is displayed for 5 seconds on a full screen
        public void VerifyInterstitialAdvert(Classes.Browser browserInstance)
        {
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(1) > a"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(1) > a"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/fullScreenAd"));
            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/stores"));
        }

        // Test Case: 2. Verify that the advert  is clickable  
        // Test Output: 2.The Advert is clickable
        public void VerifyInterstitialAdvertClick(Classes.Browser browserInstance)
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(1) > a"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#landingPage > div > div.rightBlock > div > div > div:nth-child(1) > div:nth-child(1) > a"));

            browserInstance.Instance.Assert.Exists("div > img");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "div > img"));
        }

        // Test Case: 1.Click on the catalogues block at the bottom     
        // Test Output: 1. The catalogues page is displayed with catalogues that were already added activation setup
        public void VerifyCatalogueBlockClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/stores"), TimeSpan.FromSeconds(30));

            var catalogues = browserInstance.Instance.FindMultiple("#categoryCarousel > div");
            browserInstance.Instance.Assert.True(() => catalogues.Elements.Count > 0);
        }

        // Test Case: Verify that the catalogues landing page is displayed with the following                                                                                                        
        // Test Case: 2. Verify that the available subcategories from catalogues are displayed and active  
        // Test Output: 2. The available subcategories are available and displayed
        public void VerifyActiveCatalogueAndSubCategories(Classes.Browser browserInstance)
        {
            var activeCategories = browserInstance.Instance.FindMultiple("#categoryCarousel > div.categoryFilterBarHeight.ng-scope.active");
            var activeSubCategoriesContainers = browserInstance.Instance.FindMultiple("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active > ul.ACTIVE");

            // Verify that the number of active categories matches the number of active sub categories
            browserInstance.Instance.Assert.True(() => activeSubCategoriesContainers.Elements.Count == activeCategories.Elements.Count);

            activeSubCategoriesContainers.Elements.ForEach((elementTuple) =>
            {
                // Verify that a active sub category has mutliple subcategories
                FluentAutomation.ElementProxy proxy = new FluentAutomation.ElementProxy(elementTuple.Item1, elementTuple.Item2);
                LogWriter.Instance.Log(proxy.ToString(), LogWriter.eLogType.Debug);
                if (proxy != null)
                    LogWriter.Instance.Log(string.Format("ERROR: VerifyActiveCatalogueAndSubCategories -> Cannot find proxy for {0}", elementTuple.ToString()), LogWriter.eLogType.Error);
                else
                    browserInstance.Instance.Assert.True(() => proxy.Children.Count > 0);
            });
        }

        // Test Case: 3. Verify that the unavailable sub categories must be displayed and greyed out
        // Test Output: 3.  The unavailable subcategories are  displayed and made inactive  by greying out their colour
        public void VerifyInActiveCatalogueAndSubCategories(Classes.Browser browserInstance)
        {
            var inactiveCategories = browserInstance.Instance.FindMultiple("#categoryCarousel > div.categoryFilterBarHeight.ng-scope:not(.active)");
            var inactiveSubCategoriesContainers = browserInstance.Instance.FindMultiple("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope > ul.INACTIVE");


            browserInstance.Instance.Assert.True(() => inactiveSubCategoriesContainers.Elements.Count == inactiveCategories.Elements.Count);
        }

        // Test Case: 4. Verify that the list of catalogues is displayed and user is able to scroll left to right   
        // Test Output: 4. The list of catalogue icons are displayed and user is able to scroll form left to right
        public void VerifyCatalogueIconList(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#categoryCarousel");
            Helpers.Instance.CheckClass(browserInstance, "headerScroller", Helpers.Instance.GetProxy(browserInstance, "#categoryCarousel"));
            var catalogueIcons = browserInstance.Instance.FindMultiple("#categoryCarousel > div > a > img");
            browserInstance.Instance.Assert.True(() => catalogueIcons.Elements.Count > 0);
            LogWriter.Instance.Log(@"ISSUE 111: TESTCASE:_02_CatalogueLandingPage -> Test step we do not have the functionality to scroll for the catalogues.'
                                    '4. Verify that the list of catalogues is displayed and user is able to scroll left to right ' - Test case to be updated.", LogWriter.eLogType.Error);
        }

        public void VerifyCategoryIcons(Classes.Browser browserInstance)
        {
            // Test Case: o Bakery
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(2) > a > img", "#categoryCarousel > div:nth-child(2) > ul > li", "#categoryCarousel > div:nth-child(2) > ul", "Bakery");

            // Test Case: o Fruit & Vegetables
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(8) > a > img", "#categoryCarousel > div:nth-child(8) > ul > li", "#categoryCarousel > div:nth-child(8) > ul", "Fruit & Vegetables");

            // Test Case: o Dairy & Eggs
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(5) > a > img", "#categoryCarousel > div:nth-child(5) > ul > li", "#categoryCarousel > div:nth-child(5) > ul", "Dairy & Eggs");

            // Test Case: o Meat, Fish & Poultry
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(11) > a > img", "#categoryCarousel > div:nth-child(11) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(11) > ul", "Meat,Fish & Poultry");

            // Test Case: o Frozen
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(7) > a > img", "#categoryCarousel > div:nth-child(7) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(7) > ul", "Frozen");

            // Test Case: o Tins, Jars and Cooking
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(14) > a > img", "#categoryCarousel > div:nth-child(14) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(14) > ul", "Tins, Jars and Cooking");

            // Test Case: o Packets and Cereals
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(12) > a > img", "#categoryCarousel > div:nth-child(12) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(12) > ul", "Packets and Cereals");

            // Test Case: o Baking
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(3) > a > img", "#categoryCarousel > div:nth-child(3) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(3) > ul", "Baking");

            // Test Case: o Coffee, Teas and Creamers
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(4) > a > img", "#categoryCarousel > div:nth-child(4) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(4) > ul", "Coffee, Teas & Creamers");

            // Test Case: o Snacks, Sweets & Biscuits
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(13) > a > img", "#categoryCarousel > div:nth-child(13) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(13) > ul", "Snacks, Sweets & Biscuits");

            // Test Case: o Drinks
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(6) > a > img", "#categoryCarousel > div:nth-child(6) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(6) > ul", "Drinks");

            // Test Case: o Household
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(10) > a > img", "#categoryCarousel > div:nth-child(10) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(10) > ul", "Household");

            // Test Case: o Baby
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(1) > a > img", "#categoryCarousel > div:nth-child(1) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(1) > ul", "Baby");

            // Test Case: o Health and Beauty 
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(9) > a > img", "#categoryCarousel > div:nth-child(9) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(9) > ul", "Health and Beauty");

            // Test Case: o Other  
            VerifyCategoryIcon(browserInstance, "#categoryCarousel > div:nth-child(15) > a > img", "#categoryCarousel > div:nth-child(15) > ul > li.heading.ng-binding", "#categoryCarousel > div:nth-child(15) > ul", "Other");

        }

        public void VerifyCategoryIcon(Classes.Browser browserInstance, string iconImagePath, string categoryItemPath, string categoryClassPath, string categoryName)
        {
            browserInstance.Instance.Assert.Exists(iconImagePath);
            browserInstance.Instance.Assert.Exists(categoryItemPath);
            var categoryItem = Helpers.Instance.GetProxy(browserInstance, categoryItemPath);

            //Category name is only available when the item is clicked
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, iconImagePath.Replace(" > img", "")));

            var categoryItemUL = Helpers.Instance.GetProxy(browserInstance, categoryClassPath);

            Helpers.Instance.CheckProxyValue(browserInstance, categoryItem, categoryName);
            //browserInstance.Instance.Assert.True(() => categoryItem.Element.Text == categoryName);
        }

        // Test Case: 7. Verify that brands are displayed and user can scroll from left to right  
        // Test Output: 7.The brands are displayed and you can scroll from left to right
        public void VerifyBrandList(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#storesContent > div.storesbody > div.filteredContentContainer > div > div > div > div > ul");
            var brands = browserInstance.Instance.FindMultiple("#storesContent > div.storesbody > div.filteredContentContainer > div > div > div > div > ul > li");
            browserInstance.Instance.Assert.True(() => brands.Elements.Count > 0);

            var brandsContainer = Helpers.Instance.GetProxy(browserInstance, "#storesContent > div.storesbody > div.filteredContentContainer > div > div > div > div");
            browserInstance.Instance.Assert.Css("overflow-x", "scroll").On(brandsContainer);
            LogWriter.Instance.Log(@"TESTCASE:ISSUE 112: _02_CatalogueLandingPage -> Test step we do not have the functionality to scroll for the brands.'
                                    '7. Verify that brands are displayed and user can scroll from left to right' - Test case to be updated.", LogWriter.eLogType.Error);
        }

        // Test Case: 8. Verify that specials are displayed and user can scroll from left to right 
        // Test Output: 8. The specials are displayed on the left hand side of the landing page and user can scroll from left to right
        public void VerifySpecials(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#storesContent > div.storesbody > div.leftBlock > div > div > div.specialsGroup.ng-scope");
            var products = browserInstance.Instance.FindMultiple("#storesContent > div.storesbody > div.leftBlock > div > div > div.specialsGroup.ng-scope > div > div.outerProductContainer");
            browserInstance.Instance.Assert.True(() => products.Elements.Count > 0);
            var specialsContainer = Helpers.Instance.GetProxy(browserInstance, "#storesContent > div.storesbody > div.leftBlock > div");
            browserInstance.Instance.Assert.Css("overflow-x", "scroll").On(specialsContainer);
        }

        // Test Case: 1.1 Select on any of the fixed categories        
        // Test Output: 1.1 Subcategories under that category are displayed as a list and the selected category is displayed in red
        public void VerifyCategoryClick(Classes.Browser browserInstance, out FluentAutomation.ElementProxy category, out FluentAutomation.ElementProxy subCategories)
        {
            var activeCategories = browserInstance.Instance.FindMultiple("#categoryCarousel > div.categoryFilterBarHeight.ng-scope.active");

            // Select an active category and click on it.
            browserInstance.Instance.Assert.True(() => activeCategories.Elements.Count > 0);
            category = new FluentAutomation.ElementProxy(activeCategories.Elements[0].Item1, activeCategories.Elements[0].Item2);
            Helpers.Instance.ClickButton(browserInstance, category);

            browserInstance.Instance.Assert.Exists("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul > li.heading.ng-binding");
            var selectCategory = Helpers.Instance.GetProxy(browserInstance, "#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul > li.heading.ng-binding");

            // Check the selected category is displayed in red
            browserInstance.Instance.Assert.Css("background", "red").On(selectCategory);
            subCategories = Helpers.Instance.GetProxy(browserInstance, "#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul");

            // Check subcategories under that category are displayed as a list
            int subCategoriesCount = subCategories.Children.Count;
            browserInstance.Instance.Assert.True(() => subCategoriesCount > 1);
        }

        public void VerifyElementScroll(Classes.Browser browserInstance, FluentAutomation.ElementProxy scrollElement, bool isVertical)
        {
            if (isVertical)
            {
                browserInstance.Instance.Assert.Css("overflow-y", "scroll").On(scrollElement);
            }
            else
            {
                browserInstance.Instance.Assert.Css("overflow-x", "scroll").On(scrollElement);
            }
        }

        // Test Case: 1.3 Scroll down and select any subcategory
        // Test Output: 1.3  The selected subcategory is displayed in red  
        public void VerifyActiveSubCategorySelect(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul");
            browserInstance.Instance.Scroll("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul");
            browserInstance.Instance.Assert.Exists("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul > li.ng-scope.ng-binding.active");

            var activeSubCategories = browserInstance.Instance.FindMultiple("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul > li.ng-scope.ng-binding.active");

            FluentAutomation.ElementProxy subCategory = new FluentAutomation.ElementProxy(activeSubCategories.Elements[activeSubCategories.Elements.Count - 1].Item1, activeSubCategories.Elements[activeSubCategories.Elements.Count - 1].Item2);
            browserInstance.Instance.Hover(subCategory);
            browserInstance.Instance.Assert.Css("background", "red").On(subCategory);
        }

        // Test Case: 1.4 Click on the category and verify if it collapses the sub categories list
        // Test Output: 1.4  The  subcategories list is collapsed
        public void VerifyCategoryUnSelect(Classes.Browser browserInstance, FluentAutomation.ElementProxy category, FluentAutomation.ElementProxy subCategories)
        {
            Helpers.Instance.ClickButton(browserInstance, category);
            browserInstance.Instance.Assert.Css("display", "none").On(subCategories);
        }

        // Test Case: 4 Click on the selected subcategory you wish to view products for 
        // Test Output: 4 The selected subcategory products are displayed
        public void VerifySubCategoryClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul > li.ng-scope.ng-binding.active");
            var activeSubCategories = browserInstance.Instance.FindMultiple("#categoryCarousel > div.catagoryItemContainer.categoryFilterBarHeight.ng-scope.active.open > ul > li.ng-scope.ng-binding.active");
            FluentAutomation.ElementProxy subCategory = new FluentAutomation.ElementProxy(activeSubCategories.Elements[activeSubCategories.Elements.Count - 1].Item1, activeSubCategories.Elements[activeSubCategories.Elements.Count - 1].Item2);
            Helpers.Instance.ClickButton(browserInstance, subCategory);

            browserInstance.Instance.Assert.Exists("#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div > div");
            var subCategoryProducts = browserInstance.Instance.FindMultiple("#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div > div");
            browserInstance.Instance.Assert.True(() => subCategoryProducts.Elements.Count > 1);
        }

        public void VerifyProductView(Classes.Browser browserInstance)
        {
            // 5.1 Verify that the product icon is displayed
            // 5.1 The product icon is displayed  
            browserInstance.Instance.Assert.Exists("#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.img > div.realImg > img");

            // 5.2 Verify that the product price is displayed 
            // 5.2 The product price is displayed 
            browserInstance.Instance.Assert.Exists("#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.price > div");

            // 5.3 Verify that the <buy now> button is available 
            // 5.3 The buy now button is displayed
            browserInstance.Instance.Assert.Exists("#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.price > button");

            // 5.4 Verify that the product description is displayed 
            // 5.4 The product description is displayed
            browserInstance.Instance.Assert.Exists("#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.img > div.decsriptionOverlay.ng-binding");
        }

        public void ClickSubCategory(Classes.Browser browserInstance, bool verifyProductPopUp = false)
        {
            // Test Case: 1 Select on any of the fixed categories  
            FluentAutomation.ElementProxy category;
            FluentAutomation.ElementProxy subCategories;
            VerifyCategoryClick(browserInstance, out category, out subCategories);

            // Test Case: 2 Make sure that you can scroll up and down on that list   
            VerifyElementScroll(browserInstance, subCategories, true);

            // Test Case: 3 Scroll down and select any subcategory 
            VerifyActiveSubCategorySelect(browserInstance);

            // Test Case: 4 Click on the selected subcategory you wish to view products for  
            VerifySubCategoryClick(browserInstance);

            if (verifyProductPopUp)
            {
                // Test Case: 5  Verify the following on the product view    
                VerifyProductView(browserInstance);
            }
        }

        // Test Case: 6. Click on the product  
        // Test Output: 6. The product view screen is displayed
        public void VerifyProductClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.productbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.img"));
            Helpers.Instance.VerifyPopPup(browserInstance, "#product_modal");
            browserInstance.Instance.Assert.Exists("#product_modal > div > div");
        }

        public void VerifyProductItemClickPopup(Classes.Browser browserInstance)
        {
            // Test Case: 7.1 Verify that the product description is displayed 
            // Test Output: 7.1 The product description is displayed
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");

            // Test Case: 7.2 Verify that product unit price is displayed  
            // Test Output: 7.2 The product unit price is displayed
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.unitPrice.ng-binding");

            // Test Case: 7.3 Verify that the edit buttons are available for adding and removing products quantity 
            // Test Output: 7.3  The edit buttons are display
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button");
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button");

            // Test Case: 7.4 Verify that the quantity field is displayed and not editable 
            // Test Output: 7.4 The quantity field is displayed and is not editable 
            browserInstance.Instance.Assert.Exists("#itemQuantity");
            var quantityInput = Helpers.Instance.GetProxy(browserInstance, "#itemQuantity");
            Helpers.Instance.FieldInput(browserInstance, quantityInput, "3");
            browserInstance.Instance.Assert.Value("3").Not.In(quantityInput);

            // Test Case: 7.5 Verify that the total price field is displayed and not editable
            // Test Output: 7.5  The total price field is displayed and not editable
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");
            var totalPrice = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");
            Helpers.Instance.FieldInput(browserInstance, totalPrice, "3");
            browserInstance.Instance.Assert.Value("3").Not.In(totalPrice);

            // Test Case: 7.6  Verify that the favourite icon represented by a star with a plus sign  is displayed
            // Test Output: 7.6  A star with a plus sign is displayed
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");
            var favouriteButton = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");
            Helpers.Instance.CheckClass(browserInstance, "addToFavButton", favouriteButton);

            // Test Case: 7.7 Verify that the save button is displayed  
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button");
            var saveButton = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button");
            browserInstance.Instance.Assert.True(() => saveButton.Element.Text == "SAVE");

            LogWriter.Instance.Log(@"TESTCASE:ISSUE 113: _05_CatalogueViewProductDetail -> Test step the test output is not correct.' 
                                    '7.7 Verify that the save button is displayed' - Test case to be updated.", LogWriter.eLogType.Error);

        }

        // Click on the favourites icon
        public void VerifyProductFavouritesIconClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button"));
        }

        // Click on the save button 
        public void VerifyProductSaveClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"));
        }

        // Test Case: 7. Verify step 6 by selecting the favourites tab, to see if the recently added product is displayed
        // Test Output: 7. The recently added product is displayed in the favourites menu
        public void VerifyFavouriteBlockClick(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div"));
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li:nth-child(1) > div.brandinfo > div.itemView");
            browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope > div > ul > li:nth-child(1) > div.brandinfo > div.itemSelector.ng-binding");
            var itemsCount = Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope > div > ul > li:nth-child(1) > div.brandinfo > div.itemSelector.ng-binding");
            browserInstance.Instance.Assert.True(() => itemsCount.Element.Text.Trim() == "1 ITEMS");
        }

        // Test Case: 6. On the product view screen click on the - sign for removing and + adding quantity and save
        // Test Output: 6. The quantity addition button are working as expected
        public void VerifyProductQuantityClick(Classes.Browser browserInstance)
        {
            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");
            var total = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");

            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));
            browserInstance.Instance.Assert.Value(" 0.00 ").Not.In(total);

            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button"));
            browserInstance.Instance.Assert.Value(" 0.00 ").In(total);

            // Click on the save button 
            VerifyProductSaveClick(browserInstance);
        }

        // Test Case: 7. verify the formula used for adding and removing product quantity                                       
        // Test Case: 7.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct
        // Test Output: 7.1 The total is correct 
        public void VerifyProductTotal(Classes.Browser browserInstance)
        {
            // Click the product
            VerifyProductClick(browserInstance);

            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");
            var total = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");

            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.unitPrice.ng-binding");
            var price = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.unitPrice.ng-binding");

            browserInstance.Instance.Assert.Exists("#itemQuantity");
            var quantityInput = Helpers.Instance.GetProxy(browserInstance, "#itemQuantity");

            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));

            string totalValue = (Convert.ToDecimal(quantityInput.Element.Value.Trim()) * Convert.ToDecimal(price.Element.Text.Trim())).ToString();
            browserInstance.Instance.Assert.Value(totalValue).In(total);

            browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button"));

            totalValue = (Convert.ToDecimal(quantityInput.Element.Value.Trim()) * Convert.ToDecimal(price.Element.Text.Trim())).ToString();
            browserInstance.Instance.Assert.Value(totalValue).In(total);
        }

        public void AddFavouriteProduct(Classes.Browser browserInstance)
        {
            ClickSubCategory(browserInstance);
            VerifyProductClick(browserInstance);
            VerifyProductFavouritesIconClick(browserInstance);
            VerifyProductSaveClick(browserInstance);
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/main"));
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div"), TimeSpan.FromMinutes(30));
        }
    }
}
