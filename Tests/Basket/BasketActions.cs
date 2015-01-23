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
    public class BasketActions : Interfaces.IBasketActions
    {

        public void CheckListViewButtonExists(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button");
        }

        public void ClickClearAllButton(Classes.Browser browserInstance)
        {

            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
            Helpers.Instance.CheckClearPopup(browserInstance);
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "0 Items");
        }

        public void ClickOrderDeleteButton(Classes.Browser browserInstance)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope > div > ul > li > div.delete > button"));
            Helpers.Instance.CheckConfirmDeletePopup(browserInstance);

            // 3.2 The order is deleted from that supplier
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "0 Items");
        }

        public void ClickConfirmOrderPopupClose(Classes.Browser browserInstance)
        {
            LogWriter.Instance.Log("ISSUE 39: TESTCASE:_01_BasketGridView -> Test case step missed -> CLICK CLOSE BUTTON", LogWriter.eLogType.Error);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
            Thread.Sleep(1000);
        }

        public void ClickOrderNowButton(Classes.Browser browserInstance, FluentAutomation.ElementProxy button)
        {
            Helpers.Instance.ClickButton(browserInstance, button);
        }

        public void CheckConfirmPopup(Classes.Browser browserInstance)
        {
            // 2.2 Click on the order now
            // 2.2 The confirm order pop-up is displayed 
            Thread.Sleep(2000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#orderNow"), 30);
            //Helpers.Instance.Exists(browserInstance, "#orderNow > div");
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-header.vodaBackgroundGrey > div.successMsg > strong"), "Order now");
        }

        public void ClickBasketBlock(Classes.Browser browserInstance)
        {
            Helpers.Instance.GoToBasket(browserInstance);
            Thread.Sleep(3000);
        }

        public void VerifyConfirmPopupValues(Classes.Browser browserInstance, FluentAutomation.ElementProxy noItems, FluentAutomation.ElementProxy itemsPrice)
        {
            // 3.3 Make sure that the total number of items and total price of items to be ordered are displayed 
            // 3.3  The total number of items and total price of items are displayed on the pop-up 
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > p:nth-child(2)"), noItems.Element.Text);
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > p:nth-child(3)"), "Total: " + itemsPrice.Element.Text);
        }

        public void VerifyConfirmPopup(Classes.Browser browserInstance)
        {
            // 3.2 The message asking the user about confirm the order is displayed with a yes and no buttons  
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > p:nth-child(1)"), "Are you sure you want to place this order?");
            Helpers.Instance.Exists(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)");
            Helpers.Instance.Exists(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(2)");
        }

        public void CheckClearAllFunction(Classes.Browser browserInstance)
        {
            /// 6.1 Multiple records on the list view table are selected  
            // 6.2 Click on the clear <all> button    
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button"));
            Helpers.Instance.CheckClearPopup(browserInstance);
            /// 6.2 This clear all selected catalogue basket      
            Helpers.Instance.CheckProxyValue(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding"), "0 Items");
        }

        public void CheckOrderAllFunction(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {

            // 5.2 Click on the order all button  
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            Thread.Sleep(2000);
            Helpers.Instance.AddOrders(browserInstance, 1);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div:nth-child(1) > div.headerLogo.left > a"));
            Thread.Sleep(2000);
            Helpers.Instance.AddOrders(browserInstance, 2);
            Thread.Sleep(3000);
            basketActions.ClickBasketBlock(browserInstance);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(4)"));
        }

        public void DeleteOrderFromList(Classes.Browser browserInstance)
        {
            // 4.2 Click on the delete icon   
            /// 4.2 The order is deleted from that supplier  
            var itemCount = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(4) > div.delete > button"));
            
            Helpers.Instance.CheckConfirmDeletePopup(browserInstance);
            browserInstance.Instance.Assert.Not.Exists("#alertsView > table > tbody > tr > td:nth-child(1)");
        }

        public void VerifyListViewActions(Classes.Browser browserInstance)
        {
            // 3. Verify that order from a specific supplier functions as expected                      
            // 3.1 Select a specific supplier by clicking  on any record from the table   
            /// 3.1 The record on the list view table is selected   
            var supplierRecord = Helpers.Instance.GetProxy(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(1)");
            Helpers.Instance.ClickButton(browserInstance, supplierRecord);
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div");
            //Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div");
        }

        public void VerifyListView(Classes.Browser browserInstance)
        {
            // 2. Verify that on the list view the is a grid view button that will allow to switch back 
            /// 2. The grid view button is displayed  
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button");
        }

        public void VerifyButtons(Classes.Browser browserInstance)
        {
            // 3.5 Verify that the list view button is displayed when user is on grid view mode
            /// 3.5 The list view button is displayed   
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(1) > button");

            // 3. Verify that the grid view button is displayed when user is on grid view mode 
            /// 3. The grid  view button is displayed  
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(2) > button");
            // 4. Verify that the order all button is displayed 
            /// 4. The order all button is displayed    
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(4) > button");

            // 5. Verify that the clear all button is displayed  
            /// 5. The Clear all button is displayed      
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.rightBlock > div:nth-child(2) > div > div > div > a:nth-child(3) > button");
        }

        public void VerifyProductView(Classes.Browser browserInstance)
        {
            // 3.1 Verify that the product icon is displayed 
            /// 3.1 The product icon is displayed   
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div > div.img");
            //CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.img > div.realImg > img");

            // 3.2 Verify that the product price is displayed      
            /// 3.2 The product price is displayed 
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div > div.price > div.txt.bpgtfontsizep.ng-binding");
            //CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.price > div");
            //LogWriter.Instance.Log("ISSUE 48: TESTCASE: _04_BasketDetailGridView -> Price is displayed without the currency indicator", LogWriter.eLogType.Error);

            // 3.3 Verify that the <clear button > button is available        
            /// 3.3 The clear  button is displayed 
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div > div.price > button");
            //CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.price > button");

            // 3.4 Verify that the product description is displayed    
            /// 3.4 The product description is displayed  
            CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div > div.img > div.decsriptionOverlay.ng-binding");
            //CheckElementExists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div:nth-child(1) > div > div.img > div.decsriptionOverlay.ng-binding");


        }
        public void VerifyDetailsValues(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions)
        {
            // 2.2 Verify that the product price is displayed 
            /// 2.2 The product price is displayed  
            basketActions.CheckElementExists(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(4)");
            LogWriter.Instance.Log("ISSUE 55: TESTCASE: _05_BasketDetailListView -> Price does not have a currency indicator", LogWriter.eLogType.Error);

            // 2.3 Verify that the <clear button > button is available   
            /// 2.3 The clear  button is displayed 
            basketActions.CheckElementExists(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(7) > div > button");

            // 2.4 Verify that the product description is displayed 
            /// 2.4 The product description is displayed   
            basketActions.CheckElementExists(browserInstance, "#alertsView > table > tbody > tr > td:nth-child(1)");
        }

        public void ClickPopupClose(Classes.Browser browserInstance, string path)
        {
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, path));
            Thread.Sleep(3000);
        }

        public void CheckElementExists(Classes.Browser browserInstance, string element)
        {
            Helpers.Instance.Exists(browserInstance, element);
        }

        public void VerifyPopupValues(Classes.Browser browserInstance)
        {
            // 2.1 Verify that the product description is displayed   
            /// 2.1 The product description is displayed

            Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");

            // 2.2 Verify that product unit price is displayed  
            /// 2.2 The product unit price is displayed   
            Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");

            // 2.3 Verify that the edit buttons are available for adding and removing products quantity 
            /// 2.3  The edit buttons are displayed  
            Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button");
            Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button");

            // 2.4 Verify that the quantity field is displayed and not editable  
            /// 2.4 The quantity field is displayed and is not editable  
            var itemQty = Helpers.Instance.GetProxy(browserInstance, "#itemQuantity");
            Helpers.Instance.Exists(browserInstance, itemQty);
            browserInstance.Instance.Append("a").To(itemQty);
            browserInstance.Instance.Assert.Value("3a").Not.In(itemQty);

            // 2.5 Verify that the total price field is displayed and not editable 
            /// 2.5  The total price field is displayed and not editable
            var price = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");
            Helpers.Instance.Exists(browserInstance, price);
            browserInstance.Instance.Assert.True(() => price.Element.TagName == "div");

            // 2.6  Verify that the favourite icon represented by a star with a plus sign  is displayed 
            /// 2.6  A star with a plus sign is displayed  
            Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");

            // 2.7 Verify that the save button is displayed    
            /// 2.7 The save button is displayed
            Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button");
        }
        public void CheckFavAdded(Classes.Browser browserInstance, string prodDescription)
        {
            // 3. Verify step 2 by selecting the favourites tab, to see if the recently added product is displayed 
            /// 3.The recently added product is displayed in the favourites 
            //Click Favourites block
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(4) > div"));
            //click on the supplier/wholesaler
            Thread.Sleep(3000);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.searchgridviewblcok.ng-scope > div > ul > li > div.brandinfo > div.itemView"));
            Thread.Sleep(3000);
            //Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.brandinfo > div.itemView"));
            //check prod exists
            Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div.product");
            //Helpers.Instance.Exists(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div.product");
            //Click on product
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div.product > div.img"));
            //Wait for popup to show
            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"), 30);
            //check prod description to be the same as the one that was added
            var NewProd = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");
            Helpers.Instance.CheckProxyValue(browserInstance, NewProd, prodDescription);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.modal-header > div > button"));
            Thread.Sleep(2000);
        }

        public void TestFavButtonOnPopup(Classes.Browser browserInstance)
        {
            // 2. On the product view screen click on the favourites icon which is represented by a star and save
            /// 2. The product is saved to favourites  
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button"));

            var favButton = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.devilsFeatureContainer > button");

            Thread.Sleep(3000);
            browserInstance.Instance.Assert.True(() => favButton.Element.Attributes.Get("class") == "btn addToFavButton");
            Thread.Sleep(3000);
            Helpers.Instance.ClickButton(browserInstance, favButton);
            Thread.Sleep(3000);
            browserInstance.Instance.Assert.True(() => favButton.Element.Attributes.Get("class") == "btn removeToFavButton");
            //close the popup
            Thread.Sleep(3000);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.modal-header > div > button"));
            Thread.Sleep(2000);
        }

        public string ClickProduct(Classes.Browser browserInstance)
        {
            // 1. Click on the product   
            /// 1. The product view screen is displayed 
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div.productContainerBlockScroll.ng-scope > div > div > div > div.img"));
            //Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > div > div > div.img"));

            Thread.Sleep(5000);
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.finalControls > div.addToBasketContainer > button"), 30);

            var selectedProduct = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.productDesc.ng-binding");
            string prodDescription = selectedProduct.Element.Text;
            return prodDescription;
        }

        public void VerifyFormula(Classes.Browser browserInstance, FluentAutomation.ElementProxy qtyBox, int qty)
        {
            // 3. verify the formula used for adding and removing product quantity                                             
            // 3.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct
            /// 3.1 The total is correct 
            /// 
            var unitPrice = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > div.unitPrice.ng-binding");
            var test = unitPrice.Element.Text.Replace("Unit Price ", "").Replace("\r", "").Replace("\n", "").Replace("R", "").Replace(" ", "");
            LogWriter.Instance.Log(test, LogWriter.eLogType.Debug);
            decimal unitP = Convert.ToDecimal(test);//i + (i2 / 100);
            //If you get an error here (string not in correct format), check that your decimal char is set to '.' and not ',' in computer settings

            var totalPrice = Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.itemTotal.centered.ng-binding");

            PressAddIcon(browserInstance, qtyBox, qty);
            decimal totalP = unitP * (qty + 1);
            Helpers.Instance.CheckProxyValue(browserInstance, totalPrice, string.Format("R {0}", totalP));

            PressRemoveIcon(browserInstance, qtyBox, qty);
            totalP = unitP * (qty);
            Helpers.Instance.CheckProxyValue(browserInstance, totalPrice, string.Format("R {0}", totalP));
        }

        public void TestAddRemoveButtons(Classes.Browser browserInstance, out FluentAutomation.ElementProxy qtyBox, out int qty)
        {
            // 2. On the product view screen  click on the - sign for removing and + adding quantity and save   
            /// 2.  The quantity addition button are working as expected 
            qtyBox = Helpers.Instance.GetProxy(browserInstance, "#itemQuantity");
            qty = int.Parse(qtyBox.Element.Text);
            PressAddIcon(browserInstance, qtyBox, qty);
            PressRemoveIcon(browserInstance, qtyBox, qty);
        }

        public void VerifyUnConfirmOrderPopup(Classes.Browser browserInstance)
        {
            Helpers.Instance.VerifyPopPup(browserInstance, "#errorModal");

            Helpers.Instance.Exists(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div.errorSubHeading.ng-binding");
            Helpers.Instance.Exists(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(2) > div.col-sm-9 > div.errorDetailsList > ul > li.ng-binding");
            Helpers.Instance.Exists(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(3) > div > button");

            Helpers.Instance.CheckProxyText(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div.errorSubHeading.ng-binding"), "Pending Order");

            var messageProxy = Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(2) > div.col-sm-9 > div.errorDetailsList > ul > li.ng-binding");
            browserInstance.Instance.Assert.True(() => messageProxy.Element.Text.Contains("You have an unconfirmed order"));

            var backButtonProxy = Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-body.text-center > div.errorContentMiddle > div:nth-child(3) > div > button");
            browserInstance.Instance.Assert.True(() => backButtonProxy.Element.Text.Contains("Back"));


            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#errorModal > div > div > div.modal-header.vodaBackgroundGrey > div:nth-child(2) > button"));
        }

        private void PressRemoveIcon(Classes.Browser browserInstance, FluentAutomation.ElementProxy qtyBox, int qty)
        {
            //Press -
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.decrease > button"));
            Helpers.Instance.CheckProxyValue(browserInstance, qtyBox, (qty).ToString());
        }

        private void PressAddIcon(Classes.Browser browserInstance, FluentAutomation.ElementProxy qtyBox, int qty)
        {
            //Press +
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#product_modal > div > div > div.basketControl.modal-body > div.productControlContainer > form > div.quantityControl > div.increase > button"));
            Helpers.Instance.CheckProxyValue(browserInstance, qtyBox, (qty + 1).ToString());
        }
    }
}
