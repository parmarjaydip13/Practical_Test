using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using PracticalTask.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace PracticalTask.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AuthorViewModel viewModel)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var author = new Author { Name = viewModel.Name, Email = viewModel.Email, PhoneNumber = viewModel.PhoneNumber };
                _unitOfWork.Authors.Add(author);
                _unitOfWork.Complete();

                return RedirectToAction("Index", "Author");
            }
            catch (Exception ex)
            {
                //TODO: Log error 
                throw;
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            AuthorEditViewModel authorViewModel = new AuthorEditViewModel();
            authorViewModel.BookViewModel = new BookViewModel();
            if (id.HasValue)
            {
                var author = _unitOfWork.Authors.GetById(id.Value);
                if (author != null)
                {
                    authorViewModel = _mapper.Map<AuthorEditViewModel>(author);
                }
            }
            return View(authorViewModel);
        }

        [HttpPost]
        public IActionResult Edit(AuthorEditViewModel authorEditViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(authorEditViewModel);
            }

            var author = _unitOfWork.Authors.GetById(authorEditViewModel.AuthorId);

            if (author != null)
            {
                author.Name = authorEditViewModel.Name;
                author.Email = authorEditViewModel.Email;
                author.PhoneNumber = authorEditViewModel.PhoneNumber;
            }
            _unitOfWork.Complete();
            return RedirectToAction("Index", "Author");
        }

        public IActionResult GetAll()
        {
            try
            {
                var data = _unitOfWork.Authors.Find(x => x.IsDelete != true);
                var authorViewMode = _mapper.Map<IEnumerable<AuthorViewModel>>(data);
                return Ok(new { responseCode = "Ok", responseData = authorViewMode });
            }
            catch (Exception ex)
            {
                //TODO: Log error 
                return new JsonResult("Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            try
            {
                if (id.HasValue)
                {
                    var author = _unitOfWork.Authors.GetById(id.Value);
                    if (author != null)
                    {
                        var books = _unitOfWork.Books.Find(x => x.Author.AuthorId == author.AuthorId && x.IsDelete != true);
                        books.ToList().ForEach(c => c.IsDelete = true);
                        author.IsDelete = true;
                    }
                    _unitOfWork.Complete();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: Log error 
                throw;
            }
            
           
        }
    }
}
