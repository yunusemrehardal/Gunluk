using Microsoft.AspNetCore.Mvc;

namespace Gunluk.Components
{
    public class KategorilerViewComponent : ViewComponent
    {
        private readonly UygulamaDbContext _db;

        public KategorilerViewComponent(UygulamaDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_db.Kategoriler.ToList());
        }
    }
}
