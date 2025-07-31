using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
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
            var kurslar = await _context.Kurslar.ToListAsync();
            // Kursları görüntüle   
            return View(kurslar);
        }

        // Örnek: Yeni kurs ekleme
        [HttpGet]
        public IActionResult Create()
        {
            // Yeni kurs ekleme formunu görüntüle
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Kurs model)
        {
            if (ModelState.IsValid)
            {
                // Model geçerliyse kursu ekle
                _context.Kurslar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // Model geçerli değilse formu tekrar göster
            return View("Index", model);
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
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Kurs model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            // Model geçerli değilse formu tekrar göster
            return View(model);
        }
    }
}