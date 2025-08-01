using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace efcoreApp.Data
{
    public class OgretmenController:Controller
    {
        private readonly DataContext _context;

        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ogretmenler = await _context.Ogretmenler.ToListAsync();
            return View(ogretmenler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            if (ModelState.IsValid)
            {
                model.BaslamaTarihi = DateTime.Now;
                _context.Ogretmenler.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Ogretmenler.Update(model);
                await _context.SaveChangesAsync();
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
            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if (ogretmen == null)
            {
                return NotFound();
            }
            return View(ogretmen);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ogretmen = await _context.Ogretmenler.FindAsync(id);
            if (ogretmen != null)
            {
                _context.Ogretmenler.Remove(ogretmen);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
       