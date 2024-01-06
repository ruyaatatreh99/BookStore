using BookStore.Model;

namespace BookStore.Services
{
    public interface IBookWeb
    {
        int RatingBook(int bookid,int rating);
        int reviewBook(int bookid,string review, int customerid);
        IEnumerable<Book> ViewBookByCatgory(string Catgory);
        IEnumerable<Book> RecommendationBook();
        Book GetBookById(int id);
        IEnumerable<Book> GetBookBySearch(string name);
        IEnumerable<Review> ViewBookReview(int bookid);
        int BuyBook(Book book,int customerID);
        void CheckOut(int customerID);
        IEnumerable<ShoppingCart> ViewShoppingCart(int customerID);
    }
}
