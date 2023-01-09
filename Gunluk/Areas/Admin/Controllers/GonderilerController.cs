using Gunluk.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gunluk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GonderilerController : Controller
    {
        private readonly UygulamaDbContext _db;

        public GonderilerController(UygulamaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string? durum)
        {
            ViewBag.Mesaj =
                durum == "eklendi" ? "Gönderi başarıyla oluşturuldu" :
                durum == "düzenlendi" ? "Gönderi başarıyla güncellendi" :
                durum == "silindi" ? "Gönderi başarıyla silindi." : null;

            return View(_db.Gonderiler.Include(x => x.Kategori).ToList());
        }
        public IActionResult Yeni()
        {
            KategorileriYukle();
            return View("Yonet");
        }
        private void KategorileriYukle()
        {
            ViewBag.Kategoriler = _db.Kategoriler.Select(x=> 
            new SelectListItem() { Value = x.Id.ToString(), Text = x.Ad }).ToList();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(GonderiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var gonderi = new Gonderi()
                {
                    Baslik = vm.Baslik,
                    Icerik = vm.Icerik,
                    KategoriId = vm.KategoriId!.Value
                };
                _db.Gonderiler.Add(gonderi);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { durum = "eklendi" });
            }
            KategorileriYukle();
            return View("Yonet");
        }
        public IActionResult Duzenle(int id)
        {
            return View("Yonet");
        }
    }
}
