using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lepasi;
using PagedList;


namespace lepasi.Controllers
{
    public class samochodsController : Controller
    {
        private MotoJanuszEntities db = new MotoJanuszEntities();

        // GET: samochods
        public async Task<ActionResult> Index()
        {
            var samochods = db.samochods.Include(s => s.marka).Include(s => s.status).Include(s => s.typ);
            return View(await samochods.ToListAsync());
        }

        // GET: samochods/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            samochod samochod = await db.samochods.FindAsync(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        // GET: samochods/Create
        public ActionResult Create()
        {
            ViewBag.MA_id = new SelectList(db.markas, "MA_id", "MA_nazwa");
            ViewBag.ST_id = new SelectList(db.status, "ST_id", "ST_nazwa");
            ViewBag.TY_id = new SelectList(db.typs, "TY_id", "TY_nazwa");
            return View();
        }

        // POST: samochods/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SA_id,TY_id,MA_id,ST_id,SA_model,SA_stan,SA_cena,SA_kolor,SA_rok_produkcji,SA_kraj_pochodzenia,SA_liczba_miejsc")] samochod samochod)
        {
            
            if (ModelState.IsValid)
            {
                db.samochods.Add(samochod);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MA_id = new SelectList(db.markas, "MA_id", "MA_nazwa", samochod.MA_id);
            ViewBag.ST_id = new SelectList(db.status, "ST_id", "ST_nazwa", samochod.ST_id);
            ViewBag.TY_id = new SelectList(db.typs, "TY_id", "TY_nazwa", samochod.TY_id);
            return View(samochod);
        }

        // GET: samochods/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            samochod samochod = await db.samochods.FindAsync(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_id = new SelectList(db.markas, "MA_id", "MA_nazwa", samochod.MA_id);
            ViewBag.ST_id = new SelectList(db.status, "ST_id", "ST_nazwa", samochod.ST_id);
            ViewBag.TY_id = new SelectList(db.typs, "TY_id", "TY_nazwa", samochod.TY_id);
            return View(samochod);
        }

        // POST: samochods/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SA_id,TY_id,MA_id,ST_id,SA_model,SA_stan,SA_cena,SA_kolor,SA_rok_produkcji,SA_kraj_pochodzenia,SA_liczba_miejsc")] samochod samochod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samochod).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MA_id = new SelectList(db.markas, "MA_id", "MA_nazwa", samochod.MA_id);
            ViewBag.ST_id = new SelectList(db.status, "ST_id", "ST_nazwa", samochod.ST_id);
            ViewBag.TY_id = new SelectList(db.typs, "TY_id", "TY_nazwa", samochod.TY_id);
            return View(samochod);
        }

        // GET: samochods/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            samochod samochod = await db.samochods.FindAsync(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        // POST: samochods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            samochod samochod = await db.samochods.FindAsync(id);
            db.samochods.Remove(samochod);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
