using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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
        public Employee AddEmployee(Employee Employee)
        {
            Employee u = new Employee();
            Employee? checkemail = _db.Employee.FirstOrDefault(x => x.email == Employee.email);
            if (checkemail != null) return null;
            else
            {
                var password = Encoding.UTF8.GetBytes(Employee.password);
                u.password = Convert.ToBase64String(password);
                u.email = Employee.email;
                u.username = Employee.username;
                u.phone = Employee.phone;
                u.role = "staff";
                _db.Employee.Add(u);
                _db.SaveChanges();
                return u;
            }
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
        public Employee UpdateEmployee(Employee Employee, int id) 
        {
            var password = Encoding.UTF8.GetBytes(Employee.password);
            var check = _db.Employee.First(x => x.EmpID == id);
            check.phone = Employee.phone;
            check.username = Employee.username;
            check.email = Employee.email;
            check.password = Convert.ToBase64String(password);
            _db.Employee.Update(check);
            _db.SaveChanges();
            return check;
        }
        public Employee GetEmployeekById(int id) 
        {
            var Employee = _db.Employee.First(x => x.EmpID == id);
            return Employee;
        }
        public IEnumerable<Employee> GetAllEmployee() 
        {
            IEnumerable<Employee> EmployeeList;
            EmployeeList = _db.Employee.OrderBy(emp => emp.EmpID).ToList();
            return EmployeeList;
        }
        public void DeleteEmployee(int id) 
        {
            var Employee = _db.Employee.First(x => x.EmpID == id);
            _db.Employee.Remove(Employee);
            _db.SaveChanges();
        }
    }
}
