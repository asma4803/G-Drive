using Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class loginClass
    {
        public String login;
        public String password;
        public loginClass(String l, String p)
        {
            this.login = l;
            this.password = p;
        }
    }
    public class UserAPIController : ApiController
    {
        [HttpPost]
        public User ValidateUser(loginClass l)
        {
            User u = BAL.UserBAL.validateUser(l.login, l.password);
            return u;
        }
    }
}
