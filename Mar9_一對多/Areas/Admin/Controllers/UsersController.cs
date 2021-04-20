using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Mar9_一對多.Filter;
using Mar9_一對多.Models;
using Newtonsoft.Json;

namespace Mar9_一對多.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private myModel db = new myModel();

        // GET: Admin/Users
        [Permission]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Departments);
            return View(users.ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.DepID = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            var userData = db.Permissionss.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(GetPermissions(userData.Where(x => x.Pid == null).ToList()));
            sb.Append("]");
            ViewBag.tree = sb.ToString();
            return View();
        }

        public string GetPermissions(ICollection<Permissions> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var permission in list)
            {
                sb.Append("{\"id\":\"" + permission.PValue + "\",\"text\":\"" + permission.Name + "\"");
                if (permission.PremissionsSon.Count > 0)
                {
                    sb.Append(",\"children\":[");
                    sb.Append(GetPermissions(permission.PremissionsSon));
                    sb.Append("]");
                }
                sb.Append("},");
                sb.ToString().TrimEnd(',');
            }
            return sb.ToString();
        }

        // POST: Admin/Users/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.passwordSalt = Utility.CreateSalt();
                user.password = Utility.GenerateHashWithSalt(user.password, user.passwordSalt);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepID = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepID);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepID = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepID);
            var userData = db.Permissionss.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(GetPermissions(userData.Where(x => x.Pid == null).ToList()));
            sb.Append("]");
            ViewBag.tree = sb.ToString();
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepID = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepID);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/LoginViewModels/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Id,customerName,password")] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(f => f.customerName.Equals(loginViewModel.customerName));
                if (user == null)
                {
                    TempData["error"] = "登入失敗";
                    return View(loginViewModel);
                }
                string password = Utility.GenerateHashWithSalt(loginViewModel.password, user.passwordSalt);
                if (user.password != password)
                {
                    TempData["error"] = "登入失敗";
                    return View(loginViewModel);
                }
                string data = JsonConvert.SerializeObject(user);
                Utility.SetAuthenTicket(user.customerName, data);
            }
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