using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //var newInovoicesLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(1) > label");
            //var unconfirmedOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(2) > label");
            //var catalogueOutOfOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(1) > ul > li:nth-child(3) > label");

            //browserInstance.Instance.Assert.True(() => newInovoicesLabel.Element.Text == "You have received new invoices");
            //browserInstance.Instance.Assert.True(() => unconfirmedOrderLabel.Element.Text == "you have an unconfirmed order");
            //browserInstance.Instance.Assert.True(() => catalogueOutOfOrderLabel.Element.Text == "your catalogue is out of sync");
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

            //var manageYourCatalogueLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(1) > label");
            //var connactionIssuesOrderLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(2) > label");
            //var changeActiveSpazaLabel = Helpers.Instance.GetProxy(browserInstance, "#alertsView > div.leftBlock > div:nth-child(2) > ul > li:nth-child(3) > label");

            //browserInstance.Instance.Assert.True(() => manageYourCatalogueLabel.Element.Text == "manage your catalogue");
            //browserInstance.Instance.Assert.True(() => connactionIssuesOrderLabel.Element.Text == "you have connection issues");
            //browserInstance.Instance.Assert.True(() => changeActiveSpazaLabel.Element.Text == " change active spaza");
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
    }
}
