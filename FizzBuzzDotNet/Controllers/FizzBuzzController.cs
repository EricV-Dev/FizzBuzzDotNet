using System.Collections.Generic;
using System.Web.Http;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;


namespace FizzBuzzDotNet.Controllers
{

    public class FizzBuzz
    {
        public int num { get; set; }
        public string result { get; set; }

    }

    public class FizzBuzzController : ApiController
    {

        [HttpGet]
        public object GetAllFizzBuzz(int num)
        {
            List<FizzBuzz> fizzBuzzList = new List<FizzBuzz>();

            if (num == 0)
            {
                num = 10;
            }

            for (int i = 1; i <= num; i++)
            {
                fizzBuzzList.Add(new FizzBuzz { num = i, result = GetFizzBuzz(i) });
            }
            return fizzBuzzList;
        }


        public string GetFizzBuzz(int i)
        {
            string str = "";

            if (i % 3 == 0)
            {
                str += "Fizz";
            }

            if (i % 5 == 0)
            {
                str += " Buzz";
            }

            if (str.Length == 0)
            {
                return i.ToString();
            }

            return str;

        }
    }
}