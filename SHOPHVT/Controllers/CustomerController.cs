using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHOPHVT.Data;
using SHOPHVT.Models;

namespace SHOPHVT.Controllers
{
    public class CustomerController : Controller
    {
        private DataContext _dataContext;
        private int _customerID = 1;

        public CustomerController()
        {
            _dataContext = ServiceFactory.GetService<DataContext>();
        }

        // GET: CustomerInfo
        public ActionResult Index()
        {
            if (Session["KhachHangID"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            _customerID = (int)Session["KhachHangID"];

            var customer = _dataContext.Customers.FirstOrDefault(c => c.ID == _customerID);
            return View(customer);
        }

        // GET
        public ActionResult Update()
        {
            if (Session["KhachHangID"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            _customerID = (int)Session["KhachHangID"];

            var customer = _dataContext.Customers.FirstOrDefault(c => c.ID == _customerID);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Update(Customer customerInForm)
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("Invalid");
                return View(customerInForm);
            }
            //System.Diagnostics.Debug.WriteLine($"{customerInForm.ID} | {customerInForm.Birthday.ToString("dd/MM/yyyy")} | {customerInForm.Gender} | {customerInForm.Address}");
            //int index = _dataContext.Customers.FindIndex(c => c.ID == _customerID);
            //_dataContext.Customers[index] = customerInForm;
            _dataContext.SaveCustomer(customerInForm);
            return RedirectToAction("Index");
        }

        // GET
        public ActionResult UpdatePassword()
        {
            if (Session["KhachHangID"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            _customerID = (int)Session["KhachHangID"];
            return View();
        }

        // POST
        [HttpPost]
        public ActionResult UpdatePassword(string oldPass, string newPass, string confirmPass)
        {
            if (oldPass.Equals(string.Empty) || newPass.Equals(string.Empty) || confirmPass.Equals(string.Empty))
            {
                ModelState.AddModelError("Error1", "Nhập đầy đủ thông tin");
                return View();
            }
            if (!confirmPass.Equals(newPass))
            {
                ModelState.AddModelError("Error2", "Mật khẩu nhập lại không trùng mật khẩu mới");
                return View();
            }
            var customer = _dataContext.Customers.FirstOrDefault(c => c.ID == _customerID);
            if (!oldPass.Equals(customer.Password))
            {
                ModelState.AddModelError("Error3", "Mật khẩu cũ không trùng");
                return View();
            }
            customer.Password = newPass;
            _dataContext.SaveCustomer(customer);
            return RedirectToAction("Index");
        }
    }
}