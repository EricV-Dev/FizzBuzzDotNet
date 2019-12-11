using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using UserDataAccess;


namespace FizzBuzzDotNet.Controllers
{
    public class UpdateUserController : ApiController
    {
        public class updatedUserArgs
        {
            public string user { get; set; }
            public string ogUserSend { get; set; }
            public string password { get; set; }
            public string admin { get; set; }
            public string passChanged { get; set; }
            public string userChanged { get; set; }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage updateUser(updatedUserArgs args)
        {
            var entities = new UsersEntities();

            if (args.passChanged == "true")
            {
                var hash = Helpers.SecurePasswordHasher.Hash(args.password);

                args.password = hash;
            }           

            var duplicate = entities.Users.SingleOrDefault(x => x.username == args.user);

            if (duplicate == null || args.userChanged == "false")
            {

                User foundUser = entities.Users.First(x => x.username == args.ogUserSend);

                foundUser.username = args.user;
                foundUser.password = args.password;
                foundUser.admin = args.admin;

                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, foundUser);

            }

            else return Request.CreateResponse(HttpStatusCode.Forbidden);
            
        }
    }
}



