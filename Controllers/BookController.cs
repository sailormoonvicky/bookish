using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookish.Models.Data;
using bookish.Models.View;
using Microsoft.EntityFrameworkCore;

namespace bookish.Controllers;

public class BookController : Controller

{
    private readonly Library _library;
    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger, Library library)
    {
        _library = library;
        _logger = logger;
    }

    public IActionResult Navigation()
    {
        return View();
    }


    public IActionResult ViewAll( int pageNum = 0)
    {
        var books = _library.Books.ToList();
        var pageSize = 15;
        var count = _library.Books.Count();

        var viewModel = books.Skip(pageSize*pageNum).Take(pageSize).ToList();
        var bookData = new BookViewModel
        {
            Books = viewModel,
        };
        ViewBag.MaxPage = (count/pageSize) - (count % pageSize==0 ? 1: 0);
        ViewBag.Page = pageNum;

        return View(bookData);
    }

    public IActionResult BookDetail([FromRoute] int id)
    {
        var matchingBook = _library.Books.Include(book => book.Copies)
                                        .SingleOrDefault(book=> book.Id == id);
        if(matchingBook == null)
        {
            return NotFound();
        }
        return View(matchingBook);
    }


}
