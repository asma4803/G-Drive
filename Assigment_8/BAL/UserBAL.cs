using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class UserBAL
    {
        public static User validateUser(String login, String password) {
            return DAL.UserDAL.validateUser(login, password);
        }
    }
}
