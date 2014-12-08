using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProj.Classes;
using TestProj.Tests.Common;

namespace TestProj.Tests.Alerts
{
    public class AlertsActions : Interfaces.IAlertsActions
    {
        public void VerifyVodacomLogoAndBanner(Classes.Browser browserInstance)
        {
            // 1. Verify that the Vodacom logo and the red banner are displayed on the activation screen
            // 1. The Vodacom banner logo and banner are displayed 
            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step worded incorrectly. '1. Verify that the Vodacom logo and the red banner are displayed on the activation screen' - Update test case", LogWriter.eLogType.Error);
            Helpers.Instance.CheckLogoAndBanner(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.headerLogo.left > a > img", "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed");
        }

        public void VerifyOnlineOffilineIndicator(Classes.Browser browserInstance)
        {
            // 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
            // 2. The online/offline indicator is displayed on the top left hand corner of the screen
            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step cannot be implemented. '2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen' - Cannot test this since we do not have the online class/indicator anymore.", LogWriter.eLogType.Error);
        }

        public void VerifyContactUsHelpMeLinks(Classes.Browser browserInstance)
        {
            // 3. Verify that contact us and help me hyperlinks are displayed
            // 3. The contact us and help me hyperlinks are displayed
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.contactUsContainer");
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.helpMeContainer");
        }

        public void VerifySpazaAliasAndName(Classes.Browser browserInstance)
        {
            // 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name  
            // 5. The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name    
            Helpers.Instance.CheckMultiSpazaPreferedAlias(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.aliasName.ng-binding");
            Helpers.Instance.CheckMultiSpazaName(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.spazaSection > div > span.spazaName > a");
        }

        public void VerifyMarbilAd(Classes.Browser browserInstance)
        {
            // 6. Verify that the Marbil add is displayed     
            // 6. The Marbil add is displayed  
            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step spelling incorrect, should be Marbil ad. '6. Verify that the Marbil add is displayed' - Update test case", LogWriter.eLogType.Error);
            Helpers.Instance.CheckMarbilAd(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.headerAdd.normalHeaderWidth > img");
        }

        public void VerifyBottomNavigationBlocks(Classes.Browser browserInstance)
        {
            // 8. Verify that the catalogue , basket, orders and favourites blocks are displayed
            // 8. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page  
            Helpers.Instance.CheckBottomNav(browserInstance, "body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div", "body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div",
                                           "body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div", "body > div.ui-footer.ng-scope > ul > li:nth-child(1) > div");
        }

        public void VerifyNotificationAndLabel(Classes.Browser browserInstance)
        {
            // 9. Verify that the Alert Notification and label are displayed       
            // 9. The Alert Notification and label are displayed  
            Helpers.Instance.CheckAlertNotification(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus",
                                                    "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div");
        }

        public void VerifyBasketTotal(Classes.Browser browserInstance)
        {
            // 10. Verify that the basket total value field is displayed
            // 10. The basket total value field is displayed   
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.itemCount.ng-binding");

            // 11. Verify that the basket label is displayed 
            // 11. The basket label is displayed 
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketLinkContainer > div.basketicon");

            // 12. Verify that the basket total amount of items field is displayed  
            // 12. The basket total amount field is displayed
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.basketStatus > div.basketValue.ng-binding");
        }

        public void VerifySearchField(Classes.Browser browserInstance)
        {
            // 13. Verify that the search field is displayed on the top right hand corner of the screen  
            // 13. The Search field is displayed     
            Helpers.Instance.Exists(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > input");

            // 14. Verify the text in the search field, it states that I am looking for    
            // 14. The text that is displayed within the field I am looking for 
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.bottomRow.vodaBackgroundRed > div > div.searchInputContainer > div > input");

            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step the value with the search is 'I 'm looking for ...'. '14. Verify the text in the search field, it states that I am looking for' - Please update the test case.", LogWriter.eLogType.Error);

            browserInstance.Instance.Assert.True(() => searchInput.Element.Attributes.Get("placeholder") == "I'm looking for...");

            // 15. Verify that the search text field is editable 
            // 15. The search text field is editable 
            Helpers.Instance.FieldInput(browserInstance, searchInput, "TEST");
            browserInstance.Instance.Assert.True(() => searchInput.Element.Text == "TEST");
        }

        public void VerifyNotificationSection(Classes.Browser browserInstance)
        {
            // 16. Verify that the notification section is displayed
            // 16. The Notification section is displayed    
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentHeader > div");
        }

        public void VerifyActionSection(Classes.Browser browserInstance)
        {
            // 17. Verify that actions section in the screen is displayed with an notification exclamation   
            // 17. Verify that actions are displayed section and label is displayed  
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.subtitle > ul.alerts > li");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.subtitle > span");
        }

        public void VerifyOrderAlerts(Classes.Browser browserInstance)
        {
            // 18. Verify that  Order alerts label is displayed 
            // 18. The order label is displayed     
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > span");

            // 19. Verify that the Order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync  
            // 19. The order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync     
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > label");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > label");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > label");

            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step the labels do not match the test case. '19. Verify that the Order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync' - Labels on the page different to the test case labels", LogWriter.eLogType.Error);

            var newInovoicesLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > label");
            var unconfirmedOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > label");
            var catalogueOutOfOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > label");

            browserInstance.Instance.Assert.True(() => newInovoicesLabel.Element.Text == "You have received new invoices");
            browserInstance.Instance.Assert.True(() => unconfirmedOrderLabel.Element.Text == "you have an unconfirmed order");
            browserInstance.Instance.Assert.True(() => catalogueOutOfOrderLabel.Element.Text == "your catalogue is out of sync");
        }

        public void VerifySystemAlerts(Classes.Browser browserInstance)
        {
            // 20. Verify that the system alerts label is displayed
            // 20. The system alerts label is displayed  
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > span");

            // 21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed  
            // 21. The system alerts label is displayed, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed     
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > label");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > label");

            LogWriter.Instance.Log("TESTCASE:_01_AlertsLandingPageVerfication -> Test step we only have to sub items here and the labels do not match the test case. '21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed' - Update test case", LogWriter.eLogType.Error);

            var manageYourCatalogueLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > label");
            var connactionIssuesOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > label");
            var changeActiveSpazaLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(3) > label");

            browserInstance.Instance.Assert.True(() => manageYourCatalogueLabel.Element.Text == "manage your catalogue");
            browserInstance.Instance.Assert.True(() => connactionIssuesOrderLabel.Element.Text == "you have connection issues");
            browserInstance.Instance.Assert.True(() => changeActiveSpazaLabel.Element.Text == " change active spaza");
        }

        public void VerifySideButtons(Classes.Browser browserInstance)
        {
            // 22. Verify that the following buttons are displayed on the screen, view invoices button, confirm now button, sync now button, manage button, diagnose button and change now  
            // 22. The notifications buttons are displayed
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > button");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > button");
        }

        public void VerifyUrgentActions(Classes.Browser browserInstance)
        {
            AddUnconfirmedOrder(browserInstance);
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a > div.alertredicon"), TimeSpan.FromMinutes(30));
        }

        public void AddUnconfirmedOrder(Classes.Browser browserInstance)
        {
            // 1.1 Perform the order process and don't confirm order  
            //    1.1.1 Add special to basket
            Helpers.Instance.AddSpecialToBasket(browserInstance);
            //    1.1.2 Go to basket
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp/#/basket-catalog-view"));
            //    1.1.3 In the basket view click the order now button
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#brandStore > div.basketbody > div.leftBlock > div > div > div > div > div > ul > li > div.orderNow > button"));
            Thread.Sleep(3000);
            //    1.1.4 After the popup has appeared click the Yes Order button
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Exists("#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)"), TimeSpan.FromMinutes(30));
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#orderNow > div > div > div.modal-body.text-center > div > button:nth-child(1)"));
            Thread.Sleep(3000);
            //    1.1.5 Wait until the alerts icon changes to red
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "body > div:nth-child(1) > div > div > ng-include > div > div > div.statusElements.left > div.topRow > div.alertStatus > a"));
        }

        public void VerifyTextHighlightedRed(Classes.Browser browserInstance, string labelPath)
        {
            Helpers.Instance.CheckClass(browserInstance, "", Helpers.Instance.GetProxy(browserInstance, labelPath));
        }

        public void VerifyConfirmNowButtonClick(Classes.Browser browserInstance)
        {
            // Test:   2. Click on the <confirm now> button
            // Output: 2. The notification confirm now page is displayed
            ClickAlertButton(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > button");
            browserInstance.Instance.WaitUntil(() => Helpers.Instance.Exists(browserInstance, "#alertsView > div.contentBody > div.leftBlock > table > tbody:nth-child(1) > tr:nth-child(1) > td"), TimeSpan.FromMinutes(30));
            LogWriter.Instance.Log(@"TESTCASE:_04_AlertsConfirmNow -> Dear Tony, Is there a way to check if the url contains some text, this is for when the url contains a query string which we do not know its value before hand. 
                                    '2. Click on the <confirm now> button' - Please update the test case.", LogWriter.eLogType.Error);

        }

        public void VerifyAsyncNow(Classes.Browser browserInstance)
        {
            //Test:   3. Click on the <sync now> button
            //Output: 3. The progress bar is  displayed  to show that an update to the catalogue is in progress    and the application becomes in-active 
            Helpers.Instance.CheckButtonEnabled(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > button"));
            Helpers.Instance.Exists(browserInstance, "#loading-wating-messages");
        }

        public void ClickAlertButton(Classes.Browser browserInstance, string buttonPath)
        {
            Helpers.Instance.CheckButtonEnabled(browserInstance, buttonPath);
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, buttonPath));
        }

        public void VerifyManageButtonClick(Classes.Browser browserInstance)
        {
            // 2. Click on the <manage> button 
            // 2. The manage your catalogue actions page is displayed
            ClickAlertButton(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > button");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/activation-managecatalogue"), TimeSpan.FromMinutes(30));
        }

        public void VeriftyManageCatalogueSearch(Classes.Browser browserInstance)
        {
            // 3. Search returning one or multiple results
            // 3.1 Enter the allowable wholesaler <Makro>  in search field which  give any results and verify the user interface
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "#searchCatalog");

            Helpers.Instance.FieldInput(browserInstance, searchInput, "Makro");
            browserInstance.Instance.Assert.True(() => searchInput.Element.Text == "Makro");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div.formRow.catalogsearch > button"));
            Helpers.Instance.Exists(browserInstance, "#accordion > div > div.title.rightarrow.catalog1.downarrow");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(1) > div.title.rightarrow.catalog1"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
        }

        public void VeriftyManageExpandableArrows(Classes.Browser browserInstance)
        {
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "#searchCatalog");

            /// 4. Verify groupings arrows are expandable    
            Helpers.Instance.FieldInput(browserInstance, searchInput, "");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div.formRow.catalogsearch > button"));
            Thread.Sleep(3000);

            /// 4.1 Select 0 - 25km arrow  and expand it   
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(1)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
            Thread.Sleep(3000);

            /// 4.2 Select 25 - 50km arrow and expand it 
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(2)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog2"));
            Thread.Sleep(3000);

            /// 4.3 Select  50 - 75km arrow and expand it  
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(3)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "catalog3"));
            Thread.Sleep(3000);

            /// 4.4 Select  75 - 100km arrow and expand it   
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(4)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog4"));
            Thread.Sleep(3000);
        }

        public void VeriftyManageExpandableWholesalerSelect(Classes.Browser browserInstance)
        {
            // 5. Select one store from each range                                          
            // 5.2 Select 25 - 50km  and select one wholesaler under that range by checkbox   
            // 5.3 Select 50 - 75km  and select one wholesaler under that range by checkbox   
            // 5.4 Select 75 - 100km  and select one wholesaler under that range by checkbox 
            Helpers.Instance.Exists(browserInstance, "#searchCatalog");
            var searchInput = Helpers.Instance.GetProxy(browserInstance, "#searchCatalog");

            // 5. Select one store from each range      
            Helpers.Instance.FieldInput(browserInstance, searchInput, "");
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock.managecatalogue.width862px > form:nth-child(2) > div > div.formRow.catalogsearch > button"));
            Thread.Sleep(3000);

            // 5.1 Select 0 - 25km  and select one wholesaler under that range by checkbox  
            /// 5.1 The selected wholesaler is displayed with a checkbox next to it
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(1)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
            Thread.Sleep(3000);

            // 5.2 Select 25 - 50km  and select one wholesaler under that range by checkbox 
            // 5.2  Select 25 - 50km  and select one wholesaler under that range by checkbox
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(2)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog2"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
            Thread.Sleep(3000);

            // 5.3 Select 50 - 75km  and select one wholesaler under that range by checkbox 
            // 5.3  Select 50 - 75km  and select one wholesaler under that range by checkbox
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(3)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "catalog3"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
            Thread.Sleep(3000);

            // 5.4 Select 75 - 100km  and select one wholesaler under that range by checkbox  
            // 5.4  Select 50 - 75km  and select one wholesaler under that range by checkbox
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#accordion > div:nth-child(4)"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog4"));
            Helpers.Instance.CheckClass(browserInstance, "collapse in", Helpers.Instance.GetProxy(browserInstance, "#catalog1"));
            Thread.Sleep(3000);
        }

        public void VerifyDiagnoseButtonClick(Classes.Browser browserInstance)
        {
            // 2. Click on the <manage> button 
            // 2. The manage your catalogue actions page is displayed
            ClickAlertButton(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > button");
            browserInstance.Instance.WaitUntil(() => browserInstance.Instance.Assert.Url("http://aspnet.dev.afrigis.co.za/bopapp/#/alerts-diagnose"), TimeSpan.FromMinutes(30));
        }

        public void VerifyDiagnoseCheckingNotificationLabels(Classes.Browser browserInstance)
        {
            // 3. Verify the diagnose connection actions notification screen                                                                                            
            // 3.1 Verify Checking Connection label is available, checking vital communication to the ordering process
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > span");
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > label");
        }

        public void VerifyDiagnoseCheckingNotificationTestButton(Classes.Browser browserInstance)
        {
            // 3.2 Verify that the test now button for checking connection is available 
            // 3.2  The test now checking connection button is displayed on the screen
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > button");
        }

        public void VerifyDiagnoseConnectionSpeedTestButton(Classes.Browser browserInstance)
        {
            // 3.3 Verify that the test now button for checking connection speed is available
            // 3.3 The test now button for checking connection speed is available
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > ul > li > button");
        }

        public void VerifyDiagnoseCheckingNotificationTestButtonClick(Classes.Browser browserInstance)
        {
            // 4.1 Click on check connection <test now> button
            // 4.1 An indicator is displayed, that determines whether the connection was good or bad
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > button"));
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > div:nth-child(1) > ul > li > span");
        }

        public void VerifyDiagnoseConnectionSpeedTestButtonClick(Classes.Browser browserInstance)
        {
            // 4.2 Click on the on the connection speed <test now>button  
            // 4.2 Displayed results are determined by the application whether the connection speed is poor or not
            Helpers.Instance.ClickButton(browserInstance, Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > div > ul > li > button"));
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(3) > div.resultSection");
        }
        public void VerifyDiagnoseResultPlaceholder(Classes.Browser browserInstance)
        {
            // 3.4 Verify that the results place holder label is available, displaying the results of the test 
            // 3.4 The results place holder label is available 
            Helpers.Instance.Exists(browserInstance, "#alertsView > div.leftBlock > div:nth-child(3) > div > span");
        }
    }
}
