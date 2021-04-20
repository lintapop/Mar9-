using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Mar9_一對多.Models;

namespace Mar9_一對多.Areas.Admin.Controllers
{
    public class PermissionsController : Controller
    {
        private myModel db = new myModel();

        // GET: Admin/Permissions
        public ActionResult Index()
        {
            var permissionss = db.Permissionss.Include(p => p.Permissionss);
            return View(permissionss.ToList());
        }

        // GET: Admin/Permissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permissions permissions = db.Permissionss.Find(id);
            if (permissions == null)
            {
                return HttpNotFound();
            }
            return View(permissions);
        }

        // GET: Admin/Permissions/Create
        public ActionResult Create()
        {
            ViewBag.Pid = new SelectList(db.Permissionss, "Id", "Name");
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
                sb.Append("{\"id\":\"" + permission.PValue + ",\"text\":\"" + permission.Name + "\"");
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

        // POST: Admin/Permissions/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Pid,PValue,Url")] Permissions permissions)
        {
            if (ModelState.IsValid)
            {
                db.Permissionss.Add(permissions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pid = new SelectList(db.Permissionss, "Id", "Name", permissions.Pid);
            return View(permissions);
        }

        // GET: Admin/Permissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permissions permissions = db.Permissionss.Find(id);
            if (permissions == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pid = new SelectList(db.Permissionss, "Id", "Name", permissions.Pid);
            return View(permissions);
        }

        // POST: Admin/Permissions/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Pid,PValue,Url")] Permissions permissions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permissions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pid = new SelectList(db.Permissionss, "Id", "Name", permissions.Pid);
            return View(permissions);
        }

        // GET: Admin/Permissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permissions permissions = db.Permissionss.Find(id);
            if (permissions == null)
            {
                return HttpNotFound();
            }
            return View(permissions);
        }

        // POST: Admin/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permissions permissions = db.Permissionss.Find(id);
            db.Permissionss.Remove(permissions);
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