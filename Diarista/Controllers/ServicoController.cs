using Diarista.Authorization;
using Diarista.Data;
using Diarista.Models;
using Diarista.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diarista.Controllers
{
    public class ServicoController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();
        private readonly NotificacaoService ns = new NotificacaoService();
        private User GetUser()
        {
            return (User)Session["Usuario"];
        }
        [Autorizar]
        public ActionResult Cadastrar()
        {
            var user = GetUser();
            ViewBag.Casas = new SelectList(db.Casas.Where(c => c.PerfilId == user.Perfil.Cpf).ToList(), "Id", "Descricao");
            ViewBag.Diaristas = new SelectList(db.Perfis.Where(p => p.TipoUsuario == Classifiers.TipoUsuario.Diarista).ToList(), "Cpf", "Nome");
            return View();
        }
        [HttpPost]
        [Autorizar(Roles = "Cliente")]
        public ActionResult Cadastrar(Servico servico)
        {
            if (ModelState.IsValid)
            {
                var user = GetUser();
                var s = new Servico
                {
                    DataContratacao = servico.DataContratacao,
                    Descricao = servico.Descricao,
                    ContratanteId = user.Perfil.Cpf,
                    DataPublicacao = DateTime.Now,
                    Status = Classifiers.StatusServico.Solicitado,
                    DiaristaId = servico.DiaristaId,
                    CasaId = servico.CasaId
                };
                db.Servicos.Add(s);
                db.SaveChanges();
                s = db.Servicos.Include("Diarista.Usuario").Include("Casa").Include("Contratante").First(e => e.Id == s.Id);
                //ns.NotificarDiarista(s);
                return RedirectToAction("Index", "Cliente");
            }
            return View();
        }
    }
}