using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookish.Models.Data;
using bookish.Models.View;

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

    [HttpGet]
    public IActionResult ViewAll([FromForm] int pageNum)
    {
        var books = _library.Books.ToList();
        var pageSize = 10;
        var viewModel = books.Skip(pageSize*pageNum).Take(pageSize).ToList();

        var bookData = new BookViewModel
        {
            Books = books,
        };
        return View(bookData);
    }

    public IActionResult AddCopy([FromRoute] int id)

    {
        return View();
    }


}
