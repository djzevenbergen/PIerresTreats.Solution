using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PierresTreats.Controllers
{
  public class HomeController : Controller
  {

    private readonly PierresTreatsContext _db;

    public HomeController(PierresTreatsContext db)
    {
      _db = db;
    }
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> everything = new Dictionary<string, object>();
      var allTreats = _db.Treats.ToList();
      var allFlavors = _db.Flavors.ToList();
      everything.Add("treats", allTreats);
      everything.Add("flavors", allFlavors);

      return View(everything);
    }
  }
}