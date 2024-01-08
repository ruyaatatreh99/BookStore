using System.Threading.Tasks;
using BookStore.Model;
namespace BookStore.Services
{
    public interface IBook
    {
        Book AddBook(Book book);
        Book UpdateBook(Book book, int id);
        Book GetBookById(int id);
        IEnumerable<Book> GetAllBooks();
        void DeleteBook(int id);
        
        IEnumerable<Order> GetAllOrder();
        void DeleteOrder(int id);

        Employee AddEmployee(Employee Employee);
        Employee UpdateEmployee(Employee Employee, int id);
        Employee GetEmployeekById(int id);
        IEnumerable<Employee> GetAllEmployee();
        void DeleteEmployee(int id);
    }
}
