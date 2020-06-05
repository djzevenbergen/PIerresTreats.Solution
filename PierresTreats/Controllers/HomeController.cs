using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> everything = new Dictionary<string, object>();
      var allBooks = _db.Books.ToList();
      var allFlavors = _db.Flavors.ToList();
      everything.Add("books", allBooks);
      everything.Add("flavors", allFlavors);

      return View(everything);
    }
  }
}