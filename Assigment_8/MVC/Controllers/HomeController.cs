using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home() {
            if (Assignment8.Security.SessionManager.IsValidUser)
            {
                int userID =Assignment8.Security.SessionManager.User.userid;
                dynamic mymodel = new ExpandoObject();
                mymodel.filelist = BAL.FilesBAL.getAllFiles(userID);
                mymodel.folderlist = BAL.FolderBAL.getAllFolders(userID);
                
                return View(mymodel);
            }
            else
                return Redirect("~/User/Login");
        }
    }
}