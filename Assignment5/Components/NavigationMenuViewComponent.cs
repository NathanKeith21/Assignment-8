//This new page helps the nav menu for filtering by category work properly

using Assignment5.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ILibraryRepository repository;
        public NavigationMenuViewComponent (ILibraryRepository r)
        {
            repository = r;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];

            return View(repository.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
