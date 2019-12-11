using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using UserDataAccess;



namespace FizzBuzzDotNet.Controllers
{

    public class CreateUserController : ApiController
    {
        public class newUserArgs
        {
            public string user { get; set; }
            public string password { get; set; }
            public string admin { get; set; }
        }

        public HttpResponseMessage CreateUser(newUserArgs args)
        {
            if (args.admin == null)
            {
                args.admin = "false";
            }
                        
            var entities = new UsersEntities();

            var duplicate = entities.Users.SingleOrDefault(x => x.username == args.user);

            if (duplicate == null)
                {

                var hash = Helpers.SecurePasswordHasher.Hash(args.password);

                    var newUser = new User
                    {
                    username = args.user,
                    password = hash,
                    admin = args.admin
                    };

                    entities.Users.Add(newUser);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, newUser);
               
                   }
               
            else return Request.CreateResponse(HttpStatusCode.Forbidden);


        }
    } 
}


