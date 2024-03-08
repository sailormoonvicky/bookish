using bookish.Models.Data;
using bookish.Models.View;
using bookish.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookish.Controllers;

public class CopyController : Controller

{
    private readonly Library _library;
    private readonly ILogger<CopyController> _logger;

    public CopyController(ILogger<CopyController> logger, Library library)
    {
        _library = library;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult AddCopy([FromRoute] int id)

    {
        var matchingBook = _library.Books.Include(book => book.Copies)
                            .SingleOrDefault(book => book.Id == id);    
                                            
        if(matchingBook == null)
        {
            return NotFound();
        }
        return View(matchingBook);
    }

    [HttpPost]
    public IActionResult AddCopy([FromRoute] int id, [FromForm] Copy copy)
    {
        copy.BookId = id;
        _library.Copies.Add(copy);
        _library.SaveChanges();
        return RedirectToAction(nameof(AddCopy));
    }

}
