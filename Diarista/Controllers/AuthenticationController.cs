using Diarista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public ActionResult Login(string user, string password, string ReturnURL)
        {
            try
            {
                using (var db = new DatabaseConnection())
                {
                    var usuario = db.Users.Where(u => u.UserName == user).ToList().FirstOrDefault();

                    if (usuario == null)
                        throw new Exception("Usuário Inválido");

                    if (usuario.Password != password)
                        throw new Exception("Senha Incorreta");

                    Session.Add("usuário", usuario);
                    FormsAuthentication.SetAuthCookie(usuario.UserName, true);

                    if (!string.IsNullOrWhiteSpace(ReturnURL))
                        return Redirect(ReturnURL);
                    else
                    {
                        if (usuario.Perfil == "Diarista")
                            return View();
                        else
                            return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex;
                return RedirectToAction(nameof(Login));
            }
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
    }
}