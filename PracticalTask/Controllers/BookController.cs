using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PracticalTask.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace PracticalTask.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetByAuthor(int authorId)
        {
            try
            {
                var data = _unitOfWork.Books.Find(f => f.Author.AuthorId == authorId && f.IsDelete != true);
                var bookViewModel = _mapper.Map<IEnumerable<BookViewModel>>(data);
                return Ok(new { responseCode = "Ok", responseData = bookViewModel });
            }
            catch (Exception ex)
            {
                return new JsonResult("Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        [HttpPost]
        public IActionResult AddUpdate(BookViewModel bookViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (bookViewModel.BookId == 0)
                {
                    var book = _mapper.Map<Book>(bookViewModel);
                    _unitOfWork.Books.Add(book);
                }
                else
                {
                    var book = _unitOfWork.Books.GetById(bookViewModel.BookId);

                    if (book != null)
                    {
                        book.Name = bookViewModel.Name;
                        book.Price = Convert.ToDecimal(bookViewModel.Price);
                        book.PublishDate = DateTime.ParseExact(bookViewModel.PublishDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        book.Quantity = Convert.ToInt32(bookViewModel.Quantity);
                    }
                }
                _unitOfWork.Complete();
                return Ok(new { responseCode = "Ok" });
            }
            catch (Exception ex)
            {
                return new JsonResult("Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }


        public IActionResult GetBookPartailView(int id)
        {
            BookViewModel bookViewModel = new BookViewModel();

            var book = _unitOfWork.Books.GetById(id);

            if (book != null)
            {
                bookViewModel = _mapper.Map<BookViewModel>(book);
            }

            return new PartialViewResult()
            {
                ViewName = "_BookView",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = bookViewModel,
                }
            };
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (id != null)
                {
                    var book = _unitOfWork.Books.GetById(id.Value);

                    if (book != null)
                    {
                        book.IsDelete = true;
                        _unitOfWork.Complete();
                    }
                }

                return Ok(new { responseCode = "Ok" });
            }
            catch (Exception ex)
            {
                return new JsonResult("Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }


    }
}
