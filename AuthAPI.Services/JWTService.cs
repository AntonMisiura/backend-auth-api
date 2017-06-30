using Newtonsoft.Json;
using AuthAPI.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AuthAPI.Services
{
    public static class JWTService
    {
        public static TokenKeys TokenKeysHolder;
        public static void GetTokensKeyFromFile()
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/api_secret.jwk");
            if (!File.Exists(path)) TokenKeysHolder = null;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                TokenKeysHolder = JsonConvert.DeserializeObject<TokenKeys>(json);
            }
        }
    }
}
