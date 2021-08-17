using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
   
    public class SatisController : Controller
    {
        MvcDBStokEntities db = new MvcDBStokEntities();
        // GET: Satis
        public ActionResult Index()
        {
            var degerler = db.TBLSATISLAR.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> degerler = (from i in db.TBLMUSTERILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.MUSTERIAD,
                                                 Value = i.MUSTERID.ToString()
                                             }).ToList();
            ViewBag.mstrad = degerler;
            List<SelectListItem> degerler1 = (from i in db.TBLURUNLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.URUNAD,
                                                 Value = i.URUNID.ToString()
                                             }).ToList();
            ViewBag.stsmrk = degerler1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p1)
        {
            db.TBLSATISLAR.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {
            var satis = db.TBLSATISLAR.Find(id);
            db.TBLSATISLAR.Remove(satis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GuncellemeEkrani(int id)
        {
            var satis = db.TBLSATISLAR.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLMUSTERILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.MUSTERIAD,
                                                 Value = i.MUSTERID.ToString()
                                             }).ToList();
            ViewBag.mstrad = degerler;

            return View("GuncellemeEkrani",satis);
        }
        public ActionResult Guncelle(TBLSATISLAR p1)
        {
            var satis = db.TBLSATISLAR.Find(p1.SATISIID);
            var mstrad = db.TBLMUSTERILER.Where(m => m.MUSTERID == p1.TBLMUSTERILER.MUSTERID).FirstOrDefault();
            satis.MUSTERI = mstrad.MUSTERID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    
}