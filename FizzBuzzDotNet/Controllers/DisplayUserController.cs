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
        public string Hello()
        {
            return "Hello World";

        }
    }
}


