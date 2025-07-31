namespace   efcoreApp.Controllers
{
    using efcoreApp.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class KursKayitController : Controller
    {
        private readonly DataContext _context;

        public KursKayitController(DataContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.KursKayitlari
                .Include(k => k.ogrenci)
                .Include(k => k.kurs)
                .ToListAsync());
        }
        
        public async Task<IActionResult> Create()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            var kurslar = await _context.Kurslar.ToListAsync();

            ViewBag.Ogrenciler = new SelectList(ogrenciler, "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(kurslar, "KursId", "Baslik");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            model.KayitTarihi = DateTime.Now;
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}