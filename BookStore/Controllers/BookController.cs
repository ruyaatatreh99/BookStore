﻿using BookStore.Model;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _book;
        public  BookController(IBook book)
            {
            _book = book;
            }
        [Route("admin/book")]
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
            {
            try
            {               
                Book newbook = _book.AddBook(book);
                if (newbook == null) return NotFound(new { errors = "Error in adding new book!!" });
                else
                {
                    return Ok(new { book = newbook });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
       
            }
        [Route("admin/book")]
        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _book.DeleteBook(id);
                return Ok(new { message = "deleted successfuly" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Route("admin/book")]
        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            try
            {
                Book book = _book.GetBookById(id);
                if (book == null) return NotFound(new { errors = "Error ! Not Found" });
                else return Ok(new { book = book });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Route("admin/book")]
        [HttpPut]
        public IActionResult UpdateBook(Book book, int id)
        {
            try
            {
                Book updatedbook = _book.UpdateBook(book, id);
                if (updatedbook == null) return NotFound(new { errors = "Error in updating book information!!" });
                else return Ok(new { book = updatedbook });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Route("admin/book/all")]
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                IEnumerable<Book> booksList = _book.GetAllBooks();
                return Ok(new { books = booksList });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }

        [Route("admin/order/all")]
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            try
            {
                IEnumerable<Order> OrderList = _book.GetAllOrder();
                return Ok(new { Orders = OrderList });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
        [Route("admin/order")]
        [HttpDelete]
        public IActionResult DeleteOrder(int orderid)
        {
            try
            {
                _book.DeleteOrder(orderid);
                return Ok(new { message = "deleted successfuly" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }

        [Route("admin/employee/all")]
        [HttpGet]
        public IActionResult GetAllemployee()
        {
            try
            {
                IEnumerable<Employee> employeesList = _book.GetAllEmployee();
                return Ok(new { employees = employeesList });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Route("admin/employee")]
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee Employee)
        {
            try
            {
                Employee newEmployee = _book.AddEmployee(Employee);
                if (newEmployee == null) return NotFound(new { errors = "Error in adding new Employee!!" });
                else
                {
                    return Ok(new {Employee = newEmployee});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }

        }
        [Route("employee/")]
        [HttpDelete]
        public IActionResult DeletEmployee(int employeeid)
        {
            try
            {
                _book.DeleteEmployee(employeeid);
                return Ok(new { message = "deleted successfuly" });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 500, message = "Error" });
            }
        }
        [Route("employee/")]
        [HttpGet]
        public IActionResult GetEmployeekById(int employeeid)
        {
            try
            {
                Employee employee = _book.GetEmployeekById(employeeid);
                if (employee == null) return NotFound(new { errors = "Error ! Not Found" });
                else return Ok(new { employee = employee });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
        [Route("employee/")]
        [HttpPut]
        public IActionResult Updateemployee(Employee employee, int id)
        {
            try
            {
                Employee updatedemployee = _book.UpdateEmployee(employee, id);
                if (updatedemployee == null) return NotFound(new { errors = "Error in updating Employee information!!" });
                else return Ok(new {Employee = updatedemployee });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 500, message = ex });
            }
        }
    }
        }
