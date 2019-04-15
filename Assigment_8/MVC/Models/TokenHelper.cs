using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MVC.Models
{
    public class TokenHelper
    {
        public static Dictionary<string, string> GetTokenDetails() {
            Dictionary <string, string> tokenDetails= null;

            try {
                using (var client=new HttpClient()) {
                    var login = new Dictionary<string, string> {
                        { "grant_type", "password"},
                        { "username", ""},
                        { "password", ""}
                    };

                    var resp = client.PostAsync("http://localhost:60598/token", new FormUrlEncodedContent(login));
                    var r = resp.Result.Content.ReadAsStringAsync().Result;
                    if (resp.IsCompleted) {
                        if (r.Contains("access_token")) {
                            tokenDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
                        }
                    }
                };
            }
            catch(Exception ex)
            {
               Console.Write( ex.Message);
            }

            return tokenDetails;
        }
    }
}