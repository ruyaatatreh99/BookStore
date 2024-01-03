using BookStore.Model;

namespace BookStore.Services
{
    public class BookWebService : IBookWeb
    {
        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public void RatingBook(int bookid, int rating)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> RecommendationBook()
        {
            throw new NotImplementedException();
        }

        public void reviewBook(int bookid, string review, Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> ViewBookByCatgory(string Catgory)
        {
            throw new NotImplementedException();
        }
    }
}
