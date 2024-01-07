using BookStore.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace BookStore.Services
{
    public class BookWebService : IBookWeb
    {
        private DBContext _db;
        public BookWebService(DBContext db)
        {
            _db = db;
        }
        public int BuyBook(int bookid, int customerID)
        {
            var book = _db.Book.First(b => b.ISBN == bookid);
            var check = _db.ShoppingCart.FirstOrDefault(x => x.ISBN == bookid && x.CustomerID == customerID);

            if (check == null && book.NoBook != 0 && book.status != 0)
            {

                ShoppingCart newitem =new ShoppingCart();
                newitem.ISBN = book.ISBN;
                newitem.CustomerID = customerID;
                newitem.NoBook =1;
                newitem.Bookprice = book.price;

                Customer customer = _db.Customer.First(x => x.ID == customerID);
                customer.Totalprice += book.price;
                customer.TotalNoBook += 1;

                _db.Customer.Update(customer);
                _db.SaveChanges();

                book.NoPurchased++;
                book.NoBook--;
                if (book.NoBook == 0)
                {
                    book.status = 0;
                }
                _db.Book.Update(book);
                _db.SaveChanges();

                _db.ShoppingCart.Add(newitem);
                _db.SaveChanges();

                return 1;
            }
           else if(check != null && book.NoBook != 0 && book.status != 0)
            {
                check.ISBN = book.ISBN;
                check.CustomerID = customerID;
                check.NoBook += 1;
                check.Bookprice += book.price;

                var customer = _db.Customer.First(x => x.ID == customerID);
                customer.TotalNoBook += 1;
                customer.Totalprice += book.price;
                _db.ShoppingCart.Update(check);
                _db.SaveChanges();

                _db.Customer.Update(customer);
                _db.SaveChanges();

                book.NoPurchased++;
                book.NoBook--;
                if (book.NoBook == 0)
                {
                    book.status = 0;
                }
                _db.Book.Update(book);
                _db.SaveChanges();  
               
                return 1;
            }
            else return 0;
        }
        public int DeleteBook(int bookid, int customerID)
        {
            var book = _db.Book.First(b => b.ISBN == bookid);
            var check = _db.ShoppingCart.First(x => x.ISBN == bookid && x.CustomerID == customerID);

            if (check.NoBook == 1 )
            {
                _db.ShoppingCart.Remove(check);
                _db.SaveChanges();

                Customer customer = _db.Customer.First(x => x.ID == customerID);
                customer.Totalprice -= book.price;
                customer.TotalNoBook -= 1;
                _db.Customer.Update(customer);
                _db.SaveChanges();

                book.NoPurchased--;
                book.NoBook++;
                if (book.status == 0) book.status = 1;
                _db.Book.Update(book);
                _db.SaveChanges();

                return 1;
            }
            else if (check.NoBook > 1)
            {
                check.NoBook--;
                check.Bookprice -= book.price;
                _db.ShoppingCart.Update(check);
                _db.SaveChanges();

                Customer customer = _db.Customer.First(x => x.ID == customerID);
                customer.Totalprice -= book.price;
                customer.TotalNoBook -= 1;
                _db.Customer.Update(customer);
                _db.SaveChanges();

                book.NoPurchased--;
                book.NoBook++;
                if (book.status == 0) book.status = 1;
                _db.Book.Update(book);
                _db.SaveChanges();


                return 2;
            }
            else return 0;
        }
        public void CheckOut(int customerID)
        {
            var customer = _db.Customer.First(x => x.ID == customerID);
            IEnumerable<ShoppingCart> booksList;
            booksList = _db.ShoppingCart.ToList();
            Order order = new Order();
            foreach (var item in booksList)
            {
                if (item.ID == customerID)
                {
                    order.Bookprice = item.Bookprice;
                    order.NoBook = item.NoBook;
                    order.ISBN = item.ISBN;
                    order.CustomerName = customer.Name;
                    order.CustomerPhone = customer.phone;
                    order.CustomerAdrress = customer.phone;
                    //Send booksList to  admin 
                    _db.Order.Add(order);
                    _db.SaveChanges();
                    
                    _db.ShoppingCart.Remove(item);
                    _db.SaveChanges();
                }
            }
            customer.TotalNoBook = 0;
            customer.Totalprice = 0;
        }
        public Book GetBookById(int id)
        {
            var book = _db.Book.First(x => x.ISBN == id);
            return book;
        }
        public IEnumerable<Book> GetBookBySearch(string name)
        {
            IEnumerable<Book> booksList;
            booksList = _db.Book.ToList();
            foreach (var x in booksList) {
                if (x.Title == name || x.Description == name || x.author == name || x.Category == name)
                    booksList.Append(x);
            }
            return booksList;
        }
        public int RatingBook(int bookid, int rating)
        {
            if (rating > 5) return 0;
            else
            {
                var check = _db.Book.First(x => x.ISBN == bookid);
                check.rating = (double)(rating / 5 + check.rating);
                _db.Book.Update(check);
                _db.SaveChanges();
                return 1;
            }
        }
        public IEnumerable<Book> RecommendationBook()
        {
            IEnumerable<Book> booksList;
            booksList = _db.Book.OrderByDescending(book => book.NoPurchased).ToList().Take(10);
            return booksList;
        }
        public int reviewBook(int bookid, string review, int customerid)
        {
            var customer = _db.Customer.First(x => x.ID == customerid);
            Review newReview = new Review();
            newReview.ISBN = bookid;
            newReview.customerid = customerid;
            newReview.CustomerName = customer.Name;
            newReview.CustomerReview=review;
            _db.Review.Add(newReview);
            _db.SaveChanges(); 
            return 1;
        }
        public IEnumerable<Book> ViewBookByCatgory(string Catgory)
        {
            IEnumerable<Book> booksList;
            booksList = _db.Book.ToList();
            foreach (var x in booksList)
            {
                if ( x.Category == Catgory)
                    booksList.Append(x);
            }
            return booksList;
        }
        public IEnumerable<Review> ViewBookReview(int bookid)
        {
            IEnumerable<Review> ReviewList;
            ReviewList = _db.Review.ToList();
            foreach (var x in ReviewList)
            {
                if (x.ISBN == bookid)
                    ReviewList.Append(x);
            }
            return ReviewList;
        }
        public IEnumerable<ShoppingCart> ViewShoppingCart(int customerID)
        {
            IEnumerable<ShoppingCart> ShoppingList;
            ShoppingList = _db.ShoppingCart.ToList();
            foreach (var x in ShoppingList)
            {
                if (x.ISBN == customerID)
                    ShoppingList.Append(x);
            }
            return ShoppingList;
        }
        public Customer GetCustomer(int Customerid) {
            Customer customer = _db.Customer.First(x => x.ID == Customerid);
            return customer;
        }
        public Customer GetCustomerbyemail(string email)
        {
            Customer? customer = _db.Customer.FirstOrDefault(x => x.email == email);
            return customer;
        }
        public Customer loginCustomer(string email, string password)
        {
            Customer? Customer = _db.Customer.FirstOrDefault(x => x.email == email);
            if (Customer == null) return null;
            else
            {
                var decryptedpassword = Convert.FromBase64String(Customer.password);
                var result = Encoding.UTF8.GetString(decryptedpassword);
                result = result.Substring(0, result.Length);
                if (result == password) return Customer;
                else return null;
            }
        }
        public Customer reisterCustomer(string name, string phone, string email, string newpassword, string repeatepassword)
        {
            if (String.Equals(newpassword,repeatepassword))
            {
               
                Customer user = new Customer();
                Customer? checkemail = _db.Customer.FirstOrDefault(x => x.email == email);
                if (checkemail != null) return null;
                else
                {
                    var password = Encoding.UTF8.GetBytes(newpassword);
                    user.password = Convert.ToBase64String(password);
                    user.email = email;
                    user.Name = name;
                    user.phone = phone;
                    user.Totalprice = 0;
                    user.TotalNoBook = 0;
                    _db.Customer.Add(user);
                    _db.SaveChanges();
                    return user;
                }
            }
            else return null;
        }
        public int UpdateCustomer(int id,string name, string phone, string email ,string newpassword, string repeatepassword) {

            if (newpassword == repeatepassword)
            {

                Customer check = _db.Customer.First(x => x.ID == id);
                check.email = email;
                check.Name = name;
                check.phone = phone;
                check.password = newpassword;
                _db.Customer.Update(check);
                _db.SaveChanges();
                return 1;
            }
            else return 0;   
        }

    }
}
