using Assignment8.Security;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class loginClass {
        public String login;
        public String password;
        public loginClass(String l , String p) {
            this.login = l;
            this.password = p;
        }
    }
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Login(String login, String password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:60598");
            loginClass obj = new loginClass(login, password);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync("api/UserAPI/ValidateUser", obj);
            User u = null;
            if (response.IsSuccessStatusCode)
            {
                u = response.Content.ReadAsAsync<User>().Result;
            }
            if (u == null)
            {
                ViewBag.myError = "wrong login or password";
                ViewBag.username = login;
                return View();
            }
            else {
                Session["User"] = u;
                return Redirect("~/Home/Home");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            SessionManager.ClearSession();
            return RedirectToAction("Login");
        }
    }
}