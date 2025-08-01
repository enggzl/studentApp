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
        public async Task<IActionResult> Edit(int id)
        {
            var kayit = await _context.KursKayitlari
                .Include(k => k.ogrenci)
                .Include(k => k.kurs)
                .FirstOrDefaultAsync(k => k.KursKayitId == id);

            if (kayit == null)
            {
                return NotFound();
            }

            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad", kayit.OgrenciId);
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik", kayit.KursId);
            return View(kayit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KursKayit model)
        {
            if (id != model.KursKayitId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.KursKayitlari.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            var kurslar = await _context.Kurslar.ToListAsync();
            ViewBag.Ogrenciler = new SelectList(ogrenciler, "OgrenciId", "AdSoyad", model.OgrenciId);
            ViewBag.Kurslar = new SelectList(kurslar, "KursId", "Baslik", model.KursId);
            ModelState.AddModelError("", "Güncelleme işlemi sırasında bir hata oluştu.");
            // Return the view with the model to show validation errors
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad", model.OgrenciId);
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik", model.KursId);
            ModelState.AddModelError("", "Güncelleme işlemi sırasında bir hata oluştu.");
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var kayit = await _context.KursKayitlari
                .Include(k => k.ogrenci)
                .Include(k => k.kurs)
                .FirstOrDefaultAsync(k => k.KursKayitId == id);

            if (kayit == null)
            {
                return NotFound();
            }

            return View(kayit);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, KursKayit model)
        {
            var kayit = await _context.KursKayitlari.FindAsync(id);
            if (kayit != null)
            {
                _context.KursKayitlari.Remove(kayit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}