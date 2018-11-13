using Diarista.Authorization;
using Diarista.Data;
using Diarista.Models;
using Diarista.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Diarista.Controllers
{
    public class AuthenticationController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        // GET: Authentication
        [HttpPost]
        public ActionResult Login(string email, string password, string ReturnURL)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var users = db.Users;
                    var usuario = users.Include("Perfil").Where(u => u.Email == email).FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("Usuário Inválido");

                    if (usuario.Password != password)
                        throw new Exception("Senha Incorreta");

                    Session.Add("Usuario", usuario);
                    FormsAuthentication.SetAuthCookie(usuario.Email, true);

                    if (!string.IsNullOrWhiteSpace(ReturnURL))
                        return Redirect(ReturnURL);
                    else
                    {
                        if (usuario.Perfil.TipoUsuario == Classifiers.TipoUsuario.Diarista)
                            return RedirectToAction("Index", "Diarista");
                        else
                            return RedirectToAction("Index", "Cliente");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex;
                return RedirectToAction(nameof(Login));
            }
        }

        public ActionResult Cadastrar()
        {
            if (Session["Usuario"] == null)
                return View();
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuario cadastro)
        {
            if (ModelState.IsValid)
            {
                var user = (User)cadastro;
                using (var db = new DatabaseContext())
                {
                    var u = db.Users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Login");
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logoff(string ReturnURL)
        {
            // Perform Logoff operation
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear();

            FormsAuthentication.SignOut();

            // Redirect
            return RedirectToAction(nameof(Login), new { ReturnURL });
        }
        [Autorizar]
        public ActionResult TesteLogado()
        {
            var user = (User)Session["Usuario"];
            return View(user);
        }
    }
}