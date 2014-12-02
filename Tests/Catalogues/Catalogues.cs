using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProj.Tests.Catalogues
{
    [TestFixture, Description("Catalogues"), Category("Catalogues")]
    public class Catalogues
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

            container.RegisterType<Interfaces.ICataloguesActions, Tests.Catalogues.CataloguesActions>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<Classes.Timer>(), new InterceptionBehavior<Classes.ScreenCapture>());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            browserInstance.Instance.Dispose();
            container.RemoveAllExtensions();
            container.Dispose();
        }

        /// <summary>
        /// TEST: SPLASH/INTERSTIAL ADVERT
        /// Test Case ID: 18_FRS_Ref_5.2.1
        /// Category: Catalogues
        /// Feature: Interstial Advert
        /// Pre-Condition: None
        /// Environment: Any Landing Page
        /// TEST STEPS:
        /// 1. Verify that  the advert is displayed on a full page and also for 5 seconds   
        /// 2. Verify that the advert  is clickable  
        /// 3. Verify that the advert redirects user to the relevant product catalogue that is advertised                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        /// TEST OUTPUT:
        /// 1. The Interstitial advert is displayed for 5 seconds on a full screen
        /// 2.The Advert is clickable     
        /// 3. The splash ad redirects the user to the relevant product after clicking it     
        /// </summary>
        [Test, Description("_01_SplashInterstialAdvert"), Category("Catalogues"), Repeat(1)]
        public void _01_SplashInterstialAdvert()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }

        /// <summary>
        /// TEST: CATALOGUES LANDING PAGE
        /// Test Case ID: 19_FRS_Ref_5.2.1
        /// Category: Catalogues
        /// Feature: Catalogues
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1.Click on the catalogues block at the bottom     
        /// Verify that the catalogues landing page is displayed with the following                                                                                                        
        /// 2. Verify that the available subcategories from catalogues are displayed and active  
        /// 3. Verify that the unavailable sub categories must be displayed and greyed out   
        /// 4. Verify that the list of catalogues is displayed and user is able to scroll left to right      
        /// 5. Verify that the category icons are fixed on top of the page                                                               
        /// o Bakery
        /// o Fruit & Vegetables
        /// o Dairy & Eggs
        /// o Meat, Fish & Poultry
        /// o Frozen
        /// o Tins, Jars and Cooking
        /// o Packets and Cereals
        /// o Baking
        /// o Coffee, Teas and Creamers
        /// o Snacks, Sweets & Biscuits
        /// o Drinks
        /// o Household
        /// o Baby
        /// o Health and Beauty 
        /// o Other   
        /// 7. Verify that brands are displayed and user can scroll from left to right  
        /// 8. Verify that specials are displayed and user can scroll from left to right                                                                                                                                                                                                                                                                                                                                                                
        /// TEST OUTPUT:
        /// 1. The catalogues page is displayed with catalogues that were already added activation setup     
        /// 2. The available subcategories are available and displayed        
        /// 3.  The unavailable subcategories are  displayed and made inactive  by greying out their colour 
        /// 4. The list of catalogue icons are displayed and user is able to scroll form left to right 
        /// 5.  The Categories are fixed and the list as follows                                                                                                                                  
        /// o Bakery
        /// o Fruit & Vegetables
        /// o Dairy & Eggs
        /// o Meat, Fish & Poultry
        /// o Frozen
        /// o Tins, Jars and Cooking
        /// o Packets and Cereals
        /// o Baking
        /// o Coffee, Teas and Creamers
        /// o Snacks, Sweets & Biscuits
        /// o Drinks
        /// o Household
        /// o Baby
        /// o Health and Beauty 
        /// o Other   
        /// 7.The brands are displayed and you can scroll from left to right  
        /// 8. The specials are displayed on the left hand side of the landing page and user can scroll from left to right                                                                                                        
        /// </summary>
        [Test, Description("_02_CatalogueLandingPage"), Category("Catalogues"), Repeat(1)]
        public void _02_CatalogueLandingPage()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }

        /// <summary>
        /// TEST: CATALOGUES VIEW SUB MENU
        /// Test Case ID: 20_FRS_Ref_5.2.1
        /// Category: Catalogues
        /// Feature: Catalogues
        /// Pre-Condition: None
        /// Environment: Catalogues
        /// TEST STEPS:
        /// 1.Verify that sub categories are scrollable up and down                                                              
        /// 1.1 Select on any of the fixed categories        
        /// 1.2 Make sure that you can scroll up and down on that list     
        /// 1.3 Scroll down and select any subcategory  
        /// 1.4 Click on the category and verify if it collapses the sub categories list   
        /// TEST OUTPUT:
        /// 1                                                                                                                                                                                                                  
        /// 1.1 Subcategories under that category are displayed as a list and the selected category is displayed in red
        /// 1.2 The user can scroll up and down on the sub categories 
        /// 1.3  The selected subcategory is displayed in red  
        /// 1.4  The  subcategories list is collapsed
        /// </summary>
        [Test, Description("_03_CatalogueViewSubMenu"), Category("Catalogues"), Repeat(1)]
        public void _03_CatalogueViewSubMenu()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }

        /// <summary>
        /// TEST: CATALOGUES VIEW PRODUCTS
        /// Test Case ID: 21_FRS_Ref_5.2.1
        /// Category: Catalogues
        /// Feature: Catalogues
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1 Select on any of the fixed categories  
        /// 2 Make sure that you can scroll up and down on that list   
        /// 3 Scroll down and select any subcategory 
        /// 4 Click on the selected subcategory you wish to view products for  
        /// 5  Verify the following on the product view                                                                                  
        /// 5.1 Verify that the product icon is displayed  
        /// 5.2 Verify that the product price is displayed 
        /// 5.3 Verify that the <buy now> button is available 
        /// 5.4 Verify that the product description is displayed                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        /// TEST OUTPUT:
        /// 1 Subcategories under that category are displayed as a list and the selected category is displayed in red     
        /// 2 The user can scroll up and down on the sub categories 
        /// 3  The selected subcategory is displayed in red     
        /// 4   The selected subcategory products are displayed   
        /// 5                                                                                                                                                                                                               
        /// 5.1 The product icon is displayed     
        /// 5.2 The product price is displayed      
        /// 5.3 The buy now button is displayed  
        /// 5.4 The product description is displayed    
        /// </summary>
        [Test, Description("_04_CatalogueViewProducts"), Category("Catalogues"), Repeat(1)]
        public void _04_CatalogueViewProducts()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }

        /// <summary>
        /// TEST: CATALOGUES VIEW PRODUCT DETAIL
        /// Test Case ID: 22_FRS_Ref_5.1.3
        /// Category: Catalogues
        /// Feature: Catalogues
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1 Select on any of the fixed categories   
        /// 2 Make sure that you can scroll up and down on that list  
        /// 3 Scroll down and select any subcategory                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        /// 4 Click on the selected subcategory you wish to view products for  
        /// 5  Verify the following on the product view                                                                                        
        /// 5.1 Verify that the product icon is displayed   
        /// 5.2 Verify that the product price is displayed    
        /// 5.3 Verify that the <buy now> button is available   
        /// 5.4 Verify that the product description is displayed  
        /// 6. Click on the product     
        /// 7. Verify that the product view screen                                                                                            
        /// 7.1 Verify that the product description is displayed   
        /// 7.2 Verify that product unit price is displayed  
        /// 7.3 Verify that the edit buttons are available for adding and removing products quantity 
        /// 7.4 Verify that the quantity field is displayed and not editable  
        /// 7.5 Verify that the total price field is displayed and not editable  
        /// 7.6  Verify that the favourite icon represented by a star with a plus sign  is displayed
        /// 7.7 Verify that the save button is displayed                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        /// TEST OUTPUT:
        /// 1 Subcategories under that category are displayed as a list and the selected category is displayed in red     
        /// 2 The user can scroll up and down on the sub categories  
        /// 3 The selected subcategory is displayed in red   
        /// 4   The selected subcategory products are displayed    
        /// 5                                                                                                                                                                                                               
        /// 5.1 The product icon is displayed    
        /// 5.2 The product price is displayed   
        /// 5 .3 The buy now button is displayed   
        /// 5. 4 The product description is displayed      
        /// 6. The product view screen is displayed  
        /// 7.                                                                                                                                                                                                                 
        /// 7.1 The product description is displayed  
        /// 7.2 The product unit price is displayed  
        /// 7.3  The edit buttons are display
        /// 7.4 The quantity field is displayed and is not editable  
        /// 7.5  The total price field is displayed and not editable  
        /// 7.6  A star with a plus sign is displayed 
        /// 7.6  A star with a plus sign is displayed 
        /// </summary>
        [Test, Description("_05_CatalogueViewProductDetail"), Category("Catalogues"), Repeat(1)]
        public void _05_CatalogueViewProductDetail()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }

        /// <summary>
        /// TEST: CATALOGUES VIEW ADD PRODUCT TO FAVOURITES
        /// Test Case ID: 22_FRS_Ref_5.1.3
        /// Category: Catalogues
        /// Feature: Catalogues
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1.Select on any of the fixed categories  
        /// 2. Make sure that you can scroll up and down on that list   
        /// 3. Scroll down and select any subcategory   
        /// 4. Click on the selected subcategory you wish to view products for     
        /// 5. Click on the product   
        /// 6. On the product view screen click on the favourites icon which is represented by a star and save                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        /// 7. Verify step 6 by selecting the favourites tab, to see if the recently added product is displayed                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        /// TEST OUTPUT:
        /// 1. Subcategories under that category are displayed as a list and the selected category is displayed in red 
        /// 2. The user can scroll up and down on the sub categories   
        /// 3. The selected subcategory is displayed in red    
        /// 4.   The selected subcategory products are displayed    
        /// 5. The product view screen is displayed
        /// 6.  The product is saved to favourites                                                                                                                                                                                        
        /// 7.The recently added product is displayed in the favourites menu
        /// </summary>
        [Test, Description("_06_CatalogueViewAddProductToFavourites"), Category("Catalogues"), Repeat(1)]
        public void _06_CatalogueViewAddProductToFavourites()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }

        /// <summary>
        /// TEST: CATALOGUES VIEW ADDING AND REMOVING PRODUCT QUANTITY
        /// Test Case ID: 22_FRS_Ref_5.1.3
        /// Category: Catalogues
        /// Feature: Catalogues
        /// Pre-Condition: None
        /// Environment: Application landing page
        /// TEST STEPS:
        /// 1.Select on any of the fixed categories   
        /// 2. Make sure that you can scroll up and down on that list    
        /// 3. Scroll down and select any subcategory  
        /// 4. Click on the selected subcategory you wish to view products for 
        /// 5. Click on the product     
        /// 6. On the product view screen  click on the - sign for removing and + adding quantity and save   
        /// 7. verify the formula used for adding and removing product quantity                                       
        /// 7.1 Total price = Unit price * Quantity, while adding and removing products make sure that the total is correct
        /// TEST OUTPUT:
        /// 1. Subcategories under that category are displayed as a list and the selected category is displayed in red   
        /// 2. The user can scroll up and down on the sub categories   
        /// 3. The selected subcategory is displayed in red   
        /// 4.   The selected subcategory products are displayed  
        /// 5. The product view screen is displayed     
        /// 6.  The quantity addition button are working as expected      
        /// 7.                                                                                                                                                                                                                         
        /// 7.1 The total is correct      
        /// </summary>
        [Test, Description("_07_CatalogueViewAddingAndRemovingProductQuantity"), Category("Catalogues"), Repeat(1)]
        public void _07_CatalogueViewAddingAndRemovingProductQuantity()
        {
            browserInstance.Navigate(new Uri("http://aspnet.dev.afrigis.co.za/bopapp"));
            Interfaces.ICataloguesActions catalogueActions = container.Resolve<Interfaces.ICataloguesActions>();
            //TODO
        }
    }
}
