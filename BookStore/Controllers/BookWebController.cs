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
        public IActionResult BuyBook([FromBody]Book book, int customerID)
        {
            try
            {
                int result = _Web.BuyBook(book, customerID);
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
        public IActionResult GetBookById(int id)
        {
            try
            {
                Book booksList = _Web.GetBookById(id);
                return Ok(new { books = booksList });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }

        [Route("customers/book/search")]
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
        public IActionResult reviewBook([FromBody] dynamic requestBody)
        {
            try
            {
              int result= _Web.reviewBook(requestBody.bookid, requestBody.review, requestBody.customerid);
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
    }
}
