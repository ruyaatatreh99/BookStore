using BookStore.Model;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookService : IBook
    {
        private DBContext _db;
        public BookService(DBContext db)
        {
            _db = db;
        }
        public Book AddBook(Book book)
        {
            _db.Book.Add(book);
            _db.SaveChanges();
            return book;
        }
        public void DeleteBook(int id)
        {
            var book = _db.Book.First(x => x.ISBN == id);
            _db.Book.Remove(book);
            _db.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            var order = _db.Order.First(x => x.ID == id);
            _db.Order.Remove(order);
            _db.SaveChanges();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            IEnumerable<Book> booksList;
            try
            {
                booksList = _db.Book.OrderBy(book =>book.ISBN).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return booksList;
        }

        public IEnumerable<Order> GetAllOrder()
        {
            IEnumerable<Order> OrderList;
            OrderList = _db.Order.OrderBy(book => book.ID).ToList();
            return OrderList;
        }

        public Book GetBookById(int id)
        {
            var check = _db.Book.First(x => x.ISBN == id);
            return check;
        }
        public Book UpdateBook(Book book, int id)
        {
            var check = _db.Book.First(x => x.ISBN == id);
            check.NoBook = book.NoBook;
            check.Title = book.Title;
            check.Description = book.Description;
            check.Category = book.Category;
            check.author = book.author;
            check.status = book.status;
            check.price = book.price;
            _db.Book.Update(check);
            _db.SaveChanges();
                return check;
        }
    }
}
