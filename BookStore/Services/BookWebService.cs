using BookStore.Model;

namespace BookStore.Services
{
    public class BookWebService : IBookWeb
    {
        public int BuyBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void CheckOut()
        {
            throw new NotImplementedException();
        }

        public Book GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetBookBySearch(string name)
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
