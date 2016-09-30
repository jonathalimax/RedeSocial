using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RedeSocial.Models;

namespace RedeSocial.Controllers
{
    public class UsuariosController : Controller
    {
        private RedeSocialContext db = new RedeSocialContext();

        // GET: Usuarios/Create
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Usuarios/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,Email,Senha,Idade")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                setSession(usuario.Id, usuario.Nome, usuario.Email);
                return Redirect("~/Social/Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult AlterarSenha()
        {            
            return View();
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarSenha(Usuario usuario)
        {
            using (db = new RedeSocialContext())
            {
                try
                {
                    var usr = db.Usuarios.Single(u => u.Email == usuario.Email);
                    if (usr != null)
                    {
                        usr.Senha = usuario.Senha;
                        db.SaveChanges();
                        Session["IdUsuario"] = usr.Id;
                        Session["Email"] = usr.Email;
                        return Redirect("~/Social");
                    } 
                }
                catch (Exception)
                {
                    ViewBag.Mensagem = "Email incorreto ou não cadastrado";
                }
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult VoltarInicio()
        {
            return Redirect("~/Home/Index");
        }
        
        public ActionResult Logar()
        {
            if (Session["IdUsuario"] == null)
                return View();
            else
                return Redirect("~/Social/Index");
        }

        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {
            using (db = new RedeSocialContext())
            {
                try
                {
                    var usr = db.Usuarios.Single(u => u.Email == usuario.Email && u.Senha == usuario.Senha);
                    if (usr != null)
                    {
                        setSession(usr.Id, usr.Nome, usr.Email);
                        return Redirect("~/Social/Index");
                    }
                } catch (Exception)
                {
                    ModelState.AddModelError("", "Email ou senha incorreto");
                }
                return View();
            }
        }

        private void setSession(int id, string nome, string email)
        {
            Session["IdUsuario"] = id;
            Session["Nome"] = nome;
            Session["Email"] = email;
        }
    }
}
