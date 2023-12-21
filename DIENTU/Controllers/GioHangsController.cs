using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DIENTU;

namespace DIENTU.Controllers
{
    public class GioHangsController : Controller
    {
        private DB_HVTShopEntities db = new DB_HVTShopEntities();

        // GET: GioHangs
        public ActionResult Index()
        {
            var gioHangs = db.GioHangs.Include(g => g.SanPham).Include(g => g.KhachHang);
            return View(gioHangs.ToList());
        }

        // GET: GioHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // GET: GioHangs/Create
        public ActionResult Create()
        {
            ViewBag.sanphamID = new SelectList(db.SanPhams, "sanPhamID", "tenSanPham");
            ViewBag.khachHangID = new SelectList(db.KhachHangs, "khachHangID", "hoTen");
            return View();
        }

        // POST: GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gioHangID,khachHangID,sanphamID,soLuong,donGia")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sanphamID = new SelectList(db.SanPhams, "sanPhamID", "tenSanPham", gioHang.sanphamID);
            ViewBag.khachHangID = new SelectList(db.KhachHangs, "khachHangID", "hoTen", gioHang.khachHangID);
            return View(gioHang);
        }

        // GET: GioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanphamID = new SelectList(db.SanPhams, "sanPhamID", "tenSanPham", gioHang.sanphamID);
            ViewBag.khachHangID = new SelectList(db.KhachHangs, "khachHangID", "hoTen", gioHang.khachHangID);
            return View(gioHang);
        }

        // POST: GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gioHangID,khachHangID,sanphamID,soLuong,donGia")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sanphamID = new SelectList(db.SanPhams, "sanPhamID", "tenSanPham", gioHang.sanphamID);
            ViewBag.khachHangID = new SelectList(db.KhachHangs, "khachHangID", "hoTen", gioHang.khachHangID);
            return View(gioHang);
        }

        // GET: GioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // POST: GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GioHang gioHang = db.GioHangs.Find(id);
            db.GioHangs.Remove(gioHang);
            db.SaveChanges();
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
