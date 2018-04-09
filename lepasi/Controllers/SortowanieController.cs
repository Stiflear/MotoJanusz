using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace lepasi.Controllers
{
    public class SortowanieController : Controller
    {
        // GET: Sortowanie
        public ActionResult Index(int page=1, string sort = "SA_model", string sortdir="asc", string search="")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetSamochods(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public List<samochod> GetSamochods(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (MotoJanuszEntities db = new MotoJanuszEntities())
            {
                var v = (from a in db.samochods
                         where
                            a.SA_model.Contains(search) ||
                            a.SA_stan.Contains(search) ||
                            a.SA_kolor.Contains(search)
                         select a
                        );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }
    }
}