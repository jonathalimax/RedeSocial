using RedeSocial.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedeSocial.Controllers
{
    public class SocialController : Controller
    {
        private RedeSocialContext db = new RedeSocialContext();
        // GET: Social
        public ActionResult Index()
        {
            using (db = new RedeSocialContext())
            {
                if (Session.Count != 0)
                {
                    int id = (int)Session["IdUsuario"];
                    IQueryable<Publicacao> query = from i in db.Publicacao where i.IdUsuario == id orderby i.DtPublicacao descending select i;
                    ViewBag.List = query.ToList();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(myModels pub)
        {
            using (db = new RedeSocialContext())
            { 
                if (ModelState.Values.Count == 1)
                {
                    try
                    {
                        pub.publicacao.IdUsuario = (int)Session["IdUsuario"];
                        db.Database.ExecuteSqlCommand("Exec Publicar @Publicacao, @IdUsuario", 
                            new SqlParameter("@Publicacao",pub.publicacao.Publicacao1),
                            new SqlParameter("@IdUsuario",pub.publicacao.IdUsuario));
                        return RedirectToAction("Index");
                    } catch (Exception e)
                    {
                        ModelState.AddModelError("erro", "Deu errado");
                        return RedirectToAction("Index");
                    }
                }
            return View();
            }
        }

        public ActionResult Sair()
        {
            Session["IdUsuario"] = null;
            Session["Nome"] = null;
            Session["Email"] = null;
            return Redirect("~/Usuarios/Logar");
        }
    }
}