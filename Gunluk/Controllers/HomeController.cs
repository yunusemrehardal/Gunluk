using Gunluk.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gunluk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UygulamaDbContext _db;

        public HomeController(ILogger<HomeController> logger, UygulamaDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int? kategoriId)
        {
            IQueryable<Gonderi> gonderiler = _db.Gonderiler;

            if (kategoriId != null)
            {
                gonderiler = gonderiler.Where(x => x.KategoriId == kategoriId);
                ViewBag.Baslik = _db.Kategoriler.Find(kategoriId)?.Ad;
            }

            return View(gonderiler.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}