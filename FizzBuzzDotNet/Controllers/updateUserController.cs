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
            public bool admin { get; set; }
            public bool passChanged { get; set; }
            public bool userChanged { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage updateUser(updatedUserArgs args)
        {
            var entities = new UsersEntities();

            if (args.passChanged == true)
            {
                var hash = Helpers.SecurePasswordHasher.Hash(args.password);

                args.password = hash;
            }        

            var duplicate = entities.Users.SingleOrDefault(x => x.UserName == args.user);

            if (duplicate == null || args.userChanged == false)
            {

                User foundUser = entities.Users.First(x => x.UserName == args.ogUserSend);

                if (args.password == null)
                {
                    args.password = foundUser.Password;
                }

                foundUser.UserName = args.user;
                foundUser.Password = args.password;
                foundUser.IsAdmin = args.admin;

                entities.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, foundUser);

            }

            else return Request.CreateResponse(HttpStatusCode.Forbidden);
            
        }
    }
}



