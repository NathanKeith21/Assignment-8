using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment5.Infrastructure;
using Assignment5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment5.Pages
{
    public class CartModel : PageModel
    {
        private ILibraryRepository repository;
        
        public CartModel (ILibraryRepository repo, Cart cartservice)
        {
            repository = repo;
            Cart = cartservice;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(long bookId, string returlUrl)
        {
            Book book = repository.Books.FirstOrDefault(p => p.BookId == bookId);

            Cart.AddItem(book, 1);

            return RedirectToPage(new { returnUrl = ReturnUrl });
        }

        public IActionResult OnPostRemove(long bookId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Book.BookId == bookId).Book);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
