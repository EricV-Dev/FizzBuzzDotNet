using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Diagnostics;
using UserDataAccess;

namespace FizzBuzzDotNet.Controllers
{
    public class LoginController : ApiController
    {
        public class loginUserArgs
        {
            public string user { get; set; }
            public string password { get; set; }
        }

        public class isAdmin
        {
            public string response { get; set; }
            public bool admin { get; set; }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage loginUser(loginUserArgs args)

        {

        var entities = new UsersEntities();

            var foundUser = entities.Users
                   .Where(x => x.username == args.user)
                   .FirstOrDefault();

            if (foundUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Not Valid User");
            }

            var result = Helpers.SecurePasswordHasher.Verify(args.password, foundUser.password);

            if (foundUser != null && result == true)
            {

                if (foundUser.admin == "true")
                {

                    var isAdmin = new isAdmin
                    {
                        response = "Access Granted / Admin",
                        admin = true
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, isAdmin);
                }

                if (foundUser.admin == "false")
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK, "Valid User");
                }
                
            } 

            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Not Valid User");

        }

    } 
    
}
