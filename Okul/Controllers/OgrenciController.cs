using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Okul.Models;//veritabanını kullanabilmek için projemiz içerisinde yer alan Models dosyasını gösteriyoruz.

namespace Okul.Controllers
{

    public class OgrenciController : Controller
    {
        OKULEntities _db = new OKULEntities();//Models dosyasının altında yer alan veritabanını çağırarak _db değişkenine atıyoruz.
      // GET: 
        [HttpGet]
        public ActionResult Liste()
        {
            List<Ogrenci> ogrenciList = _db.Ogrenci.ToList();
            return View(ogrenciList);
        }
        public ActionResult Ekle()
        {
            return View();
        }
        // GET: Ogrenci/Details/...
        public ActionResult Detaylar(int? id)
        {
           
           Ogrenci ogrenciDetayı = _db.Ogrenci.Find(id);
           
            return View(ogrenciDetayı);
        }
        [HttpPost]
        /*Ekle View ekranına gelen sayfada kullanıcının girdiği bilgileri alıp; kendi tanımladığımız yeniOgrenci nesnesine atıyoruz.Son olarak bu nesneyi veritabanındaki Öğrenci tablosuna ekleyip "SaveChanges()" ile kayıt işlemi yapıyoruz.*/
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(Ogrenci model)
        {
            Ogrenci yeniOgrenci = new Ogrenci();
            yeniOgrenci.OgrenciNo = model.OgrenciNo;
            yeniOgrenci.Adi = model.Adi;
            yeniOgrenci.Soyadi = model.Soyadi;
            yeniOgrenci.Bolum = model.Bolum;
            _db.Ogrenci.Add(yeniOgrenci);
            _db.SaveChanges();
            return RedirectToAction("Liste");

        }
        [HttpGet]
        /*Duzenle metodu ile düzenlenmek istenen öğrencinin OgrenciNo'su ile veritabanında öğrenci bulunur.View'a gönderilir.*/
        public ActionResult Duzenle(int id)
        {
            Ogrenci duzenlenecekOgrenci = _db.Ogrenci.Find(id);
            return View(duzenlenecekOgrenci);
        }
        [HttpPost]
        /*Kendi oluşturduğumuz Ogrenci nesnesi düzenleme yapacaımız öğrencinin OgrenciNo'su sayesine eşitlenir.Karşımıza gelen sayfada yeralan öğrencinin bilgileri düzenlenerek kayıt edilir.*/
        public ActionResult Duzenle(Ogrenci model)
        {
            Ogrenci duzenlenecekOgrenci = _db.Ogrenci.Find(model.OgrenciNo);
            duzenlenecekOgrenci.OgrenciNo = model.OgrenciNo;
            duzenlenecekOgrenci.Adi = model.Adi;
            duzenlenecekOgrenci.Soyadi = model.Soyadi;
            duzenlenecekOgrenci.Bolum = model.Bolum;
            _db.SaveChanges();
            return RedirectToAction("Liste");
        }
        [HttpGet]
        public ActionResult Sil(int id)
        {
            Ogrenci silinecekOgrenci = _db.Ogrenci.Find(id);
            return View(silinecekOgrenci);
        }
        //POST:
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        /*Silmek istediğimiz öğrenci numarası ile öğrenci veritabanında bulunur.Remove() metodu ile bu öğrenci silinir ve son değişiklikler kaydedilir*/
        public ActionResult SilmeOnaylama(int id)
        {
            Ogrenci silinecekOgrenci = _db.Ogrenci.Find(id);
            _db.Ogrenci.Remove(silinecekOgrenci);
            _db.SaveChanges();
            return RedirectToAction("Liste");
;
        }
    }
}