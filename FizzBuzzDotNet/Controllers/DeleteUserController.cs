using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserDataAccess;

namespace FizzBuzzDotNet.Controllers
{
    public class DeleteUserController : ApiController
    {
        public class deleteUserArgs
        {
            public string Delete { get; set; }
            public string User { get; set; }
        }

        [HttpPost]
        public void DeleteUser(deleteUserArgs args)
        {
            
            var entities = new UsersEntities();

            entities.Users.RemoveRange(entities.Users.Where(x => x.UserName == args.User));
            entities.SaveChanges();
            
        }
  
        
    }
     
    
}
