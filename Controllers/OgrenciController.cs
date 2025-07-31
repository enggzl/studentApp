using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly DataContext _context;
        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ogrenciler = await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {

            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogrenci = await _context
                                        .Ogrenciler
                                        .Include(o => o.KursKayitlari)
                                        .ThenInclude(k => k.kurs)
                                        .FirstOrDefaultAsync(o => o.OgrenciId == id);
            // Include KursKayitlari and Kurs for detailed view
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Ogrenciler.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(e => e.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)   
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            return View(ogrenci);
        }

        public async Task<IActionResult> Delete(int? id, Ogrenci model)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogrenci = await _context.Ogrenciler.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
    
}