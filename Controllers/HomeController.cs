using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RideOut.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace RideOut.Controllers;

public class HomeController : Controller
{
    private MyContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("user/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
            // Verify if the email is Unique
            if(_context.Users.Any(e =>e.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "Email already in use");
                return View("Index");
            }
            // Hash password
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", newUser.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    }

    [HttpPost("user/login")]
    public IActionResult Login(LogUser loginUser)
    {
        if(ModelState.IsValid)
        {
            //Verify if email is in db
            User? userInDb = _context.Users.FirstOrDefault(e => e.Email == loginUser.LogEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            // Verify if the password matches
            PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
            var result = hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LogPassword);
            if(result == 0)
            {
                ModelState.AddModelError("LogEmail", "Invalid Login Attempt");
                return View("Index");
            }
            HttpContext.Session.SetInt32("userId", userInDb.UserId);
            return RedirectToAction("Dashboard");
        }
        return View("Index");
    }

    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        User? userInDb =_context.Users.Include(s => s.RidesPosted).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        ViewBag.LoggedIn = userInDb;
        ViewBag.AllRideOuts = _context.Rides.OrderBy(a => a.DateNTime).Include(a => a.RideCoordinator).Include(u => u.PeopleWhoJoined).ToList();
        // foreach(Ride w in ViewBag.AllRideOuts)
        // {
        // if(w.DateNTime < DateTime.Now)
        // {
        //     Ride? ExpiredRide = _context.Rides.FirstOrDefault(r => r.RideId == w.RideId);
        //     _context.Remove(ExpiredRide);
        //     _context.SaveChanges();
        // }
        // }
        return View();
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    [HttpGet("ride/new")]
    public IActionResult NewRide()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        return View();
    }

    [HttpPost("ride/new/add")]
    public IActionResult AddRide(Ride newRide)
    {
        if(ModelState.IsValid)
        {
            newRide.UserId = (int)HttpContext.Session.GetInt32("userId");
            if(newRide.DateNTime < DateTime.Now)
            {
                ModelState.AddModelError("DateNTime", "Date and Time must be in the Future");
                return View("NewRide");
            }
            _context.Add(newRide);
            _context.SaveChanges();
            return Redirect($"/oneride/{newRide.RideId}");
        }
        return View("NewRide");
    }

    [HttpGet("oneride/{rideId}")]
    public IActionResult OneRide(int rideId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Ride? RideToDispalay = _context.Rides.Include(u => u.RideCoordinator).Include(p => p.PeopleWhoJoined).ThenInclude(g => g.UserWhoJoined).FirstOrDefault(r => r.RideId == rideId);
        if(RideToDispalay == null)
        {
            return View("Dashboard");
        }
        User? userInDb = _context.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
        return View(RideToDispalay);
    }

    [HttpGet("ride/join/{rideId}")]
    public IActionResult Join(int rideId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Join newJoin = new Join()
        {
            UserId = (int)HttpContext.Session.GetInt32("userId"),
            RideId = rideId
        };
        _context.Joins.Add(newJoin);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("ride/leave/{rideId}")]
    public IActionResult Leave(int rideId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Join? JoinToDelete = _context.Joins.SingleOrDefault(s => s.RideId == rideId && s.UserId == HttpContext.Session.GetInt32("userId"));
        if(JoinToDelete == null)
        {
            return RedirectToAction("Dashboard");
        }
        if(JoinToDelete.UserId != HttpContext.Session.GetInt32("userId"))
        {
            return RedirectToAction("Logout");
        }
        _context.Joins.Remove(JoinToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("ride/delete/{rideId}")]
    public IActionResult DeleteRide(int rideId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Ride? RideToDelete = _context.Rides.SingleOrDefault(s => s.RideId == rideId);
        if(RideToDelete == null)
        {
            return RedirectToAction("Dashboard");
        }
        if(RideToDelete.UserId != HttpContext.Session.GetInt32("userId"))
        {
            return RedirectToAction("Logout");
        }
        _context.Rides.Remove(RideToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    [HttpGet("ride/edit/{rideId}")]
    public IActionResult EditRide(int rideId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Ride? RideToEdit = _context.Rides.FirstOrDefault(r => r.RideId == rideId);
        if(RideToEdit == null)
        {
            return RedirectToAction("Dashboard");
        }
        return View(RideToEdit);
    }

    [HttpPost("ride/edit/update/{rideId}")]
    public IActionResult UpdateRide(Ride updatedRide, int rideId)
    {
        if(ModelState.IsValid){
            Ride? RideToUpdate = _context.Rides.FirstOrDefault(r => r.RideId == rideId);
            if(updatedRide.DateNTime < DateTime.Now)
            {
                ModelState.AddModelError("DateNTime", "Date and Time must be in the Future");
                return View("EditRide",updatedRide);
            }
            RideToUpdate.Title = updatedRide.Title;
            RideToUpdate.Address = updatedRide.Address;
            RideToUpdate.City = updatedRide.City;
            RideToUpdate.State = updatedRide.State;
            RideToUpdate.Zip = updatedRide.Zip;
            RideToUpdate.DateNTime = updatedRide.DateNTime;
            RideToUpdate.Distance = updatedRide.Distance;
            RideToUpdate.BikeType = updatedRide.BikeType;
            RideToUpdate.Exclusive = updatedRide.Exclusive;
            RideToUpdate.Description = updatedRide.Description;
            _context.SaveChanges();
            return Redirect($"/oneride/{updatedRide.RideId}");
        }
        return View("EditRide",updatedRide);
    }

    [HttpGet("bike/allbikes")]
    public IActionResult AllBikes()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        ViewBag.AllBikes = _context.Bikes.Include(o => o.BikeOwner).ToList();
        return View();
    }

    [HttpGet("bike/mybikes")]
    public IActionResult MyBikes()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        ViewBag.MyBikes = _context.Bikes.Where(b => b.UserId == (int)HttpContext.Session.GetInt32("userId")).ToList();
        return View();
    }

    [HttpGet("bike/new")]
    public IActionResult NewBike()
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        return View();
    }

    [HttpPost("bike/new/add")]
    public IActionResult AddBike(Bike newBike)
    {
        if(ModelState.IsValid)
        {
            newBike.UserId = (int)HttpContext.Session.GetInt32("userId");
            _context.Add(newBike);
            _context.SaveChanges();
            return Redirect($"/onebike/{newBike.BikeId}");
        }
        return View("NewBike",newBike);
    }

    [HttpGet("onebike/{bikeId}")]
    public IActionResult OneBike(int bikeId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Bike? BikeToDisplay = _context.Bikes.Include(o => o.BikeOwner).FirstOrDefault(b => b.BikeId == bikeId);
        return View(BikeToDisplay);
    }

    [HttpGet("bike/edit/{bikeId}")]
    public IActionResult EditBike(int bikeId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Bike? BikeToEdit = _context.Bikes.FirstOrDefault(b => b.BikeId == bikeId);
        if(BikeToEdit == null)
        {
            return RedirectToAction("MyBikes");
        }
        return View(BikeToEdit);
    }

    [HttpPost("bike/edit/update/{bikeId}")]
    public IActionResult UpdateBike(Bike updatedBike, int bikeId)
    {
        if(ModelState.IsValid)
        {
            Bike? BikeToUpdate = _context.Bikes.FirstOrDefault(b => b.BikeId == bikeId);
            BikeToUpdate.Year = updatedBike.Year;
            BikeToUpdate.Make = updatedBike.Make;
            BikeToUpdate.Model = updatedBike.Model;
            BikeToUpdate.Discplacement = updatedBike.Discplacement;
            BikeToUpdate.NickName = updatedBike.NickName;
            BikeToUpdate.BikeType = updatedBike.BikeType;
            BikeToUpdate.ForSale = updatedBike.ForSale;
            _context.SaveChanges();
            return Redirect($"/onebike/{updatedBike.BikeId}");
        }
        return View("EditBike", updatedBike);
    }

    [HttpGet("bike/delete/{bikeId}")]
    public IActionResult DeleteBike(int bikeId)
    {
        if(HttpContext.Session.GetInt32("userId") == null)
        {
            ModelState.AddModelError("LogEmail", "Please Login");
            return View("Index");
        }
        Bike? BikeToDelete = _context.Bikes.SingleOrDefault(b => b.BikeId == bikeId);
        User? UserInDb = _context.Users.FirstOrDefault(u => u.UserId == (int)HttpContext.Session.GetInt32("userId"));
        if(BikeToDelete == null)
        {
            return RedirectToAction("MyBikes",UserInDb);
        }
        if(BikeToDelete.UserId != HttpContext.Session.GetInt32("userId"))
        {
            return RedirectToAction("Logout");
        }
        _context.Remove(BikeToDelete);
        _context.SaveChanges();
        return RedirectToAction("MyBikes",UserInDb);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
