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
            public string User { get; set; }
            public string Password { get; set; }
            public bool Admin { get; set; }
        }

        public HttpResponseMessage CreateUser(newUserArgs args)
        {
            if (args.Admin == false)
            {
                args.Admin = false;
            }
                        
            var entities = new UsersEntities2(); //userentititues 2 is the neame of the other databause azure


            var duplicate = entities.Users.SingleOrDefault(x => x.UserName == args.User);

            if (duplicate == null)
                {

                var hash = Helpers.SecurePasswordHasher.Hash(args.Password);

                    var newUser = new User
                    {
                    UserName = args.User,
                    Password = hash,
                    IsAdmin = args.Admin
                    };

                    entities.Users.Add(newUser);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, newUser);
               
                   }
               
            else return Request.CreateResponse(HttpStatusCode.Forbidden);


        }
    } 
}


