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
        int BuyBook(int bookid, int customerID);
       int  DeleteBook(int bookid, int customerID);
        void CheckOut(int customerID);
        IEnumerable<ShoppingCart> ViewShoppingCart(int customerID);
    }
}
