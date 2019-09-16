using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuizaLabs.Wishlist.Core.Criptography;
using LuizaLabs.Wishlist.Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LuizaLabs.Wishlist.Apis.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IHttpContextAccessor accessor;

        protected UserToken UserToken
        {
            get
            {
                UserToken user = null;

                // TODO: validar header property que terá TOKEN (Authentication?)
                var header = accessor.HttpContext.Request.Headers["token"].FirstOrDefault();

                if (header != null)
                {
                    var decrypt = Crypto.Decrypt(header);
                    user = JsonConvert.DeserializeObject<UserToken>(decrypt);
                    if (user != null)
                    {
                        user.EditPermissions = new EditPermissions
                        {
                            EventType = user.Profiles.FirstOrDefault(p => p.Key == "tipoevento").Value,
                            Event = user.Profiles.FirstOrDefault(p => p.Key == "evento").Value,
                            Compliance = user.Profiles.FirstOrDefault(p => p.Key == "compliance").Value
                        };
                    }
                }

                return user;
            }
        }

        public BaseController(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }
    }
}
