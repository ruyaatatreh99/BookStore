using System.Threading.Tasks;
using BookStore.Model;
namespace BookStore.Services
{
    public interface IBook
    {
        Book AddBook(Book book);
        Book UpdateBook(Book book, int id);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Order> GetAllOrder();
        Book GetBookById(int id);
        void DeleteBook(int id);
        void DeleteOrder(int id);
    }
}
