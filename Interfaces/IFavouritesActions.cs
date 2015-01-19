namespace TestProj.Interfaces
{
    public interface IFavouritesActions
    {
        void VerifyFavouriteIconClick(Classes.Browser browserInstance);
        void VerifyDeleteIcon(Classes.Browser browserInstance);
        void VerifyClearAllButtonClick(Classes.Browser browserInstance);
        void VerifyListViewButton(Classes.Browser browserInstance);
        void VerifyListViewClick(Classes.Browser browserInstance);
        void VerifyListViewProductClick(Classes.Browser browserInstance);
        void VerifyGridViewButton(Classes.Browser browserInstance);
        void VerifyGridViewButtonClick(Classes.Browser browserInstance);
        void VerifyGridViewButtonOnListView(Classes.Browser browserInstance);
        void VerifyConfirmOrderPopup(Classes.Browser browserInstance);
        void VerifyGridViewProductClick(Classes.Browser browserInstance);
        void VerifyGridViewProductOrderDelete(Classes.Browser browserInstance);
        void VerifyGridViewClearAllButtonClick(Classes.Browser browserInstance);
        void VerifyListViewProductOrderDelete(Classes.Browser browserInstance);
        void VerifyListViewClearAllButtonClick(Classes.Browser browserInstance);
        void ClickFavouriteProduct(Classes.Browser browserInstance);
        void VerifyProductViewScreen(Classes.Browser browserInstance);
        FluentAutomation.ElementProxy ClickBasketProduct(Classes.Browser browserInstance, Interfaces.IBasketActions basketActions);
        void AddBasketProductToFavourites(Classes.Browser browserInstance);
        void VerifyAddFavouriteProduct(Classes.Browser browserInstance, FluentAutomation.ElementProxy productDescription);
    }
}
