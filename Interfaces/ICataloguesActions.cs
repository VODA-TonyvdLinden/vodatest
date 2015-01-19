using System;
namespace TestProj.Interfaces
{
    public interface ICataloguesActions
    {
        void VerifyInterstitialAdvert(Classes.Browser browserInstance);
        void VerifyInterstitialAdvertClick(Classes.Browser browserInstance);
        void VerifyCatalogueBlockClick(Classes.Browser browserInstance);
        void VerifyActiveCatalogueAndSubCategories(Classes.Browser browserInstance);
        void VerifyInActiveCatalogueAndSubCategories(Classes.Browser browserInstance);
        void VerifyCatalogueIconList(Classes.Browser browserInstance);
        void VerifyCategoryIcons(Classes.Browser browserInstance);
        void VerifyCategoryIcon(Classes.Browser browserInstance, string iconImagePath, string categoryItemPath,string categoryClassPath, string categoryName);
        void VerifyBrandList(Classes.Browser browserInstance);
        void VerifySpecials(Classes.Browser browserInstance);
        void VerifyCategoryClick(Classes.Browser browserInstance);
        void VerifySubCategoriesScroll(Classes.Browser browserInstance);
        void VerifyElementScroll(Classes.Browser browserInstance, FluentAutomation.ElementProxy scrollElement, bool isVertical);
        void VerifyActiveSubCategorySelect(Classes.Browser browserInstance);
        void VerifyCategoryUnSelect(Classes.Browser browserInstance);
        void VerifySubCategoryClick(Classes.Browser browserInstance);
        void VerifyProductView(Classes.Browser browserInstance);
        void ClickSubCategory(Classes.Browser browserInstance, bool verifyProductPopUp = false);
        void VerifyProductItemClickPopup(Classes.Browser browserInstance);
        void VerifyFavouriteBlockClick(Classes.Browser browserInstance);
        void VerifyProductSaveClick(Classes.Browser browserInstance);
        void VerifyProductClick(Classes.Browser browserInstance);
        void VerifyProductFavouritesIconClick(Classes.Browser browserInstance);
        void VerifyProductQuantityClick(Classes.Browser browserInstance);
        void VerifyProductTotal(Classes.Browser browserInstance);
    }
}
