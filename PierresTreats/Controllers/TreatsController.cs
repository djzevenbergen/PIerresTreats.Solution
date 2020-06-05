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

  //new line
  public class TreatsController : Controller
  {
    private readonly PierresTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager; //new line

    //updated constructor
    public TreatsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    // //updated Index method
    // public ActionResult Index()
    // {
    //   // var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //   // var currentUser = await _userManager.FindByIdAsync(userId);
    //   // var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
    //   var userTreats = _db.Treats.ToList();
    //   return View(userTreats);
    // }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    //updated Create post method
    [HttpPost, Authorize]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {

      _db.Treats.Add(treat);
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
          .Include(treat => treat.Flavors)
          .ThenInclude(join => join.Flavor)
          .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost, Authorize]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }
    [Authorize]
    public ActionResult AddFlavor(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost, Authorize]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }
    [Authorize]
    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, Authorize, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    [HttpPost, Authorize]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index", "Home");
    }

    [HttpGet("/search")]
    public ActionResult Search(string search, string searchParam)
    {



      if (!string.IsNullOrEmpty(search))
      {
        if (searchParam == "Treat")
        {
          var model = from m in _db.Treats select m;
          model = model.Where(n => n.Name.Contains(search));
          List<Treat> matchesTreat = new List<Treat> { };
          matchesTreat = model.ToList();
          return View(matchesTreat);

        }
        else
        {
          var model = from m in _db.Flavors select m;
          model = model.Where(n => n.Name.Contains(search));
          List<Flavor> matchesFlavor = new List<Flavor> { };
          matchesFlavor = model.ToList();
          return View(matchesFlavor);
        }
      }
      else
      {
        var model = from m in _db.Treats select m;
        List<Treat> allTreats = new List<Treat> { };
        allTreats = model.ToList();
        return View(allTreats);
      }
    }

  }
}