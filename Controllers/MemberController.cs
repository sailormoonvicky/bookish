using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookish.Models.Data;
using bookish.Models.View;
using Microsoft.EntityFrameworkCore;

namespace bookish.Controllers;

public class MemberController : Controller

{
    private readonly Library _library;
    private readonly ILogger<MemberController> _logger;

    public MemberController(ILogger<MemberController> logger, Library library)
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
        var members = _library.Members.ToList();
        var pageSize = 15;
        var count = _library.Members.Count();

        var viewModel = members.Skip(pageSize*pageNum).Take(pageSize).ToList();
        var memberData = new MemberViewModel
        {
            Members = viewModel,
        };
        ViewBag.MaxPage = (count/pageSize) - (count % pageSize==0 ? 1: 0);
        ViewBag.Page = pageNum;

        return View(memberData);
    }

    public IActionResult MemberDetail([FromRoute] int id)
    {
        var matchingMember = _library.Members.Include(member => member.LoanHistory)
                                            .ThenInclude(loan => loan.Copy)
                                            .ThenInclude(copy => copy.Book)
                                        .SingleOrDefault(member=> member.Id == id);
        if(matchingMember == null)
        {
            return NotFound();
        }
        return View(matchingMember);
    }


}
