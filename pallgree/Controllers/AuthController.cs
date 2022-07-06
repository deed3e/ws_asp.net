using Microsoft.AspNetCore.Mvc;
using pallgree.Models;
using pallgree.Logic;

namespace pallgree.Controllers
{
    public class AuthController : Controller
    {
        

        public IActionResult Register() 
        {
            return View();
        }

        public IActionResult PostRegister()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            string dislay_name = Request.Form["dislay_name"];
            string hashPass = Crypto.GenerateHash(password,"salt");

            Account a = new Account();
            a.DisplayName = dislay_name;
            a.Password = hashPass;
            a.Username = username;

            using (var context = new pallgree_cafeContext())
            { 
                context.Accounts.Add(a);
                context.SaveChanges();
            }

                return View();
        }

        public IActionResult Login() 
        {
            return View();
        }

       
    }
}
