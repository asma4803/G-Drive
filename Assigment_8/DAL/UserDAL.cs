using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        public static User validateUser(String login, String password) {
            var query = String.Format("Select * from dbo.Users Where Login='{0}' and Password='{1}'", login, password);

            using (DB_Class helper = new DB_Class())
            {
                var reader = helper.ExecuteReader(query);

                User dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }
        private static User FillDTO(SqlDataReader reader)
        {
            var dto = new User();
            dto.userid = reader.GetInt32(0);
            dto.name = reader.GetString(1);
            dto.login = reader.GetString(2);
            dto.password = reader.GetString(3);
            dto.email = reader.GetString(4);
            return dto;
        }
    }
}
