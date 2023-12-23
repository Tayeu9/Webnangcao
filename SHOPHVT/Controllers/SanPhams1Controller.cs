using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SHOPHVT;

namespace SHOPHVT.Controllers
{
    public class SanPhams1Controller : Controller
    {
        private DB_HVTShopEntities db = new DB_HVTShopEntities();

        // GET: SanPhams1
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams1/Create
        public ActionResult Create()
        {
            ViewBag.danhMucID = new SelectList(db.DanhMucs, "danhMucID", "tenDanhMuc");
            return View();
        }

        // POST: SanPhams1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sanPhamID,tenSanPham,donGia,soLuong,hinhAnh,moTa,danhMucID")] SanPham sanPham, HttpPostedFileBase file)
        {
             try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        file.SaveAs(_path);

                        sanPham.hinhAnh = _FileName;
                        ViewBag.Message = "File Uploaded Successfully!!";
                    }


                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";

                }
            if (ModelState.IsValid)
            {


                db.SanPhams.Add(sanPham);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.danhMucID = new SelectList(db.DanhMucs, "danhMucID", "tenDanhMuc", sanPham.danhMucID);
            return View(sanPham);
        }

        // GET: SanPhams1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.danhMucID = new SelectList(db.DanhMucs, "danhMucID", "tenDanhMuc", sanPham.danhMucID);
            return View(sanPham);
        }

        // POST: SanPhams1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sanPhamID,tenSanPham,donGia,soLuong,hinhAnh,moTa,danhMucID")] SanPham sanPham, HttpPostedFileBase file)
        
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);

                    sanPham.hinhAnh = _FileName;
                    ViewBag.Message = "File Uploaded Successfully!!";
                }


            }
            catch
            {
                ViewBag.Message = "File upload failed!!";

            }
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.danhMucID = new SelectList(db.DanhMucs, "danhMucID", "tenDanhMuc", sanPham.danhMucID);
            return View(sanPham);
        }

        // GET: SanPhams1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
        public ActionResult Dangxuat()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Home()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
