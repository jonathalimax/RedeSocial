using RedeSocial.Controllers;
using RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedeSocial.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return Redirect("~/Usuarios/Cadastrar");
        }

        public ActionResult Logar()
        {
            return Redirect("~/Usuarios/Logar");
        }
    }
}
