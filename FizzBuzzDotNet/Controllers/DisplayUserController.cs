using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserDataAccess;

namespace FizzBuzzDotNet.Controllers
{
    public class DisplayUserController : ApiController
    {
        [HttpGet]
        public IEnumerable<User> Get()
        {
            using (UsersEntities2 entities = new UsersEntities2())
            {
                return entities.Users.ToList();
            }

        }
    }
}
