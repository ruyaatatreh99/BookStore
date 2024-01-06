using BookStore.Model;
using System.Net;
using System.Text.Json;
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
            foreach (var item in booksList)
            {
                if (item.ID == customerID)
                {
                    booksList.Append(item);
                    _db.ShoppingCart.Remove(item);
                    _db.SaveChanges();
                }
            }
            customer.TotalNoBook = 0;
            customer.Totalprice = 0;
            //Send booksList to  admin  

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
    }
}
