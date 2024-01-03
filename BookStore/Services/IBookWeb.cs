using BookStore.Model;

namespace BookStore.Services
{
    public interface IBookWeb
    {
        void RatingBook(int bookid,int rating);
        void reviewBook(int bookid,string review, Customer customer);
        IEnumerable<Book> ViewBookByCatgory(string Catgory);
        IEnumerable<Book> RecommendationBook();

        Book GetBookById(int id);
    }
}
