//This is my controller, which handles the functionality of the views and models. It also connects the database

using Assignment5.Models;
using Assignment5.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ILibraryRepository _repository;
        //this line is new and helps determine how many books to display on each page
        public int PageSize = 5;
        public HomeController(ILogger<HomeController> logger, ILibraryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        //this part is the biggest change for assignment 6; it creates a view that is passed BookListViewModel, which contains info for the books to be displayed on this particular page
        //in assignment 7, I added CurrentCategory to keep track of the category the user would like to filter by
        public IActionResult Index(string category, int pageNum = 1)
        {
            return View(new BookListViewModel
            {
                Books = _repository.Books
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.BookId)
                .Skip((pageNum - 1) * PageSize)
                .Take(PageSize)
                ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = PageSize,
                    TotalNumItems = category == null ? _repository.Books.Count() :
                        _repository.Books.Where (x => x.Category == category).Count()
                },
                CurrentCategory = category
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
