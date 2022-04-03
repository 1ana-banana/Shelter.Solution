using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shelter.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shelter.Controllers
{
  public class ShelterController : Controller
  {
    private readonly ShelterContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ShelterController(UserManager<ApplicationUser> userManager, ShelterContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userDogs = _db.Dogs.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userDogs);
    }

    public ActionResult Create()
    {
      ViewBag.BreedId = new SelectList(_db.Breeds, "BreedId", "BreedName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Dog dog, int BreedId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      dog.User = currentUser;
      _db.Dogs.Add(dog);
      _db.SaveChanges();
      if (BreedId != 0)
      {
        _db.BreedDogs.Add(new BreedDog() { BreedId = BreedId, DogId = dog.DogId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details (int id)
    {
      var thisDog = _db.Dogs
          .Include(dog => dog.JoinIR)
          .ThenInclude (join => join.Breed)
          .Include (dog => dog.JoinRR)
          .ThenInclude(join => join.Priority)
          .FirstOrDefault(dog => dog.DogId == id);
      return View(thisDog);
    }

    public ActionResult Edit (int id)
    {
      var thisDog = _db.Dogs.FirstOrDefault(dog => dog.DogId == id);
      ViewBag.PriorityId = new SelectList(_db.Priorities, "PriorityId", "Stars");
      ViewBag.BreedId = new SelectList(_db.Breeds, "BreedId", "BreedId");
      return View (thisDog);
    }

    [HttpPost]
    public ActionResult Edit(Dog dog, int BreedId, int PriorityId)
    {
      if (BreedId != 0 && PriorityId != 0)
      {
        _db.BreedDogs.Add(new BreedDog() { BreedId = BreedId, DogId = dog.DogId });
        _db.PriorityDogs.Add(new PriorityDog() {PriorityId = PriorityId, DogId = dog.DogId });
      }
      else if (BreedId != 0 && PriorityId < 1)
      {
        _db.BreedDogs.Add(new BreedDog() {BreedId = BreedId, DogId = dog.DogId });
      }
      else if (BreedId < 1 && PriorityId != 0 )
      {
        _db.PriorityDogs.Add(new PriorityDog() {PriorityId = PriorityId, DogId = dog.DogId });
      }
      _db.Entry(dog).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddBreed (int id)
    {
      var thisDog = _db.Dogs.FirstOrDefault(dog => dog.DogId == id);
      ViewBag.BreedId = new SelectList(_db.Breeds, "BreedId", "Name");
      return View(thisDog);
    }

    [HttpPost]
    public ActionResult AddBreed (Dog dog, int BreedId)
    {
      if (BreedId != 0)
      {
        _db.BreedDogs.Add(new BreedDog() { BreedId = BreedId, DogId = dog.DogId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPriority (int id)
    {
      var thisDog = _db.Dogs.FirstOrDefault(dog => dog.DogId == id);
      ViewBag.PriorityId = new SelectList(_db.Priorities, "PriorityId", "Stars");
      return View(thisDog);
    }

    [HttpPost]
    public ActionResult AddPriority(Dog dog, int PriorityId)
    {
      if (PriorityId != 0)
      {
        _db.PriorityDogs.Add(new PriorityDog() { PriorityId = PriorityId, DogId = dog.DogId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete (int id)
    {
      var thisDog = _db.Dogs.FirstOrDefault(dog => dog.DogId == id);
      return View(thisDog);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisDog = _db.Dogs.FirstOrDefault(dog => dog.DogId == id);
      _db.Dogs.Remove(thisDog);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBreed(int joinId)
    {
      var joinEntry = _db.BreedDogs.FirstOrDefault(entry => entry.BreedDogId == joinId);
      _db.BreedDogs.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeletePriority(int joinId)
    {
      var joinEntry = _db.PriorityDogs.FirstOrDefault(entry => entry.PriorityDogId == joinId);
      _db.PriorityDogs.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}