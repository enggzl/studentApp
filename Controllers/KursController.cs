using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }

        // Örnek: Tüm kursları listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Kursları veritabanından çek
            var kurslar = await _context
                                    .Kurslar
                                    .Include(k => k.Ogretmen) // Öğretmen bilgilerini de dahil et
                                    .Include(k => k.KursKayitlari) // Kurs kayıtlarını dahil et
                                    .ThenInclude(k => k.ogrenci) // Öğrenci bilgilerini de dahil
                                    .ToListAsync();
            // Kursları görüntüle   
            return View(kurslar);
        }

        // Örnek: Yeni kurs ekleme
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad"); 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs model)
        {
            _context.Kurslar.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
        // Örnek: Kurs silme
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            return View(kurs);
        }
           
        // Örnek: Kurs silme
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, Kurs model)
        {
            var kurs = await _context.Kurslar.FindAsync(id);
            if (kurs != null)
            {
                _context.Kurslar.Remove(kurs);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Örnek: Kurs güncelleme
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var kurs = await _context
                                    .Kurslar
                                    .Include(k => k.KursKayitlari)
                                    .ThenInclude(k => k.ogrenci)
                                    .FirstOrDefaultAsync(k => k.KursId == id);
            if (kurs == null)
            {
                return NotFound();
            }
            
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad"); 
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Kurs model)
        {
          
                _context.Kurslar.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            
        }
    }
}