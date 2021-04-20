using Mar9_一對多.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mar9_一對多.Filter
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        private readonly myModel db = new myModel();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.menu = "";
                filterContext.Result = new RedirectResult("~/Admin/Users/Login");
                return;
            }
            StringBuilder sb = new StringBuilder();
            var permission = db.Permissionss.ToList();
            sb.Append(GetPermission(permission.Where(x => x.Pid == null).ToList()));
            filterContext.Controller.ViewBag.menu = sb.ToString();
            //base.OnActionExecuting(filterContext);
        }

        private string GetPermission(ICollection<Permissions> list)
        {
            StringBuilder sb = new StringBuilder();
            string data = ((FormsIdentity)HttpContext.Current.User.Identity).Name;
            User user = JsonConvert.DeserializeObject<User>(data);
            //sb.Append("<ul class='pcoded-item pcoded-left-item'>");
            foreach (Permissions permissions in list)
            {
                if (user.Authority.IndexOf(permissions.PValue) > -1)
                {
                    if (permissions.PremissionsSon.Count > 0)
                    {
                        sb.Append("<li class='pcoded-hasmenu'>");
                        sb.Append("<a href='javascript:void(0)'>");
                        sb.Append("<span class='pcoded-micon'><i class='ti-layout-grid2-alt'></i></span>");
                        sb.Append($"<span class='pcoded-mtext'>{permissions.Name}</span>");
                        sb.Append("<span class='pcoded-mcaret'></span>");
                        sb.Append("</a>");
                        sb.Append(GetPermission(permissions.PremissionsSon));
                    }
                    else
                    {
                        if (permissions.Pid == null)
                        {
                            sb.Append("<li>");
                            sb.Append("<a href='form-elements-component.html'>");
                            sb.Append("<span class='pcoded-micon'><i class='ti-layers'></i></span>");
                            sb.Append($"<span class='pcoded-mtext'>{permissions.Name}</span>");
                            sb.Append("<span class='pcoded-mcaret'></span>");
                            sb.Append("</a>");
                            sb.Append("</li>");
                        }
                        else
                        {
                            sb.Append("<ul class='pcoded-submenu'>");
                            sb.Append("<li class=' '>");
                            sb.Append("<a href='accordion.html'>");
                            sb.Append("<span class='pcoded-micon'><i class='ti-angle-right'></i></span>");
                            sb.Append($"<span class='pcoded-mtext'>{permissions.Name}</span>");
                            sb.Append("<span class='pcoded-mcaret'></span>");
                            sb.Append("</li>");
                            sb.Append("</ul>");
                        }
                    }
                }
            }
            //sb.Append("</ul>");
            return sb.ToString();
        }
    }
}