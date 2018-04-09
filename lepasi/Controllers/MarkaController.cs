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

namespace lepasi.Controllers
{
    public class MarkaController : Controller
    {
        private MotoJanuszEntities db = new MotoJanuszEntities();

        // GET: Marka
        public async Task<ActionResult> Index()
        {
            return View(await db.markas.ToListAsync());
        }

        // GET: Marka/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka marka = await db.markas.FindAsync(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // GET: Marka/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marka/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MA_id,MA_nazwa")] marka marka)
        {
            if (ModelState.IsValid)
            {
                db.markas.Add(marka);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(marka);
        }

        // GET: Marka/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka marka = await db.markas.FindAsync(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Marka/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MA_id,MA_nazwa")] marka marka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marka).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(marka);
        }

        // GET: Marka/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            marka marka = await db.markas.FindAsync(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Marka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            marka marka = await db.markas.FindAsync(id);
            db.markas.Remove(marka);
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
