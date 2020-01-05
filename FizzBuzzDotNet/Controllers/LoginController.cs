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
            public string User { get; set; }
            public string Password { get; set; }
        }

        public class isAdminLocal
        {
            public string Response { get; set; }
            public bool admin { get; set; }
        }

        [HttpPost]
        public HttpResponseMessage loginUser(loginUserArgs args)

        {

        var entities = new UsersEntities2();

            var foundUser = entities.Users
                   .Where(x => x.UserName == args.User)
                   .FirstOrDefault();

            if (foundUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Not Valid User");
            }

            var result = Helpers.SecurePasswordHasher.Verify(args.Password, foundUser.Password);

            if (foundUser != null && result)
            {

                if (foundUser.IsAdmin == true)
                {

                    var isAdmin = new isAdminLocal
                    {
                        Response = "Access Granted / Admin",
                        admin = true
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, isAdmin);
                }

                if (foundUser.IsAdmin == false)
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK, "Valid User");
                }
                
            } 

            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Not Valid User");

        }

    } 
    
}
