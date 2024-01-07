using BookStore.Model;

namespace BookStore.Services
{
    public interface IBookWeb
    {
        int RatingBook(int bookid,int rating);
        int reviewBook(int bookid,string review, int customerid);
        int BuyBook(int bookid, int customerID);
        int DeleteBook(int bookid, int customerID);
        IEnumerable<Book> ViewBookByCatgory(string Catgory);
        IEnumerable<Book> RecommendationBook();
        IEnumerable<Book> GetBookBySearch(string name);
        IEnumerable<Review> ViewBookReview(int bookid);
        IEnumerable<ShoppingCart> ViewShoppingCart(int customerID);
        Book GetBookById(int id);
        Customer GetCustomer(int Customerid);
        Customer GetCustomerbyemail(string email);
        Customer loginCustomer(string email, string encryptpassword);
        Customer reisterCustomer(string name, string phone, string email, string newpassword, string repeatepassword);
        int UpdateCustomer(int id, string name, string phone, string email, string newpassword, string repeatepassword);
        void CheckOut(int customerID);
       
    }
}
