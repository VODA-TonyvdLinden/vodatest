using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Alerts
{
    [TestFixture, Description("Alerts"), Category("Alerts")]
    public class Alerts
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

            container.RegisterType<Interfaces.IAlertsActions, Tests.Alerts.AlertsActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST: ALERTS  LANDING PAGE VERIFICATION
        /// Test Case ID: 8_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1. Verify that the Vodacom logo and the red banner are displayed on the activation screen
        /// 2. Verify that the online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. Verify that contact us and help me hyperlinks are displayed
        /// 4. See spelling, grammar and alignment  
        /// 5. Verify that the preferred alias name is displayed on top right hand corner of the app with the spaza owner's alias name and spaza name      
        /// 6. Verify that the Marbil add is displayed    
        /// 7. Verify that the sub application are displayed and also greyed out   
        /// 8. Verify that the catalogue , basket, orders and favourites blocks are displayed
        /// 9. Verify that the Alert Notification and label are displayed      
        /// 10. Verify that the basket total value field is displayed
        /// 11. Verify that the basket label is displayed
        /// 12. Verify that the basket total amount of items field is displayed  
        /// 13. Verify that the search field is displayed on the top right hand corner of the screen  
        /// 14. Verify the text in the search field, it states that I am looking for    
        /// 15. Verify that the search text field is editable      
        /// 16. Verify that the notification section is displayed
        /// 17. Verify that actions section in the screen is displayed with an notification exclamation     
        /// 18. Verify that  Order alerts label is displayed 
        /// 19. Verify that the Order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync      
        /// 20. Verify that the system alerts label is displayed  
        /// 21.Veriy that the system alerts label, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed    
        /// 22. Verify that the following buttons are displayed on the screen, view invoices button, confirm now button, sync now button, manage button, diagnose button and change now   
        /// TEST OUTPUT:
        /// 1. The Vodacom banner logo and banner are displayed 
        /// 2. The online/offline indicator is displayed on the top left hand corner of the screen
        /// 3. The contact us and help me hyperlinks are displayed
        /// 4. Spelling, Grammar and alignment correct (Screen should resize on all devices also able to rotate from Portrait to landscape)  
        /// 5. The preferred alias name is displayed on the top right hand corner of the app, with the name spaza owner's alias name and the spaza name   
        /// 6. The Marbil add is displayed  
        /// 7. The Sub Applications are displayed with a grey colour to show they are not active
        /// 8. The catalogue, basket, orders and favourites blocks are displayed on the bottom of the bottom of the page    
        /// 9. The Alert Notification and label are displayed  
        /// 10. The basket total value field is displayed   
        /// 11. The basket label is displayed 
        /// 12. The basket total amount field is displayed
        /// 13. The Search field is displayed     
        /// 14. The text that is displayed within the field I am looking for 
        /// 15. The search text field is editable
        /// 16. The Notification section is displayed    
        /// 17. Verify that actions are displayed section and label is displayed  
        /// 18. The order label is displayed     
        /// 19. The order Alerts label, has sub labels namely You have received new invoices, you have an unconfirmed order, your catalogue is out of sync  
        /// 20. The system alerts label is displayed    
        /// 21. The system alerts label is displayed, has sub labels namely manage your catalogue, you have connection issues and change active spaza label is displayed    
        /// 22. The notifications buttons are displayed
        /// </summary>
        [Test, Description("_01_AlertsLandingPageVerfication"), Category("Alerts"), Repeat(1)]
        public void _01_AlertsLandingPageVerfication()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS  POLLING SERVICE
        /// Test Case ID: 8_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1. verify that the application places urgent actions  on the alerts page
        /// NB. In order to achieve this you need to follow the order process and then don't confirm order "
        /// 1.1 Perform the order process and don't confirm order  
        /// 1.2  Wait for approximately 30 min, to see if the alert is displayed
        /// 1.3  Confirm the Order on alerts page and verify if its not back after 30 min
        /// 1.4 Repeat step 1.1 for all processes that requires confirmation to see if polling service work   
        /// TEST OUTPUT:
        /// 1                                                                                                                                                                                                                   
        /// NB. In order to achieve this you need to follow the order process and then don't confirm order
        /// 1.1 The Order process is performed but the order is not confirmed   
        /// 1.2 This confirms that the polling service works, when an alert is displayed 
        /// 1.3 Then the confirmed order is no longer displayed on the alerts action    
        /// 1.4 The polling service functioning as expected  
        /// </summary>
        [Test, Description("_02_AlertsPollingService"), Category("Alerts"), Repeat(1)]
        public void _02_AlertsPollingService()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS VIEW INVOICES
        /// Test Case ID: 9_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1.Verify that the active alert's text  is highlighted red "You have received new invoices"    
        /// 2. Click on the <view invoices> button 
        /// 3  Click on the <view orders> button 
        /// 4. view invoice for orders page verification                                                                                         
        /// 4.1 Verify that the invoice order number is displayed     
        /// 4.2 Verify invoice number, supplier, invoice date and value are displayed in a tabular format   
        /// 4.3  Verify that the total value column of invoice is displayed on the page  
        /// 5. Click on the back on the <back to actions> button 
        /// TEST OUTPUT:
        /// 1. The " You have received new invoices" text is highlighted in red 
        /// 2. The notification invoices page is displayed and the new Invoice flag is cleared    
        /// 3.  The list of invoices for all orders requiring users attention is displayed
        /// 4.                                                                                                                                                                                                                     
        /// 4.1 The invoices number, supplier, invoice date and value are displayed a tabular format  
        /// 4.2  The required columns are displayed with data   
        /// 4.3 The total value column is displayed  
        /// 5. The user is returned to the alerts notification page
        /// </summary>
        [Test, Description("_03_AlertsViewInvoices"), Category("Alerts"), Repeat(1)]
        public void _03_AlertsViewInvoices()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS  CONFIRM NOW 
        /// Test Case ID: 10_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1.Verify that the active alert's text  is highlighted red "You have an unconfirmed order "   
        /// 2. Click on the <confirm now> button
        /// 3. Click on the <confirm  order> button 
        /// 4. view confirm order  page verification                                                                                              
        /// 4.1 Verify that the unconfirmed order number is displayed and is the only one
        /// 4.2 Verify item code, name, brand, pack size, price, qty, total and value are displayed
        /// 5. Click on the back on the <back to actions> button  
        /// 6. Click on the <back to orders> button and order basket still intact 
        /// 7. Click on <back to actions> button    
        /// TEST OUTPUT:
        /// 1. The " You have an unconfirmed order " text is highlighted in red   
        /// 2. The notification confirm now page is displayed     
        /// 3.  The list of unconfirmed orders requiring users attention is displayed
        /// 4.1 The invoices number, supplier, invoice date and value are displayed a tabular format  and is the only one at a time      
        /// 4.2 The required number of columns are displayed with data 
        /// 5. The user is returned to the alerts active page 
        /// 6. The orders page is displayed  and there are no changes to the basket   
        /// 7. The user is returned to the alerts notification page
        /// </summary>
        [Test, Description("_04_AlertsConfirmNow"), Category("Alerts"), Repeat(1)]
        public void _04_AlertsConfirmNow()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS  SYNC NOW
        /// Test Case ID: 11_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1.Verify that the active alert's text  is highlighted red "You catalogue is out of sync"   
        /// 2. Verify if the application is online, in order to update the catalogue   
        /// 3. Click on the <sync now> button  
        /// TEST OUTPUT:
        /// 1. The " Your catalogue is out of sync " text is highlighted in red   
        /// 2. The application is online
        /// 3. The progress bar is  displayed  to show that an update to the catalogue is in progress    and the application becomes in-active  
        /// </summary>
        [Test, Description("_05_AlertsSyncNow"), Category("Alerts"), Repeat(1)]
        public void _05_AlertsSyncNow()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS MANAGE CATALOGUE
        /// Test Case ID: 12_FRS_Ref_5.1.5
        /// Category: Setup_Catalogue
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1.Verify that the active alert's text  is highlighted red "Manage your Catalogue "   
        /// 2. Click on the <manage> button     
        /// 3. Search returning one or multiple results                                                                                                            
        /// 3.1 Enter the allowable wholesaler <Makro>  in search field which  give any results and verify the user interface  
        /// 4. Verify groupings arrows are expandable                                                                                                                              
        /// 4.1 Select 0 - 25km arrow  and expand it         
        /// 4.2 Select 25 - 50km arrow and expand it 
        /// 4.3 Select  50 - 75km arrow and expand it  
        /// 4.4 Select  75 - 100km arrow and expand it   
        /// 5.  Select one store from each range                                                                                                  
        /// 5.1 Select 0 - 25km  and select one wholesaler under that range by checkbox      
        /// 5.2 Select 25 - 50km  and select one wholesaler under that range by checkbox   
        /// 5.3 Select 50 - 75km  and select one wholesaler under that range by checkbox   
        /// 5.4  Select 75 - 100km  and select one wholesaler under that range by checkbox  
        /// 5.5 Press the <update> button  
        /// TEST OUTPUT:
        /// 1. The " Manage your catalogue " text is highlighted in red   
        /// 2. The manage your catalogue actions page is displayed     
        /// 3.                                                                                                                                                                                                                  
        /// 3.1 The wholesaler records which are found are displayed as catalogue – outlet name  on the screen, with a location list sorted by group and grouped into groups on increments 25km    
        /// 4.                                                                                                                                                                                                                     
        /// 4.1 The 0 - 25km is expanded and also displaying stores within that distance proximity     
        /// 4.2 The 25 - 50km is expanded and also displaying stores within that distance proximity   
        /// 4.3 The 50 - 75km is expanded and also displaying stores within that distance proximity  
        /// 4.4 The 75 - 100km is expanded and also displaying stores within that distance proximity   
        /// 5.                                                                                                                                                                                                                 
        /// 5.1 The selected wholesaler is displayed with a checkbox next to it   
        /// 5.2  Select 25 - 50km  and select one wholesaler under that range by checkbox 
        /// 5.3  Select 50 - 75km  and select one wholesaler under that range by checkbox   
        /// 5.4  Select 50 - 75km  and select one wholesaler under that range by checkbox  
        /// 5.5   The Application Landing Page is Displayed    
        /// </summary>
        [Test, Description("_06_AlertsManageCatalogue"), Category("Setup_Catalogue"), Repeat(1)]
        public void _06_AlertsManageCatalogue()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS  DIAGNOSE
        /// Test Case ID: 13_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1.Verify that the active alert's text  is highlighted red "You have connection issues "  
        /// 2. Click on the <diagnose> button   
        /// 3. Verify the diagnose connection actions notification screen                                                                                            
        /// 3.1 Verify Checking Connection label is available, checking vital communication to the ordering process
        /// 3.2 Verify that the test now button for checking connection is available   
        /// 3.3 Verify that the test now button for checking connection speed is available
        /// 4. Check Button Functionality                                                                                                               
        /// 4.1 Click on check connection <test now> button
        /// 4.2 Click on the on the connection speed <test now>button                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        /// 3.4 Verify that the results place holder label is available, displaying the results of the test 
        /// 3.5 Verify that the <OK> button is available  
        /// TEST OUTPUT:
        /// 1. The " You have connection issues" text is highlighted in red     
        /// 2. The notification diagnose connection page is displayed    
        /// 3.                                                                                                                                                                                                            
        /// 3.1 The Checking Connection label is available, checking vital communication to the ordering service, connection speed, test your speed connection now  are displayed on the diagnose connection screen  
        /// 3.2  The test now checking connection button is displayed on the screen  
        /// 3.3 The test now button for checking connection speed is available    
        /// 4                                                                                                                                                                                                                  
        /// 4.1 An indicator is displayed, that determines whether the connection was good or bad 
        /// 4.2 Displayed results are determined by the application whether the connection speed is poor or not
        /// 3.4 The results place holder label is available   
        /// 3.5 The OK button is displayed     
        /// </summary>
        [Test, Description("_07_AlertsDiagnose"), Category("Alerts"), Repeat(1)]
        public void _07_AlertsDiagnose()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }

        /// <summary>
        /// TEST: ALERTS  MESSAGING
        /// Test Case ID: 15_FRS_Ref_5.1.5
        /// Category: Alerts
        /// Feature: Alerts
        /// Pre-Condition: None
        /// Environment:Alerts Page
        /// TEST STEPS:
        /// 1.Verify Received Message Format  from MAS                                                                                                                                                
        /// 1.1 Verify that the received message has an expiry time     
        /// 1.2 Verify that the message is plain text  
        /// 1.3 Click on the <dismiss> button to clear message                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
        /// TEST OUTPUT:
        /// 1.                                                                                                                                                                                                                 
        /// 1.1 The Message is displayed with an expiry time    
        /// 1.2 The displayed message is plain text  
        /// 1.3 The Message is cleared                                                                                                                                               
        /// </summary>
        [Test, Description("_09_AlertsMessaging"), Category("Alerts"), Repeat(1)]
        public void _09_AlertsMessaging()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.IAlertsActions alertActions = container.Resolve<Interfaces.IAlertsActions>();
            //TODO
        }
    }
}
