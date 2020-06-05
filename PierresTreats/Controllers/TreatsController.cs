using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

//new using directives
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresTreats.Controllers
{
  [Authorize]
  //new line
  public class BooksController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager; //new line

    //updated constructor
    public BooksController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    //updated Index method
    public ActionResult Index()
    {
      // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      // var currentUser = await _userManager.FindByIdAsync(userId);
      // var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
      var userBooks = _db.Books.ToList();
      return View(userBooks);
    }


    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View();
    }

    //updated Create post method
    [HttpPost]
    public async Task<ActionResult> Create(Book book, int AuthorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      book.User = currentUser;
      _db.Books.Add(book);
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var copiesOfBook = _db.Copies.Where(entry => entry.BookId == id).ToList();
      ViewBag.numberOfCopies = copiesOfBook.Count();
      var thisBook = _db.Books
          .Include(book => book.Authors)
          .ThenInclude(join => join.Author)
          .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }


    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBook.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("/search")]
    public ActionResult Search(string search, string searchParam)
    {



      if (!string.IsNullOrEmpty(search))
      {
        if (searchParam == "Book")
        {
          var model = from m in _db.Books select m;
          model = model.Where(n => n.Title.Contains(search));
          List<Book> matchesBook = new List<Book> { };
          matchesBook = model.ToList();
          return View(matchesBook);

        }
        else
        {
          var model = from m in _db.Authors select m;
          model = model.Where(n => n.Name.Contains(search));
          List<Author> matchesAuthor = new List<Author> { };
          matchesAuthor = model.ToList();
          return View(matchesAuthor);
        }
      }
      else
      {
        var model = from m in _db.Books select m;
        List<Book> allBooks = new List<Book> { };
        allBooks = model.ToList();
        return View(allBooks);
      }
    }

  }
}