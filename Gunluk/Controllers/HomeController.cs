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

        public IActionResult Index(int? kategoriId, int sayfa = 1)
        {
            IQueryable<Gonderi> gonderiler = _db.Gonderiler;

            if (kategoriId != null)
            {
                gonderiler = gonderiler.Where(x => x.KategoriId == kategoriId);
                ViewBag.Baslik = _db.Kategoriler.Find(kategoriId)?.Ad;
            }

            int sayfaAdet = (int)Math.Ceiling((double)gonderiler.Count() / Sabitler.SAYFA_BASINA_GONDERI);

            gonderiler = gonderiler
                .Skip((sayfa - 1) * Sabitler.SAYFA_BASINA_GONDERI)
                .Take(Sabitler.SAYFA_BASINA_GONDERI);

            var vm = new HomeViewModel() 
            {
                KategoriId= kategoriId,
                Gonderiler = gonderiler.ToList(),
                Sayfa = sayfa,
                SayfaAdet= sayfaAdet
            };

            return View(vm);
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