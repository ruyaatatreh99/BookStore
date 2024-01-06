using BookStore.Model;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BookStore.Controllers
{
    [ApiController]
    public class BookWebController : ControllerBase
    {
        private readonly IBookWeb _Web;
        public BookWebController(IBookWeb bWeb)
        {
            _Web = bWeb;
        }
        [Route("customers/buy")]
        [HttpPost]
        public IActionResult BuyBook(int bookid, int customerID)
        {
            try
            {
                int result = _Web.BuyBook(bookid, customerID);
                if (result == 0) return NotFound(new { errors = "Error in adding new book!!" });
                else
                {
                    return Ok(new { result = "Add successfully to shopping cart" });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(new { status = 500, message = exc });
            }
        }

            [Route("customers/buy")]
            [HttpDelete]
            public IActionResult DeleteBook(int bookid, int customerID)
            {
                try
                {
                    int result = _Web.DeleteBook(bookid, customerID);
                    if (result == 0) return NotFound(new { errors = "Error in deleting new book!!" });
                    else if (result == 1)
                {
                        return Ok(new { result = "deleted successfully from shopping cart" });
                    }
                else 
                {
                    return Ok(new { result =  " "});
                }
            }
                catch (Exception exc)
                {
                    return BadRequest(new { status = 500, message = exc });
                }
            }
        [Route("customers/buy")]
        [HttpGet]
        public IActionResult CheckOut(int customerID)
        {
            try
            {
                _Web.CheckOut(customerID);
                return Ok(new { message = "happy purchased !" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
        [Route("customers/book")]
        [HttpGet]
        public IActionResult GetBookById(int bookid)
        {
            try
            {
                Book booksList = _Web.GetBookById(bookid);
                return Ok(new { books = booksList });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
        [Route("customers/search")]
        [HttpGet]
        public IActionResult GetBookBySearch(string name)
        {
            try
            {
                IEnumerable<Book> book = _Web.GetBookBySearch(name);
                if (book == null) return NotFound(new { errors = "Error ! Not Found" });
                else return Ok(new { books = book });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
        [Route("customers/book")]
        [HttpPut]
        public IActionResult RatingBook(int bookid, int rating)
        {
            try
            {
                int status = _Web.RatingBook(bookid, rating);
                if (status == 0) return NotFound(new { errors = "Error in Rating book !!" });
                else return Ok(new { result = "success"  });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
        [Route("customers/Review")]
        [HttpPost]
        public IActionResult reviewBook([FromBody] Dictionary<string, string> data)
        {
            try
            {
              int result= _Web.reviewBook(Int16.Parse(data["bookid"]), data["review"], Int16.Parse(data["customerid"]));
                if (result !=1) return NotFound(new { errors = "Error in adding review!!" });
                else
                {
                    return Ok(new { result = "Added successfully" });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(new { status = 500, message = exc });
            }

        }
        [Route("customers/book/catgory")]
        [HttpGet]
        public IActionResult ViewBookByCatgory(string Catgory)
        {
            try
            {
                IEnumerable<Book> result = _Web.ViewBookByCatgory(Catgory);
                if (result == null) return NotFound(new { errors = "Empty !!" });
                else
                {
                    return Ok(new { result = result });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(new { status = 500, message = exc });
            }

        }
        [Route("customers/book/review")]
        [HttpGet]
        public IActionResult ViewBookReview(int bookid)
        {
            try
            {
                IEnumerable<Review> result = _Web.ViewBookReview(bookid);
                if (result == null) return NotFound(new { result ="No reviews Added !" });
                else
                {
                    return Ok(new { result = result });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(new { status = 500, message = exc });
            }

        }
        [Route("customers/cart")]
        [HttpGet]
        public IActionResult ViewShoppingCart(int customerID)
        {
            try
            {
                IEnumerable<ShoppingCart> result = _Web.ViewShoppingCart(customerID);
                if (result == null) return NotFound(new { result = "Empty!" });
                else
                {
                    return Ok(new { result = result });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(new { status = 500, message = exc });
            }

        }
        [Route("book/RecommendBook")]
        [HttpGet]
        public IActionResult RecommendationBook()
        {
            try
            {
                IEnumerable<Book> result = _Web.RecommendationBook();
                if (result == null) return NotFound(new { result = "Empty!" });
                else
                {
                    return Ok(new { result = result });
                }
            }
            catch (Exception exc)
            {
                return BadRequest(new { status = 500, message = exc });
            }

        }

    }
}
